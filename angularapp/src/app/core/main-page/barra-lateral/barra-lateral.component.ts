import { Component } from '@angular/core';

@Component({
  selector: 'app-barra-lateral',
  templateUrl: './barra-lateral.component.html',
  styleUrls: ['./barra-lateral.component.css'],
})
export class BarraLateralComponent {
  menuItems = [
    { icon: 'school', name: 'Materias' },
    { icon: 'subscriptions', name: 'Tarjetas' },
    { icon: 'list_alt', name: 'Listas' },
    { icon: 'code', name: 'Oportunidades' },
  ];
}
