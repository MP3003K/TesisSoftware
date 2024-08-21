import { Component, OnInit } from '@angular/core';
import { StudentService } from '../../student.service';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink } from '@angular/router';
import { DatePipe, JsonPipe, NgClass } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

@Component({
    selector: 'app-questionary-list',
    templateUrl: './questionary-list.component.html',
    standalone: true,
    imports: [
        MatCardModule,
        MatIconModule,
        RouterLink,
        JsonPipe,
        DatePipe,
        NgClass,
        MatButtonModule,
    ],
})
export class QuestionaryListComponent implements OnInit {
    questionary: any = [];
    constructor(
        private studentService: StudentService,
        private router: Router
    ) { }
    ngOnInit(): void {
        this.studentService.getStudentEvaluations().subscribe((response) => {
            if (response) {
                this.questionary = response[0];
                console.log(this.questionary);
            }
        });
    }

    goToQuestionary(id: number) {
        console.log(id);
        this.router.navigate(['evaluation', id]);
    }
}
