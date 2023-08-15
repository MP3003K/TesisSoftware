import { AfterViewInit, Component, OnInit } from '@angular/core';
import { TestPsicologicoService } from '../../test-psicologico.service';
interface Unity {
  name: string;
  years: number[];
}
@Component({
  selector: 'app-resultados-test',
  templateUrl: './resultados-test.component.html',
  styleUrls: ['./resultados-test.component.css'],
})
export class ResultadosTestComponent implements OnInit, AfterViewInit {
  public selectedUnity!: Unity;
  public selectedDimension!: number;
  selectedStudent: any;
  dimensions: any[] = [];

  unities: Unity[] = [
    {
      name: '2021-1',
      years: [1, 2, 3, 4, 5],
    },
    {
      name: '2021-2',
      years: [1, 2, 3, 4, 5],
    },
    {
      name: '2022-1',
      years: [1, 2, 3, 4, 5],
    },
    {
      name: '2022-2',
      years: [1, 2, 3, 4, 5],
    },
    {
      name: '2023-1',
      years: [1, 2, 3, 4, 5],
    },
    {
      name: '2023-2',
      years: [1, 2, 3, 4, 5],
    },
  ];
  selectedScales: any[] = [];
  constructor(private testService: TestPsicologicoService) {}
  ngOnInit(): void {
    this.dimensions = this.testService.getDimensions();
  }
  ngAfterViewInit(): void {}

  public unityChange({ value }: any) {
    this.selectedUnity = this.unities[value];
  }

  public yearChange(event: any) {
    console.log(event);
  }
  onDimensionChange(event: any) {
    this.selectedScales = this.testService.getScalesByDimension(event.value);
  }
  redirectStudent(index: number) {
    this.selectedStudent = this.getStudents()[index];
  }
  getStudents() {
    return this.testService.students;
  }
}
