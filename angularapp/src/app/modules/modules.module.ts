import { NgModule } from '@angular/core';
import { ModulesComponent } from './modules.component';
import { ModulesRoutingModule } from './modules-routing.module';
import { CoreModule } from '../core/core.module';

@NgModule({
  imports: [CoreModule, ModulesRoutingModule],
  declarations: [ModulesComponent],
  providers: [],
})
export class ModulesModule {}
