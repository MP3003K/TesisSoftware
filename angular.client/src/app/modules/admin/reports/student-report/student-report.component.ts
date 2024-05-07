import { Component } from '@angular/core';
import { OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { StudentService } from '../student.service';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { SharedModule } from 'app/shared/shared.module';
import { EvaluationService } from '../../evaluation/evaluation.service';
@Component({
    selector: 'app-student-report',
    templateUrl: './student-report.component.html',
    standalone: true,
    imports: [SharedModule, MatIconModule, MatButtonToggleModule],
})
export class StudentReportComponent implements OnInit {
    student: any;
    scales = [];
    reportTypes: string[] = ['HSE', 'FR'];
    selectedDimension = this.reportTypes[0];
    evaluationId = 302;
    classroomEvaluationId = 0;
    studentId = 0;
    filtros: string = '';
    constructor(
        private route: ActivatedRoute,
        private evaluationService: EvaluationService,
        private router: Router,
        private studentService: StudentService
    ) { }
    ngOnInit(): void {
        const studentId = +this.route.snapshot.paramMap.get('id');
        const { classroomEvaluationId, filtros } = this.route.snapshot.queryParams;
        this.filtros = filtros;
        this.classroomEvaluationId = classroomEvaluationId;
        this.studentId = studentId;
        if (this.classroomEvaluationId && this.studentId) {
            this.getStudentReport();
        } else {
            console.log('ERROR');
        }
    }

    returnRoute() {
        this.router.navigate(['/reports'], {
            queryParams: {
                filtros: this.filtros
            },
        });
    }
    getStudentInfo() {
        const studentInfo = {
            ...this.route.snapshot.queryParams,
            ...this.route.snapshot.params,
        };
        return studentInfo;
    }


    getMeanClasses(mean: number) {
        let dimensionId = this.selectedDimension == 'HSE' ? 1 : 2;
        if (mean >= 0 && mean <= 1) {
            return dimensionId == 1
                ? 'bg-[#b6d7a8] text-black'
                : 'bg-[#00b050] text-white';
        } else if (mean > 1 && mean <= 3) {
            return dimensionId == 1
                ? 'bg-[#70ad47] text-black'
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
        let dimensionId = this.selectedDimension == 'HSE' ? 1 : 2;
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
        this.evaluationService
            .getStudentEvaluation(this.classroomEvaluationId, this.studentId)
            .subscribe((response) => {
                if (response.succeeded) {
                    const { id } = response.data;
                    if (id) {
                        this.evaluationService
                            .getStudentAnswers(id, this.selectedDimension)
                            .subscribe((response) => {
                                if (response.succeeded) {
                                    const rr = response.data.reduce(
                                        (
                                            a,
                                            {
                                                scale,
                                                scaleMean,
                                                indicator: name,
                                                indicatorMean: mean,
                                            }
                                        ) => {
                                            if (scale in a) {
                                                a[scale]?.indicators.push({
                                                    name,
                                                    mean,
                                                });
                                            } else {
                                                a[scale] = {
                                                    mean: scaleMean,
                                                    indicators: [
                                                        { name, mean },
                                                    ],
                                                };
                                            }
                                            return a;
                                        },
                                        {}
                                    );

                                    this.scales = Object.keys(rr).map(
                                        (key) => ({
                                            name: key,
                                            ...rr[key],
                                        })
                                    );
                                }
                            });
                    }
                }
            });
    }
}
