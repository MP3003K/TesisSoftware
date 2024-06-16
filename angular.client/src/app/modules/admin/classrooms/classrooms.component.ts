import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { MatTableDataSource, MatTableModule } from "@angular/material/table";
import { FuseConfirmationService } from "@fuse/services/confirmation";
import { ItemOptions } from "./enums";
import { FiltrosSeleccionados } from "./models/filtros-seleccionados.model";
import { ClassroomsService } from "./services";
import { CommonModule } from "@angular/common";
import { SharedModule } from "app/shared/shared.module";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { MatButtonToggleModule } from "@angular/material/button-toggle";
import { AsignarEstudianteComponent, EditarEstudianteComponent, ListarEstudiantesComponent, RegistrarEstudiantesComponent, ReporteSalonComponent } from "./components";
import { MatSelectModule } from "@angular/material/select";
import { MatSortModule } from "@angular/material/sort";
import { Estudiante } from "./models/estudiante.model";
import { first } from "lodash";
import { firstValueFrom } from "rxjs";




@Component({
    selector: 'app-classrooms',
    templateUrl: 'classrooms.component.html',
    standalone: true,
    imports: [
        CommonModule,
        SharedModule,
        MatIconModule,
        MatSelectModule,
        MatButtonToggleModule,
        MatButtonModule,
        MatTableModule,
        MatSortModule,
        RegistrarEstudiantesComponent,
        EditarEstudianteComponent,
        ListarEstudiantesComponent,
        AsignarEstudianteComponent,
        ReporteSalonComponent,
    ],
})

export class ClassroomsComponent implements OnInit {

    itemOption: ItemOptions;
    filtrosSeleccionados = new FiltrosSeleccionados();

    unidades: any = [];
    classrooms = [];
    unidadSeleccionada: any;
    cargarValoresInicialesHijo: boolean = false;

    modificarAula: boolean = false;
    datosEstudianteParaEditar: Estudiante;

    constructor(
        private classroomsService: ClassroomsService,
        private confirmationService: FuseConfirmationService,
    ) { }

    async ngOnInit() {
        await this.obtenerListadeUnidades();
        await this.obtenerListadeAulas();
        this.cargarValoresIniciales();
    }

    cargarValoresIniciales() {
        this.filtrosSeleccionados.unidad = 1003;
        this.filtrosSeleccionados.grado = 1;
        this.filtrosSeleccionados.seccion = 1;
        this.changeItemOption(ItemOptions.reportClassroom);
    }

    private async obtenerListadeUnidades(): Promise<any> {
        this.classroomsService.getUnidadesAll().subscribe({
            next: (response) => {
                if (response.succeeded) {
                    this.unidades = response.data;
                }
            },
            error: (err) => {
                console.error(err);
            },
        });
    }

    private async obtenerListadeAulas(): Promise<any> {
        this.classroomsService.getAulas().subscribe({
            next: (response) => {
                if (response.succeeded) {
                    this.classrooms = response.data;
                }
            },
            error: (err) => {
                console.error(err);
            },
        });
    }


    openCreateUnitModal(tipo: string): void {
        const dialogRef = this.confirmationService.open({
            title: '¿Deseas Crear ' + tipo + '?',
            message: null,
            dismissible: true,
            actions: {
                cancel: {
                    label: 'Cancelar',
                },
                confirm: {
                    label: 'Crear',
                },
            },
            icon: { color: 'info', name: 'info', show: true },
        });
        dialogRef.afterClosed().subscribe((res) => {
            if (res === 'confirmed') {
                const tipoMap = {
                    Unidad: 0,
                    Año: 1,
                };

                if (tipo in tipoMap) {
                    this.classroomsService
                        .crearNUnidadOAnio(tipoMap[tipo], '')
                        .subscribe(
                            (response) => {
                                this.obtenerListadeUnidades();
                            },
                            (error) =>
                                console.error(
                                    'Error al realizar la solicitud: ',
                                    error
                                )
                        );
                }
            }
        });
    }

    get distinctClassrooms() {
        const classrooms = this.classrooms.map((e) => e.grado);
        return [...new Set(classrooms)];
    }
    get sections() {
        return this.classrooms.filter((e) => e.grado == this.filtrosSeleccionados.grado);
    }

    seleccionarEstadoUnidad(unidad: number) {
        console.log(unidad, 'unidad')
        if (unidad) {
            let estado: boolean = this.unidades.find(x => x.id == unidad)?.estado;
            console.log(estado, 'estado')
            this.filtrosSeleccionados.estadoUnidad = estado;
        }

    }


    editarEstudiante(estudiante: Estudiante) {
        this.datosEstudianteParaEditar = estudiante;
        this.changeItemOption(ItemOptions.EditarEstudiante);
    }

    changeItemOption(option?: ItemOptions) {
        console.log('option', this.filtrosSeleccionados)
        if (!option) this.itemOption = ItemOptions.ListarEstudiantes;
        else this.itemOption = option;

        this.seleccionarEstadoUnidad(this.filtrosSeleccionados.unidad);
        this.modificarAula = this.filtrosSeleccionados.estadoUnidad;

        this.cargarValoresInicialesHijo = !this.cargarValoresInicialesHijo;
    }
}
