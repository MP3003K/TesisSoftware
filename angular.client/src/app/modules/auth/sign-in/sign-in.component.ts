import { NgIf } from '@angular/common';
import { Component, inject, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseAlertComponent, FuseAlertType } from '@fuse/components/alert';
import { AuthService } from 'app/core/auth/auth.service';
import { DatabaseService } from 'app/modules/admin/evaluation/components/questionary/database.service';
import { EvaluationService } from 'app/modules/admin/evaluation/evaluation.service';
import { firstValueFrom } from 'rxjs/internal/firstValueFrom';

@Component({
    selector: 'auth-sign-in',
    templateUrl: './sign-in.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations,
    standalone: true,
    imports: [
        NgIf,
        FuseAlertComponent,
        FormsModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatIconModule,
        MatProgressSpinnerModule,
    ],
})
export class AuthSignInComponent implements OnInit {
    @ViewChild('signInNgForm') signInNgForm: NgForm;
    token: string = '';
    authService = inject(AuthService);

    alert: { type: FuseAlertType; message: string } = {
        type: 'success',
        message: '',
    };
    signInForm: UntypedFormGroup;
    showAlert: boolean = false;

    constructor(
        private evaluationService: EvaluationService,
        private _activatedRoute: ActivatedRoute,
        private _authService: AuthService,
        private _formBuilder: UntypedFormBuilder,
        private _router: Router,
        private databaseService: DatabaseService
    ) { }

    ngOnInit(): void {
        this.guardarPreguntasPsicologicasLocalStorage();
        // Create the form
        this.signInForm = this._formBuilder.group({
            email: ['', [Validators.required]],
            password: ['', Validators.required],
            rememberMe: [''],
        });
    }

    desabilitarBoton: boolean = false;

    signIn(): void {
        // Return if the form is invalid
        if (this.signInForm.invalid) {
            return;
        }

        // Disable the form
        this.desabilitarBoton = true;

        // Hide the alert
        this.showAlert = false;

        // Sign in
        this.signInForm.value.password = this.signInForm.value.password.trim();
        this.signInForm.value.email = this.signInForm.value.email.trim();

        this._authService.signIn(this.signInForm.value).subscribe({
            next: ({ user: { redirect } }) => {
                let redirectURL
                if (redirect) {
                    redirectURL = redirect
                    this._router.navigateByUrl(redirectURL);
                }
            },
            error: (response) => {
                // Re-enable the form
                this.desabilitarBoton = false;

                // Reset the form
                this.signInNgForm.resetForm();

                // Set the alert
                this.alert = {
                    type: 'error',
                    message: 'Correo o contraseña equivocada',
                };

                // Show the alert
                this.showAlert = true;
            },
        });
    }

        async guardarPreguntasPsicologicasLocalStorage(): Promise<void> {
        try {
            // Llamada a la API para obtener las preguntas psicológicas
            const response = await firstValueFrom(this.evaluationService.getQuestionsAll());
            let preguntasPsicologicas: any = response?.data;

            // Asegurarse de que preguntasPsicologicas sea un array
            preguntasPsicologicas = Array.isArray(preguntasPsicologicas) ? preguntasPsicologicas : Object.values(preguntasPsicologicas);

            // Esperar a que la base de datos esté lista antes de guardar las preguntas
            await this.databaseService.ready();

            // Guardar las preguntas psicológicas en la base de datos
            await this.databaseService.guardarPreguntasPsicologicas(preguntasPsicologicas);

            console.log('Preguntas psicológicas guardadas exitosamente.');
        } catch (error) {
            console.error('Error al obtener o guardar las preguntas psicológicas:', error);
        }
    }
}
