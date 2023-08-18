import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginService } from '../../login.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
})
export class MainComponent implements OnInit {
  hide: boolean = true;
  public userForm = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });
  constructor(
    private formBuilder: FormBuilder,
    private activateRoute: ActivatedRoute,
    private router: Router,
    private loginService: LoginService,
    private _snackbar: MatSnackBar
  ) {}

  ngOnInit(): void {}

  public onSubmit() {
    if (this.userForm.valid) {
      const { username, password } = this.userForm.value;
      this.loginService
        .authenticate(username, password)
        .subscribe(({ succeeded, data }) => {
          if (succeeded) {
            const { persona, rol } = data;
            localStorage.setItem('persona', JSON.stringify(persona));
            localStorage.setItem('rol', JSON.stringify(rol));
            this.router.navigate(['pages']);
            return;
          }
          this._snackbar.open('Datos Invalidos', '', { duration: 1000 });
        });
    }
  }
}
