import { Routes } from '@angular/router';
import { QuestionaryComponent } from './components/questionary/questionary.component';
import { QuestionaryListComponent } from './components/questionary-list/questionary-list.component';

export default [
    {
        path: '',
        component: QuestionaryListComponent,
    },
    {
        path: ':id',
        component: QuestionaryComponent,
    },
] as Routes;
