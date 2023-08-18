import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  constructor(private http: HttpClient) {}
  public authenticate(username: any, password: any): Observable<any> {
    return this.http.get<any>(`/usuario/${username}/${password}`);
  }
}
