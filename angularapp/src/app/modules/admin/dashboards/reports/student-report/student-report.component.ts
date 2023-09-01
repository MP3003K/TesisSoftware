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
    dimension = 1;
    unity = 1;
    classroom = 1;
    selectedDimension = 1;
    students!: any;
    constructor(
        private route: ActivatedRoute,
        private evaluationService: EvaluationService,
        private router: Router,
        private studentService: StudentService,
    ) {}
    ngOnInit(): void {
        const studentId = this.getStudentId();
        if (studentId && typeof studentId === 'number') {
            this.getStudent(studentId);
            this.getStudentReport(studentId);
        }
    }

    returnRoute() {
        this.router.navigate(['/dashboards/reports']);
    }
    getStudentId() {
        return parseInt(this.route.snapshot.paramMap.get('id'));
    }

    getStudent(studentId: number) {
        this.evaluationService.getStudent(studentId).subscribe(({ data }) => {
            console.log(data);
        });
    }

    getStudentReport(index: number) {
        this.evaluationService
            .getStudentAnswers(
                index,
                this.unity,
                this.classroom,
                this.dimension,
            )
            .subscribe(({ data }: { data: any }) => {
                console.log(data);
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
                            }),
                        ),
                    }),
                );
                this.student = this.studentService.students.find(
                    (e) => (e.id = index),
                );
            });
    }
}
