import { NgModule } from '@angular/core';
import { MainPageComponent, HeaderComponent } from './';
import { BarraLateralComponent } from './main-page/barra-lateral/barra-lateral.component';

const CORE_COMPONENTS: any[] = [
  HeaderComponent,
  MainPageComponent,
  BarraLateralComponent,
];
@NgModule({
  declarations: [...CORE_COMPONENTS],
  exports: [MainPageComponent, HeaderComponent, BarraLateralComponent],
})
export class CoreModule {}
