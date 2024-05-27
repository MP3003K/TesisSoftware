import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ClassroomsService } from '../classrooms.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';

@Component({
    selector: 'app-editar-estudiante',
    standalone: true,
    imports: [
        CommonModule,
        MatFormFieldModule,
        MatInputModule,
        ReactiveFormsModule,
        MatButtonModule
    ],
    templateUrl: './editar-estudiante.component.html',
    styleUrl: './editar-estudiante.component.scss'
})
export class EditarEstudianteComponent {
    @Input() formEditarEstudiante: FormGroup;
    @Output() editarEstudianteSuccessful = new EventEmitter<boolean>();

    constructor(
        private classroomsService: ClassroomsService,
    ) { }


    editEstudiante(): void {
        this.classroomsService
            .actualizarEstudiante(this.formEditarEstudiante.value)
            .subscribe(
                (response) => {
                    if (response.succeeded) {
                        this.editarEstudianteSuccessful.emit(true);
                    }
                },
                (error) => {
                    console.error('Error al realizar la solicitud: ', error);
                }
            );
    }
}
