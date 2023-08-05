import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainComponent } from './components/main/main.component';
import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { ModalComponent } from '../shared/components';

const COMPONENTS_SHARED: any[] = [
  // Add components here
  ModalComponent,
];
@NgModule({
  declarations: [
    LoginComponent,
    MainComponent,
    ...COMPONENTS_SHARED
  ],
  imports: [
    CommonModule,
    LoginRoutingModule
    ]
})
export class LoginModule { }
