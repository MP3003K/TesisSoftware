import { MatSnackBar } from '@angular/material/snack-bar';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { EvaluationService } from './evaluation.service';
import { AuthService } from 'app/core/auth/auth.service';

@Component({
    templateUrl: './evaluation.component.html',
})
export class EvaluationComponent {
    endedTest = false;
    evaPsiEstId!: number;
    public questions: any = [];
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
        private authService: AuthService
    ) {}
    updatePaginator() {
        this.paginator.pageNumber = 1;
        this.paginator.length = this.questions.length;
        this.paginator.pages = Math.ceil(
            this.paginator.length / this.paginator.pageSize
        );
    }

    ngOnInit(): void {
        const { id: studentId } = this.authService.localUser;
        if (studentId) {
            this.evaluationService.getStudent(studentId).subscribe({
                next: ({ data }) => {
                    console.log(data)
                    const { evaPsiEstId } = data;
                    if (evaPsiEstId) {
                        this.evaPsiEstId = evaPsiEstId;
                        this.evaluationService
                            .getQuestionsApi(evaPsiEstId)
                            .subscribe({
                                next: (res) => {
                                    this.questions = res;
                                    this.updatePaginator();
                                },
                                error: (err) => {
                                    if (err.status == 400) {
                                        this.endedTest = true;
                                        this._snackbar.open(
                                            'No hay test disponibles para este usuario',
                                            '',
                                            {
                                                duration: 3000,
                                            }
                                        );
                                    }
                                },
                            });
                    } else {
                        this.endedTest = true;
                        this._snackbar.open(
                            'No hay test disponibles para este usuario',
                            '',
                            {
                                duration: 3000,
                            }
                        );
                    }
                },
            });
        }
    }

    public goToLogin() {
        this.router.navigate(['../../login']);
    }
    itemOptions: any[] = [
        {
            name: 'Nunca',
            value: '1',
        },
        {
            name: 'Casi Nunca',
            value: '2',
        },
        {
            name: 'A veces',
            value: '3',
        },
        {
            name: 'Casi Siempre',
            value: '4',
        },
        {
            name: 'Siempre',
            value: '5',
        },
    ];

    public radioChange(value: string, questionId: number) {
        this.evaluationService
            .updateQuestion(value, questionId, this.evaPsiEstId)
            .subscribe((res) => {
                console.log(res);
            });
    }

    public filterQuestions() {
        const { pageSize, pageNumber } = this.paginator;
        const firstItemIndex = (pageNumber - 1) * pageSize;
        const lastItemIndex = firstItemIndex + pageSize;
        return this.questions.slice(firstItemIndex, lastItemIndex);
    }
    public countCompletedAnswers() {
        const completedAnswersCount = this.questions.reduce(
            (a: number, b: any) => {
                if (b.answer) return a + 1;
                return a;
            },
            0
        );
        return completedAnswersCount;
    }
    getPreviousItem() {
        this.paginator.pageNumber -= 1;
    }

    getNextItem() {
        if (!this.validatePage()) {
            this._snackbar.open('Datos Faltantes', '', { duration: 1000 });
            return;
        }
        //this.evaluationService.updateQuestion('1', 1, 1);
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
        console.log(this.questions);

        this.evaluationService
            .updateTestState(this.evaPsiEstId, 'F')
            .subscribe((res) => {
                console.log(res);
                this.endedTest = true;
            });
    }
    public getPValue() {
        return `${
            (this.countCompletedAnswers() * 100) / this.paginator.length
        }%`;
    }
    validatePage() {
        return this.filterQuestions().every((e: any) => e.answer);
    }
}
