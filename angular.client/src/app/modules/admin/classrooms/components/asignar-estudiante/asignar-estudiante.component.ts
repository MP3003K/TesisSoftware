import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatAutocompleteModule, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from 'app/shared/shared.module';
import { forkJoin } from 'rxjs';
import { ClassroomsService, PersonaService } from '../../services'
import { ItemOptions } from '../../enums';
import { FiltrosSeleccionados } from '../../models/filtros-seleccionados.model';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

@Component({
    selector: 'app-asignar-estudiante',
    standalone: true,
    imports: [
        CommonModule,
        SharedModule,
        FormsModule,
        ReactiveFormsModule,
        MatChipsModule,
        MatIconModule,
        MatFormFieldModule,
        MatInputModule,
        MatAutocompleteModule,
        MatButtonModule
    ],
    templateUrl: './asignar-estudiante.component.html',
    styleUrl: './asignar-estudiante.component.scss'
})
export class AsignarEstudianteComponent {

    @Input() filtrosSeleccionados: FiltrosSeleccionados;

    @Output() itemOption = new EventEmitter<ItemOptions>();

    searchedStudents = [];
    selectedStudents = [];
    inputValue: string = '';
    constructor(
        private classroomsService: ClassroomsService,
        private personaService: PersonaService,

    ) { }
    ngInit() {
    }

    @ViewChild('autocompleteInput')
    autocompleteInput: ElementRef<HTMLInputElement>;

    get filteredSearchedStudents() {
        return this.searchedStudents.filter(
            (e) => !this.selectedStudentsId.includes(e.id)
        );
    }

    get selectedStudentsId() {
        return this.selectedStudents.map((e) => e.id);
    }

    asignarEstudiante() {
        const observables = this.selectedStudents.map((e) =>
            this.classroomsService.asignarEstudianteAula(
                e.id,
                this.filtrosSeleccionados.seccion, this.filtrosSeleccionados.unidad
            )
        );

        forkJoin(observables).subscribe((responses) => {
            if (responses.every((e) => e.succeeded)) {
                this.selectedStudents = [];
                this.searchedStudents = [];
                this.itemOption.emit(ItemOptions.ListarEstudiantes);
            }
        });
    }
    remove(index: number): void {
        this.selectedStudents.splice(index, 1);
    }
    selected(event: MatAutocompleteSelectedEvent): void {
        if (event?.option?.value) {
            this.selectedStudents.push(event.option.value);
            this.autocompleteInput.nativeElement.value = '';
            this.inputValue = '';
        }
    }
    displayFn(person: any): string {
        return person?.nombre ?? '';
    }

    onInputChange(val: any) {
        const query = val?.nombre?.trim() ?? val?.trim();
        if (query) {
            this.personaService
                .getStudentsByQuery(
                    query,
                    this.filtrosSeleccionados.seccion,
                    this.filtrosSeleccionados.unidad
                )
                .subscribe(({ data }) => {
                    this.searchedStudents = data;
                });
        } else {
            this.searchedStudents = [];
        }
    }

}
