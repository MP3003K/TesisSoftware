import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Route } from '@angular/router';
import { EvaluationComponent } from './evaluation.component';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatRadioModule } from '@angular/material/radio';
import { MatCardModule } from '@angular/material/card';

import { SharedModule } from 'app/shared/shared.module';
const routes: Route[] = [
    {
        path: '',
        component: EvaluationComponent,
    },
];
@NgModule({
    declarations: [EvaluationComponent],
    imports: [
        SharedModule,
        RouterModule.forChild(routes),
        MatIconModule,
        MatButtonModule,
        MatSnackBarModule,
        MatRadioModule,
        MatCardModule,
    ],
})
export class EvaluationModule {}
