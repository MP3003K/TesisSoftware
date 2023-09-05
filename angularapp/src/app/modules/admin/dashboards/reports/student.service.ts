import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';
import { baseURL } from 'environment';
@Injectable({
    providedIn: 'root',
})
export class StudentService {
    students: any[] = [];
    constructor(private http: HttpClient) {}
    getStudents(classroomId: number, unityId: number, dimensionId: number) {
        this.students = [];
        return this.http
            .get<any>(
                `${baseURL}/estudiante/${classroomId}/${unityId}/${dimensionId}`
            )
            .pipe(
                map(({ data }) => {
                    const students = data.map((student: any) => {
                        const {
                            id,
                            codigoEstudiante: code,
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
                            mean: (Math.random() * (4 - 1) + 1).toFixed(1),
                            name: `${nombres} ${apellidoPaterno} ${apellidoMaterno}`,
                        };
                    });
                    this.students = students;
                    return students;
                })
            );
    }
}
