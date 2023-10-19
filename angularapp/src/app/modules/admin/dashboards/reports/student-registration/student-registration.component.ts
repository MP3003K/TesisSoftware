import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'; // Importa FormGroup y FormBuilder

@Component({
    selector: 'app-student-registration',
    templateUrl: './student-registration.component.html',
    styleUrls: ['./student-registration.component.scss']
})
export class StudentRegistrationComponent {
    studentForm: FormGroup; // Declara el FormGroup

    constructor(
        private dialogRef: MatDialogRef<StudentRegistrationComponent>,
        private http: HttpClient,
        private fb: FormBuilder // Inyecta FormBuilder
    ) {
        this.studentForm = this.fb.group({
            nombreEstudiante: ['', Validators.required],
            apePat: ['', Validators.required],
            apeMat: ['', Validators.required],
            dni: ['', Validators.required]
        });
    }

    // Resto del código...

    registerStudent() {
        if (this.studentForm.valid) {
            const studentData = this.studentForm.value;
            // Resto del código...
        }
    }
    closeDialog() {
        this.dialogRef.close();
    }

    // Resto del código...
}
