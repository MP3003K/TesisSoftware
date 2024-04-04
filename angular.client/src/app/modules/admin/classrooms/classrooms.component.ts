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
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { ClassroomUnit, ClassroomsService } from './classrooms.service';
import { Aula } from 'app/shared/interfaces';
import { FuseConfirmationService } from '@fuse/services/confirmation';
import { PersonaService } from './persona.service';

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
    unidades: any = [];
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
        nombre: new FormControl('', [Validators.required]),
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
        private personaService: PersonaService
    ) {}

    ngOnInit() {
        this.obtenerListadeUnidades();
        this.obtenerListadeAulas();
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
        return this.unidades;
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
                    this.unidades = response.data;
                    console.log(this.unidades);
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
                    console.log(this.classrooms);
                }
            },
            error: (err) => {
                console.log(err);
            },
        });
    }

    registerStudent() {
        console.log(this.formRegistrarEstudiante.value);
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
}
