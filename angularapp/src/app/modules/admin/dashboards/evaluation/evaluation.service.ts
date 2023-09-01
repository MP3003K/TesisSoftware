import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from 'app/core/auth/auth.service';
import { Observable, map } from 'rxjs';
import { baseURL } from 'environment';

@Injectable({
    providedIn: 'root',
})
export class EvaluationService {
    constructor(private http: HttpClient, private authService: AuthService) {}

    ngOnInit(): void {}

    getTechs(): Observable<any> {
        return this.http.get<any>(`${baseURL}/unidad/all`);
    }

    getActualUnity() {
        return this.http.get<any>(`${baseURL}/unidad/unidadActual`);
    }

    getUnitiesByStudent(studentId: number) {
        return this.http.get<any>(
            `${baseURL}/unidad/unidadesEstudiantes/${studentId}`
        );
    }

    getClasroomAnswers(
        classroomId: number,
        dimensionId: number,
        unityId: number
    ): Observable<any> {
        return this.http
            .get<any>(
                `${baseURL}/respuestasPsicologicas/RespuestasAula/${classroomId}/${dimensionId}/${unityId}`
            )
            .pipe(
                map(({ data }) => {
                    return data;
                })
            );
    }

    getStudentAnswers(
        studentId: number,
        unityId: number,
        classroomId: number,
        dimensionId: number
    ) {
        return this.http.get(
            `${baseURL}/respuestasPsicologicas/RespuestasEstudiante/${studentId}/${unityId}/${classroomId}/${dimensionId}`
        );
    }

    getClassrooms() {
        return this.http.get<any>(`${baseURL}/aula/all`);
    }

    getClasroomByStudent(id: number) {
        return this.http.get<any>(`${baseURL}/aula/aulasEstudiante/${id}`);
    }

    getQuestionsApi(studentId: number): Observable<any> {
        return this.http
            .get<any>(`${baseURL}/preguntasPsicologicas/${studentId}/1/66`)
            .pipe(
                map(({ data }: { data: any[] }) => {
                    return data.map(
                        (
                            {
                                pregunta: name,
                                respuestasPsicologicas: {
                                    0: { respuesta: answer },
                                },
                                id,
                            },
                            index
                        ) => ({
                            id,
                            index: index + 1,
                            name,
                            answer,
                        })
                    );
                })
            );
    }

    getStudent(studentId: number) {
        const { id } = this.authService.localUser;
        return this.http.get<any>(`${baseURL}/estudiante/${studentId}`);
    }
    getStudents(classroomId: number, unityId: number) {
        return this.http.get<any>(
            `${baseURL}/estudiante/${classroomId}/${unityId}`
        );
    }

    updateQuestion(answer: string, answerId: number, evaluationId: number) {
        return this.http.post<any>(`${baseURL}/respuestasPsicologicas`, {
            respuesta: answer,
            preguntaPsicologicaId: answerId,
            evaPsiEstId: evaluationId,
        });
    }
    updateTestState(evaluationId: number, state: string) {
        return this.http.get<any>(
            `${baseURL}/respuestasPsicologicas/ActualizarEstadoEvaPsiEst/${evaluationId}/${state}`
        );
    }
}
