import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from 'app/core/auth/auth.service';
import { Observable, map } from 'rxjs';
import { environment } from 'environments/environment';
import { UserService } from 'app/core/user/user.service';

@Injectable({
    providedIn: 'root',
})
export class EvaluationService {
    constructor(private http: HttpClient, private authService: AuthService, private userService: UserService) { }

    ngOnInit(): void { }

    getTechs(): Observable<any> {
        return this.http.get<any>(`${environment.baseURL}/unidad/all`);
    }

    getActualUnity() {
        return this.http.get<any>(`${environment.baseURL}/unidad/unidadActual`);
    }

    getUnitiesByStudent(studentId: number) {
        return this.http.get<any>(
            `${environment.baseURL}/unidad/unidadesEstudiantes/${studentId}`
        );
    }

    getClasroomAnswers(
        classroomId: number,
        dimensionId: number,
        unityId: number
    ): Observable<any> {
        return this.http
            .get<any>(
                `${environment.baseURL}/respuestasPsicologicas/RespuestasAula/${classroomId}/${dimensionId}/${unityId}`
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
            `${environment.baseURL}/respuestasPsicologicas/RespuestasEstudiante/${studentId}/${unityId}/${classroomId}/${dimensionId}`
        );
    }

    getClassrooms() {
        return this.http.get<any>(`${environment.baseURL}/aula/all`);
    }

    getClasroomByStudent(id: number) {
        return this.http.get<any>(`${environment.baseURL}/aula/aulasEstudiante/${id}`);
    }

    getQuestionsApi(studentId: number): Observable<any> {
        return this.http
            .get<any>(`${environment.baseURL}/preguntasPsicologicas/${studentId}/1/66`)
            .pipe(
                map(({ data }: { data: any[] }) => {
                    const parsedData = data.map((item, index) => {
                        const {
                            pregunta: name,
                            respuestasPsicologicas: { 0: answer },
                            id,
                        } = item;
                        return {
                            id,
                            index: index + 1,
                            name,
                            answer: answer?.respuesta ?? '',
                        };
                    });
                    return parsedData;
                })
            );
    }

    getStudent() {
        return this.http.get<any>(`${environment.baseURL}/estudiante`);
    }

    updateQuestion(answer: string, answerId: number, evaluationId: number) {
        return this.http.post<any>(`${environment.baseURL}/respuestasPsicologicas`, {
            respuesta: answer,
            preguntaPsicologicaId: answerId,
            evaPsiEstId: evaluationId,
        });
    }
    updateTestState(evaluationId: number, state: string) {
        return this.http.get<any>(
            `${environment.baseURL}/respuestasPsicologicas/ActualizarEstadoEvaPsiEst/${evaluationId}/${state}`
        );
    }
}
