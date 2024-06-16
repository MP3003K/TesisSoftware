import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'app/shared/shared.module';

import { FuseConfirmationService } from '@fuse/services/confirmation';
import { FiltrosSeleccionados } from '../../models/filtros-seleccionados.model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ClassroomsService } from '../../services';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { FilterTablePipe } from '../../filter-table.pipe';
import { ItemOptions } from '../../enums';
import { MatButtonModule } from '@angular/material/button';
import { Estudiante } from '../../models/estudiante.model';


@Component({
    selector: 'app-listar-estudiantes',
    standalone: true,
    imports: [
        CommonModule,
        SharedModule,
        MatTableModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        FilterTablePipe,
        MatButtonModule
    ],
    templateUrl: './listar-estudiantes.component.html',
    styleUrl: './listar-estudiantes.component.scss'
})
export class ListarEstudiantesComponent {
    @Input() filtrosSeleccionados: FiltrosSeleccionados;
    @Input() cargarValoresIniciales: boolean;
    @Output() itemOption = new EventEmitter<ItemOptions>();
    @Output() estudianteParaEditar = new EventEmitter<Estudiante>();

    ngOnChanges(changes: SimpleChanges): void {
        if (changes.cargarValoresIniciales)
            this.obtnerEstudiantesPorAulaYUnidad();
    }

    classroomStudents = [];
    filteredStudents: any[] = [];

    estadoEvalucionPsicologicaAula: string = 'N';
    modificarAula: boolean = false;

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
        private confirmationService: FuseConfirmationService,
        private classroomsService: ClassroomsService
    ) { }

    obtnerEstudiantesPorAulaYUnidad() {
        this.classroomStudents = [];
        this.classroomsService
            .getStudentsByAulaYUnidad(this.filtrosSeleccionados.unidad, this.filtrosSeleccionados.seccion)
            .subscribe((response) => {
                if (response.succeeded) {
                    this.classroomStudents = response.data;
                    this.estadoEvalucionPsicologicaAula = this.classroomStudents[0]?.estadoAula ?? 'N';
                    this.updateEstadoModificarAula();
                }
            });
    }



    cambiarEstadoEvaluacionPsicologicaAula() {
        let estado = this.estadoEvalucionPsicologicaAula;
        if (estado == 'F') return;

        if (['N', 'P'].includes(estado)) {
            let opcion = estado === 'N' ? 1 : 0; // 1: Iniciar, 0: Finalizar
            this.classroomsService
                .updateEstadoEvaPsiAula(
                    this.filtrosSeleccionados.unidad,
                    this.filtrosSeleccionados.seccion,
                    opcion,
                    ''
                )
                .subscribe(
                    () => {
                        this.obtnerEstudiantesPorAulaYUnidad();
                    },
                    (error) => {
                        console.error(error);
                    }
                );
        }
    }

    openUpdateEstadoEvaPsiAulaModal(estado: string): void {
        if (estado == 'Evaluación Terminado') return;

        const dialogRef = this.confirmationService.open({
            title: '¿Deseas cambiar el estado del test psicologico?',
            message: 'Una vez aceptado no se podra revertir los cambios',
            dismissible: true,
            actions: {
                cancel: {
                    label: 'Cancelar',
                },
                confirm: {
                    label: 'Actualizar',
                },
            },
            icon: { color: 'info', name: 'info', show: true },
        });
        dialogRef.afterClosed().subscribe((res) => {
            if (res === 'confirmed') {
                this.cambiarEstadoEvaluacionPsicologicaAula();
            }
        });
    }


    updateEstadoModificarAula() {
        this.filtrosSeleccionados.unidad;
        this.modificarAula = this.filtrosSeleccionados.estadoUnidad;
    }

    editarEstudianteComponente(student: any) {
        if (!student) return;

        let estudiante = {
            id: student.estudianteId,
            nombres: student.nombres,
            apellidoPaterno: student.apellidoPaterno,
            apellidoMaterno: student.apellidoMaterno,
            dni: student.dni,
        };
        this.estudianteParaEditar.emit(estudiante);
        this.itemOption.emit(ItemOptions.EditarEstudiante);
    }

    deleteStudent(id: number) {
        this.classroomsService
            .eliminarEstudianteAula(
                id,
                this.filtrosSeleccionados.seccion,
                this.filtrosSeleccionados.unidad
            )
            .subscribe((res) => {
                if (res.succeeded) {
                    this.filteredStudents = [];
                    this.obtnerEstudiantesPorAulaYUnidad();
                }
            });
    }
}
