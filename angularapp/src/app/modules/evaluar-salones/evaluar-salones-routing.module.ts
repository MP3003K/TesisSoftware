import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EvaluarSalonesMenuComponent } from './components/evaluar-salones-menu/evaluar-salones-menu.component';
import { EvaluarSalonesComponent } from './evaluar-salones.component';

const routes: Routes = [
  {
    path: '',
    component: EvaluarSalonesComponent,
    children: [
      {
        path: 'menu',
        component: EvaluarSalonesMenuComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EvaluarSalonesRoutingModule {}
