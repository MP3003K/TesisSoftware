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
import { MatSort } from '@angular/material/sort';
import { ItemOptions } from '../../enums';
import { ChangeDetectionStrategy } from '@angular/core';
import { MatButtonToggleModule } from '@angular/material/button-toggle';


@Component({
    selector: 'app-reporte-salon',
    standalone: true,
    imports: [
        CommonModule,
        ReporteEstudianteComponent,
        MatButtonToggleModule,
        MatTableModule, MatCheckboxModule,],
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './reporte-salon.component.html'
})
export class ReporteSalonComponent implements AfterViewInit {
    @Input() filtrosSeleccionados: FiltrosSeleccionados;
    @Input() cargarValoresIniciales: boolean;

    @Output() itemOption = new EventEmitter<ItemOptions>();

    @ViewChild('estudiantesTable', { read: MatSort }) estudiantesTableMatSort: MatSort;

    reporteSalon = new MatTableDataSource<Student>([]);
    evaluacionPsicologicaAulaId: number = 0;
    mensajeSnackbar: string = "";
    dimensionSeleccionadoIndicador = signal(false);





    displayedColumns: string[] = ['nombre', 'DNI', 'estadoEvaluacionEstudiante', 'promedio', 'opciones']; dataSource = new MatTableDataSource<Student>([]);

    ngOnChanges(changes: SimpleChanges): void {
        if (changes.cargarValoresIniciales) {
            this.obtenerReporteSalon();
        }
    }
    constructor(
        private classroomsService: ClassroomsService,
        private evaluationService: EvaluationService,
        private _snackbar: MatSnackBar,
    ) {

    }

    ngAfterViewInit() {
        this.reporteSalon.sort = this.estudiantesTableMatSort;
    }

    async obtenerReporteSalon() {
        this.reporteSalon.data = [];
        if (!this.filtrosSeleccionados.succeeded) return;

        this.classroomsService
            .getEvaluation(this.filtrosSeleccionados.unidad, this.filtrosSeleccionados.seccion)
            .subscribe((response) => {
                if (response.succeeded) {
                    let id = response.data?.id;
                    if (id) {
                        this.evaluacionPsicologicaAulaId = id;
                        this.evaluationService
                            .getClasroomAnswers(id, this.filtrosSeleccionados.dimension)
                            .subscribe({
                                next: (response) => {
                                    if (response.succeeded) {
                                        this.reporteSalon.data = response.data;
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

    getEstudiantesPorEstado(estado: string): number {
        return this.reporteSalon.data.filter(estudiante => estudiante.estadoEvaluacionEstudiante === estado).length;
    }

    hideSingleSelectionIndicator = signal(false);
    hideMultipleSelectionIndicator = signal(false);

    toggleSingleSelectionIndicator() {
        this.hideSingleSelectionIndicator.update(value => !value);
    }

    toggleMultipleSelectionIndicator() {
        this.hideMultipleSelectionIndicator.update(value => !value);
    }
}
