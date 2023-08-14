import { NgModule } from '@angular/core';
import { MainPageComponent, HeaderComponent } from './';
import { BarraLateralComponent } from './main-page/barra-lateral/barra-lateral.component';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
const CORE_COMPONENTS: any[] = [
  HeaderComponent,
  MainPageComponent,
  BarraLateralComponent,
];
@NgModule({
  declarations: [...CORE_COMPONENTS],
  imports: [CommonModule, HttpClientModule, MatIconModule],
  exports: [...CORE_COMPONENTS],
})
export class CoreModule {}
