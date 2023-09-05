import { Component } from '@angular/core';
import { OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EvaluationService } from '../../evaluation/evaluation.service';
import { StudentService } from '../student.service';
@Component({
    templateUrl: './student-report.component.html',
})
export class StudentReportComponent implements OnInit {
    scales!: any;
    student!: any;
    studentId: number;
    unityId = 1;
    classroomId = 1;
    students!: any;
    constructor(
        private route: ActivatedRoute,
        private evaluationService: EvaluationService,
        private router: Router,
        private studentService: StudentService
    ) {}
    ngOnInit(): void {
        const { id, classroomId, unityId } = this.getStudentInfo();
        this.studentId = parseInt(id);
        this.unityId = unityId;
        this.classroomId = classroomId;

        if (this.studentId && typeof this.studentId === 'number') {
            this.getStudent(this.studentId);
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

    getStudent(studentId: number) {
        this.evaluationService.getStudent(studentId).subscribe(({ data }) => {
            console.log(data);
            this.student = this.studentService.students.find(
                (e) => (e.id = this.studentId)
            );
        });
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
