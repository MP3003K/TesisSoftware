import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from 'app/core/auth/auth.service';
import { Observable, map } from 'rxjs';
import { environment } from 'environments/environment';
import { UserService } from 'app/core/user/user.service';
import { HttpResponse } from 'app/shared/interfaces';

@Injectable({
    providedIn: 'root',
})
export class EvaluationService {
    constructor(
        private http: HttpClient,
        private authService: AuthService,
        private userService: UserService
    ) { }

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

    getClasroomAnswers(evaluationId: number, dimension: number): Observable<HttpResponse<any>> {
        return this.http.get<HttpResponse<any>>(
            `${environment.baseURL}/Classroom/getRespuestasEstudianteAula/${evaluationId}/${dimension}`
        );
    }

    getStudentAnswers(
        evaluationId: number,
        code: string
    ): Observable<HttpResponse<any>> {
        return this.http.get<HttpResponse<any>>(
            `${environment.baseURL}/respuestasPsicologicas/RespuestasEstudiante/${code}/${evaluationId}`
        );
    }
    getStudentEvaluation(
        classroomEvaluationId: number,
        studentId: number
    ): Observable<HttpResponse<any>> {
        return this.http.get<HttpResponse<any>>(
            `${environment.baseURL}/evaluation/${classroomEvaluationId}/students/${studentId}`
        );
    }

    getClassrooms() {
        return this.http.get<any>(`${environment.baseURL}/aula/all`);
    }

    getClasroomByStudent(id: number) {
        return this.http.get<any>(
            `${environment.baseURL}/aula/aulasEstudiante/${id}`
        );
    }

    getQuestions(studentId: number): Observable<HttpResponse<any>> {
        return this.http.get<HttpResponse<any>>(
            `${environment.baseURL}/preguntasPsicologicas/${studentId}`
        );
    }

    getStudent() {
        return this.http.get<any>(`${environment.baseURL}/estudiante`);
    }

    bulkInsertAnswers(answers: any[]): Observable<HttpResponse<any>> {
        return this.http.post<HttpResponse<any>>(
            `${environment.baseURL}/respuestasPsicologicas/bulk`,
            answers
        );
    }

    updateQuestion(answer: string, questionId: number, evaluationId: number) {
        return this.http.post<any>(
            `${environment.baseURL}/respuestasPsicologicas`,
            {
                answer,
                questionId,
                evaluationId,
            }
        );
    }

    registrarRespuestaEstudiante(params: {esEmocional: boolean, respuesta: string}): Observable<any> {
        return this.http.post<any>(`${environment.baseURL}/Persona/registrarExamenEstudiante`, params);
    }


    updateTestState(evaluationId: number) {
        return this.http.put<any>(
            `${environment.baseURL}/respuestasPsicologicas/ActualizarEstadoEvaPsiEst/${evaluationId}`,
            {}
        );
    }

    obtenerExamenesEstudiante(){
        return this.http.get<any>(`${environment.baseURL}/Aula/examenEstudiante`);
    }
}
