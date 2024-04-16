import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatAutocompleteModule, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { SharedModule } from 'app/shared/shared.module';
import { ClassroomsService } from './classrooms.service';
import { FuseConfirmationService } from '@fuse/services/confirmation';
import { PersonaService } from './persona.service';
import { StudentService } from './student.service';

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
    ],
})
export class ClassroomsComponent implements OnInit {
    // Propiedades
    unidades: any = [];
    displayedColumns: string[] = ['numero', 'NombreCompleto', 'EstadoEstudiante'];
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
        dni: new FormControl('', [Validators.required, Validators.pattern('^[0-9]{8}$')]),
    });

    items: string = 'list';
    estadoEvalucionPsicologicaAula: string = 'N';

    @ViewChild('autocompleteInput') autocompleteInput: ElementRef<HTMLInputElement>;
    inputValue: string = '';

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
    obtnerEstudiantesPorAulaYUnidad() {
        this.classroomsService
            .getStudentsByAulaYUnidad(this.selectedUnity, this.selectedSection)
            .subscribe((response) => {
                if (response.succeeded) {
                    this.classroomStudents = response.data;
                    this.estadoEvalucionPsicologicaAula = this.classroomStudents[1].EstadoAula ?? 'N';
                    console.log(this.estadoEvalucionPsicologicaAula);
                }
            });
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
    getEstadoPruebaPsicologicaAula(status: string): { text: string, class: string } {
        let classStyle = 'text-white w-auto';
        switch (status) {
            case 'F':
                return { text: 'Evaluación finalizada', class: `${classStyle} bg-red-500` };
            case 'N':
                return { text: 'Evaluación pendiente', class: `${classStyle} bg-gray-500` };
            case 'P':
                return { text: 'Evaluación activa', class: `${classStyle} bg-green-500` };
            default:
                return { text: '', class: '' };
        }
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
                    console.log(response.data);
                    this.items = 'list';
                    this.obtnerEstudiantesPorAulaYUnidad();
                }
            });
    }

    openCreateUnitModal(): void {
        const dialogRef = this.confirmationService.open({
            title: '¿Deseas Crear Unidad?',
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
                console.log('TODO: Crear Unidad');
            }
        });
    }

    onInputChange(val: any) {
        const query = val?.nombre?.trim() ?? val?.trim();
        if (query) {
            this.personaService
                .getStudentsByQuery(query)
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
            let searchTextLower = searchText.toLowerCase();
            this.filteredStudents = this.classroomStudents.filter(student =>
                student.NombreCompleto.toLowerCase().includes(searchTextLower)
            );
        }
    }

    deleteStudent() {

    }

    formatName(name: string): string {
        return name.toLowerCase().split(' ').map(word => word.charAt(0).toUpperCase() + word.substring(1)).join(' ');
    }

    showDNI: boolean = true;
    showOptions: boolean = true;
    onResize(event) {
        this.showDNI = event.target.innerWidth > 768;
        this.showOptions = event.target.innerWidth > 640;
    }
    // #region Hola
}
