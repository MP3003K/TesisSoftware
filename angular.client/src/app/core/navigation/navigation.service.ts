import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Navigation } from 'app/core/navigation/navigation.types';
import { environment } from 'environments/environment';
import { Observable, ReplaySubject, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class NavigationService {
    private _httpClient = inject(HttpClient);
    private _navigation: ReplaySubject<Navigation> =
        new ReplaySubject<Navigation>(1);
    constructor(private router: Router) { }

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Getter for navigation
     */
    get navigation$(): Observable<Navigation> {
        return this._navigation.asObservable();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Get all navigation data
     */
    get(): Observable<Navigation> {
        return this._httpClient
            .get<Navigation>(`${environment.baseURL}/auth/navigation`)
            .pipe(
                tap((navigation) => {
                    if (!navigation) {
                        throw new Error('Navigation data es vacio');
                    }
                    this._navigation.next(navigation);
                }),
                catchError((e) => {
                    return new Observable<never>(subscriber => {
                        subscriber.error(e);
                    });
                })
            );
    }
}
