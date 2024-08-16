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
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';

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
    estudianteId: number = 0;
    errorValidador: boolean = false;
    errorTexto: string = '';

    isConcentrationModeActive: boolean = false;
    currentQuestionIndex: number = 0;

    @ViewChild('parentSection') miDivRef!: ElementRef;

    paginator = {
        length: 33,
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
        private fb: FormBuilder,
    ) { }

    esEmocional: boolean = null;
    itemsPerPage = 10;
    questionaryForm: FormGroup;
    async ngOnInit(): Promise<void> {
        this.questionaryForm = this.fb.group({
            questions: this.fb.array([])
        });
        this.db.start();
        this.paginator.pages = Math.ceil(this.paginator.length / this.itemsPerPage);

        const { id } = this.route.snapshot.params;
        if ([0, 1].includes(Number(id))) {
            this.esEmocional = id == 0 ? false : true;
            this.cargarOpcionesDeRespuesta();
            this.cargarPreguntasPsicologicas();

            try {
                const examenHabilitado = await this.sePuedeRealizarExamen();
                if (!examenHabilitado) {
                    this.endedTest = true;
                    return;
                }
                await this.cargarRespuestasGuardadas();
                this.loadQuestions();
            } catch (error) {
                this.errorValidador = true;
                this.errorTexto = error;
            }
        }
    }

    async cargarRespuestasGuardadas() {
        try {
            let preguntasGuardadas = await this.db.getSavedQuestions(this.estudianteId);

            preguntasGuardadas.forEach((preguntaGuardada: { id: number, answer: number }) => {
                const pregunta = this.preguntasPsicologicas.find(x => x.id === preguntaGuardada.id);
                if (pregunta && pregunta.respuesta && preguntaGuardada.answer) {
                    pregunta.respuesta = '' + preguntaGuardada.answer;
                }
            });
        } catch (error) {
            console.error('Error al cargar las respuestas guardadas:', error);
        }
    }
    get questions(): FormArray {
        return this.questionaryForm.get('questions') as FormArray;
    }

    loadQuestions(): void {
        this.preguntasPsicologicas.forEach(question => {
            this.questions.push(this.fb.group({
                id: [question.id],
                pregunta: [question.pregunta],
                respuesta: [question.respuesta, Validators.required],
                esEmocional: [question.esEmocional]
            }));
        });
    }

    esValidoLaRespuesta(index: number): boolean {
        let respuesta = Number(this.questions.at(index).get('respuesta').value);


        return respuesta && isFinite(respuesta);
    }

    onSubmit(): void {
        if (this.questionaryForm.valid) {
            console.log(this.questionaryForm.value);
            // Lógica para enviar el formulario
        } else {
            this._snackbar.open('Por favor, responde todas las preguntas.', '', { duration: 2500 });
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

    radioChange(value: any, questionId: number) {
        console.log(value, questionId);
        this.db.updateQuestion(this.estudianteId, questionId, value);
    }

    filterQuestions() {
        const start = (this.paginator.pageNumber - 1) * this.itemsPerPage;
        const end = start + this.itemsPerPage;
        return this.preguntasPsicologicas.slice(start, end);
    }


    getNextItem() {
        const startId = (this.paginator.pageNumber - 1) * 10 + 1;
        const endId = this.paginator.pageNumber * 10;
        const resultado = this.sonValidasLasRespuestas(startId, endId);
        if (!resultado.esValido) {
            let mensaje = 'Preguntas faltantes';
            if (resultado.respuestasInvalidas) {
                mensaje += `: ${resultado.respuestasInvalidas}`;
            }
            this._snackbar.open(mensaje, '', { duration: 2000 });
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
    }
    public getPValue() {
        return `${(this.countCompletedAnswers * 100) / this.paginator.length}%`;
    }

    sonValidasLasRespuestas(startId: number, endId: number): { esValido: boolean, respuestasInvalidas?: string } {
        const preguntasEnRango = this.questions.controls.filter((question: AbstractControl) => {
            const questionValue = question.value;
            return questionValue.id >= startId && questionValue.id <= endId;
        });

        const respuestasInvalidas = preguntasEnRango
            .filter((question: FormGroup) => {
                const respuesta = Number(question.get('respuesta')?.value);
                return !respuesta || !isFinite(respuesta);
            })
            .map((question: FormGroup) => question.get('id')?.value);

        const esValido = respuestasInvalidas.length === 0;

        let respuestasInvalidasStr;
        if (!esValido) {
            if (respuestasInvalidas.length === 1) {
                respuestasInvalidasStr = respuestasInvalidas[0] + '.';
            } else {
                respuestasInvalidasStr = respuestasInvalidas.slice(0, -1).join(', ') + ' y ' + respuestasInvalidas[respuestasInvalidas.length - 1] + '.';
            }
        }

        return {
            esValido,
            respuestasInvalidas: respuestasInvalidasStr
        };
    }
    // #region Responder una pregunta a la ves
    nextQuestion() {
        if (this.currentQuestionIndex < this.preguntasPsicologicas.length - 1) {
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
        respuesta: string;
        esEmocional: boolean;
    }[] = [];

    cargarPreguntasPsicologicas() {
        if (this.esEmocional == true) { // Inteligencia Emocional
            this.preguntasPsicologicas = [
                { id: 1, pregunta: "Presto mucha atención a los sentimientos", respuesta: '0', esEmocional: true },
                { id: 2, pregunta: "Normalmente me preocupo mucho por lo que siento", respuesta: '0', esEmocional: true },
                { id: 3, pregunta: "Normalmente dedico tiempo a pensar en mis emociones", respuesta: '0', esEmocional: true },
                { id: 4, pregunta: "Pienso que merece la pena prestar atención a mis emociones y estado de ánimo", respuesta: '0', esEmocional: true },
                { id: 5, pregunta: "Dejo que mis sentimientos afecten a mis pensamientos", respuesta: '0', esEmocional: true },
                { id: 6, pregunta: "Pienso en mi estado de ánimo constantemente", respuesta: '0', esEmocional: true },
                { id: 7, pregunta: "A menudo pienso en mis sentimientos", respuesta: '0', esEmocional: true },
                { id: 8, pregunta: "Presto mucha atención a cómo me siento", respuesta: '0', esEmocional: true },
                { id: 9, pregunta: "Tengo claros mis sentimientos", respuesta: '0', esEmocional: true },
                { id: 10, pregunta: "Frecuentemente puedo definir mis sentimientos", respuesta: '0', esEmocional: true },
                { id: 11, pregunta: "Casi siempre sé cómo me siento", respuesta: '0', esEmocional: true },
                { id: 12, pregunta: "Normalmente conozco mis sentimientos sobre las personas", respuesta: '0', esEmocional: true },
                { id: 13, pregunta: "A menudo me doy cuenta de mis sentimientos en diferentes situaciones", respuesta: '0', esEmocional: true },
                { id: 14, pregunta: "Siempre puedo decir cómo me siento", respuesta: '0', esEmocional: true },
                { id: 15, pregunta: "A veces puedo decir cuáles son mis emociones", respuesta: '0', esEmocional: true },
                { id: 16, pregunta: "Puedo llegar a comprender mis sentimientos", respuesta: '0', esEmocional: true },
                { id: 17, pregunta: "Aunque a veces me siento triste, puedo tener una visión optimista", respuesta: '0', esEmocional: true },
                { id: 18, pregunta: "Aunque me sienta mal, procuro pensar en cosas agradables", respuesta: '0', esEmocional: true },
                { id: 19, pregunta: "Cuando estoy triste, pienso en todos los placeres de la vida", respuesta: '0', esEmocional: true },
                { id: 20, pregunta: "Intento tener pensamientos positivos aunque me sienta mal", respuesta: '0', esEmocional: true },
                { id: 21, pregunta: "Si doy demasiadas vueltas a las cosas, complicándolas, trato de calmarme", respuesta: '0', esEmocional: true },
                { id: 22, pregunta: "Me preocupo por tener un buen estado de ánimo", respuesta: '0', esEmocional: true },
                { id: 23, pregunta: "Tengo mucha energía cuando me siento feliz", respuesta: '0', esEmocional: true },
                { id: 24, pregunta: "Cuando estoy enfadado intento cambiar mi estado de ánimo", respuesta: '0', esEmocional: true },
                { id: 25, pregunta: "Prefiero mantenerme callado(a) para evitarme problemas", respuesta: '0', esEmocional: false },
                { id: 26, pregunta: "Si un amigo (a) habla mal de mi le insulto", respuesta: '0', esEmocional: false },
                { id: 27, pregunta: "Si necesito ayuda la pido de buena manera", respuesta: '0', esEmocional: false },
                { id: 28, pregunta: "Me es difícil felicitar a la persona que hace algo bueno", respuesta: '0', esEmocional: false },
                { id: 29, pregunta: "Agradezco cuando alguien me ayuda", respuesta: '0', esEmocional: false },
                { id: 30, pregunta: "Me acerco a saludar a mi amigo (a) cuando cumple años", respuesta: '0', esEmocional: false },
                { id: 31, pregunta: "Si un amigo (a) falta a una cita acordada le expreso mi amargura", respuesta: '0', esEmocional: false },
                { id: 32, pregunta: "Cuando me siento triste prefiero no hablar", respuesta: '0', esEmocional: false },
                { id: 33, pregunta: "Le digo a mi amigo (a) cuando hace algo que no me gusta", respuesta: '0', esEmocional: false }
            ];
        }
        else if (this.esEmocional == false) { // Habilidades Sociales
            this.preguntasPsicologicas = [
                { id: 1, pregunta: "Protesto en voz alta cuando alguien se cuela delante de mi", respuesta: '0', esEmocional: false },
                { id: 2, pregunta: "Si una persona mayor me ofende le insulto", respuesta: '0', esEmocional: false },
                { id: 3, pregunta: "No hago caso cuando mis amigos (as) me presionan para hacer lo que ellos quieren", respuesta: '0', esEmocional: false },
                { id: 4, pregunta: "Me distraigo fácilmente cuando una persona habla", respuesta: '0', esEmocional: false },
                { id: 5, pregunta: "Pregunto cada vez que sea necesario para entender lo que me dicen", respuesta: '0', esEmocional: false },
                { id: 6, pregunta: "Miro a los ojos cuando me hablan", respuesta: '0', esEmocional: false },
                { id: 7, pregunta: "Cuando hablo no me dejo entender", respuesta: '0', esEmocional: false },
                { id: 8, pregunta: "Utilizo un tono de voz con gestos apropiados para que escuchen y me entiendan mejor", respuesta: '0', esEmocional: false },
                { id: 9, pregunta: "Pregunto a las personas si me han entendido", respuesta: '0', esEmocional: false },
                { id: 10, pregunta: "Hago la cosas sin pensar", respuesta: '0', esEmocional: false },
                { id: 11, pregunta: "Si estoy tenso (a) trato de relajarme para ordenar mis pensamientos", respuesta: '0', esEmocional: false },
                { id: 12, pregunta: "Antes de opinar ordeno mis ideas con calma", respuesta: '0', esEmocional: false },
                { id: 13, pregunta: "Evito hacer cosas que pueden dañar mi salud", respuesta: '0', esEmocional: false },
                { id: 14, pregunta: "No me siento conforme con mi aspecto físico", respuesta: '0', esEmocional: false },
                { id: 15, pregunta: "Me gusta verme arreglado (a)", respuesta: '0', esEmocional: false },
                { id: 16, pregunta: "Puedo cambiar mi comportamiento cuando me doy cuenta de que estoy equivocado (a)", respuesta: '0', esEmocional: false },
                { id: 17, pregunta: "Me da vergüenza reconocer mis errores", respuesta: '0', esEmocional: false },
                { id: 18, pregunta: "Reconozco fácilmente mi cualidad positiva y negativa", respuesta: '0', esEmocional: false },
                { id: 19, pregunta: "Puedo hablar sobre mis temores", respuesta: '0', esEmocional: false },
                { id: 20, pregunta: "Cuando algo me sale mal no sé como expresar mi cólera", respuesta: '0', esEmocional: false },
                { id: 21, pregunta: "Comparto mi alegría con mis amigos (as)", respuesta: '0', esEmocional: false },
                { id: 22, pregunta: "Me esfuerzo para ser mejor estudiante", respuesta: '0', esEmocional: false },
                { id: 23, pregunta: "Guardo los secretos de mis amigos (as)", respuesta: '0', esEmocional: false },
                { id: 24, pregunta: "Me niego hacer las tareas de mi casa", respuesta: '0', esEmocional: false },
                { id: 25, pregunta: "Pienso en varias soluciones frente a un problema", respuesta: '0', esEmocional: false },
                { id: 26, pregunta: "Me decido por lo que la mayoría decide", respuesta: '0', esEmocional: false },
                { id: 27, pregunta: "Pienso en las posibles consecuencias de mis decisiones", respuesta: '0', esEmocional: false },
                { id: 28, pregunta: "No me agrada hablar del futuro", respuesta: '0', esEmocional: false },
                { id: 29, pregunta: "Hago planes para mis vacaciones", respuesta: '0', esEmocional: false },
                { id: 30, pregunta: "Busco apoyo de otras personas para decir algo importante para mi futuro", respuesta: '0', esEmocional: false },
                { id: 31, pregunta: "Me cuesta decir no", respuesta: '0', esEmocional: false },
                { id: 32, pregunta: "Mantengo mi idea cuando veo que mis amigos están equivocados", respuesta: '0', esEmocional: false },
                { id: 33, pregunta: "Rechazo una invitación sin sentirme culpable", respuesta: '0', esEmocional: false }
            ];
        }
    }

    async sePuedeRealizarExamen(): Promise<boolean> {
        return new Promise((resolve, reject) => {
            this.evaluationService.obtenerExamenesEstudiante().subscribe(
                (response) => {
                    if (response && response.succeeded && response.data) {
                        const filteredData = response.data.filter(x => x.esemocional === this.esEmocional);
                        if (filteredData.length > 0 && typeof filteredData[0].validacion === 'boolean') {
                            this.estudianteId = filteredData[0].estudianteId
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

    countCompletedAnswers: number = 0;


    getPreviousItem() {
        if (this.paginator.pageNumber > 1) {
            this.paginator.pageNumber--;
            this.scrollToTop();
        }
    }


    trackQuestionById(index: number, question: any) {
        return question.id;
    }

    trackOptionByValue(index: number, option: any) {
        return option.value;
    }

    trackByIndex(index: number) {
        return index;
    }
    getOptions(esemocional: boolean) {
        if (esemocional) {
            return this.itemOptionsEmocional;
        } else {
            return this.itemOptionsSocial;
        }

    }


}
