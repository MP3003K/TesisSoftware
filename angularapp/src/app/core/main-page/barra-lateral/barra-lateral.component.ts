import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
@Component({
  selector: 'app-barra-lateral',
  templateUrl: './barra-lateral.component.html',
  styleUrls: ['./barra-lateral.component.css'],
})
export class BarraLateralComponent implements OnInit {
  fullName: string = '';
  ngOnInit(): void {
    const { nombres, apellidoPaterno, apellidoMaterno } = JSON.parse(
      localStorage.getItem('persona') ?? ''
    );
    this.fullName = `${nombres} ${apellidoPaterno} ${apellidoMaterno}`;
  }
  menuItems = [{ icon: 'school', name: 'Evaluar Salones' }];
}
