import { Component, ElementRef, EventEmitter, Injectable, Input, OnInit, Output, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { AbstractControl, ValidationErrors } from '@angular/forms';


import * as XLSX from 'xlsx';
import { ClassroomsService } from '../classrooms.service';
import { set } from 'lodash';


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
            estudiantes: this.fb.array([this.crearFormularioEstudiante()], [this.validadorDniDuplicadoEnFormEstudiantes()])
        });
        this.llenarRegistrosEstudiantes();
    }




    llenarRegistrosEstudiantes() {
        // Datos de ejemplo para 4 estudiantes
        const datosEstudiantes = [
            { nombres: 'Juan', apellidoPaterno: 'Perez', apellidoMaterno: 'Gomez', dni: '73705438' },
            { nombres: 'Ana', apellidoPaterno: 'Lopez', apellidoMaterno: 'Martinez', dni: '84736291' },
            { nombres: 'Luis', apellidoPaterno: 'Fernandez', apellidoMaterno: 'Ruiz', dni: '95847263' },
            { nombres: 'Sofia', apellidoPaterno: 'Garcia', apellidoMaterno: 'Torres', dni: '76294815' }
        ];

        // Iterar sobre los datos de los estudiantes y agregarlos al FormArray
        datosEstudiantes.forEach(estudianteData => {
            let estudianteForm = this.crearFormularioEstudiante();
            estudianteForm.setValue(estudianteData);
            (this.formEstudiantes.get('estudiantes') as FormArray).push(estudianteForm);
        });
    }


    // #region Formulario

    /**
     * Crea y retorna un FormGroup para un estudiante
     * @returns FormGroup
     */
    get estudiantes(): FormArray {
        return this.formEstudiantes.get('estudiantes') as FormArray;
    }

    crearFormularioEstudiante(): FormGroup {
        return this.fb.group({
            nombres: ['', Validators.required],
            apellidoPaterno: ['', Validators.required],
            apellidoMaterno: ['', Validators.required],
            dni: ['', [Validators.required, Validators.pattern('^[0-9]{8}$')]]
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

    async registrarEstudiantesApi() {
        console.log(this.formEstudiantes.invalid);
        if (this.formEstudiantes.invalid) {
            console.error('Formulario estudiantes es invalido');
            return;
        }

        let validador = await this.sonDnisUnicosEnBaseDeDatos();

        if (!validador) return;

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
                console.error(`Error al registrar estudiantes, Message: ${error}`);
            }
        });
    }


    limpiarRegistrosFormulario() {
        this.formEstudiantes.reset();
        (this.formEstudiantes.get('estudiantes') as FormArray).clear();
        this.agregarEstudiante();
    }
    // #endregion


    // #region Validadores


    /**
   * La funcion valida que en FormEstudiantes no existan DNIs duplicados (solo para DNIs de 8 dígitos)
   */
    validadorDniDuplicadoEnFormEstudiantes(): ValidatorFn {
        return (formArray: AbstractControl): ValidationErrors | null => {
            if (formArray instanceof FormArray) {
                formArray.controls.forEach(control => {
                    const dniControl = control.get('dni');

                    if (dniControl?.errors) {
                        const { dniDuplicado, dniRegistrado, ...remainingErrors } = dniControl.errors;
                        const hasTargetErrors = dniDuplicado || dniRegistrado;
                        if (hasTargetErrors) {
                            dniControl.setErrors(Object.keys(remainingErrors).length ? remainingErrors : null);
                        }
                    }
                });

                const dnis = formArray.controls.map(control => control.get('dni')?.value)
                    .filter(dni => dni && dni.toString().length == 8);

                const dnisDuplicados = dnis.filter((dni, index, self) => self.indexOf(dni) !== index);

                if (dnisDuplicados.length > 0) {
                    formArray.controls.forEach(control => {
                        if (dnisDuplicados.includes(control.get('dni')?.value)) {
                            const dniControl = control.get('dni');
                            const currentErrors = dniControl.errors || {};
                            dniControl.setErrors({ ...currentErrors, dniDuplicado: true });
                        }
                    });
                    return { dniDuplicado: true };
                }
            }
            return null;
        };
    }


    async sonDnisUnicosEnBaseDeDatos(): Promise<boolean> {
        const dnis = (this.formEstudiantes.get('estudiantes') as FormArray).controls
            .map(control => ({ dni: control.value.dni }));

        if (!dnis || dnis.length === 0) return true;

        let dnisJson = JSON.stringify(dnis);

        try {
            const response = await this.classroomsService.validarDnisUnicos({ jsonDnis: dnisJson }).toPromise();
            if (response?.succeeded) {
                return true;
            }
            if (!response?.succeeded && response.data) {
                let dnisRegistrados = new Set<number>(
                    JSON.parse(response.data)
                        .map(obj => Number(obj.dni))
                        .filter(dni => !isNaN(dni))
                );
                console.log('dnisRegistrados', dnisRegistrados)
                if (dnisRegistrados && dnisRegistrados.size > 0) {
                    this.marcarDnisDuplicadosEnFormularioEstudiantes(dnisRegistrados);
                }

                return false;
            }
            return false;
        } catch (error) {
            console.error(`Error al verificar existencia de DNIs: ${error}`);
            return false;
        }
    }

    marcarDnisDuplicadosEnFormularioEstudiantes(dnisRegistrados: Set<number>) {
        const estudiantesFormArray = this.formEstudiantes.get('estudiantes') as FormArray;
        estudiantesFormArray.controls.forEach(control => {
            console.log('dnisRegistrados', dnisRegistrados);
            const dniValue = control.get('dni')?.value;
            const dni: number = parseInt(dniValue);

            if (!isNaN(dni) && dnisRegistrados.has(dni)) {
                console.log('dni registrado bd', dni);
                const dniControl = control.get('dni');
                const currentErrors = dniControl.errors || {};
                dniControl.setErrors({ ...currentErrors, dniRegistrado: true });
            }
        });
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
