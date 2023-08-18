import { NgModule } from '@angular/core';
import { MainComponent } from './components/main/main.component';
import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { SharedModule } from '../shared/shared.module';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
  declarations: [LoginComponent, MainComponent],
  imports: [
    SharedModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    LoginRoutingModule,
    MatSnackBarModule,
  ],
})
export class LoginModule {}
