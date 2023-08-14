import { AfterViewInit, Component, OnInit } from '@angular/core';
import { polygon, map, tileLayer, Map } from 'leaflet';

@Component({
  selector: 'app-evaluar-salones-menu',
  templateUrl: './evaluar-salones-menu.component.html',
  styleUrls: ['./evaluar-salones-menu.component.css'],
})
export class EvaluarSalonesMenuComponent implements OnInit, AfterViewInit {
  public map!: Map;
  constructor() {}
  ngAfterViewInit(): void {
    this.initMap();
  }
  ngOnInit(): void {}
  onMapClick(e:any) {
    console.log(e);
  }
  initMap() {
    this.map = map('map').setView([51.505, -0.09], 13);
    this.map.on('click', this.onMapClick);

    tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 19,
      attribution:
        'Map data © <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
    }).addTo(this.map);
    polygon(
      [
        [51.509, -0.08],
        [51.503, -0.06],
        [51.51, -0.047],
      ],
      { color: 'red' }
    )
      .addTo(this.map)
      .bindPopup('¡Este es un polígono!');
  }
}
