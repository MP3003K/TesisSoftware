import { MatSnackBar } from '@angular/material/snack-bar';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SharedModule } from 'app/shared/shared.module';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatRadioModule } from '@angular/material/radio';
import { EvaluationService } from '../../evaluation.service';
import { MatButtonModule } from '@angular/material/button';
import { DatabaseService } from './database.service';

@Component({
    selector: 'app-questionary',
    templateUrl: './questionary.component.html',
    standalone: true,
    imports: [
        SharedModule,
        MatIconModule,
        MatCardModule,
        MatRadioModule,
        MatButtonModule,
    ],
})
export class QuestionaryComponent {
    endedTest = false;
    evaPsiEstId!: number;
    public questions: any = [];

    errorValidador: boolean = false;
    errorTexto: string = '';

    isConcentrationModeActive: boolean = false;
    currentQuestionIndex: number = 0;

    @ViewChild('parentSection') miDivRef!: ElementRef;

    paginator = {
        length: 0,
        pageSize: 10,
        pageNumber: 1,
        pages: 1,
    };
    constructor(
        private router: Router,
        private _snackbar: MatSnackBar,
        public evaluationService: EvaluationService,
        private route: ActivatedRoute,
        private db: DatabaseService,
    ) { }

    updatePaginator() {
        this.paginator.pageNumber = 1;
        this.paginator.length = this.questions.length;
        this.paginator.pages = Math.ceil(
            this.paginator.length / this.paginator.pageSize
        );
    }
    esEmocional: boolean = null;

    async ngOnInit(): Promise<void> {
        this.db.start();
        const { id } = this.route.snapshot.params;
        if ([0, 1].includes(Number(id))) {
            this.esEmocional = id == 0 ? false : true;
            this.cargarOpcionesDeRespuesta();
            this.cargarPreguntasPsicologicas();

            try {
                this.endedTest = await !this.sePuedeRealizarExamen();
            } catch (error) {
                console.error('Error al determinar si se puede realizar el examen:', error);
                this.endedTest = true; // Asigna un valor por defecto en caso de error
            }
        }
    }

    trackByQuestion(index: number, question: any): number {
        return question.id;
    }

    trackByOption(index: number, option: any): string {
        return option.value;
    }

    trackByItem(index: number, item: any): number {
        return item;
    }
    public goToLogin() {
        this.router.navigate(['../../login']);
    }


    public radioChange(value: string, questionId: number) {
        // Encuentra la pregunta en el array de preguntas
        const question = this.questions.find(q => q.id === questionId);
        if (question) {
            // Actualiza la respuesta de la pregunta
            question.answer = value;
        }

        // Actualiza la respuesta de la pregunta en la base de datos IndexedDB
        this.db.updateQuestion(this.evaPsiEstId, questionId, value);
    }

    public filterQuestions() {
        const { pageSize, pageNumber } = this.paginator;
        const firstItemIndex = (pageNumber - 1) * pageSize;
        const lastItemIndex = firstItemIndex + pageSize;
        return this.questions.slice(firstItemIndex, lastItemIndex);
    }

    get countCompletedAnswers() {
        return this.questions.reduce((a: number, b: any) => {
            if (b.answer) return a + 1;
            return a;
        }, 0);
    }

    getPreviousItem() {
        this.paginator.pageNumber -= 1;
    }

    getNextItem() {
        if (!this.validatePage()) {
            this._snackbar.open('Datos Faltantes', '', { duration: 1000 });
            return;
        }
        this.paginator.pageNumber += 1;
        this.scrollToTop();
    }

    scrollToTop() {
        const miDivElement: HTMLElement = this.miDivRef.nativeElement;
        miDivElement.scrollTo({
            top: 0,
            behavior: 'smooth',
        });
    }
    submitForm() {
        const parsedAnswers = this.questions.map(
            ({ id: preg, answer: res }) => ({
                preg,
                res,
            })
        );
        this.evaluationService
            .bulkInsertAnswers(parsedAnswers)
            .subscribe((response) => {
                if (response.succeeded) {
                    this.evaluationService
                        .updateTestState(this.evaPsiEstId)
                        .subscribe((res) => {
                            this.db.deleteQuestions(this.evaPsiEstId);
                            this.endedTest = true;
                        });
                }
            });
    }
    public getPValue() {
        return `${(this.countCompletedAnswers * 100) / this.paginator.length}%`;
    }
    validatePage() {
        return this.filterQuestions().every((e: any) => e.answer);
    }
    // #region Responder una pregunta a la ves
    nextQuestion() {
        if (this.currentQuestionIndex < this.questions.length - 1) {
            this.currentQuestionIndex++;
        }
    }

    previousQuestion() {
        if (this.currentQuestionIndex > 0) {
            this.currentQuestionIndex--;
        }
    }
    requestFullScreen = (element: any) => {
        if (element.requestFullscreen) {
            element.requestFullscreen();
        } else if (element.mozRequestFullScreen) { // Firefox
            element.mozRequestFullScreen();
        } else if (element.webkitRequestFullscreen) { // Chrome, Safari & Opera
            element.webkitRequestFullscreen();
        } else if (element.msRequestFullscreen) { // IE/Edge
            element.msRequestFullscreen();
        }
    };
    exitFullScreen = (document: any) => {
        if (document.exitFullscreen) {
            document.exitFullscreen();
        } else if (document.mozCancelFullScreen) { // Firefox
            document.mozCancelFullScreen();
        } else if (document.webkitExitFullscreen) { // Chrome, Safari & Opera
            document.webkitExitFullscreen();
        } else if (document.msExitFullscreen) { // IE/Edge
            document.msExitFullscreen();
        }
    };
    toggleFullScreen() {
        const docEl = document.documentElement;

        // Verifica si el documento ya está en modo pantalla completa
        if (!document.fullscreenElement) {
            this.requestFullScreen(docEl);
        } else {
            this.exitFullScreen(document);
        }
    }
    // #endregion


    // #region Modificaciones

    itemOptionsEmocional: { name: string, value: string }[] = [];
    itemOptionsSocial: { name: string, value: string }[] = [];
    cargarOpcionesDeRespuesta() {
        this.itemOptionsEmocional = [
            {
                name: 'Nada de acuerdo',
                value: '1',
            },
            {
                name: 'Algo de acuerdo',
                value: '2',
            },
            {
                name: 'Bastante de acuerdo',
                value: '3',
            },
            {
                name: 'Muy de acuerdo',
                value: '4',
            },
            {
                name: 'Totalmente de acuerdo',
                value: '5',
            },
        ];
        this.itemOptionsSocial = [
            {
                name: 'Nunca',
                value: '1',
            },
            {
                name: 'Rara vez',
                value: '2',
            },
            {
                name: 'A veces',
                value: '3',
            },
            {
                name: 'A menudo',
                value: '4',
            },
            {
                name: 'Siempre',
                value: '5',
            },
        ];
    }

    preguntasPsicologicas: {
        id: number;
        pregunta: string;
        respuesta: number;
        esEmocional: boolean;
    }[] = [];

    cargarPreguntasPsicologicas() {
        if (this.esEmocional == true) { // Inteligencia Emocional
            this.preguntasPsicologicas = [
                { id: 1, pregunta: "Presto mucha atención a los sentimientos", respuesta: 0, esEmocional: true },
                { id: 2, pregunta: "Normalmente me preocupo mucho por lo que siento", respuesta: 0, esEmocional: true },
                { id: 3, pregunta: "Normalmente dedico tiempo a pensar en mis emociones", respuesta: 0, esEmocional: true },
                { id: 4, pregunta: "Pienso que merece la pena prestar atención a mis emociones y estado de ánimo", respuesta: 0, esEmocional: true },
                { id: 5, pregunta: "Dejo que mis sentimientos afecten a mis pensamientos", respuesta: 0, esEmocional: true },
                { id: 6, pregunta: "Pienso en mi estado de ánimo constantemente", respuesta: 0, esEmocional: true },
                { id: 7, pregunta: "A menudo pienso en mis sentimientos", respuesta: 0, esEmocional: true },
                { id: 8, pregunta: "Presto mucha atención a cómo me siento", respuesta: 0, esEmocional: true },
                { id: 9, pregunta: "Tengo claros mis sentimientos", respuesta: 0, esEmocional: true },
                { id: 10, pregunta: "Frecuentemente puedo definir mis sentimientos", respuesta: 0, esEmocional: true },
                { id: 11, pregunta: "Casi siempre sé cómo me siento", respuesta: 0, esEmocional: true },
                { id: 12, pregunta: "Normalmente conozco mis sentimientos sobre las personas", respuesta: 0, esEmocional: true },
                { id: 13, pregunta: "A menudo me doy cuenta de mis sentimientos en diferentes situaciones", respuesta: 0, esEmocional: true },
                { id: 14, pregunta: "Siempre puedo decir cómo me siento", respuesta: 0, esEmocional: true },
                { id: 15, pregunta: "A veces puedo decir cuáles son mis emociones", respuesta: 0, esEmocional: true },
                { id: 16, pregunta: "Puedo llegar a comprender mis sentimientos", respuesta: 0, esEmocional: true },
                { id: 17, pregunta: "Aunque a veces me siento triste, puedo tener una visión optimista", respuesta: 0, esEmocional: true },
                { id: 18, pregunta: "Aunque me sienta mal, procuro pensar en cosas agradables", respuesta: 0, esEmocional: true },
                { id: 19, pregunta: "Cuando estoy triste, pienso en todos los placeres de la vida", respuesta: 0, esEmocional: true },
                { id: 20, pregunta: "Intento tener pensamientos positivos aunque me sienta mal", respuesta: 0, esEmocional: true },
                { id: 21, pregunta: "Si doy demasiadas vueltas a las cosas, complicándolas, trato de calmarme", respuesta: 0, esEmocional: true },
                { id: 22, pregunta: "Me preocupo por tener un buen estado de ánimo", respuesta: 0, esEmocional: true },
                { id: 23, pregunta: "Tengo mucha energía cuando me siento feliz", respuesta: 0, esEmocional: true },
                { id: 24, pregunta: "Cuando estoy enfadado intento cambiar mi estado de ánimo", respuesta: 0, esEmocional: true },
                { id: 25, pregunta: "Prefiero mantenerme callado(a) para evitarme problemas", respuesta: 0, esEmocional: false },
                { id: 26, pregunta: "Si un amigo (a) habla mal de mi le insulto", respuesta: 0, esEmocional: false },
                { id: 27, pregunta: "Si necesito ayuda la pido de buena manera", respuesta: 0, esEmocional: false },
                { id: 28, pregunta: "Me es difícil felicitar a la persona que hace algo bueno", respuesta: 0, esEmocional: false },
                { id: 29, pregunta: "Agradezco cuando alguien me ayuda", respuesta: 0, esEmocional: false },
                { id: 30, pregunta: "Me acerco a saludar a mi amigo (a) cuando cumple años", respuesta: 0, esEmocional: false },
                { id: 31, pregunta: "Si un amigo (a) falta a una cita acordada le expreso mi amargura", respuesta: 0, esEmocional: false },
                { id: 32, pregunta: "Cuando me siento triste prefiero no hablar", respuesta: 0, esEmocional: false },
                { id: 33, pregunta: "Le digo a mi amigo (a) cuando hace algo que no me gusta", respuesta: 0, esEmocional: false }
            ];
        }
        else if (this.esEmocional == false) { // Habilidades Sociales
            this.preguntasPsicologicas = [
                { id: 1, pregunta: "Protesto en voz alta cuando alguien se cuela delante de mi", respuesta: 0, esEmocional: false },
                { id: 2, pregunta: "Si una persona mayor me ofende le insulto", respuesta: 0, esEmocional: false },
                { id: 3, pregunta: "No hago caso cuando mis amigos (as) me presionan para hacer lo que ellos quieren", respuesta: 0, esEmocional: false },
                { id: 4, pregunta: "Me distraigo fácilmente cuando una persona habla", respuesta: 0, esEmocional: false },
                { id: 5, pregunta: "Pregunto cada vez que sea necesario para entender lo que me dicen", respuesta: 0, esEmocional: false },
                { id: 6, pregunta: "Miro a los ojos cuando me hablan", respuesta: 0, esEmocional: false },
                { id: 7, pregunta: "Cuando hablo no me dejo entender", respuesta: 0, esEmocional: false },
                { id: 8, pregunta: "Utilizo un tono de voz con gestos apropiados para que escuchen y me entiendan mejor", respuesta: 0, esEmocional: false },
                { id: 9, pregunta: "Pregunto a las personas si me han entendido", respuesta: 0, esEmocional: false },
                { id: 10, pregunta: "Hago la cosas sin pensar", respuesta: 0, esEmocional: false },
                { id: 11, pregunta: "Si estoy tenso (a) trato de relajarme para ordenar mis pensamientos", respuesta: 0, esEmocional: false },
                { id: 12, pregunta: "Antes de opinar ordeno mis ideas con calma", respuesta: 0, esEmocional: false },
                { id: 13, pregunta: "Evito hacer cosas que pueden dañar mi salud", respuesta: 0, esEmocional: false },
                { id: 14, pregunta: "No me siento conforme con mi aspecto físico", respuesta: 0, esEmocional: false },
                { id: 15, pregunta: "Me gusta verme arreglado (a)", respuesta: 0, esEmocional: false },
                { id: 16, pregunta: "Puedo cambiar mi comportamiento cuando me doy cuenta de que estoy equivocado (a)", respuesta: 0, esEmocional: false },
                { id: 17, pregunta: "Me da vergüenza reconocer mis errores", respuesta: 0, esEmocional: false },
                { id: 18, pregunta: "Reconozco fácilmente mi cualidad positiva y negativa", respuesta: 0, esEmocional: false },
                { id: 19, pregunta: "Puedo hablar sobre mis temores", respuesta: 0, esEmocional: false },
                { id: 20, pregunta: "Cuando algo me sale mal no sé como expresar mi cólera", respuesta: 0, esEmocional: false },
                { id: 21, pregunta: "Comparto mi alegría con mis amigos (as)", respuesta: 0, esEmocional: false },
                { id: 22, pregunta: "Me esfuerzo para ser mejor estudiante", respuesta: 0, esEmocional: false },
                { id: 23, pregunta: "Guardo los secretos de mis amigos (as)", respuesta: 0, esEmocional: false },
                { id: 24, pregunta: "Me niego hacer las tareas de mi casa", respuesta: 0, esEmocional: false },
                { id: 25, pregunta: "Pienso en varias soluciones frente a un problema", respuesta: 0, esEmocional: false },
                { id: 26, pregunta: "Me decido por lo que la mayoría decide", respuesta: 0, esEmocional: false },
                { id: 27, pregunta: "Pienso en las posibles consecuencias de mis decisiones", respuesta: 0, esEmocional: false },
                { id: 28, pregunta: "No me agrada hablar del futuro", respuesta: 0, esEmocional: false },
                { id: 29, pregunta: "Hago planes para mis vacaciones", respuesta: 0, esEmocional: false },
                { id: 30, pregunta: "Busco apoyo de otras personas para decir algo importante para mi futuro", respuesta: 0, esEmocional: false },
                { id: 31, pregunta: "Me cuesta decir no", respuesta: 0, esEmocional: false },
                { id: 32, pregunta: "Mantengo mi idea cuando veo que mis amigos están equivocados", respuesta: 0, esEmocional: false },
                { id: 33, pregunta: "Rechazo una invitación sin sentirme culpable", respuesta: 0, esEmocional: false }
            ];
        }
    }

    sePuedeRealizarExamen(): Promise<boolean> {
        return new Promise((resolve, reject) => {
            this.evaluationService.obtenerExamenesEstudiante().subscribe(
                (response) => {
                    if (response && response.succeeded && response.data) {
                        const filteredData = response.data.filter(x => x.esemocional === this.esEmocional);

                        if (filteredData.length > 0 && typeof filteredData[0].validacion === 'boolean') {
                            const examenHabilitado = !filteredData[0].validacion;
                            resolve(examenHabilitado);
                        } else {
                            resolve(false);
                        }
                    } else {
                        resolve(false);
                    }
                },
                (error) => {
                    reject(false);
                }
            );
        });
    }

    // #endregion
}
