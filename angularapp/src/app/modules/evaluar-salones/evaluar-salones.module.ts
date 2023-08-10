import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EvaluarSalonesMenuComponent } from './evaluar-salones-menu/evaluar-salones-menu.component';
import { EvaluarSalonComponent } from './components/evaluar-salon/evaluar-salon.component';
import { EvaluarEstudianteComponent } from './components/evaluar-estudiante/evaluar-estudiante.component';
import { EvaluarSalonesRoutingModule } from './evaluar-salones-routing.module';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    EvaluarSalonesMenuComponent,
    EvaluarSalonComponent,
    EvaluarEstudianteComponent,
  ],
  imports: [EvaluarSalonesRoutingModule],
})
export class EvaluarSalonesModule {}
