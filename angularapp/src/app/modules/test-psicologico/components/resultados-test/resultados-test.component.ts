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
  public years: number[] = [];
  public selectedUnity!: number;
  public selectedDimension!: number;
  public selectedYear!: number;
  public selectedGrade!: number;
  selectedStudent: any;
  dimensions: any[] = [];
  unities: any[] = [];
  selectedScales: any[] = [];
  answers: any;
  constructor(private testService: TestPsicologicoService) {}
  ngOnInit(): void {
    this.dimensions = this.testService.getDimensions();
    this.getTechs();
  }
  ngAfterViewInit(): void {}
  filterUnities() {
    return this.unities.filter((e) => e['año'] == this.selectedYear) ?? [];
  }
  onDimensionChange({ value }: any) {
    console.log(this.selectedYear, this.selectedUnity, this.selectedGrade);
    this.testService
      .getClasroomAnswers(this.selectedGrade, value, this.selectedUnity)
      .subscribe((data: any[]) => {
        this.selectedScales = data.map(
          ({
            indicadoresPsicologicos,
            nombre: name,
          }: {
            indicadoresPsicologicos: any[];
            nombre: string;
          }) => ({
            name,
            indicators: indicadoresPsicologicos.map(
              ({ nombreIndicador: name, promedioIndicador: mean }) => ({
                name,
                mean,
              })
            ),
          })
        );
      });
    return;
    this.selectedScales = this.testService.getScalesByDimension(value);
  }
  redirectStudent(index: number) {
    this.selectedStudent = this.getStudents()[index];
  }
  getStudents() {
    return this.testService.students;
  }

  getTechs() {
    this.testService.getTechs().subscribe(({ data }) => {
      console.log(data);
      this.years = [...new Set(data.map((e: any) => e['año']))] as number[];
      console.log(this.years);
      this.unities = data;
    });
  }
}
