import { ElementRef, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
    MatAutocompleteModule,
    MatAutocompleteSelectedEvent,
} from '@angular/material/autocomplete';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSelectModule } from '@angular/material/select';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { SharedModule } from 'app/shared/shared.module';
import { ClassroomsService } from './classrooms.service';
import { FuseConfirmationService } from '@fuse/services/confirmation';
import { PersonaService } from './persona.service';
import { StudentService } from './student.service';
import { FilterTablePipe } from './filter-table.pipe';
import { forkJoin } from 'rxjs';
import { EditarEstudianteComponent } from './editar-estudiante/editar-estudiante.component';
import { RegistrarEstudiantesComponent } from './registrar-estudiantes/registrar-estudiantes.component';
import { AsignarEstudiantesComponent } from './asignar-estudiantes/asignar-estudiantes.component';
import { Component, ViewChild, EventEmitter, Output } from '@angular/core';

@Component({
    selector: 'app-classrooms',
    templateUrl: 'classrooms.component.html',
    standalone: true,
    imports: [
        SharedModule,
        MatInputModule,
        MatFormFieldModule,
        MatSelectModule,
        MatTableModule,
        MatCheckboxModule,
        MatButtonModule,
        MatButtonToggleModule,
        MatAutocompleteModule,
        MatChipsModule,
        MatIconModule,
        FilterTablePipe,
        EditarEstudianteComponent,
        RegistrarEstudiantesComponent,
        AsignarEstudiantesComponent,
    ],
})
export class ClassroomsComponent implements OnInit {
    dataSource = new MatTableDataSource();
    unidades: any = [];
    displayedColumns: string[] = [
        'numero',
        'NombreCompleto',
        'EstadoEstudiante',
    ];
    selectedStudents = [];
    searchedStudents = [];
    classroomStudents = [];
    classrooms = [];
    selectedDegree: number = null;
    selectedSection: number = null;
    selectedUnity: number = null;
    searchText: string = '';
    filteredStudents: any[] = [];
    formRegistrarEstudiante = new FormGroup({
        nombres: new FormControl('', [Validators.required]),
        apellidoPaterno: new FormControl('', [Validators.required]),
        apellidoMaterno: new FormControl('', [Validators.required]),
        dni: new FormControl('', [
            Validators.required,
            Validators.pattern('^[0-9]{8}$'),
        ]),
    });

    items: string = 'list';
    estadoEvalucionPsicologicaAula: string = 'N';

    @ViewChild('autocompleteInput')
    autocompleteInput: ElementRef<HTMLInputElement>;
    inputValue: string = '';

    estado: boolean = false;

    // Constructor
    constructor(
        private classroomsService: ClassroomsService,
        private confirmationService: FuseConfirmationService,
        private personaService: PersonaService,
        private studentService: StudentService,
        private snack: MatSnackBar
    ) { }

    // Métodos de ciclo de vida
    ngOnInit() {
        this.obtenerListadeUnidades();
        this.obtenerListadeAulas();
        this.applyFilter('');
    }

    // MétodosngOnInit() {

    get filteredSearchedStudents() {
        return this.searchedStudents.filter(
            (e) => !this.selectedStudentsId.includes(e.id)
        );
    }

    get selectedStudentsId() {
        return this.selectedStudents.map((e) => e.id);
    }

    get academicDegrees() {
        return this.unidades;
    }

    get distinctClassrooms() {
        const classrooms = this.classrooms.map((e) => e.grado);
        return [...new Set(classrooms)];
    }

    get sections() {
        return this.classrooms.filter((e) => e.grado == this.selectedDegree);
    }
    asignarEstudiante() {
        const observables = this.selectedStudents.map((e) =>
            this.classroomsService.asignarEstudianteAula(
                e.id,
                this.selectedSection,
                this.selectedUnity
            )
        );

        forkJoin(observables).subscribe((responses) => {
            if (responses.every((e) => e.succeeded)) {
                this.selectedStudents = [];
                this.searchedStudents = [];
                this.obtnerEstudiantesPorAulaYUnidad();
                this.items = 'list';
            }
        });
    }

    obtnerEstudiantesPorAulaYUnidad() {
        this.classroomStudents = [];
        this.classroomsService
            .getStudentsByAulaYUnidad(this.selectedUnity, this.selectedSection)
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
                    this.selectedUnity,
                    this.selectedSection,
                    opcion,
                    ''
                )
                .subscribe(
                    () => {
                        this.obtnerEstudiantesPorAulaYUnidad();
                    },
                    (error) => {
                        console.log(error);
                    }
                );
        }
    }

    createStudent() {
        const dialogRef = this.confirmationService.open({
            title: '¿Deseas Crear Estudiante?',
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
                this.items = 'register';
            }
        });
    }

    remove(index: number): void {
        this.selectedStudents.splice(index, 1);
    }

    selected(event: MatAutocompleteSelectedEvent): void {
        if (event.option.value) {
            this.selectedStudents.push(event.option.value);
            this.autocompleteInput.nativeElement.value = '';
            this.inputValue = '';
        } else {
            this.createStudent();
        }
    }

    displayFn(person: any): string {
        return person?.nombre ?? '';
    }

    private obtenerListadeUnidades(): void {
        this.classroomsService.getUnidadesAll().subscribe({
            next: (response) => {
                if (response.succeeded) {
                    this.unidades = response.data;
                }
            },
            error: (err) => {
                console.log(err);
            },
        });
    }

    private obtenerListadeAulas(): void {
        this.classroomsService.getAulas().subscribe({
            next: (response) => {
                if (response.succeeded) {
                    this.classrooms = response.data;
                }
            },
            error: (err) => {
                console.log(err);
            },
        });
    }

    get selectedPrincipalData() {
        return this.selectedSection && this.selectedUnity;
    }

    registerStudent() {
        if (!this.selectedPrincipalData) {
            return this.snack.open(
                'Seleccione una unidad y aula',
                'Cerrar',
                {}
            );
        }
        this.studentService
            .create({
                ...this.formRegistrarEstudiante.value,
                unidadId: this.selectedUnity,
                aulaId: this.selectedSection,
            })
            .subscribe((response) => {
                if (response.succeeded) {
                    this.formRegistrarEstudiante.reset();
                    this.items = 'list';
                    this.obtnerEstudiantesPorAulaYUnidad();
                }
            });
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
                    console.log(tipoMap[tipo]);
                    this.classroomsService
                        .crearNUnidadOAnio(tipoMap[tipo], '')
                        .subscribe(
                            (response) => {
                                console.log(
                                    'Respuesta del servidor: ',
                                    response
                                );
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

    onInputChange(val: any) {
        const query = val?.nombre?.trim() ?? val?.trim();
        if (query) {
            this.personaService
                .getStudentsByQuery(
                    query,
                    this.selectedSection,
                    this.selectedUnity
                )
                .subscribe(({ data }) => {
                    this.searchedStudents = data;
                });
        } else {
            this.searchedStudents = [];
        }
    }

    applyFilter(searchText: string) {
        if (!searchText) {
            this.filteredStudents = this.classroomStudents;
        } else {
            this.filteredStudents = this.classroomStudents.filter((student) =>
                student.nombreCompleto
                    .normalize('NFD')
                    .replace(/[\u0300-\u036f]/g, '')
                    .toLowerCase()
                    .includes(
                        searchText
                            .normalize('NFD')
                            .replace(/[\u0300-\u036f]/g, '')
                            .toLowerCase()
                    )
            );
        }
    }

    deleteStudent(id: number) {
        this.classroomsService
            .eliminarEstudianteAula(
                id,
                this.selectedSection,
                this.selectedUnity
            )
            .subscribe((res) => {
                if (res.succeeded) {
                    this.filteredStudents = [];
                    this.obtnerEstudiantesPorAulaYUnidad();
                }
            });
    }



    showDNI: boolean = true;
    showOptions: boolean = true;
    onResize(event) {
        this.showDNI = event.target.innerWidth > 768;
        this.showOptions = event.target.innerWidth > 640;
    }


    modificarAula: boolean = false;
    updateEstadoModificarAula() {
        this.selectedUnity;
        let estado = this.unidades.find(x => x.id == this.selectedUnity).estado;
        this.modificarAula = estado;
    }


    // #region Editar Estudiante
    @ViewChild(EditarEstudianteComponent) editarEstudianteComponent: EditarEstudianteComponent;

    @Output() editarEstudianteSuccessful = new EventEmitter<FormGroup>();

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

    editarEstudianteComponente(student: any) {
        if (!student) return;

        this.formEditarEstudiante.patchValue({
            id: student.estudianteId,
            nombres: student.nombres,
            apellidoPaterno: student.apellidoPaterno,
            apellidoMaterno: student.apellidoMaterno,
            dni: student.dni,
        });
        this.items = 'edit';
    }

    onEditarEstudianteSuccessful(event: boolean) {
        if (event == true)
            this.obtnerEstudiantesPorAulaYUnidad();
        this.items = 'list';
    }
    // #endregion


    hol() {
    let hola;
        hola = hola + 1;
        hola = hola + 1;
        hola = hola + 1;
        hola = hola + 1;
        hola = hola + 1;
        hola = hola + 1;
        hola = hola + 1;
        console.log(hola);
        hola = hola + 1;
        console.log(hola);
    }
    
}
