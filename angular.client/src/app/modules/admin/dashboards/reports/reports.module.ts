import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'app/shared/shared.module';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';

import { StudentReportComponent } from './student-report/student-report.component';
import { ClassroomReportComponent } from './classroom-report/classroom-report.component';
import { ReportsComponent } from './reports.component';

@NgModule({
    declarations: [
        ClassroomReportComponent,
        StudentReportComponent,
        ReportsComponent,
    ],
    imports: [
        SharedModule,
        RouterModule.forChild([
            {
                path: '',
                component: ReportsComponent,
                children: [
                    {
                        path: '',
                        component: ClassroomReportComponent,
                    },
                    {
                        path: ':id',
                        component: StudentReportComponent,
                    },
                ],
            },
        ]),
        MatIconModule,
        MatButtonModule,
        MatButtonToggleModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatDialogModule,
        MatSnackBarModule,
        MatCardModule,
    ],
})
export class ReportsModule {}
