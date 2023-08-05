import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ModulesComponent } from './modules.component';

const routes: Routes = [
  {
    path: '',
    component: ModulesComponent,
    children: [
      {
        path: 'test-psicologico',
        loadChildren: () =>
          import('./test-psicologico/test-psicologico.module').then(
            (m) => m.TestPsicologicoModule
          ),
      },
      {
        path: '',
        redirectTo: 'test-psicologico',
        pathMatch: 'full',
      },
      {
        path: '**',
        redirectTo: 'test-psicologico',
        pathMatch: 'full',
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ModulesRoutingModule {}
