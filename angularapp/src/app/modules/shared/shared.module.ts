import { NgModule } from '@angular/core';
import { ModalComponent } from './components/modal/modal.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

const COMPONENTS: any[] = [ModalComponent];

const ANGULAR_MODULES: any[] = [
  HttpClientModule,
  FormsModule,
  ReactiveFormsModule,
  CommonModule,
];

@NgModule({
  declarations: [...COMPONENTS],

  imports: [...ANGULAR_MODULES],
  exports: [...ANGULAR_MODULES, ...COMPONENTS],
})
export class SharedModule {}
