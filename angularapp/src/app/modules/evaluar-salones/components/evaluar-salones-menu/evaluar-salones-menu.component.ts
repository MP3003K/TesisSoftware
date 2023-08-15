import { AfterViewInit, Component, OnInit } from '@angular/core';
import * as L from 'leaflet';
import 'leaflet-editable';
import 'leaflet.path.drag';
declare global {
  interface Window {
    LAYER: any;
  }
}

@Component({
  selector: 'app-evaluar-salones-menu',
  templateUrl: './evaluar-salones-menu.component.html',
  styleUrls: ['./evaluar-salones-menu.component.css'],
})
export class EvaluarSalonesMenuComponent implements OnInit {
  lines: any[] = [];
  lineLayer: any = null;
  totalDistance = 0;
  ngOnInit(): void {
    this.initializeMap();
  }

  initializeMap() {
    const map = L.map('map', { editable: true }).setView([43.1249, 1.254], 16);
    L.tileLayer('http://{s}.tile.openstreetmap.fr/hot/{z}/{x}/{y}.png', {
      maxZoom: 20,
      attribution:
        '\u00a9 <a href="http://www.openstreetmap.org/copyright"> OpenStreetMap Contributors </a>',
    }).addTo(map);
    map.on('editable:drawing:end', (e: any) => {
      if (e.layer instanceof L.Polyline) {
        this.lineLayer = e.layer;
        const latlngs = this.lineLayer.getLatLngs();

        this.totalDistance = 0;
        for (let i = 0; i < latlngs.length - 1; i++) {
          this.totalDistance += latlngs[i].distanceTo(latlngs[i + 1]);
        }
      }
    });

    map.on('editable:vertex:drag', () => {
      if (this.lineLayer) {
        const latlngs = this.lineLayer.getLatLngs();

        this.totalDistance = 0;
        for (let i = 0; i < latlngs.length - 1; i++) {
          this.totalDistance += latlngs[i].distanceTo(latlngs[i + 1]);
        }
      }
    });
    const EditControl = L.Control.extend({
      options: {
        position: 'topleft',
        callback: null,
        kind: '',
        html: '',
      },

      onAdd: function (map: L.Map) {
        const container = L.DomUtil.create(
          'div',
          'leaflet-control leaflet-bar'
        );
        const link = L.DomUtil.create('a', '', container);

        link.href = '#';
        link.title = 'Create a new ' + this.options.kind;
        link.innerHTML = this.options.html;
        L.DomEvent.on(link, 'click', L.DomEvent.stop).on(
          link,
          'click',
          () => {
            const callback: any = this['options']['callback'];
            window.LAYER = callback.call(map.editTools);
          },
          this
        );

        return container;
      },
    });

    const NewLineControl = EditControl.extend({
      options: {
        position: 'topleft',
        callback: () => {
          const line = map.editTools.startPolyline();
          this.lines.push(line);
          console.log(line);
        },
        kind: 'line',
        html: '\\/\\',
      },
    });

    const NewPolygonControl = EditControl.extend({
      options: {
        position: 'topleft',
        callback: map.editTools.startPolygon,
        kind: 'polygon',
        html: '▰',
      },
    });

    const NewMarkerControl = EditControl.extend({
      options: {
        position: 'topleft',
        callback: map.editTools.startMarker,
        kind: 'marker',
        html: '🖈',
      },
    });

    const NewRectangleControl = EditControl.extend({
      options: {
        position: 'topleft',
        callback: map.editTools.startRectangle,
        kind: 'rectangle',
        html: '⬛',
      },
    });

    const NewCircleControl = EditControl.extend({
      options: {
        position: 'topleft',
        callback: map.editTools.startCircle,
        kind: 'circle',
        html: '⬤',
      },
    });

    // map.addControl(new NewMarkerControl());
    map.addControl(new NewLineControl());
    // map.addControl(new NewPolygonControl());
    // map.addControl(new NewRectangleControl());
    // map.addControl(new NewCircleControl());
  }
}
