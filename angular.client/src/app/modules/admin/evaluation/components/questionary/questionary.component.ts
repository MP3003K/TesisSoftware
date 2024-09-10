import { MatSnackBar } from '@angular/material/snack-bar';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SharedModule } from 'app/shared/shared.module';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { DatabaseService } from './database.service';
import { firstValueFrom, lastValueFrom } from 'rxjs';
import { StudentService } from '../../student.service';
import { EvaluationService } from '../../evaluation.service';
import { FSDocument, FSDocumentElement } from '@fuse/components/fullscreen/fullscreen.types';


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
    [x: string]: any;
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
        public studentService: StudentService,
        private route: ActivatedRoute,
        private db: DatabaseService,
        private evaluationService: EvaluationService
    ) { }

    updatePaginator() {
        this.paginator.pageNumber = 1;
        this.paginator.length = this.questions.length;
        this.paginator.pages = Math.ceil(
            this.paginator.length / this.paginator.pageSize
        );
    }
    cargandoAPIs: boolean = true;


    async ngOnInit(): Promise<void> {
        try {
            await this.db.ready();

            this._fsDoc = document as FSDocument;
            this._fsDocEl = document.documentElement as FSDocumentElement;

            const { id } = this.route.snapshot.params;
            if (isNaN(id)) {
                return this.navigateToEvaluationList();
            }

            this.evaPsiEstId = Number(id);

            await this.cargarEvaluacionPsicologicaYPreguntas();
            this.cargandoAPIs = false;
        } catch (error) {
            console.error('Error during initialization:', error);
            this.navigateToEvaluationList();
        }
    }
    public async cargarEvaluacionPsicologicaYPreguntas(): Promise<void> {

        const evaluaciones = await this.getEvaluaciones();
        if (!evaluaciones) {
            return this.navigateToEvaluationList();
        }

        const evaluacion = this.findEvaluacion(evaluaciones, this.evaPsiEstId);
        if (!evaluacion) {
            return this.navigateToEvaluationList();
        }

        const { estadoAula, Estado, evaluacionPsicologicaId } = evaluacion;

        if (estadoAula === 'N') {
            return this.handleError('Evaluación Psicológica del salón no disponible en este momento');
        }

        if (estadoAula === 'F') {
            return this.handleError('Evaluación Psicológica del salón cerrada');
        }

        if (Estado === 'F') {
            return this.terminarEvaluacionPsicologica();
        }

        if (this.shouldLoadQuestions(evaluacion)) {
            await this.loadQuestions(evaluacionPsicologicaId);
        }
    }

    private async getEvaluaciones(): Promise<any[]> {
        const evaluaciones = await firstValueFrom(this.studentService.getStudentEvaluations());
        return evaluaciones ? Object.values(evaluaciones) : null;
    }

    private findEvaluacion(evaluaciones: any[], evaPsiEstId: number): any {
        return evaluaciones.find(evaluacion => evaluacion.Id === evaPsiEstId);
    }

    private async handleError(message: string): Promise<void> {
        this.errorTexto = message;
        this.errorValidador = true;
        setTimeout(() => this.router.navigate(['']), 8000);
    }

    private navigateToEvaluationList(): void {
        this.router.navigate(['/evaluation']);
    }

    private shouldLoadQuestions(evaluacion: any): boolean {
        return evaluacion && evaluacion.estadoAula === 'P' && ['P', 'N'].includes(evaluacion.Estado);
    }

    private async loadQuestions(evaluacionPsicologicaId: number): Promise<void> {
        await this.cargarPreguntasGenericas(evaluacionPsicologicaId);

        if (this.questions.length > 0) {
            const localQuestions = await this.db.getSavedQuestions(this.evaPsiEstId);
            this.mergeLocalQuestions(localQuestions);
        }

        if (this.questions.length === 0) {
            await this.getEvaluations();
        }
    }

    private mergeLocalQuestions(localQuestions: any[]): void {
        localQuestions.forEach(localPregunta => {
            const pregunta = this.questions.find(q => q.id === localPregunta.id);
            if (pregunta) pregunta.answer = localPregunta.answer;
        });
    }



    async cargarPreguntasGenericas(tipoTest: number) {
        this.db.ready();
        let preguntas = await this.db.recuperarPreguntasPsicologicas(tipoTest);

        if (preguntas == null) {
            await this.guardarPreguntasPsicologicasLocalStorage();
            preguntas = await this.db.recuperarPreguntasPsicologicas(tipoTest);
        }


        preguntas = Array.isArray(preguntas) ? preguntas : Object.values(preguntas);

        preguntas.forEach((pregunta: any) => {
            if (typeof pregunta === 'object' && pregunta !== null) {
                pregunta.answer = null;
            }
        });

        if (preguntas.length) {
            this.questions = preguntas;
            this.updatePaginator();
        } else {
            return null;
        }
    }

    async guardarPreguntasPsicologicasLocalStorage(): Promise<void> {
        try {
            // Llamada a la API para obtener las preguntas psicológicas
            const response = await lastValueFrom(this.evaluationService.getQuestionsAll());
            let preguntasPsicologicas: any = response?.data;

            // Asegurarse de que preguntasPsicologicas sea un array
            preguntasPsicologicas = Array.isArray(preguntasPsicologicas) ? preguntasPsicologicas : Object.values(preguntasPsicologicas);

            // Esperar a que la base de datos esté lista antes de guardar las preguntas
            await this.db.ready();

            // Guardar las preguntas psicológicas en la base de datos
            await this.db.guardarPreguntasPsicologicas(preguntasPsicologicas);

        } catch (error) {
            console.error('Error al obtener o guardar las preguntas psicológicas:', error);
        }
    }

    async getEvaluations() {
        try {
            const response = await firstValueFrom(this.evaluationService.getQuestions(this.evaPsiEstId));

            if (response.succeeded) {
                this.questions = response.data;
                this.updatePaginator();
            }
        } catch (x) {
            const errorMessages: { [key: number]: string } = {
                50001: x.error.error,
                50002: 'La evaluación psicológica ya ha terminado.',
                50003: x.error.error,
            };

            const errorNumber = x.error?.errorNumber;
            const message = errorMessages[errorNumber] || 'Error desconocido';

            if (errorNumber === 50001 || errorNumber === 50003) {
                this.errorValidador = true;
                this.errorTexto = x.error.error;
            } else if (errorNumber === 50002) {
                this.endedTest = true;
            }

            console.error('Error al obtener preguntas:', x);
            this._snackbar.open(message, '', { duration: 3000 });
        }
    }

    itemOptions: any[] = [
        {
            name: 'Totalmente en desacuerdo',
            value: '1',
        },
        {
            name: 'En desacuerdo',
            value: '2',
        },
        {
            name: 'De acuerdo',
            value: '3',
        },
        {
            name: 'Totalmente de acuerdo',
            value: '4',
        },
    ];

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
        let validation = this.validatePage();
        if (!validation.isValid) {
            const formattedMessage = `Debe responder las preguntas: ${validation.unansweredQuestions.slice(0, -1).join(', ')}${validation.unansweredQuestions.length > 1 ? ' y ' : ''}${validation.unansweredQuestions.slice(-1)}.`;
            this._snackbar.open(formattedMessage, '', { duration: 3000 });
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

    async submitForm() {
        const parsedAnswers = this.questions.map(
            ({ id: preg, answer: res }) => ({
                preg,
                res,
            })
        );
        this.evaluationService
            .bulkInsertAnswers(parsedAnswers)
            .subscribe(async (response) => {
                if (response.succeeded) {
                    await this.db.deleteQuestions(this.evaPsiEstId);
                    this.terminarEvaluacionPsicologica();
                }
            });
    }

    terminarEvaluacionPsicologica() {
        this.endedTest = true;
        this.navigateToEvaluationList()
    }
    public getPValue() {
        return `${(this.countCompletedAnswers * 100) / this.paginator.length}%`;
    }

    validatePage() {
        const unansweredQuestions = this.filterQuestions()
            .filter(question => question.answer === null)
            .map(question => question.order);

        return {
            isValid: unansweredQuestions.length === 0,
            unansweredQuestions: unansweredQuestions
        };
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


    // #region Fullscreen
    private _fsDoc: FSDocument;
    public openFullscreen(): void {
        if (this._fsDocEl.requestFullscreen) {
            this._fsDocEl.requestFullscreen();
            return;
        }

        // Firefox
        if (this._fsDocEl.mozRequestFullScreen) {
            this._fsDocEl.mozRequestFullScreen();
            return;
        }

        // Chrome, Safari and Opera
        if (this._fsDocEl.webkitRequestFullscreen) {
            this._fsDocEl.webkitRequestFullscreen();
            return;
        }

        // IE/Edge
        if (this._fsDocEl.msRequestFullscreen) {
            this._fsDocEl.msRequestFullscreen();
            return;
        }
    }

    public closeFullscreen(): void {
        if (this._fsDoc.exitFullscreen) {
            this._fsDoc.exitFullscreen();
            return;
        }

        // Firefox
        if (this._fsDoc.mozCancelFullScreen) {
            this._fsDoc.mozCancelFullScreen();
            return;
        }

        // Chrome, Safari and Opera
        if (this._fsDoc.webkitExitFullscreen) {
            this._fsDoc.webkitExitFullscreen();
            return;
        }

        // IE/Edge
        else if (this._fsDoc.msExitFullscreen) {
            this._fsDoc.msExitFullscreen();
            return;
        }
    }
    // #endregion
}
