import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { ClassroomsService } from '../../services'
import { ItemOptions } from '../../enums';
import { FiltrosSeleccionados } from '../../models/filtros-seleccionados.model';
import { Estudiante } from '../../models/estudiante.model';

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
export class EditarEstudianteComponent implements OnInit {

    @Input() filtrosSeleccionados: FiltrosSeleccionados;
    @Input() estudiante: Estudiante;

    @Output() itemOption = new EventEmitter<ItemOptions>();

    ngOnInit() {
        let estudiante = this.estudiante;
        this.formEditarEstudiante.setValue({
            id: estudiante.id.toString(),
            nombres: estudiante.nombres,
            apellidoPaterno: estudiante.apellidoPaterno,
            apellidoMaterno: estudiante.apellidoMaterno,
            dni: estudiante.dni,
        });
    }

    formEditarEstudiante = new FormGroup({
        id: new FormControl('', [Validators.required]),
        nombres: new FormControl('', [Validators.required]),
        apellidoPaterno: new FormControl('', [Validators.required]),
        apellidoMaterno: new FormControl('', [Validators.required]),
        dni: new FormControl('', [
            Validators.required,
            Validators.pattern('^[0-9]{8}$'),
        ]),
    });

    constructor(
        private classroomsService: ClassroomsService,
    ) { }


    editEstudiante(): void {
        this.classroomsService
            .actualizarEstudiante(this.formEditarEstudiante.value)
            .subscribe(
                (response) => {
                    if (response.succeeded) {
                        this.itemOption.emit(ItemOptions.ListarEstudiantes);
                    }
                },
                (error) => {
                    console.error('Error al realizar la solicitud: ', error);
                }
            );
    }
}
