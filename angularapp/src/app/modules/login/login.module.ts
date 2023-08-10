import { NgModule } from '@angular/core';
import { MainComponent } from './components/main/main.component';
import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [LoginComponent, MainComponent],
  imports: [SharedModule, LoginRoutingModule],
})
export class LoginModule {}
