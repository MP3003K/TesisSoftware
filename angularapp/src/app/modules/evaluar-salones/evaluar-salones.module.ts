import { NgModule } from '@angular/core';
import { EvaluarEstudianteComponent } from './components/evaluar-estudiante/evaluar-estudiante.component';
import { EvaluarSalonesRoutingModule } from './evaluar-salones-routing.module';
import { EvaluarSalonesMenuComponent } from './components/evaluar-salones-menu/evaluar-salones-menu.component';
import { EvaluarSalonComponent } from './components/evaluar-salon/evaluar-salon.component';
import { SharedModule } from '../shared/shared.module';
import { EvaluarSalonesComponent } from './evaluar-salones.component';

@NgModule({
  declarations: [
    EvaluarSalonesMenuComponent,
    EvaluarSalonComponent,
    EvaluarEstudianteComponent,
    EvaluarSalonesComponent,
  ],
  imports: [SharedModule, EvaluarSalonesRoutingModule],
})
export class EvaluarSalonesModule {}
