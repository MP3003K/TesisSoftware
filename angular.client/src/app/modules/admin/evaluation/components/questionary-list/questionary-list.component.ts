import { Component, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink } from '@angular/router';
import { CommonModule, DatePipe, JsonPipe, NgClass } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { EvaluationService } from '../../evaluation.service';

@Component({
    selector: 'app-questionary-list',
    templateUrl: './questionary-list.component.html',
    standalone: true,
    imports: [
        CommonModule,
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
    questionnaires: any = [];
    constructor(
        private evaluationService: EvaluationService,
        private router: Router
    ) { }
    examenes: {
        nombreCompleto: string;
        esemocional: boolean;
        validacion: boolean;
        respuesta: string;
    }[] = [];
    ngOnInit(): void {
        this.evaluationService.obtenerExamenesEstudiante().subscribe((response) => {
            if (response.succeeded && response!.data) {
                this.examenes = response.data;
            }
        });
    }
    goToQuestionary(esemocional: boolean) {
        let numeroRuta :Number = esemocional ? 1 : 0;
        this.router.navigate(['evaluation', numeroRuta]);
    }
}
