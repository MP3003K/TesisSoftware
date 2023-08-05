import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-realizar-test',
  templateUrl: './realizar-test.component.html',
  styleUrls: ['./realizar-test.component.css']
})
export class RealizarTestComponent implements OnInit {

  constructor(
    private router: Router,
  ) { }

  ngOnInit(
  ): void {
  }

  public goToLogin(){
    console.log("goToLogin");
    this.router.navigate(['../../login']);
  }
}
