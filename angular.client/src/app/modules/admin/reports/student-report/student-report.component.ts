import { Component } from '@angular/core';
import { OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { StudentService } from '../student.service';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { SharedModule } from 'app/shared/shared.module';
import { EvaluationService } from '../../evaluation/evaluation.service';
@Component({
    selector: "app-student-report",
    templateUrl: './student-report.component.html',
    standalone: true,
    imports: [SharedModule, MatIconModule, MatButtonToggleModule,]
})
export class StudentReportComponent implements OnInit {
    scales!: any;
    student!: any;
    studentId: number;
    unityId = 1;
    classroomId = 1;
    dimensionId = 1;

    students!: any;
    constructor(
        private route: ActivatedRoute,
        private evaluationService: EvaluationService,
        private router: Router,
        private studentService: StudentService
    ) { }
    ngOnInit(): void {
        const { id, classroomId, unityId } = this.getStudentInfo();
        this.studentId = parseInt(id);
        this.unityId = unityId;
        this.classroomId = classroomId;

        if (this.studentId && typeof this.studentId === 'number') {
            this.getStudent();
            this.toggleChange(1);
        }
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

    getStudent() {
        this.evaluationService.getStudent().subscribe(({ data }) => {
            this.student = this.studentService.students.find(
                (e) => (e.id = this.studentId)
            );
        });
    }
    getMeanClasses(mean: number) {
        if (mean >= 1 && mean <= 2) {
            return this.dimensionId == 1 ? 'bg-[#b6d7a8]' : 'bg-[#00b050]'
        } else if (mean >= 2.1 && mean <= 3.9) {
            return this.dimensionId == 1 ? 'bg-[#70ad47]' : 'bg-[#ffff00]'
        } else if (mean >= 4 && mean <= 5) {
            return this.dimensionId == 1 ? 'bg-[#548135]' : 'bg-[#ff0000]'
        } else {
            return 'bg-black'
        }
    }
    toggleChange(dimensionId: number) {
        this.evaluationService
            .getStudentAnswers(
                this.studentId,
                this.unityId,
                this.classroomId,
                dimensionId,
            )
            .subscribe(({ data }: { data: any }) => {
                this.scales = data.escalasPsicologicas.map(
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
            });
    }
}
