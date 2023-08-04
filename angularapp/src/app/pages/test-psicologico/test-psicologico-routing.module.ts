import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TestPsicologicoComponent } from './test-psicologico.component';
import { RealizarTestComponent } from './components/realizar-test/realizar-test.component';
import { ResultadosTestComponent } from './components/resultados-test/resultados-test.component';

const routes: Routes = [
  {
    path: '',
    component: TestPsicologicoComponent,
    children: [
      {
        path: '',
        component: RealizarTestComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TestPsicologicoRoutingModule {}
