import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModulesComponent } from './modules.component';
import { ModulesRoutingModule } from './modules-routing.module';
import {CoreModule} from "../core/core.module";



@NgModule({
  imports: [
    CommonModule,
    CoreModule,
    ModulesRoutingModule,
  ],
  declarations: [
    ModulesComponent,
  ],
  providers: [],
})
export class ModulesModule { }
