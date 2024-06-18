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
        private db: DatabaseService
    ) { }

    updatePaginator() {
        this.paginator.pageNumber = 1;
        this.paginator.length = this.questions.length;
        this.paginator.pages = Math.ceil(
            this.paginator.length / this.paginator.pageSize
        );
    }

    ngOnInit(): void {
        this.db.start();
        const { id } = this.route.snapshot.params;
        if (!isNaN(parseInt(id))) {
            this.evaPsiEstId = +id;
            this.getEvaluations();
        } else {
            this.router.navigate(['..']);
        }
    }

    public getEvaluations() {
        this.evaluationService.getQuestions(this.evaPsiEstId).subscribe({
            next: (response) => {
                if (response.succeeded) {
                    this.db.getSavedQuestions(this.evaPsiEstId).then(savedQuestions => {
                        if (savedQuestions.length > 0) {
                            // Si hay preguntas guardadas, las usa
                            this.questions = savedQuestions;
                        } else {
                            // Si no hay preguntas guardadas, obtiene las preguntas del servidor y las guarda en la base de datos IndexedDB
                            this.questions = response.data;
                            this.db.saveQuestions(this.questions, this.evaPsiEstId);
                        }
                        this.updatePaginator();
                    }).catch(error => {
                        // Maneja el error
                    });
                }
            },
            error: (x) => {
                if (x.error && x.error.errorNumber) {
                    let message = '';
                    switch (x.error.errorNumber) {
                        case 50001:
                            this.errorValidador = true;
                            this.errorTexto = x.error.error;
                            break;
                        case 50002:
                            this.endedTest = true; // Ya ha terminado la Evaluacion Psicologica el estudiante
                            break;
                        case 50003:
                            this.errorValidador = true;
                            this.errorTexto = x.error.error;
                            break;
                        default:
                            message = x.error;
                    }
                    this._snackbar.open(message, '', { duration: 3000 });
                }
            }
        });
    }

    public goToLogin() {
        this.router.navigate(['../../login']);
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
}
