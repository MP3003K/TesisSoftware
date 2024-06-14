import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from 'environments/environment';

@Injectable({
    providedIn: 'root',
})
export class StudentService {
    http = inject(HttpClient);

    create(student: any) {
        return this.http.post<any>(
            `${environment.baseURL}/estudiante`,
            student
        );
    }
}
