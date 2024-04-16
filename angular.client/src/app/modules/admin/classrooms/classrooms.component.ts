import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { SharedModule } from 'app/shared/shared.module';
import {
    MatAutocompleteModule,
    MatAutocompleteSelectedEvent,
} from '@angular/material/autocomplete';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { ClassroomsService } from './classrooms.service';
import { FuseConfirmationService } from '@fuse/services/confirmation';
import { PersonaService } from './persona.service';
import { StudentService } from './student.service';
import { MatSnackBar } from '@angular/material/snack-bar';

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
    unities: any = [];
    isTestEnabled?: boolean = null;
    displayedColumns: string[] = [
        'numero',
        'NombreCompleto',
        'EstadoEstudiante',
    ];
    items: string = 'list';

    selectedStudents = [];
    searchedStudents = [];
    classroomStudents = [];
    classrooms = [];
    selectedDegree: number = null;
    selectedSection: number = null;
    selectedUnity: number = null;

    formRegistrarEstudiante = new FormGroup({
        nombres: new FormControl('', [Validators.required]),
        apellidoPaterno: new FormControl('', [Validators.required]),
        apellidoMaterno: new FormControl('', [Validators.required]),
        dni: new FormControl('', [
            Validators.required,
            Validators.pattern('^[0-9]{8}$'),
        ]),
    });
    @ViewChild('autocompleteInput')
    autocompleteInput: ElementRef<HTMLInputElement>;
    inputValue: string = '';

    constructor(
        private classroomsService: ClassroomsService,
        private confirmationService: FuseConfirmationService,
        private personaService: PersonaService,
        private studentService: StudentService,
        private snack: MatSnackBar
    ) { }
    searchText: string = ''; // Aquí se almacena el texto de búsqueda
    filteredStudents: any[] = []; // Aquí se almacenan los estudiantes filtrados

    ngOnInit() {
        this.obtenerListadeUnidades();
        this.obtenerListadeAulas();
        this.applyFilter('');
    }

    get filteredSearchedStudents() {
        return this.searchedStudents.filter(
            (e) => !this.selectedStudentsId.includes(e.id)
        );
    }
    get selectedStudentsId() {
        return this.selectedStudents.map((e) => e.id);
    }

    get academicDegrees() {
        return this.unities;
    }

    get distinctClassrooms() {
        const classrooms = this.classrooms.map((e) => e.Grado);
        return [...new Set(classrooms)];
    }
    get sections() {
        return this.classrooms.filter((e) => e.Grado == this.selectedDegree);
    }

    obtnerEstudiantesPorAulaYUnidad() {
        this.classroomsService
            .getStudentsByAulaYUnidad(this.selectedUnity, this.selectedSection)
            .subscribe((response) => {
                if (response.succeeded) {
                    this.classroomStudents = response.data;
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
                    this.unities = response.data;
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
    // Fin Codigo Jhonatan

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
    //Fin codigo de elias

    applyFilter(searchText: string) {
        // Si el texto de búsqueda está vacío, mostramos todos los estudiantes
        if (!searchText) {
            this.filteredStudents = this.classroomStudents;
        } else {
            // Convertimos el texto de búsqueda a minúsculas para hacer la búsqueda insensible a mayúsculas y minúsculas
            let searchTextLower = searchText.toLowerCase();

            // Filtramos los estudiantes cuyo nombre contenga el texto de búsqueda
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
}
