import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from 'environments/environment';

@Injectable({
    providedIn: 'root',
})
export class PersonaService {
    http = inject(HttpClient);

    getStudentsByQuery(query: string, classroomId: number, unityId: number) {
        return this.http.get<any>(`${environment.baseURL}/persona`, {
            params: {
                query,
                classroomId,
                unityId,
            },
        });
    }
}
