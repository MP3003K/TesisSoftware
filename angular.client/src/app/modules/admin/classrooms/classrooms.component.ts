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
import { MatDialog } from '@angular/material/dialog';
import { ClassroomUnit, ClassroomsService, Grado, Seccion } from "./classrooms.service";

export interface PeriodicElement {
    name: string;
    position: number;
    weight: number;
    symbol: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
    { position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H' },
    { position: 2, name: 'Helium', weight: 4.0026, symbol: 'He' },
    { position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li' },
    { position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be' },
    { position: 5, name: 'Boron', weight: 10.811, symbol: 'B' },
    { position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C' },
    { position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N' },
    { position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O' },
    { position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F' },
    { position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne' },
];

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
    displayedColumns: string[] = ['select', 'position', 'name', 'weight', 'symbol'];
    dataSource = new MatTableDataSource<PeriodicElement>(ELEMENT_DATA);
    selection = new SelectionModel<PeriodicElement>(true, []);
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

    checkboxLabel(row?: PeriodicElement): string {
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
          next: (data) => {
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
            if (!uniqueGrados.has(aula.grado.id)) {
                uniqueGrados.set(aula.grado.id, aula.grado);
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
