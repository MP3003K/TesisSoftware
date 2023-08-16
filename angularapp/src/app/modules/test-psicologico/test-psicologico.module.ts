import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RealizarTestComponent } from './components/realizar-test/realizar-test.component';
import { ResultadosTestComponent } from './components/resultados-test/resultados-test.component';
import { TestPsicologicoRoutingModule } from './test-psicologico-routing.module';
import { TestPsicologicoComponent } from './test-psicologico.component';
import { MatRadioModule } from '@angular/material/radio';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { SharedModule } from '../shared/shared.module';
import { MatIconModule } from '@angular/material/icon';
const SERVICES: any[] = [];

@NgModule({
  declarations: [
    TestPsicologicoComponent,
    RealizarTestComponent,
    ResultadosTestComponent,
  ],
  imports: [
    SharedModule,
    TestPsicologicoRoutingModule,
    MatRadioModule,
    MatSnackBarModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    MatButtonToggleModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
  ],
  providers: [...SERVICES],
})
export class TestPsicologicoModule {}
