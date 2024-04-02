import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { List } from 'lodash';
import { Observable } from 'rxjs';
import { HttpResponse, Unidad, Aula } from 'app/shared/interfaces';
export interface ClassroomUnit {
    nombre: string;
    nUnidad: number;
    fechaInicio: string;
    fechaFin: string;
    año: number;
    escuelaId: number;
    estado: string;
    escuela: any; // Deberías reemplazar 'any' con una interfaz más específica si es necesario
    evaluacionesPsicologicasAula: any; // Ídem
    evaluacionesCompetenciasEstudiante: any; // Ídem
    id: number;
}
export interface Grado {
    id: number;
    nombre: string;
    nivelId: number;
    nGrado: number;
}

export interface Seccion {
    id: number;
    secccion: string;  // Cambio realizado aquí de 'secccion' a 'seccion'
    gradoId: number;
    escuelaId: number;
    grado: Grado;
}

export interface AulaApiResponse {
    succeeded: boolean;
    message: string | null;
    data: Seccion[];  // Asegúrate de que esto coincida con el tipo de datos que tu API devuelve realmente
}
@Injectable({
    providedIn: 'root'
})
export class ClassroomsService {

    constructor(private http: HttpClient) { }
    getUnidadesAll(): Observable<HttpResponse<Unidad[]>> {
        return this.http.get<HttpResponse<Unidad[]>>(`${environment.baseURL}/classroom/unidades/all`);
    }

    getClassrooms(): Observable<HttpResponse<ClassroomUnit[]>> {
        return this.http.get<HttpResponse<ClassroomUnit[]>>(`${environment.baseURL}/classroom/unidades/all`);
    }

    getAulas(): Observable<HttpResponse<Aula[]>> {
        return this.http.get<HttpResponse<Aula[]>>(`${environment.baseURL}/Aula/all`);
    }
    getStudentsByAulaYUnidad(aulaId: number, unidadId: number) {
        return this.http.get<any>(`${environment.baseURL}/classroom/getEstudiantesByAulaYUnidad/${aulaId}/${unidadId}`);
    }
}
