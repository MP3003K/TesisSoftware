import { Component, OnInit } from "@angular/core";
import { FuseConfirmationService } from "@fuse/services/confirmation";
import { ItemOptions } from "./enums";
import { FiltrosSeleccionados } from "./models/filtros-seleccionados.model";
import { ClassroomsService, Seccion } from "./services";
import { CommonModule } from "@angular/common";
import { SharedModule } from "app/shared/shared.module";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { MatButtonToggleModule } from "@angular/material/button-toggle";
import { AsignarEstudianteComponent, EditarEstudianteComponent, ListarEstudiantesComponent, RegistrarEstudiantesComponent, ReporteSalonComponent } from "./components";
import { MatSelectModule } from "@angular/material/select";
import { Estudiante } from "./models/estudiante.model";
import { ButtonToogle } from "./interfaces/buttonToogle";


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
        RegistrarEstudiantesComponent,
        EditarEstudianteComponent,
        ListarEstudiantesComponent,
        AsignarEstudianteComponent,
        ReporteSalonComponent,
    ],
})

export class ClassroomsComponent implements OnInit {

    itemOption: ItemOptions;
    ItemOptions = ItemOptions;
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
        this.filtrosSeleccionados.Unidad = 1;
        this.filtrosSeleccionados.Grado = 2;
        this.filtrosSeleccionados.Seccion = 3;
        this.changeItemOption(ItemOptions.ListarEstudiantes);
    }

    // #region ButtonToogle
    claseActiva: string = 'bg-[#f9fafb] text-[rgb(79,70,229)] font-bold text-md';
    claseInactiva: string = 'bg-white text-black text-md';


    botonesItemOptions: ButtonToogle[] = [
        {
            id: ItemOptions.ListarEstudiantes,
            texto: 'Listar Estudiantes',
            accion: () => this.changeItemOption(ItemOptions.ListarEstudiantes),
            claseActiva: this.claseActiva,
            claseInactiva: this.claseInactiva,
            esVisible: () => true
        },
        {
            id: ItemOptions.reportClassroom,
            texto: 'Reporte de Salon',
            accion: () => this.changeItemOption(ItemOptions.reportClassroom),
            claseActiva: this.claseActiva,
            claseInactiva: this.claseInactiva,
            esVisible: () => this.filtrosSeleccionados.succeeded()
        },
        {
            id: ItemOptions.RegistrarEstudiantes,
            texto: 'Registrar Estudiantes',
            accion: () => this.changeItemOption(ItemOptions.RegistrarEstudiantes),
            claseActiva: this.claseActiva,
            claseInactiva: this.claseInactiva,
            esVisible: () => this.filtrosSeleccionados.succeeded() && this.modificarAula
        },
        {
            id: ItemOptions.AsignarEstudiantes,
            texto: 'Asignar Estudiante',
            accion: () => this.changeItemOption(ItemOptions.AsignarEstudiantes),
            claseActiva: this.claseActiva,
            claseInactiva: this.claseInactiva,
            esVisible: () => this.filtrosSeleccionados.succeeded() && this.modificarAula
        },
        {
            id: ItemOptions.EditarEstudiante,
            texto: 'Editar Estudiante',
            accion: () => this.changeItemOption(ItemOptions.EditarEstudiante),
            claseActiva: this.claseActiva,
            claseInactiva: this.claseInactiva,
            esVisible: () => this.filtrosSeleccionados.succeeded() && this.modificarAula && this.itemOption == ItemOptions.EditarEstudiante
        },
    ];
    get botonesItemOptionsVisibles() {
        return this.botonesItemOptions.filter(b => b.esVisible());
    }

    getButtonClass(boton: ButtonToogle): string {
        if (this.itemOption == boton.id) {
            return boton.claseActiva;
        }
        return boton.claseInactiva;
    }
    // #endregion

    private async obtenerListadeUnidades(): Promise<any> {
        this.classroomsService.getUnidadesAll().subscribe({
            next: (response) => {
                if (response.succeeded) {
                    this.unidades = response.data;
                    this.filtrosSeleccionados.Unidad = Math.max(...this.unidades.map((e) => e.id));
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
                    { { this.classrooms } }
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
                    color: 'primary',
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
        return this.classrooms.filter((e) => e.grado == this.filtrosSeleccionados.Grado);
    }

    seleccionarEstadoUnidadYUnidadN(unidadId: number) {
        if (unidadId) {
            const unidad = this.unidades.find(x => x.id === unidadId);
            if (unidad) {
                const { estado: unidadEstado, nUnidad: unidadNumero, nombre: unidadNombre }: { estado: boolean, nUnidad: number, nombre: string } = unidad;
                if (unidadEstado) this.filtrosSeleccionados.EstadoUnidad = unidadEstado;
                if (unidadNombre) this.filtrosSeleccionados.UnidadNombre = unidadNombre;
                if (unidadNumero) this.filtrosSeleccionados.UnidadNumero = unidadNumero;
            }
        }
    }

    seleccionarSeccionN(seccionId: number) {
        if (seccionId) {
            const grado = this.sections.find(x => x.id === seccionId);
            if (grado) {
                const { seccion: seccion }: { seccion: string } = grado;
                if (seccion) this.filtrosSeleccionados.SeccionN = seccion;
            }
        }
    }


    editarEstudiante(estudiante: Estudiante) {
        this.datosEstudianteParaEditar = estudiante;
        this.changeItemOption(ItemOptions.EditarEstudiante);
    }

    changeItemOption(option: ItemOptions) {
        this.itemOption = option;

        this.seleccionarEstadoUnidadYUnidadN(this.filtrosSeleccionados.Unidad);
        this.modificarAula = this.filtrosSeleccionados.EstadoUnidad;

        this.cargarValoresInicialesHijo = !this.cargarValoresInicialesHijo;
    }
}
