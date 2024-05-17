import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EvaluationService } from '../../evaluation/evaluation.service';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentRegistrationComponent } from '../student-registration/student-registration.component';
import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from 'app/shared/shared.module';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { ClassroomsService } from '../../classrooms/classrooms.service';
import { MatButtonModule } from '@angular/material/button';

// librerias para exportar a excel
import { saveAs } from 'file-saver';
import * as ExcelJS from 'exceljs/dist/exceljs.min.js';
import { MatTableDataSource, MatTableModule } from "@angular/material/table";

// librerias para el ordenamiento de la tabla
import { MatSort, MatSortModule, Sort } from '@angular/material/sort';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Subscription } from 'rxjs';

export interface Student {
    nombre: string;
    estadoEvaluacionEstudiante: string;
    promedio: number;
    id: number;
}
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
        MatTableModule,
        MatSortModule,
    ],
})
export class ClassroomReportComponent implements AfterViewInit {
    public dataInfo: string = 'Seleccione un formulario';
    public selectedUnity!: number;
    public selectedDimension!: number;
    public selectedDegree!: number;
    public selectedSection!: number;
    public selectedClassroomEvaluationId!: number;

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
        private route: ActivatedRoute,
        private classroomsService: ClassroomsService,
        private _liveAnnouncer: LiveAnnouncer
    ) { }


    ngOnInit(): void {
        this.getUnities();
        this.getClasses();
        this.agregarValoresInicialesFiltros();
    }


    // #region Filtros

    agregarValoresInicialesFiltros() {
        const filtros = this.route.snapshot.queryParams.filtros;
        if (filtros && filtros.length > 0) {
            const valores = filtros.split(',');

            this.selectedUnity = Number(valores[0]);
            this.selectedDegree = valores[1];
            this.selectedDimension = Number(valores[3]);
            this.selectedSection = Number(valores[2]);

            if (this.selectedUnity && this.selectedDegree && this.selectedSection && this.selectedDimension)
                this.filterAll();
        }
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
    getEstudiantesPorEstado(estado: string): number {
        return this.dataSource.data.filter(estudiante => estudiante.estadoEvaluacionEstudiante === estado).length;
    }
    // #endregion
    // #region Obtener datos de los estudiantes
    displayedColumns: string[] = ['nombre', 'DNI', 'estadoEvaluacionEstudiante', 'promedio', 'opciones']; dataSource = new MatTableDataSource<Student>([]);

    @ViewChild('estudiantesTable', { read: MatSort }) estudiantesTableMatSort: MatSort;

    ngAfterViewInit() {
        this.dataSource.sort = this.estudiantesTableMatSort;
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
                            .getClasroomAnswers(id, this.selectedDimension)
                            .subscribe({
                                next: (response) => {
                                    if (response.succeeded) {
                                        this.dataSource.data = response.data;
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
    ordenamientoTabla(sort: Sort) {
        const data = this.dataSource.data.slice(); // Crea una copia de los datos

        if (!sort.active || sort.direction === '') {
            this.dataSource.data = data;
            return;
        }

        this.dataSource.data = data.sort((a, b) => {
            const isAsc = sort.direction === 'asc';
            switch (sort.active) {
                case 'nombre': return this.compare(a.nombre, b.nombre, isAsc);
                case 'estadoEvaluacionEstudiante': return this.compare(a.estadoEvaluacionEstudiante, b.estadoEvaluacionEstudiante, isAsc);
                case 'promedio': return this.compare(a.promedio, b.promedio, isAsc);
                case 'id': return this.compare(a.id, b.id, isAsc);
                default: return 0;
            }
        });
    }

    compare(a: number | string, b: number | string, isAsc: boolean) {
        return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
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
        let filtrosActuales: string = this.selectedUnity + ',' + this.selectedDegree + ',' + this.selectedSection + ',' + this.selectedDimension;
        this.router.navigate(['reports', index], {
            queryParams: {
                classroomEvaluationId: this.selectedClassroomEvaluationId,
                filtros: filtrosActuales
            },
        });
    }

    getMeanClasses(mean: number) {
        if (mean >= 0 && mean <= 1) {
            return this.selectedDimension == 1
                ? 'bg-[#b6d7a8] text-black'
                : 'bg-[#00b050] text-white';
        } else if (mean > 1 && mean <= 3) {
            return this.selectedDimension == 1
                ? 'bg-[#70ad47] text-black'
                : 'bg-[#ffff00] text-black';
        } else if (mean >= 3 && mean <= 4) {
            return this.selectedDimension == 1
                ? 'bg-[#548135] text-white'
                : 'bg-[#ff0000] text-white';
        } else {
            return 'bg-black';
        }
    }

    dimensiones_conceptos = {
        1: {
            rango1: 'En inicio',
            rango2: 'En proceso',
            rango3: 'Satisfactorio',
            fueraRango: 'Fuera de rango'
        },
        2: {
            rango1: 'Bajo Riesgo',
            rango2: 'Riesgo Moderado',
            rango3: 'Alto Riesgo',
            fueraRango: 'Fuera de rango'
        }
    };

    getConceptoDeResultadosPsi(promedio: number): string {
        const dimension = this.dimensiones_conceptos[this.selectedDimension];

        if (promedio >= 0 && promedio <= 1) {
            return dimension.rango1;
        } else if (promedio > 1 && promedio < 3) {
            return dimension.rango2;
        } else if (promedio >= 3 && promedio <= 4) {
            return dimension.rango3;
        } else {
            return dimension.fueraRango;
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

    // #endregion

    //#region DescargarExcel
    readonly PLANTILLAS = {
        1: 'assets/plantillas/plantilla_resultados_hse_1y2.xlsx',
        2: 'assets/plantillas/plantilla_resultados_hse_3,4y5.xlsx'
    };

    readonly RANGOS_GRADO = {
        1: { inicio: 1, fin: 66 },
        2: { inicio: 67, fin: 134 }
    };

    getRangoGrado(_tipo_test_psi: number) {
        return this.RANGOS_GRADO[_tipo_test_psi] || null;
    }

    getPlantilla(_tipo_test_psi: number) {
        return this.PLANTILLAS[_tipo_test_psi] || null;
    }

    async exportarExcel_resultadosPsicologicosAula() {
        try {
            let selectedDegreeNumber = Number(this.selectedDegree);
            let tipo_test_psi = [1, 2].includes(selectedDegreeNumber) ? 1 : ([3, 4, 5].includes(selectedDegreeNumber) ? 2 : 0);

            if (tipo_test_psi === 0) return;

            const resultadosPsiAulaExcel = await this.getResultadosPsiAulaExcel(this.selectedSection, this.selectedUnity, tipo_test_psi);

            if (!resultadosPsiAulaExcel) return;

            const plantilla = this.getPlantilla(tipo_test_psi);
            const response = await fetch(plantilla);
            const buffer = await response.arrayBuffer();
            const workbook = new ExcelJS.Workbook();
            await workbook.xlsx.load(buffer);
            const worksheet = workbook.getWorksheet('BASE DE RESPUESTAS');

            const worksheetIndice = workbook.getWorksheet('Índice');
            let unidad = this.toRoman(this.unities.find(u => u.id === this.selectedUnity).nUnidad);
            let grado = this.toDegree(this.selectedDegree);
            let seccion = this.classrooms.find(c => c.id === this.selectedSection).seccion;
            worksheetIndice.getCell('C7').value = unidad;
            worksheetIndice.getCell('C9').value = grado;
            worksheetIndice.getCell('C11').value = seccion;

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
            const nombreArchivo = 'eva_' + grado + seccion + '_' + 'U' + unidad;
            saveAs(blob, nombreArchivo);
        } catch (error) {
            console.error(error);
        }
    }
    toRoman(num) {
        const roman = ['M', 'CM', 'D', 'CD', 'C', 'XC', 'L', 'XL', 'X', 'IX', 'V', 'IV', 'I'];
        const decimal = [1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1];
        let romanNum = '';
        for (let i = 0; i < decimal.length; i++) {
            while (decimal[i] <= num) {
                romanNum += roman[i];
                num -= decimal[i];
            }
        }
        return romanNum;
    }
    toDegree(num) {
        return `${num}°`;
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
