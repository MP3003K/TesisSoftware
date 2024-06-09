import { Component, ElementRef, EventEmitter, Injectable, Input, OnInit, Output, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { AbstractControl, ValidationErrors } from '@angular/forms';


import * as XLSX from 'xlsx';
import { ClassroomsService } from '../classrooms.service';


@Component({
    selector: 'app-registrar-estudiantes',
    standalone: true,
    templateUrl: './registrar-estudiantes.component.html',
    styleUrl: './registrar-estudiantes.component.scss',
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatIconModule,
    ]
})

export class RegistrarEstudiantesComponent implements OnInit {
    @Input() unidadIdSeleccionada: number = 0;
    @Input() aulaIdSeleccionada: number = 0;
    @Output() itemsChange = new EventEmitter<string>();

    formEstudiantes: FormGroup;

    constructor(
        private fb: FormBuilder,
        private classroomsService: ClassroomsService
    ) { }

    ngOnInit() {
        this.formEstudiantes = this.fb.group({
            estudiantes: this.fb.array([this.crearFormularioEstudiante()], [this.validarDniUnicoForm.bind(this)])
        });
    }



    // #region Formulario

    crearFormularioEstudiante(): FormGroup {
        return this.fb.group({
            nombres: ['', Validators.required],
            apellidoPaterno: ['', Validators.required],
            apellidoMaterno: ['', Validators.required],
            dni: ['', [Validators.required, Validators.pattern('^[0-9]{8}$')]],
        });
    }

    agregarEstudiante() {
        const estudiantes = this.formEstudiantes.get('estudiantes') as FormArray;
        estudiantes.push(this.crearFormularioEstudiante());
    }

    removerEstudiante(index: number) {
        const estudiantes = this.formEstudiantes.get('estudiantes') as FormArray;
        estudiantes.removeAt(index);
    }

    registrarEstudiantesApi() {
        const { unidadIdSeleccionada, aulaIdSeleccionada, formEstudiantes } = this;

        if (unidadIdSeleccionada === 0 || aulaIdSeleccionada === 0) {
            return console.error('Parámetros para registrar estudiantes no válidos: unidad o aula no seleccionada.');
        }

        const estudiantes = formEstudiantes?.value?.estudiantes;
        if (!Array.isArray(estudiantes) || estudiantes.length === 0) {
            return console.error('Parámetros para registrar estudiantes no válidos: lista de estudiantes vacía o inválida.');
        }

        const jsonEstudiantes = JSON.stringify(estudiantes.map(estudiante => ({
            ...estudiante,
            dni: estudiante.dni.toString()
        })));

        this.classroomsService.crearYAsignarEstudiantes({
            unidadId: unidadIdSeleccionada,
            aulaId: aulaIdSeleccionada,
            jsonEstudiantes
        }).subscribe({
            next: (response) => {
                if (response.succeeded) {
                    this.itemsChange.emit('list');
                }
            },
            error: (error) => {
                console.error(`Error al registrar estudiantes: ${error}`);
            }
        });
    }

    get estudiantes(): FormArray {
        return this.formEstudiantes.get('estudiantes') as FormArray;
    }

    limpiarRegistrosFormulario() {
        this.formEstudiantes.reset();
        (this.formEstudiantes.get('estudiantes') as FormArray).clear();
        this.agregarEstudiante();
    }
    // #endregion
    dnisRechazados: Set<number> = new Set();
    dnisDuplicados: Set<number> = new Set();

    validarDniUnico() {
        const dnis = this.estudiantes.value.map(estudiante => estudiante.dni);
        this.classroomsService.validarDniUnico({ jsonDnis: JSON.stringify(dnis) }).subscribe({
            next: (response) => {
                console.log(response);
                if (response.succeeded) {
                    this.dnisRechazados.clear();
                    return true;

                } else {
                    if (response?.Data?.length > 0) {
                        this.dnisRechazados = new Set(response.Data.map(dniObj => dniObj.dni));
                    }
                    return false;
                }
            },
            error: (error) => {
                console.error(`Error al validar DNIs: ${error}`);
                return false;
            }
        });
    }

    validarDniUnicoForm(formArray: FormArray): ValidationErrors | null {
        const dnis = formArray.controls.map(control => control.get('dni')?.value);
        const tieneDnisDuplicados = dnis.some((dni, index) => dnis.indexOf(dni) !== index);
        return tieneDnisDuplicados ? { dniDuplicado: true } : null;
    }


    // #region Excel
    @ViewChild('fileInput') fileInput: ElementRef;

    descargarPlantilla() {
        const workbook: XLSX.WorkBook = XLSX.utils.book_new();

        const worksheet: XLSX.WorkSheet = XLSX.utils.aoa_to_sheet([
            ['nombres', 'apellidoPaterno', 'apellidoMaterno', 'dni']
        ]);

        worksheet['!cols'] = [
            { wch: 20 },
            { wch: 20 },
            { wch: 20 },
            { wch: 20 }
        ];

        XLSX.utils.book_append_sheet(workbook, worksheet, 'Plantilla');

        XLSX.writeFile(workbook, 'plantilla.xlsx');
    }

    cargarDatosEstudiantes_Excel(event: any) {
        const target: DataTransfer = <DataTransfer>(event.target);

        Array.from(target.files).forEach((file) => {
            const reader: FileReader = new FileReader();
            reader.onload = (e: any) => {
                const bstr: string = e.target.result;
                const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });

                const wsname: string = wb.SheetNames[0];
                const ws: XLSX.WorkSheet = wb.Sheets[wsname];

                const data = XLSX.utils.sheet_to_json(ws, { header: 1 });

                data.slice(1).forEach((row: any[]) => {
                    const estudianteForm = this.crearFormularioEstudiante();
                    estudianteForm.setValue({
                        nombres: row[0],
                        apellidoPaterno: row[1],
                        apellidoMaterno: row[2],
                        dni: !isNaN(row[3]) ? row[3] : ''  // Si el DNI no es un número, se establece como una cadena vacía
                    });
                    (this.formEstudiantes.get('estudiantes') as FormArray).push(estudianteForm);
                });
                if (this.formEstudiantes && this.formEstudiantes.get('estudiantes')) {
                    const estudiantesArray = this.formEstudiantes.get('estudiantes') as FormArray;
                    if (estudiantesArray.length > 0) {
                        const primerEstudiante = estudiantesArray.at(0) as FormGroup;
                        const camposVacios = Object.values(primerEstudiante.controls).every(control => !control.value);
                        if (camposVacios) {
                            estudiantesArray.removeAt(0);
                        }
                    }
                }
            };
            reader.readAsBinaryString(file);
        });
        if (event.target instanceof HTMLInputElement) {
            event.target.value = '';
        }
    }

    // #endregion






}
