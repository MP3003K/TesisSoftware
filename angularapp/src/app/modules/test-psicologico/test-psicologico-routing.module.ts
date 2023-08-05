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
        path: 'realizar-test',
        component: RealizarTestComponent,
      },
      {
        path: 'resultados-test',
        component: ResultadosTestComponent,
      },
      {
        path: '',
        redirectTo: 'realizar-test',
        pathMatch: 'full'
      },
      {
        path: '**',
        redirectTo: 'realizar-test',
        pathMatch: 'full'
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TestPsicologicoRoutingModule {}
