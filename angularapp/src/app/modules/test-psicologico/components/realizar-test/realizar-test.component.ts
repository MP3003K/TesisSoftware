import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-realizar-test',
  templateUrl: './realizar-test.component.html',
  styleUrls: ['./realizar-test.component.css'],
})
export class RealizarTestComponent implements OnInit {
  public formValues;
  public questions;
  @ViewChild('parentSection') miDivRef!: ElementRef;

  paginator = {
    length: 0,
    pageSize: 10,
    pageNumber: 1,
    pages: 1,
  };
  constructor(private router: Router) {
    this.formValues = this.generateFormValues();
    this.questions = this.getQuestions();
    this.updatePaginator();
  }
  updatePaginator() {
    this.paginator.pageNumber = 1;
    this.paginator.length = this.questions.length;
    this.paginator.pages = Math.floor(
      this.paginator.length / this.paginator.pageSize
    );
  }

  ngOnInit(): void {}

  public goToLogin() {
    this.router.navigate(['../../login']);
  }

  scales = [
    {
      id: 1,
      name: 'Autoconcepto',
      kpis: 2,
      dimension: 'HSE',
    },
    {
      id: 2,
      name: 'Autoestima',
      kpis: 3,
      dimension: 'HSE',
    },
    {
      id: 3,
      name: 'Conciencia emocional',
      kpis: 3,
      dimension: 'HSE',
    },
    {
      id: 4,
      name: 'Autocuidado',
      kpis: 3,
      dimension: 'HSE',
    },
    {
      id: 5,
      name: 'Regulación emocional',
      kpis: 3,
      dimension: 'HSE',
    },
    {
      id: 6,
      name: 'Creatividad',
      kpis: 3,
      dimension: 'HSE',
    },
    {
      id: 7,
      name: 'Toma de decisiones responsables',
      kpis: 3,
      dimension: 'HSE',
    },
    {
      id: 8,
      name: 'Comunicación asertiva',
      kpis: 3,
      dimension: 'HSE',
    },
    {
      id: 9,
      name: 'Trabajo en equipo',
      kpis: 3,
      dimension: 'HSE',
    },
    {
      id: 10,
      name: 'Empatía',
      kpis: 2,
      dimension: 'HSE',
    },
    {
      id: 11,
      name: 'Resolución de conflictos',
      kpis: 2,
      dimension: 'HSE',
    },
    {
      id: 12,
      name: 'Consciencia social',
      kpis: 2,
      dimension: 'HSE',
    },
    {
      id: 13,
      name: 'Comportamiento prosocial',
      kpis: 2,
      dimension: 'HSE',
    },
    {
      id: 14,
      name: 'Consumo de alcohol y drogas',
      kpis: 3,
      dimension: 'FR',
    },
    {
      id: 15,
      name: 'Trabajo adolescente',
      kpis: 3,
      dimension: 'FR',
    },
    {
      id: 16,
      name: 'Ausentismo y deserción Escolar',
      kpis: 2,
      dimension: 'FR',
    },
    {
      id: 17,
      name: 'Desinformación sobre educación sexual reproductiva',
      kpis: 3,
      dimension: 'FR',
    },
    {
      id: 18,
      name: 'Violencia familiar',
      kpis: 4,
      dimension: 'FR',
    },
    {
      id: 19,
      name: 'Limitada expectativa sobre la educación',
      kpis: 3,
      dimension: 'FR',
    },
    {
      id: 20,
      name: 'Percepción sobre Estereotipos de género',
      kpis: 4,
      dimension: 'FR',
    },
    {
      id: 21,
      name: 'Violencia Escolar',
      kpis: 4,
      dimension: 'FR',
    },
    {
      id: 22,
      name: 'Limitados recursos económicos',
      kpis: 4,
      dimension: 'FR',
    },
    {
      id: 23,
      name: 'Entorno de riesgo',
      kpis: 2,
      dimension: 'FR',
    },
  ];

  public generateFormValues() {
    return Array.from({ length: this.scales.length }, (_, i) => ({
      scaleId: this.scales[i]['id'],
      scaleName: this.scales[i]['name'],
      dimension: this.scales[i]['dimension'],
      questions: Array.from({ length: this.scales[i]['kpis'] }, (_, j) => ({
        questionId: `${i + 1}${j + 1}`,
        question: `¿Te gusta la forma en la que se llevan las clases?`,
        answer: '',
      })),
    }));
  }
  public getQuestions() {
    const questionsArray = this.formValues
      .reduce((a, b) => {
        return [...a, ...b.questions];
      }, [] as any[])
      .map((e, i) => ({ ...e, question: `${i + 1}. ${e.question}` }));
    return questionsArray;
  }
  public filterQuestions() {
    const { pageSize, pageNumber, pages, length } = this.paginator;
    const firstItemIndex = (pageNumber - 1) * pageSize;
    const lastItemIndex = firstItemIndex + pageSize;
    return this.questions.slice(firstItemIndex, lastItemIndex);
  }
  public countCompletedAnswers() {
    const completedAnswersCount = this.questions.reduce((a, b) => {
      if (b.answer) return a + 1;
      return a;
    }, 0);
    return completedAnswersCount;
  }

  getNextItem() {
    this.paginator.pageNumber += 1;
    this.scrollToTop();
  }

  scrollToTop() {
    const miDivElement: HTMLElement = this.miDivRef.nativeElement;
    miDivElement.scrollTo({
      top: 0,
      behavior: 'smooth',
    });
  }
  submitForm() {
    console.log(this.formValues);
  }
  public getPValue() {
    return `${(this.countCompletedAnswers() * 100) / this.paginator.length}%`;
  }
}
