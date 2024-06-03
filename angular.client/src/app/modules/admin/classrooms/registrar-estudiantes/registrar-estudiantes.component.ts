import { Component, ElementRef, Injectable, Input, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { AbstractControl, ValidationErrors } from '@angular/forms';


import * as XLSX from 'xlsx';

@Component({
    selector: 'app-registrar-estudiantes',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatIconModule
    ],
    templateUrl: './registrar-estudiantes.component.html',
    styleUrl: './registrar-estudiantes.component.scss'
})

export class RegistrarEstudiantesComponent implements OnInit {
    @Input() unidadIdSeleccionada: number = 0;
    @Input() seccionIdSeleccionada: number = 0;

    formEstudiantes: FormGroup;

    constructor(private fb: FormBuilder) { }

    ngOnInit() {
        this.formEstudiantes = this.fb.group({
            estudiantes: this.fb.array([this.crearFormularioEstudiante()])
        });
        console.log(this.unidadIdSeleccionada, this.seccionIdSeleccionada);
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
        console.log(this.formEstudiantes.value);
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
