import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EvaluationService } from '../../evaluation/evaluation.service';
import { StudentService } from '../student.service';
import { ExcelService } from '../excel.service';
import { Router } from '@angular/router';
import { StudentRegistrationComponent } from '../student-registration/student-registration.component';
import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from 'app/shared/shared.module';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { ClassroomsService } from '../../classrooms/classrooms.service';
import { MatButtonModule } from '@angular/material/button';

@Component({
    selector: 'app-classroom-report',
    templateUrl: './classroom-report.component.html',
    styleUrl: './reports.component.scss',
    standalone: true,
    imports: [
        SharedModule,
        MatIconModule,
        MatSelectModule,
        MatButtonToggleModule,
        MatButtonModule,
    ],
})
export class ClassroomReportComponent {
    public dataInfo: string = 'Seleccione un formulario';
    public selectedUnity!: number;
    public selectedDimension!: number;
    public selectedDegree!: number;
    public selectedSection!: number;
    public selectedClassroomEvaluationId!: number;
    public studentsClassroom: any[] = [];

    dimensions: any[] = [];
    unities: any[] = [];
    scales: any[] = [];
    classrooms: any[] = [];
    answers: any;

    constructor(
        private evaluationService: EvaluationService,
        public dialog: MatDialog,
        private _snackbar: MatSnackBar,
        private router: Router,
        private excelService: ExcelService,
        private classroomsService: ClassroomsService
    ) {}

    ngOnInit(): void {
        this.getUnities();
        this.getClasses();
    }
    getTest() {
        const scales = [].map(
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
                    ({ nombreIndicador: name, promedioIndicador: mean }) => ({
                        name,
                        mean,
                    })
                ),
            })
        );

        console.log(scales);
        //this.scales = scales;
    }

    filterAll() {
        this.classroomsService
            .getEvaluation(this.selectedUnity, this.selectedSection)
            .subscribe((response) => {
                if (response.succeeded) {
                    const { id } = response.data;
                    if (id) {
                        this.selectedClassroomEvaluationId = id;
                        this.evaluationService
                            .getClasroomAnswers(id)
                            .subscribe({
                                next: (response) => {
                                    if (response.succeeded) {
                                        console.log(response.data);
                                        this.studentsClassroom = response.data;
                                    }
                                },
                                error: ({ status }) => {
                                    this.dataInfo = 'Sin Elementos';
                                    if (status === 400) {
                                        this._snackbar.open(
                                            'Sin elementos',
                                            '',
                                            {
                                                duration: 2000,
                                            }
                                        );
                                    }
                                },
                            });
                    } else {
                        this._snackbar.open(
                            'La evaluación aún no fue iniciada',
                            '',
                            { duration: 3000 }
                        );
                    }
                }
            });
    }

    get sections() {
        return (
            this.classrooms.filter((e) => e.grado == this.selectedDegree) ?? []
        );
    }

    getUnities() {
        this.classroomsService.getUnidadesAll().subscribe({
            next: (response) => {
                if (response.succeeded) {
                    this.unities = response.data;
                }
            },
            error: (err) => {
                console.log(err);
            },
        });
    }

    getClasses() {
        this.evaluationService.getClassrooms().subscribe(({ data }) => {
            console.log(data);
            this.classrooms = data;
        });
    }

    get academicDegrees() {
        return [...new Set(this.classrooms.map((e) => e.grado))];
    }

    redirectStudent(index: number) {
        console.log(index);
        this.router.navigate(['reports', index], {
            queryParams: {
                classroomEvaluationId: this.selectedClassroomEvaluationId,
            },
        });
    }

    getMeanClasses(mean: number) {
        if (mean >= 1 && mean <= 2) {
            return this.selectedDimension == 1
                ? 'bg-[#b6d7a8]'
                : 'bg-[#00b050]';
        } else if (mean >= 2.1 && mean <= 3.9) {
            return this.selectedDimension == 1
                ? 'bg-[#70ad47] text-black'
                : 'bg-[#ffff00] text-black';
        } else if (mean >= 4 && mean <= 5) {
            return this.selectedDimension == 1
                ? 'bg-[#548135]'
                : 'bg-[#ff0000]';
        } else {
            return 'bg-black';
        }
    }
    onToggleChange() {
        this.scales = [];
    }

    generateExcel() {
        //this.excelService.downloadReport();
    }
    openStudentRegistrationModal(classroomId: number): void {
        const dialogRef = this.dialog.open(StudentRegistrationComponent, {
            data: { classroomId }, // Pasa el classroomId como dato al modal
        });

        dialogRef.afterClosed().subscribe((result) => {
            // Puede manejar acciones después de cerrar el modal si es necesario
            console.log('El modal se cerró', result);
        });
    }
}
