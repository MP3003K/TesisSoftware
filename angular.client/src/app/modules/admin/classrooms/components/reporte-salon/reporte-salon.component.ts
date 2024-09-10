import { AfterViewInit, Component, EventEmitter, Input, Output, SimpleChanges, ViewChild, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FiltrosSeleccionados } from '../../models/filtros-seleccionados.model';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ClassroomsService } from '../../services';
import { EvaluationService } from 'app/modules/admin/evaluation/evaluation.service';
import { ReporteEstudianteComponent } from '../reporte-estudiante/reporte-estudiante.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatCheckboxModule } from '@angular/material/checkbox';
'@angular/material/button-toggle';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { ItemOptions, EstadoEvaluacionEstudiante } from '../../enums';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from 'app/shared/shared.module';
import { DimensionPsicologica } from '../../enums/dimensionPsicologica.enum';
import { ButtonToogle } from '../../interfaces';
import { TooltipPosition, MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';


import { saveAs } from 'file-saver';
import * as ExcelJS from 'exceljs/dist/exceljs.min.js';


export interface Student {
    nombre: string;
    estadoEvaluacionEstudiante: string;
    promedio: number;
    id: number;
}
@Component({
    selector: 'app-reporte-salon',
    standalone: true,
    imports: [
        CommonModule,
        SharedModule,
        ReporteEstudianteComponent,
        MatButtonToggleModule,
        MatTableModule,
        MatCheckboxModule,
        MatIconModule,
        MatSortModule,
        ReporteEstudianteComponent,
        MatTooltipModule,
        MatButtonModule
    ],
    templateUrl: './reporte-salon.component.html'
})

export class ReporteSalonComponent implements AfterViewInit {
    @Input() filtrosSeleccionados: FiltrosSeleccionados;
    @Input() cargarValoresIniciales: boolean;

    @Output() itemOption = new EventEmitter<ItemOptions>();

    @ViewChild('estudiantesTable', { read: MatSort }) estudiantesTableMatSort: MatSort;

    reporteSalon = new MatTableDataSource<Student>([]);
    reporteSalonFiltrado = new MatTableDataSource<Student>([]);

    evaluacionPsicologicaAulaId: number = 0;
    visualizarReporteEstudiante: boolean = false;
    mensajeSnackbar: string = "";

    cargarValoresInicialesReporteEstudiante: boolean = false;
    estudianteId: number = 0;
    estudianteNombre: string = '';
    displayedColumns: string[] = ['nombre', 'DNI', 'estadoEvaluacionEstudiante', 'promedio', 'opciones']; dataSource = new MatTableDataSource<Student>([]);

    constructor(
        private classroomsService: ClassroomsService,
        private evaluationService: EvaluationService,
        private _snackbar: MatSnackBar,
    ) {
        this.estadoEvaluacionPsicologica = EstadoEvaluacionEstudiante.Todos;
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes.cargarValoresIniciales) {
            this.obtenerReporteSalon();
        }
    }

    ngAfterViewInit() {
        this.reporteSalonFiltrado.sort = this.estudiantesTableMatSort;
    }

    // #region buttones toogle
    claseInactiva: string = 'bg-white text-black text-sm';
    estadoEvaluacionPsicologica: EstadoEvaluacionEstudiante;

    botonesDimensiones: ButtonToogle[] = [
        {
            id: DimensionPsicologica.habilidadesSocioemocionales.toString(),
            texto: 'Habilidades Socioemocionales',
            accion: () => {
                this.filtrosSeleccionados.Dimension = DimensionPsicologica.habilidadesSocioemocionales;
                this.obtenerReporteSalon();
            },
            claseActiva: 'bg-[#f9fafb] text-green-700 font-bold text-base',
            claseInactiva: this.claseInactiva,
            esVisible: () => true
        },
        {
            id: DimensionPsicologica.factoresDeRiesgo.toString(),
            texto: 'Factores de Riesgo',
            accion: () => {
                this.filtrosSeleccionados.Dimension = DimensionPsicologica.factoresDeRiesgo;
                this.obtenerReporteSalon();
            },
            claseActiva: 'bg-[#f9fafb] text-orange-400 font-bold text-base',
            claseInactiva: this.claseInactiva,
            esVisible: () => true
        }
    ];

    claseInactivaEstado: string = 'bg-[#f1f5f9] text-black text-sm';
    claseActivaEstado: string = 'bg-white text-black text-base font-bold';

    botonesEstado: ButtonToogle[] = [
        {
            id: EstadoEvaluacionEstudiante.Todos.toString(),
            texto: EstadoEvaluacionEstudiante.Todos,
            accion: () => this.filtrarReporteSalon(EstadoEvaluacionEstudiante.Todos),
            claseActiva: this.claseActivaEstado,
            claseInactiva: this.claseInactivaEstado,
            esVisible: () => true
        },
        {
            id: EstadoEvaluacionEstudiante.NoInicio.toString(),
            texto: EstadoEvaluacionEstudiante.NoInicio,
            accion: () => this.filtrarReporteSalon(EstadoEvaluacionEstudiante.NoInicio),
            claseActiva: this.claseActivaEstado,
            claseInactiva: this.claseInactivaEstado,
            esVisible: () => true
        },
        {
            id: EstadoEvaluacionEstudiante.EnInicio.toString(),
            texto: EstadoEvaluacionEstudiante.EnInicio,
            accion: () => this.filtrarReporteSalon(EstadoEvaluacionEstudiante.EnInicio),
            claseActiva: this.claseActivaEstado,
            claseInactiva: this.claseInactivaEstado,
            esVisible: () => this.filtrosSeleccionados.Dimension === DimensionPsicologica.habilidadesSocioemocionales
        },
        {
            id: EstadoEvaluacionEstudiante.EnProceso.toString(),
            texto: EstadoEvaluacionEstudiante.EnProceso,
            accion: () => this.filtrarReporteSalon(EstadoEvaluacionEstudiante.EnProceso),
            claseActiva: this.claseActivaEstado,
            claseInactiva: this.claseInactivaEstado,
            esVisible: () => this.filtrosSeleccionados.Dimension === DimensionPsicologica.habilidadesSocioemocionales
        },
        {
            id: EstadoEvaluacionEstudiante.Satisfactorio.toString(),
            texto: EstadoEvaluacionEstudiante.Satisfactorio,
            accion: () => this.filtrarReporteSalon(EstadoEvaluacionEstudiante.Satisfactorio),
            claseActiva: this.claseActivaEstado,
            claseInactiva: this.claseInactivaEstado,
            esVisible: () => this.filtrosSeleccionados.Dimension === DimensionPsicologica.habilidadesSocioemocionales
        },
        {
            id: EstadoEvaluacionEstudiante.AltoRiesgo.toString(),
            texto: EstadoEvaluacionEstudiante.AltoRiesgo,
            accion: () => this.filtrarReporteSalon(EstadoEvaluacionEstudiante.AltoRiesgo),
            claseActiva: this.claseActivaEstado,
            claseInactiva: this.claseInactivaEstado,
            esVisible: () => this.filtrosSeleccionados.Dimension === DimensionPsicologica.factoresDeRiesgo
        },
        {
            id: EstadoEvaluacionEstudiante.RiesgoModerado.toString(),
            texto: EstadoEvaluacionEstudiante.RiesgoModerado,
            accion: () => this.filtrarReporteSalon(EstadoEvaluacionEstudiante.RiesgoModerado),
            claseActiva: this.claseActivaEstado,
            claseInactiva: this.claseInactivaEstado,
            esVisible: () => this.filtrosSeleccionados.Dimension === DimensionPsicologica.factoresDeRiesgo
        },
        {
            id: EstadoEvaluacionEstudiante.BajoRiesgo.toString(),
            texto: EstadoEvaluacionEstudiante.BajoRiesgo,
            accion: () => this.filtrarReporteSalon(EstadoEvaluacionEstudiante.BajoRiesgo),
            claseActiva: this.claseActivaEstado,
            claseInactiva: this.claseInactivaEstado,
            esVisible: () => this.filtrosSeleccionados.Dimension === DimensionPsicologica.factoresDeRiesgo
        }
    ];

    get botonesEstadosVisibles() {
        return this.botonesEstado.filter(b => b.esVisible());
    }

    getButtonClassDimension(boton: ButtonToogle): string {
        if (this.filtrosSeleccionados.Dimension.toString() == boton.id) {
            return boton.claseActiva;
        }
        return boton.claseInactiva;
    }

    getButtonClassEstado(boton: ButtonToogle): string {
        if (this.estadoEvaluacionPsicologica.toString() == boton.id) {
            return boton.claseActiva;
        }
        return boton.claseInactiva;
    }

    // #endregion

    // #region reporteSalon
    obtenerReporteSalon() {
        this.reporteSalon.data = [];
        this.reporteSalonFiltrado.data = [];
        if (!this.filtrosSeleccionados.succeeded) return;

        this.classroomsService
            .getEvaluation(this.filtrosSeleccionados.Unidad, this.filtrosSeleccionados.Seccion)
            .subscribe((response) => {
                if (response.succeeded) {
                    let id = response.data?.id;
                    if (id) {
                        this.evaluacionPsicologicaAulaId = id;
                        this.evaluationService
                            .getClasroomAnswers(id, this.filtrosSeleccionados.Dimension)
                            .subscribe({
                                next: (response) => {
                                    if (response.succeeded) {
                                        this.reporteSalon.data = response.data;
                                        this.filtrarReporteSalon(EstadoEvaluacionEstudiante.Todos);
                                    }
                                },
                                error: ({ status }) => {
                                    this.mensajeSnackbar = 'Sin Elementos';
                                    if (status === 400) {
                                        this._snackbar.open(
                                            'Sin elementos',
                                            '',
                                            { duration: 2000 }
                                        );
                                    }
                                }
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

    filtrarReporteSalon(estado: EstadoEvaluacionEstudiante) {
        this.estadoEvaluacionPsicologica = estado;

        if (estado == EstadoEvaluacionEstudiante.Todos) return this.reporteSalonFiltrado.data = this.reporteSalon.data;

        this.reporteSalonFiltrado.data = this.reporteSalon.data.filter(estudiante => this.obtenerCategoriaPorPuntajeReporteEstudiante(estudiante.promedio) === estado);
    }

    getEstudiantesPorEstado(estado: string): number {
        return this.reporteSalon.data.filter(estudiante => estudiante.estadoEvaluacionEstudiante === estado).length;
    }

    obtenerCategoriaPorPuntajeReporteEstudiante(promedio: number): string {
        if (promedio === 0) {
            return EstadoEvaluacionEstudiante.NoInicio;
        }

        if (this.filtrosSeleccionados.Dimension == DimensionPsicologica.habilidadesSocioemocionales) {
            if (promedio > 0 && promedio <= 1) {
                return EstadoEvaluacionEstudiante.EnInicio;
            } else if (promedio > 1 && promedio < 3) {
                return EstadoEvaluacionEstudiante.EnProceso;
            } else if (promedio >= 3 && promedio <= 4) {
                return EstadoEvaluacionEstudiante.Satisfactorio;
            }
        }

        if (this.filtrosSeleccionados.Dimension == DimensionPsicologica.factoresDeRiesgo) {
            if (promedio >= 3 && promedio <= 4) {
                return EstadoEvaluacionEstudiante.AltoRiesgo;
            } else if (promedio > 1 && promedio < 3) {
                return EstadoEvaluacionEstudiante.RiesgoModerado;
            } else if (promedio > 0 && promedio <= 1) {
                return EstadoEvaluacionEstudiante.BajoRiesgo;
            }
        }

        return EstadoEvaluacionEstudiante.Error;
    }

    obtenerClasePorPuntajeReporteEstudiante(promedio: number) {
        if (promedio < 0 && promedio > 4) return 'bg-black text-white'; // Fuera de rango

        const dimensionId = this.filtrosSeleccionados.Dimension;

        if (dimensionId === 1) {
            if (promedio === 0) return 'bg-gray-200 text-gray-800';
            if (promedio <= 1) return 'bg-[#b6d7a8] text-black';
            if (promedio < 3) return 'bg-[#70ad47] text-white';
            return 'bg-[#548135] text-white';
        }

        if (dimensionId === 2) {
            if (promedio === 0) return 'bg-gray-200 text-gray-800';
            if (promedio <= 1) return 'bg-[#ff0000] text-white';
            if (promedio < 3) return 'bg-[#ffff00] text-black';
            return 'bg-[#00b050] text-white';
        }
    }

    // #endregion


    //#region DescargarExcel
    classrooms: any[] = [];

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
            let gradoId = this.filtrosSeleccionados.Grado;
            let unidadId = this.filtrosSeleccionados.Unidad;
            let unidadNombre = this.filtrosSeleccionados.UnidadNombre;
            let unidadNumero = this.filtrosSeleccionados.UnidadNumero;
            let gradoDegree = this.toDegree(this.filtrosSeleccionados.Grado);
            let seccionId = this.filtrosSeleccionados.Seccion;
            let seccionN = this.filtrosSeleccionados.SeccionN;
            let tipo_test_psi = gradoId >= 1 && gradoId <= 2 ? 1 : (gradoId >= 3 && gradoId <= 5 ? 2 : 0);

            if (tipo_test_psi === 0) return;

            const resultadosPsiAulaExcel = await this.getResultadosPsiAulaExcel(seccionId, unidadId, tipo_test_psi);

            if (!resultadosPsiAulaExcel) return;

            const plantilla = this.getPlantilla(tipo_test_psi);
            const response = await fetch(plantilla);
            const buffer = await response.arrayBuffer();
            const workbook = new ExcelJS.Workbook();
            await workbook.xlsx.load(buffer);
            const worksheet = workbook.getWorksheet('Base_de_respuestas');

            const worksheetIndice = workbook.getWorksheet('Índice');

            worksheetIndice.getCell('C7').value = unidadNombre;
            worksheetIndice.getCell('C9').value = gradoDegree;
            worksheetIndice.getCell('C11').value = seccionN;

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
            const nombreArchivo = 'eva_' + gradoDegree + seccionN + '_' + 'U' + unidadNombre + '.xlsx';
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

    ocultarReporteEstudiante(visualizar: boolean) {
        this.visualizarReporteEstudiante = !visualizar;
    }
}
