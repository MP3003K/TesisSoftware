import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
})
export class MainComponent implements OnInit {
  hide: boolean = true;
  formRegistro: any = FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private activateRoute: ActivatedRoute,
    private router: Router
    ) {}

  ngOnInit(): void {
    this.buildForm();
  }

  private buildForm() {
    const controls = {
      username: [null, Validators.required],
      password: [null, Validators.required],
    };
    this.formRegistro = this.formBuilder.group(controls);
  }

  public goToTestPsicologico() {
    console.log(this.formRegistro.value);
    if(this.formRegistro.valid){
      this.router.navigate(['pages']);
    }
  }
}
