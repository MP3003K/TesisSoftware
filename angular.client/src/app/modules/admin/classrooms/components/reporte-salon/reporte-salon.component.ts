import { AfterViewInit, Component, EventEmitter, Input, Output, SimpleChanges, ViewChild, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FiltrosSeleccionados } from '../../models/filtros-seleccionados.model';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Student } from 'app/modules/admin/reports/classroom-report/classroom-report.component';
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
    claseInactiva: string = 'bg-white text-black text-md';
    estadoEvaluacionPsicologica: EstadoEvaluacionEstudiante;


    botonesDimensiones: ButtonToogle[] = [
        {
            id: DimensionPsicologica.habilidadesSocioemocionales.toString(),
            texto: 'Habilidades Socioemocionales',
            accion: () => {
                this.filtrosSeleccionados.Dimension = DimensionPsicologica.habilidadesSocioemocionales;
                this.obtenerReporteSalon();
            },
            claseActiva: 'bg-[#f9fafb] text-green-700 font-bold text-md',
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
            claseActiva: 'bg-[#f9fafb] text-orange-400 font-bold text-md',
            claseInactiva: this.claseInactiva,
            esVisible: () => true
        }
    ];

    claseInactivaEstado: string = 'bg-[#f1f5f9] text-black text-md';
    botonesEstado: ButtonToogle[] = [
        {
            id: EstadoEvaluacionEstudiante.Todos.toString(),
            texto: 'Todos',
            accion: () => this.filtrarReporteSalon(EstadoEvaluacionEstudiante.Todos),
            claseActiva: 'bg-white text-black font-bold text-md',
            claseInactiva: this.claseInactivaEstado,
            esVisible: () => true
        },
        {
            id: EstadoEvaluacionEstudiante.EnInicio.toString(),
            texto: 'En Inicio',
            accion: () => this.filtrarReporteSalon(EstadoEvaluacionEstudiante.EnInicio),
            claseActiva: 'bg-white text-black font-bold text-md',
            claseInactiva: this.claseInactivaEstado,
            esVisible: () => this.filtrosSeleccionados.Dimension === DimensionPsicologica.habilidadesSocioemocionales
        },
        {
            id: EstadoEvaluacionEstudiante.EnProceso.toString(),
            texto: 'En Proceso',
            accion: () => this.filtrarReporteSalon(EstadoEvaluacionEstudiante.EnProceso),
            claseActiva: 'bg-white text-black font-bold text-md',
            claseInactiva: this.claseInactivaEstado,
            esVisible: () => this.filtrosSeleccionados.Dimension === DimensionPsicologica.habilidadesSocioemocionales
        },
        {
            id: EstadoEvaluacionEstudiante.Satisfactorio.toString(),
            texto: 'Satisfactorio',
            accion: () => this.filtrarReporteSalon(EstadoEvaluacionEstudiante.Satisfactorio),
            claseActiva: 'bg-white text-black font-bold text-md',
            claseInactiva: this.claseInactivaEstado,
            esVisible: () => this.filtrosSeleccionados.Dimension === DimensionPsicologica.habilidadesSocioemocionales
        },
        {
            id: EstadoEvaluacionEstudiante.AltoRiesgo.toString(),
            texto: 'Alto Riesgo',
            accion: () => this.filtrarReporteSalon(EstadoEvaluacionEstudiante.AltoRiesgo),
            claseActiva: 'bg-white text-black font-bold text-md',
            claseInactiva: this.claseInactivaEstado,
            esVisible: () => this.filtrosSeleccionados.Dimension === DimensionPsicologica.factoresDeRiesgo
        },
        {
            id: EstadoEvaluacionEstudiante.RiesgoModerado.toString(),
            texto: 'Riesgo Moderado',
            accion: () => this.filtrarReporteSalon(EstadoEvaluacionEstudiante.RiesgoModerado),
            claseActiva: 'bg-white text-black font-bold text-md',
            claseInactiva: this.claseInactivaEstado,
            esVisible: () => this.filtrosSeleccionados.Dimension === DimensionPsicologica.factoresDeRiesgo
        },
        {
            id: EstadoEvaluacionEstudiante.BajoRiesgo.toString(),
            texto: 'Bajo Riesgo',
            accion: () => this.filtrarReporteSalon(EstadoEvaluacionEstudiante.BajoRiesgo),
            claseActiva: 'bg-white text-black font-bold text-md',
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
                                        this.reporteSalonFiltrado.data = response.data;
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

        if (estado == EstadoEvaluacionEstudiante.Todos) this.reporteSalonFiltrado.data = this.reporteSalon.data;
        console.log('filtrar', 'estado', estado)
        this.reporteSalonFiltrado.data = this.reporteSalon.data.filter(estudiante => estudiante.estadoEvaluacionEstudiante === estado);
    }

    getEstudiantesPorEstado(estado: string): number {
        return this.reporteSalon.data.filter(estudiante => estudiante.estadoEvaluacionEstudiante === estado).length;
    }

    obtenerCategoriaPorPuntajeReporteEstudiante(promedio: number): string {
        if (promedio === 0) {
            return 'No Inicio';
        }

        if (this.filtrosSeleccionados.Dimension == DimensionPsicologica.habilidadesSocioemocionales) {
            if (promedio > 0 && promedio <= 1) {
                return 'En Inicio';
            } else if (promedio > 1 && promedio < 3) {
                return 'En Proceso';
            } else if (promedio >= 3 && promedio <= 4) {
                return 'Satisfactorio';
            }
        }

        if (this.filtrosSeleccionados.Dimension == DimensionPsicologica.factoresDeRiesgo) {
            if (promedio >= 3 && promedio <= 4) {
                return 'Alto Riesgo';
            } else if (promedio > 1 && promedio < 3) {
                return 'Riesgo Moderado';
            } else if (promedio > 0 && promedio <= 1) {
                return 'Bajo Riesgo';
            }
        }

        return 'No inicio'; // Por defecto si no cumple ninguna condición anterior
    }

    obtenerClasePorPuntajeReporteEstudiante(promedio: number) {
        const styles = {
            1: [ // Estilos para la dimensión 1
                { range: [0, 0], class: 'bg-gray-200 text-gray-800' }, // Estilo para promedio 0
                { range: [0.01, 1], class: 'bg-[#b6d7a8] text-black' },
                { range: [1, 3], class: 'bg-[#70ad47] text-white' },
                { range: [3, 4], class: 'bg-[#548135] text-white' }
            ],
            2: [ // Estilos para la dimensión 2
                { range: [0, 0], class: 'bg-gray-200 text-gray-800' }, // Estilo para promedio 0
                { range: [0.01, 1], class: 'bg-[#00b050] text-white' },
                { range: [1, 3], class: 'bg-[#ffff00] text-black' },
                { range: [3, 4], class: 'bg-[#ff0000] text-white' }
            ]
        };

        const dimensionId = this.filtrosSeleccionados.Dimension;
        const defaultClass = 'bg-black'; // Clase por defecto si no se encuentra en ningún rango
        const dimensionStyles = styles[dimensionId];

        for (const { range, class: className } of dimensionStyles) {
            if (promedio >= range[0] && promedio <= range[1]) {
                return className;
            }
        }

        return defaultClass;
    }

    // #endregion

    redirigirReporteEstudiante(estudiante: Student) {
        console.log(estudiante, 'estudiante')
    }
    ocultarReporteEstudiante(visualizar: boolean) {
        this.visualizarReporteEstudiante = !visualizar;
    }
}
