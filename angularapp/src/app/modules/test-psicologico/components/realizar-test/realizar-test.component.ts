import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { TestPsicologicoService } from '../../test-psicologico.service';
@Component({
  selector: 'app-realizar-test',
  templateUrl: './realizar-test.component.html',
  styleUrls: ['./realizar-test.component.css'],
})
export class RealizarTestComponent implements OnInit {
  public questions: any = [];
  @ViewChild('parentSection') miDivRef!: ElementRef;

  paginator = {
    length: 0,
    pageSize: 10,
    pageNumber: 1,
    pages: 1,
  };
  constructor(
    private router: Router,
    private _snackbar: MatSnackBar,
    public testPsicologicoService: TestPsicologicoService
  ) {}
  updatePaginator() {
    this.paginator.pageNumber = 1;
    this.paginator.length = this.questions.length;
    this.paginator.pages = Math.ceil(
      this.paginator.length / this.paginator.pageSize
    );
  }

  ngOnInit(): void {
    this.getQuestionsApi();
  }

  public goToLogin() {
    this.router.navigate(['../../login']);
  }
  itemOptions: any[] = [
    {
      name: 'Nunca',
      value: 1,
    },
    {
      name: 'Casi Nunca',
      value: 2,
    },
    {
      name: 'A veces',
      value: 3,
    },
    {
      name: 'Casi Siempre',
      value: 4,
    },
    {
      name: 'Siempre',
      value: 5,
    },
  ];

  public filterQuestions() {
    const { pageSize, pageNumber } = this.paginator;
    const firstItemIndex = (pageNumber - 1) * pageSize;
    const lastItemIndex = firstItemIndex + pageSize;
    return this.questions.slice(firstItemIndex, lastItemIndex);
  }
  public countCompletedAnswers() {
    const completedAnswersCount = this.questions.reduce((a: number, b: any) => {
      if (b.answer) return a + 1;
      return a;
    }, 0);
    return completedAnswersCount;
  }
  getPreviousItem() {
    this.paginator.pageNumber -= 1;
  }

  getNextItem() {
    if (!this.validatePage()) {
      this._snackbar.open('Datos Faltantes', '', { duration: 1000 });
      return;
    }
    this.paginator.pageNumber += 1;
    this.scrollToTop();
  }
  endForm() {
    console.log('ending form');
  }

  scrollToTop() {
    const miDivElement: HTMLElement = this.miDivRef.nativeElement;
    miDivElement.scrollTo({
      top: 0,
      behavior: 'smooth',
    });
  }
  submitForm() {
    console.log(this.questions);
  }
  public getPValue() {
    return `${(this.countCompletedAnswers() * 100) / this.paginator.length}%`;
  }
  validatePage() {
    return this.filterQuestions().every((e: any) => e.answer);
  }
  getQuestionsApi() {
    this.testPsicologicoService.getQuestionsApi().subscribe((res: any) => {
      console.log(res);
      this.questions = res;
      this.updatePaginator();
    });
  }
}
