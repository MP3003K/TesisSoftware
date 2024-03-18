import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'; // Importa FormGroup y FormBuilder
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { SharedModule } from 'app/shared/shared.module';

@Component({
    selector: 'app-student-registration',
    templateUrl: './student-registration.component.html',
    standalone: true,
    imports: [SharedModule, MatFormFieldModule, MatInputModule,]
})
export class StudentRegistrationComponent {
    studentForm: FormGroup; // Declara el FormGroup

    constructor(
        private dialogRef: MatDialogRef<StudentRegistrationComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { classroomId: number },
        private http: HttpClient,
        private fb: FormBuilder // Inyecta FormBuilder
    ) {
        this.studentForm = this.fb.group({
            nombreEstudiante: ['', Validators.required],
            apePat: ['', Validators.required],
            apeMat: ['', Validators.required],
            dni: ['', [Validators.required, Validators.pattern('^[0-9]*$')]] // Solo permite números
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

}
