import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { HttpResponse } from 'app/shared/interfaces';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class RoleService {
    private http: HttpClient = inject(HttpClient);

    getRoleAccess(): Observable<HttpResponse<any>> {
        return this.http.get<HttpResponse<any>>(
            `${environment.baseURL}/role/access`
        );
    }

    validateAccess(url: string): Observable<HttpResponse<any>> {
        return this.http.post<HttpResponse<any>>(
            `${environment.baseURL}/role/access/validate`,
            {
                path: url,
            }
        );
    }
}
