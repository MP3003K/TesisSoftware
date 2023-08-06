import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  MainPageComponent, HeaderComponent,
} from './';
import { BarraLateralComponent } from './main-page/barra-lateral/barra-lateral.component';

const CORE_COMPONENTS: any[] = [
  HeaderComponent,
  MainPageComponent,
  BarraLateralComponent
];
@NgModule({
  imports: [CommonModule],
  exports: [ MainPageComponent, HeaderComponent,BarraLateralComponent],
  declarations: [...CORE_COMPONENTS, ],
  providers: [],
})
export class CoreModule {}
