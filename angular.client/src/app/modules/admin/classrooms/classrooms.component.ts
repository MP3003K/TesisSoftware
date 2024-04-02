



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
import { BehaviorSubject, Observable, map, startWith } from "rxjs";
import { LiveAnnouncer } from "@angular/cdk/a11y";
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatChipInputEvent, MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from "@angular/material/icon";
import { ClassroomUnit, ClassroomsService, Grado, Seccion } from "./classrooms.service";
import { timeout } from 'rxjs/operators';
import { Unidad, Aula } from "app/shared/interfaces";
import { distinctUntilChanged } from 'rxjs/operators';
import { FuseConfirmationService } from "@fuse/services/confirmation";

@Component({
    selector: "app-classrooms",
    templateUrl: "classrooms.component.html",
    standalone: true,
    imports: [SharedModule, MatInputModule, MatFormFieldModule, MatSelectModule, MatTableModule, MatCheckboxModule, MatButtonModule, MatButtonToggleModule, MatAutocompleteModule, MatChipsModule, MatIconModule]
})
export class ClassroomsComponent implements OnInit {
    classrooms: ClassroomUnit[] = [];
    isTestEnabled?: boolean = null
    displayedColumns: string[] = [
        'numero',
        'NombreCompleto',
        'EstadoEstudiante'
    ];
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

    estudiantes: any[] = [];

    seccionSeleccionada: string;
    aulasFiltradas: Aula[] = [];
    gradosUnicos: number[] = [];
    unidadIdSeleccionada: number;


    gradoSeleccionado: number;
    aulaIdSeleccionado: number;
    seccionesSeleccionadas: string[] = [];
    gradoSeleccionado$ = new BehaviorSubject<number>(null);
    seccionSeleccionada$ = new BehaviorSubject<string>(null);

    constructor(private fb: FormBuilder, private classroomsService: ClassroomsService, private confirmationService: FuseConfirmationService) {
        this.filteredFruits = this.fruitCtrl.valueChanges.pipe(
            startWith(null),
            map((fruit: string | null) => (fruit ? this._filter(fruit) : this.allFruits.slice())),
        );

    }


    ngOnInit() {
        this.obtenerListadeUnidades();
        this.obtenerListadeAulas();
        this.gradoSeleccionado$.pipe(
            distinctUntilChanged()
        ).subscribe(grado => {
            this.aulasFiltradas = this.aulas.filter(aula => aula.Grado === grado);
            this.seccionesSeleccionadas = [...new Set(this.aulasFiltradas.map(aula => aula.Seccion))];
        });
        this.seccionSeleccionada$.pipe(
            distinctUntilChanged()

        ).subscribe(seccion => {
            this.aulasFiltradas = this.aulasFiltradas.filter(aula => aula.Seccion === seccion);
        });
        this.crearFormularioRegistrarEstudiante();
    }

    cambiarGrado(nuevoGrado: number) {
        this.gradoSeleccionado = nuevoGrado;
        this.gradoSeleccionado$.next(nuevoGrado);
    }

    cambiarSeccion(nuevaSeccion: string) {
        this.seccionSeleccionada = nuevaSeccion;
        this.seccionSeleccionada$.next(nuevaSeccion);
        const aula = this.aulasFiltradas.find(aula => aula.Seccion === nuevaSeccion);
        if (aula) {
            this.aulaIdSeleccionado = aula.AulaId;
        }
    }

    obtnerEstudiantesPorAulaYUnidad() {
        if (this.unidadIdSeleccionada && this.aulaIdSeleccionado) {
            this.classroomsService.getStudentsByAulaYUnidad(this.unidadIdSeleccionada, this.aulaIdSeleccionado).subscribe(response => {
                if (response.succeeded) {
                    this.estudiantes = response.data;
                    console.log(this.estudiantes);
                }
            });
        }
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


    // Inicio Codigo Jhonatan
    private crearFormularioRegistrarEstudiante(): void {
        this.formRegistrarEstudiante = this.fb.group({
            nombre: ['', Validators.required],
            apellidoPaterno: ['', Validators.required],
            apellidoMaterno: ['', Validators.required],
            dni: ['', [Validators.required, Validators.pattern('^[0-9]{8}$')]]
        });
    }
    public unidades: Unidad[] = [];
    public aulas: Aula[] = []

    private obtenerListadeUnidades(): void {
        this.classroomsService.getUnidadesAll().pipe(
            timeout(5000) // Tiempo de espera en milisegundos
        ).subscribe((response) => {
            if (response.succeeded) {
                this.unidades = response.data;
            }
        }, error => {
        })

    }
    private obtenerListadeAulas(): void {
        this.classroomsService.getAulas().pipe(
            timeout(5000) // Tiempo de espera en milisegundos
        ).subscribe((response) => {
            if (response.succeeded) {
                this.aulas = response.data;
                this.gradosUnicos = [...new Set(this.aulas.map(aula => aula.Grado))];
                console.log(this.aulas);
            }
        }, error => {
        });
    }



    // Fin Codigo Jhonatan

    openCreateUnitModal(): void {
        const dialogRef = this.confirmationService.open({
            title: '¿Deseas Crear Unidad?',
            message: null, dismissible: true,
            actions: {
                cancel: {
                    label: 'Cancelar'
                },
                confirm: {
                    label: 'Crear'
                }
            },
            icon: { color: 'info', name: 'info', show: true }
        })
        dialogRef.afterClosed().subscribe(res => {
            if(res==='confirmed'){
                console.log('TODO: Crear Unidad')
            }
        })
    }


    //Fin codigo de elias

}