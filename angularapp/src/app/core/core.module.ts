import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  MainPageComponent, HeaderComponent,
} from './';

const CORE_COMPONENTS: any[] = [
  HeaderComponent,
  MainPageComponent,
];@NgModule({
  imports: [CommonModule],
  exports: [ MainPageComponent, HeaderComponent],
  declarations: [...CORE_COMPONENTS],
  providers: [],
})
export class CoreModule {}
