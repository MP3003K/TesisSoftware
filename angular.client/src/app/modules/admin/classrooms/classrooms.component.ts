import { Component, ElementRef, OnInit, ViewChild, inject } from "@angular/core";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatSelectModule } from "@angular/material/select";
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from "@angular/material/button";
import { MatButtonToggleModule } from "@angular/material/button-toggle";
import { SharedModule } from "app/shared/shared.module";
import { MatAutocompleteModule, MatAutocompleteSelectedEvent } from "@angular/material/autocomplete";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { Observable, map, startWith } from "rxjs";
import { LiveAnnouncer } from "@angular/cdk/a11y";
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatChipInputEvent, MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from "@angular/material/icon";
import { ClassroomUnit, ClassroomsService, Grado, Seccion } from "./classrooms.service";

@Component({
    selector: "app-classrooms",
    templateUrl: "classrooms.component.html",
    standalone: true,
    imports: [SharedModule, MatInputModule, MatFormFieldModule, MatSelectModule, MatTableModule, MatCheckboxModule, MatButtonModule, MatButtonToggleModule, MatAutocompleteModule, MatChipsModule, MatIconModule]
})
export class ClassroomsComponent implements OnInit {
    //empieza elias
    showCreateUnitModal: boolean = false;
    selectedUnit: number;
    classrooms: ClassroomUnit[] = [];
    grados: Grado[] = [];
    secciones: Seccion[] = [];
    //selectedGrado: Grado | null = null;
    //selectedSeccion: Seccion | null = null;
    //selectedGradoId: number | null = null;
    aulasCompletas: Seccion[] = [];
    selectedSeccionId: number | null = null;
    selectedGradoId: number | null = null;

    //termina elias

    isTestEnabled?: boolean = null
    displayedColumns: string[] = ['select', 'Nombres', 'ApellidoPaterno', 'ApellidoMaterno', 'DNI'];
    dataSource = new MatTableDataSource([]);
    selection = new SelectionModel(true, []);
    items: string = "list"

    separatorKeysCodes: number[] = [ENTER, COMMA];
    fruitCtrl = new FormControl('');
    filteredFruits: Observable<string[]>;
    fruits: string[] = [];
    allFruits: string[] = ['Apple', 'Lemon', 'Lime', 'Orange', 'Strawberry'];
    @ViewChild('fruitInput') fruitInput: ElementRef<HTMLInputElement>;

    announcer = inject(LiveAnnouncer);

    formRegistrarEstudiante: FormGroup;

    constructor(private fb: FormBuilder, private classroomsService: ClassroomsService) {
        this.filteredFruits = this.fruitCtrl.valueChanges.pipe(
            startWith(null),
            map((fruit: string | null) => (fruit ? this._filter(fruit) : this.allFruits.slice())),
        );

    }
    ngOnInit(): void {
        this.isTestEnabled = true
        this.crearFormularioRegistrarEstudiante();
        this.loadClassrooms();
        this.loadGradosYSecciones();

    }

    filterStudentsByClassroom() {
        console.log(this.selectedSeccionId)
        this.classroomsService.getStudentsByClassroom(this.selectedSeccionId).subscribe(res => {
            this.dataSource.data = res
        })
    }
    isAllSelected() {
        const numSelected = this.selection.selected.length;
        const numRows = this.dataSource.data.length;
        return numSelected === numRows;
    }

    toggleAllRows() {
        if (this.isAllSelected()) {
            this.selection.clear();
            return;
        }

        this.selection.select(...this.dataSource.data);
    }

    checkboxLabel(row?: any): string {
        if (!row) {
            return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
        }
        return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
    }






    add(event: MatChipInputEvent): void {
        const value = (event.value || '').trim();

        // Add our fruit
        if (value) {
            this.fruits.push(value);
        }

        // Clear the input value
        event.chipInput!.clear();

        this.fruitCtrl.setValue(null);
    }

    remove(fruit: string): void {
        const index = this.fruits.indexOf(fruit);

        if (index >= 0) {
            this.fruits.splice(index, 1);

            this.announcer.announce(`Removed ${fruit}`);
        }
    }

    selected(event: MatAutocompleteSelectedEvent): void {
        this.fruits.push(event.option.viewValue);
        this.fruitInput.nativeElement.value = '';
        this.fruitCtrl.setValue(null);
    }

    private _filter(value: string): string[] {
        const filterValue = value.toLowerCase();

        return this.allFruits.filter(fruit => fruit.toLowerCase().includes(filterValue));
    }



    private crearFormularioRegistrarEstudiante(): void {
        this.formRegistrarEstudiante = this.fb.group({
            nombre: ['', Validators.required],
            apellidoPaterno: ['', Validators.required],
            apellidoMaterno: ['', Validators.required],
            dni: ['', [Validators.required, Validators.pattern('^[0-9]{8}$')]]
        });
    }

    //Inicio codigo de elias

    loadClassrooms() {
        this.classroomsService.getClassrooms().subscribe({
            next: ({data}) => {
                console.log(this.classrooms)
                this.classrooms = data;
            },
            error: (err) => {
                console.error(err);
            }
        });
    }

    loadGradosYSecciones(): void {
        this.classroomsService.getAulas().subscribe({
            next: (response) => {
                if (response.succeeded) {
                    console.log('Aulas cargadas:', response.data);
                    this.aulasCompletas = response.data;
                    this.extractGradosAndSecciones(response.data);
                } else {
                    console.error('Failed to load grados y secciones:', response.message);
                }
            },
            error: (err) => {
                console.error('Error loading grados y secciones:', err);
            }
        });
    }

    private extractGradosAndSecciones(aulas: Seccion[]): void {
        // Extract unique grados
        const uniqueGrados = new Map<number, Grado>();
        aulas.forEach(aula => {
            if (!uniqueGrados.has(aula?.grado?.id)) {
                uniqueGrados.set(aula?.grado?.id, aula.grado);
            }
        });
        this.grados = Array.from(uniqueGrados.values());
        // Init secciones to empty as they will be loaded based on selected grado
        this.secciones = [];
    }

    onGradoSelected(gradoId: number): void {
        this.selectedGradoId = gradoId;
        this.selectedSeccionId = null; // Resetea la sección seleccionada

        // Filtra las secciones basándose en el grado seleccionado y actualiza la propiedad 'secciones'
        this.secciones = this.aulasCompletas
            .filter(aula => aula.gradoId === gradoId);
        console.log(this.secciones)
    }
    openCreateUnitModal(): void {
        this.showCreateUnitModal = true;
    }

    onCreateUnit(): void {
        this.showCreateUnitModal = false;
    }

    onCancelCreateUnit(): void {
        this.showCreateUnitModal = false;
    }
    //Fin codigo de elias

}
