import { HttpClient } from '@angular/common/http';
import { Component, Injectable, inject } from '@angular/core';
import { HttpResponse } from 'app/shared/interfaces';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class StudentService {
    private http = inject(HttpClient);

    getStudentEvaluations(): Observable<HttpResponse<any>> {
        return this.http.get<HttpResponse<any>>(
            `${environment.baseURL}/Estudiante/evaluations`,
            {
                params: {},
            }
        );
    }
}
