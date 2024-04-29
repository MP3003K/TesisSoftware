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

// librerias para exportar a excel
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';
import * as ExcelJS from 'exceljs/dist/exceljs.min.js';

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
    ) { }

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
            this.classrooms = data;
        });
    }

    get academicDegrees() {
        return [...new Set(this.classrooms.map((e) => e.grado))];
    }

    redirectStudent(index: number) {
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

    openStudentRegistrationModal(classroomId: number): void {
        const dialogRef = this.dialog.open(StudentRegistrationComponent, {
            data: { classroomId }, // Pasa el classroomId como dato al modal
        });

        dialogRef.afterClosed().subscribe((result) => {
            // Puede manejar acciones después de cerrar el modal si es necesario
            console.log('El modal se cerró', result);
        });
    }



    //#region DescargarExcel
    readonly PLANTILLAS = {
        HSE_1Y2: 'assets/plantillas/plantilla_resultados_hse_1y2.xlsx',
        HSE_3Y5: 'assets/plantillas/plantilla_resultados_hse_3,4y5.xlsx'
    };

    readonly RANGOS_GRADO = {
        1: { inicio: 1, fin: 66 },
        3: { inicio: 67, fin: 134 }
    };


    getRangoGrado(_tipo_test_psi: number) {
        if (_tipo_test_psi === 1) {
            return this.RANGOS_GRADO[1];
        } else if (_tipo_test_psi === 2) {
            return this.RANGOS_GRADO[3];
        } else {
            return null;
        }
    }

    getPlantilla(_tipo_test_psi: number) {
        if (_tipo_test_psi === 1) {
            return this.PLANTILLAS.HSE_1Y2;
        } else if (_tipo_test_psi === 2) {
            return this.PLANTILLAS.HSE_3Y5;
        } else {
            return null;
        }
    }

    async exportarExcel_resultadosPsicologicosAula() {

        let tipo_test_psi = [1, 2].includes(Number(this.selectedDegree)) ? 1 : ([3, 4, 5].includes(Number(this.selectedDegree)) ? 2 : 0);

        if (tipo_test_psi === 0)
            return;

        const resultadosPsiAulaExcel = await this.getResultadosPsiAulaExcel(this.selectedSection, this.selectedUnity, tipo_test_psi);

        if (!resultadosPsiAulaExcel)
            return;

        try {
            const plantilla = this.getPlantilla(tipo_test_psi);
            const response = await fetch(plantilla);
            const buffer = await response.arrayBuffer();
            const workbook = new ExcelJS.Workbook();
            await workbook.xlsx.load(buffer);
            const worksheet = workbook.getWorksheet('BASE DE RESPUESTAS');

            resultadosPsiAulaExcel.forEach((resultado, index) => {
                const { inicio, fin } = this.getRangoGrado(tipo_test_psi);

                worksheet.getCell(5, index + 2 + 5).value = resultado.NombreCompleto;

                for (let i = inicio; i <= fin; i++) {
                    const respuesta = resultado.respuestas[`r${i}`];
                    if (respuesta !== undefined) {
                        worksheet.getCell(6 + i - inicio, index + 2 + 5).value = respuesta;
                    }
                }
            });

            const bufferExcel = await workbook.xlsx.writeBuffer();
            const blob = new Blob([bufferExcel], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
            const nombreArchivo = this.crearNombreArchivo();
            saveAs(blob, nombreArchivo);
        } catch (error) {
            console.error('Error al exportar los resultados:', error);
        }
    }

    crearNombreArchivo() {
        const fecha = new Date();
        return `evaluacion_hse_1a_${fecha.getDate()}-${fecha.getMonth() + 1}-${fecha.getFullYear()}.xlsx`;
    }

    mapearRespuestas(respuestaNumerica) {
        const mapeoRespuestas = {
            0: 'Se dejó en blanco',
            1: 'Totalmente en desacuerdo',
            2: 'En desacuerdo',
            3: 'De acuerdo',
            4: 'Totalmente de acuerdo'
        };
        return mapeoRespuestas[Number(respuestaNumerica)] || 'Respuesta no válida';
    }

    async getResultadosPsiAulaExcel(_aulaId: number, _unidadId: number, _tipoTestPsi: number) {
        if (!_aulaId || !_unidadId) {
            console.warn('El aula o la unidad no son válidos. No se puede obtener los resultados.');
            return;
        }

        let resultadosPsiAulaExcel = null;

        try {
            const response = await this.classroomsService.getResultadosPsiAulaExcel(_aulaId, _unidadId).toPromise();
            if (response.succeeded) {
                resultadosPsiAulaExcel = response.data.map(estudiante => {
                    const respuestas = {};
                    const { inicio, fin } = this.getRangoGrado(_tipoTestPsi);
                    for (let i = inicio; i <= fin; i++) {
                        const claveRespuesta = `r${i}`;
                        const respuestaNumerica = estudiante[claveRespuesta];
                        if (respuestaNumerica != null) {
                            respuestas[claveRespuesta] = this.mapearRespuestas(respuestaNumerica);
                        } else {
                            respuestas[claveRespuesta] = 'Se dejó en blanco';
                        }
                    }

                    return {
                        EvaPsiEstId: estudiante.EvaPsiEstId,
                        NombreCompleto: estudiante.NombreCompleto,
                        respuestas: respuestas
                    };
                });
                return resultadosPsiAulaExcel;
            }
            return null;
        } catch (error) {
            console.warn('Error al obtener los resultados:', error);
        }
    }
    //#endregion

}
