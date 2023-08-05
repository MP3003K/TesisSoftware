import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalComponent } from './components/modal/modal.component';

const COMPONENTS: any[] = [
  // Add components here
];

const ANGULAR_MODULES: any[] = [CommonModule];

@NgModule({
  imports: [...ANGULAR_MODULES],
  exports: [...ANGULAR_MODULES, ...COMPONENTS],
  declarations: [...COMPONENTS],
})
export class SharedModule {}
