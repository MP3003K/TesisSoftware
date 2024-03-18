import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';
import { environment } from 'environments/environment';
@Injectable({
    providedIn: 'root',
})
export class StudentService {
    students: any[] = [];
    constructor(private http: HttpClient) { }
    getStudents(classroomId: number, unityId: number, dimensionId: number) {
        this.students = [];
        return this.http
            .get<any>(
                `${environment.baseURL}/estudiante/${classroomId}/${unityId}/${dimensionId}`
            )
            .pipe(
                map(({ data }) => {
                    const students = data.map((student: any) => {
                        const {
                            id,
                            codigoEstudiante: code,
                            promedioPsicologicoEstudiante: mean,
                            persona: {
                                nombres,
                                apellidoPaterno,
                                apellidoMaterno,
                                dni,
                            },
                        } = student;
                        return {
                            id,
                            code,
                            dni,
                            mean,
                            name: `${nombres} ${apellidoPaterno} ${apellidoMaterno}`,
                        };
                    });

                    this.students = students;
                    return students;
                })
            );
    }
}
