import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
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
import { Subscription, interval } from 'rxjs';


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
        this.classroomsService
            .getStudentsByAulaYUnidad(this.filtrosSeleccionados.Unidad, this.filtrosSeleccionados.Seccion)
            .subscribe((response) => {
                if (response.succeeded) {
                    this.classroomStudents = response.data;
                    this.classroomStudents.sort((a, b) => {
                        if (a.nombreCompleto < b.nombreCompleto) {
                            return -1;
                        }
                        if (a.nombreCompleto > b.nombreCompleto) {
                            return 1;
                        }
                        return 0;
                    });
                    this.estadoEvalucionPsicologicaAula = this.classroomStudents[0]?.estadoAula ?? 'N';
                    this.updateEstadoModificarAula();
                }
                else {
                    this.classroomStudents = [];
                }
            });
    }

    private seguimientoSubscripcion: Subscription;
    seguimientoActivo: boolean = false;

    iniciarSeguimientoConsulta() {
        if (this.seguimientoSubscripcion) {
            this.seguimientoSubscripcion.unsubscribe(); // Detener seguimiento anterior si existe
            this.seguimientoActivo = false; // Actualiza el estado a inactivo solo si se detiene el seguimiento
            this.seguimientoSubscripcion = null; // Asegúrate de limpiar la suscripción
        } else {
            this.seguimientoActivo = true; // Activa el seguimiento solo si se va a iniciar
            this.seguimientoSubscripcion = interval(3000) // Asegúrate de que esto es un Observable
                .subscribe(() => {
                    this.obtnerEstudiantesPorAulaYUnidad(); // Llama a tu función directamente aquí
                });
        }
    }




    cambiarEstadoEvaluacionPsicologicaAula() {
        let estado = this.estadoEvalucionPsicologicaAula;

        if (['N', 'P', 'F'].includes(estado)) {
            //0: Finalizar,  1: Iniciar,
            let opcion;
            if (estado == 'F' || estado == 'N') {
                opcion = 1;
            } else {
                opcion = 0;
            }
            this.classroomsService
                .updateEstadoEvaPsiAula(
                    this.filtrosSeleccionados.Unidad,
                    this.filtrosSeleccionados.Seccion,
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
        this.filtrosSeleccionados.Unidad;
        this.modificarAula = this.filtrosSeleccionados.EstadoUnidad;
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
                this.filtrosSeleccionados.Seccion,
                this.filtrosSeleccionados.Unidad
            )
            .subscribe((res) => {
                if (res.succeeded) {
                    this.filteredStudents = [];
                    this.obtnerEstudiantesPorAulaYUnidad();
                }
            });
    }

    cantidadDeEstudiantesPorEstadoEvalucionPsicologico(estado: string): number {
        return this.classroomStudents.filter(estudiante => estudiante.estadoEstudiante === estado).length;
    }
}



