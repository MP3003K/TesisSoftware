import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EvaluationService } from 'app/modules/admin/evaluation/evaluation.service';
import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from 'app/shared/shared.module';
import { FiltrosSeleccionados } from '../../models/filtros-seleccionados.model';
import { DimensionPsicologica } from '../../enums';
import { ButtonToogle } from '../../interfaces';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { timer, switchMap, forkJoin } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';

@Component({
    selector: 'app-reporte-estudiante',
    standalone: true,
    imports: [SharedModule, MatIconModule, MatProgressSpinnerModule, MatButtonModule],
    templateUrl: './reporte-estudiante.component.html'
})
export class ReporteEstudianteComponent {
    @Input() filtrosSeleccionados: FiltrosSeleccionados;
    @Input() evaluacionPsicologicaAulaId: number;
    @Input() estudianteId: number;
    @Input() cargarValoresIniciales: boolean;
    @Output() regregarReporteSalon = new EventEmitter<boolean>();

    reportTypes: DimensionPsicologica[] = [DimensionPsicologica.habilidadesSocioemocionales, DimensionPsicologica.factoresDeRiesgo];
    student: any;
    scales = [];

    isLoading: boolean = false;

    constructor(
        private route: ActivatedRoute,
        private evaluationService: EvaluationService,
    ) { }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes.cargarValoresIniciales) {
            this.getStudentReport();
        }
    }
    returnRoute() {
        this.regregarReporteSalon.emit(true);
    }
    getStudentInfo() {
        const studentInfo = {
            ...this.route.snapshot.queryParams,
            ...this.route.snapshot.params,
        };
        return studentInfo;
    }


    getMeanClasses(mean: number) {
        let dimensionId = this.filtrosSeleccionados.Dimension;
        if (mean >= 0 && mean <= 1) {
            return dimensionId == 1
                ? 'bg-[#b6d7a8] text-black'
                : 'bg-[#00b050] text-white';
        } else if (mean > 1 && mean <= 3) {
            return dimensionId == 1
                ? 'bg-[#70ad47] text-white'
                : 'bg-[#ffff00] text-black';
        } else if (mean >= 3 && mean <= 4) {
            return dimensionId == 1
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
        let dimensionId = this.filtrosSeleccionados.Dimension;
        const dimension = this.dimensiones_conceptos[dimensionId];

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
    getStudentReport() {
        this.isLoading = true;
        const minLoadingTime$ = timer(300); // Observable que emite después de 300 ms

        this.evaluationService.getStudentEvaluation(this.evaluacionPsicologicaAulaId, this.estudianteId)
            .pipe(
                switchMap(response => {
                    if (response.succeeded && response.data.id) {
                        // Si la primera solicitud es exitosa, procede con la segunda
                        const answers$ = this.evaluationService.getStudentAnswers(response.data.id, this.filtrosSeleccionados.Dimension == 1 ? 'HSE' : 'FR');
                        return forkJoin([answers$, minLoadingTime$]);
                    } else {
                        // Si la primera solicitud falla, solo espera el temporizador
                        return forkJoin([minLoadingTime$]).pipe(switchMap(() => { throw new Error('Failed to get student evaluation'); }));
                    }
                })
            )
            .subscribe({
                next: ([response]) => {
                    if (response.succeeded) {
                        const rr = response.data.reduce((a, { scale, scaleMean, indicator: name, indicatorMean: mean }) => {
                            if (scale in a) {
                                a[scale]?.indicators.push({ name, mean });
                            } else {
                                a[scale] = { mean: scaleMean, indicators: [{ name, mean }] };
                            }
                            return a;
                        }, {});

                        this.scales = Object.keys(rr).map(key => ({ name: key, ...rr[key] }));
                    }
                    this.isLoading = false;
                },
                error: (error) => {
                    console.error(error);
                    this.isLoading = false;
                }
            });
    }

    // #region Botones de dimensiones

    claseInactiva: string = 'bg-white text-black text-md';

    botonesDimensiones: ButtonToogle[] = [
        {
            id: DimensionPsicologica.habilidadesSocioemocionales.toString(),
            texto: 'Habilidades Socioemocionales',
            accion: () => {
                this.filtrosSeleccionados.Dimension = DimensionPsicologica.habilidadesSocioemocionales;
                this.getStudentReport();
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
                this.getStudentReport();
            },
            claseActiva: 'bg-[#f9fafb] text-orange-400 font-bold text-md',
            claseInactiva: this.claseInactiva,
            esVisible: () => true
        }
    ];

    getButtonClassDimension(boton: ButtonToogle): string {
        if (this.filtrosSeleccionados.Dimension.toString() == boton.id) {
            return boton.claseActiva;
        }
        return boton.claseInactiva;
    }

    // #endregion
}
