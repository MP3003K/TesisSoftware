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
    constructor(
        private route: ActivatedRoute,
        private evaluationService: EvaluationService,
        private router: Router,
        private studentService: StudentService
    ) {}
    ngOnInit(): void {
        this.getStudentReport();
    }

    returnRoute() {
        this.router.navigate(['/dashboards/reports']);
    }
    getStudentInfo() {
        const studentInfo = {
            ...this.route.snapshot.queryParams,
            ...this.route.snapshot.params,
        };
        return studentInfo;
    }

    getMeanClasses(mean: number) {
        if (mean >= 1 && mean <= 2) {
            return this.selectedDimension == 'HSE'
                ? 'bg-[#b6d7a8]'
                : 'bg-[#00b050]';
        } else if (mean >= 2.1 && mean <= 3.9) {
            return this.selectedDimension == 'HSE'
                ? 'bg-[#70ad47]'
                : 'bg-[#ffff00]';
        } else if (mean >= 4 && mean <= 5) {
            return this.selectedDimension == 'HSE'
                ? 'bg-[#548135]'
                : 'bg-[#ff0000]';
        } else {
            return 'bg-black';
        }
    }
    getStudentReport() {
        this.evaluationService
            .getStudentAnswers(this.evaluationId, this.selectedDimension)
            .subscribe((response) => {
                if (response.succeeded) {
                    console.log(response.data);
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
                                a[scale]?.indicators.push({ name, mean });
                            } else {
                                a[scale] = {
                                    mean: scaleMean,
                                    indicators: [{ name, mean }],
                                };
                            }
                            return a;
                        },
                        {}
                    );

                    this.scales = Object.keys(rr).map((key) => ({
                        name: key,
                        ...rr[key],
                    }));
                }
            });
    }
}
