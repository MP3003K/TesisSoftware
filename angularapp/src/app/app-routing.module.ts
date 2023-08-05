import { NgModule } from '@angular/core';

import { Routes, RouterModule } from '@angular/router';
import { ScaffoldComponent } from './core/scaffold/scaffold.component';

const routes: Routes = [
  {
    path: 'login',
    loadChildren: () =>
      import('./pages/login/login.module').then((m) => m.LoginModule),
  },
  {
    path: 'pages',
    component: ScaffoldComponent,
    children: [
      {
        path: 'test-psicologico',
        loadChildren: () =>
          import('./pages/test-psicologico/test-psicologico.module').then(
            (m) => m.TestPsicologicoModule
          ),
      },
    ],
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: 'login',
    pathMatch: 'full'
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
