import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EvaluationService } from '../../evaluation/evaluation.service';
import { StudentReportComponent } from '../student-report/student-report.component';
import { Router } from '@angular/router';
import { StudentService } from '../student.service';

@Component({
    templateUrl: './classroom-report.component.html',
    styleUrls: ['./reports.component.scss'],
})
export class ClassroomReportComponent {
    public dataInfo: string = 'Seleccione un formulario';
    public years: number[] = [];
    public selectedUnity!: number;
    public selectedDimension!: number;
    public selectedYear!: number;
    public selectedGrade!: number;
    public selectedClass!: number;

    dimensions: any[] = [];
    unities: any[] = [];
    selectedScales: any[] = [];
    clasrooms: any[] = [];
    answers: any;

    constructor(
        private evaluationService: EvaluationService,
        public dialog: MatDialog,
        private _snackbar: MatSnackBar,
        private router: Router,
        private studentService: StudentService
    ) {}

    ngOnInit(): void {
        this.getTechs();
        this.getClasses();
    }

    filterUnities() {
        return this.unities.filter((e) => e['año'] == this.selectedYear) ?? [];
    }

    filterAll() {
        if (
            this.selectedClass &&
            this.selectedDimension &&
            this.selectedUnity
        ) {
            this.getStudents();
            this.selectedScales = [];
            this.dataInfo = 'Buscando Elementos';
            this.evaluationService
                .getClasroomAnswers(
                    this.selectedClass,
                    this.selectedDimension,
                    this.selectedUnity
                )
                .subscribe({
                    next: (data: any[]) => {
                        this.selectedScales = data.map(
                            ({
                                indicadoresPsicologicos,
                                nombre: name,
                                promedioEscala: mean,
                            }: {
                                indicadoresPsicologicos: any[];
                                nombre: string;
                                promedioEscala: number;
                            }) => ({
                                name,
                                mean,
                                indicators: indicadoresPsicologicos.map(
                                    ({
                                        nombreIndicador: name,
                                        promedioIndicador: mean,
                                    }) => ({
                                        name,
                                        mean,
                                    })
                                ),
                            })
                        );
                    },
                    error: ({ status }) => {
                        this.dataInfo = 'Sin Elementos';
                        if (status === 400) {
                            this._snackbar.open('Sin elementos', '', {
                                duration: 2000,
                            });
                        }
                    },
                });
        } else {
            this._snackbar.open('Datos faltantes', '', { duration: 2000 });
        }
    }
    filterClass() {
        return (
            this.clasrooms.filter((e) => e.gradoId == this.selectedGrade) ?? []
        );
    }

    getTechs() {
        this.evaluationService.getTechs().subscribe(({ data }) => {
            this.years = (
                [...new Set(data.map((e: any) => e['año']))] as number[]
            ).sort((a, b) => b - a);
            this.unities = data;
        });
    }

    getClasses() {
        this.evaluationService.getClassrooms().subscribe(({ data }) => {
            this.clasrooms = data;
        });
    }

    getStudents() {
        this.studentService
            .getStudents(
                this.selectedClass,
                this.selectedUnity,
                this.selectedDimension
            )
            .subscribe({
                complete: () => {
                    console.log('students completed');
                },
            });
    }
    studentList() {
        return this.studentService.students;
    }
    redirectStudent(index: number) {
        console.log(index);
        this.router.navigate([`/dashboards/reports/${index}`], {
            queryParams: {
                classroomId: this.selectedClass,
                unityId: this.selectedUnity,
            },
        });
    }
}
