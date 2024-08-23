import { Injectable } from '@angular/core';
import { PreloadingStrategy, Route } from '@angular/router';
import { Observable, of } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class EvaluationPreloadStrategy implements PreloadingStrategy {
    preload(route: Route, fn: () => Observable<any>): Observable<any> {
        console.log('hola')
        if ((route.data as any).preload) {
            console.log('paso')
            return fn();
        }
        return of(null);
    }
}
