import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RealizarTestComponent } from './components/realizar-test/realizar-test.component';
import { ResultadosTestComponent } from './components/resultados-test/resultados-test.component';
import { TestPsicologicoRoutingModule } from './test-psicologico-routing.module';
import { TestPsicologicoComponent } from './test-psicologico.component';

const SERVICES: any[] = [

];


@NgModule({
  declarations: [
    TestPsicologicoComponent,
    RealizarTestComponent,
    ResultadosTestComponent,
  ],
  imports: [
    CommonModule,
    TestPsicologicoRoutingModule
  ],
  providers: [
    ...SERVICES
  ]
})
export class TestPsicologicoModule { }
