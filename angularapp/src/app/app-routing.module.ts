import { NgModule } from '@angular/core';

import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '1',
    loadChildren: () =>
      import('./pages/test-psicologico/test-psicologico.module').then(
        (m) => m.TestPsicologicoModule
      ),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}


