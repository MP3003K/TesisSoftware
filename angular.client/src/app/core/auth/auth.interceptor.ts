import {
    HttpErrorResponse,
    HttpEvent,
    HttpHandlerFn,
    HttpRequest,
} from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'app/core/auth/auth.service';
import { AuthUtils } from 'app/core/auth/auth.utils';
import { catchError, Observable, throwError } from 'rxjs';

/**
 * Intercept
 *
 * @param req
 * @param next
 */
export const authInterceptor = (
    req: HttpRequest<unknown>,
    next: HttpHandlerFn
): Observable<HttpEvent<unknown>> => {
    const authService = inject(AuthService);

    let newReq = req.clone({ withCredentials: true });

    let validacion: boolean = authService.accessToken && authService.accessToken.length > 0 && !AuthUtils.isTokenExpired(authService.accessToken);
    if (validacion) {
        newReq = req.clone({
            headers: req.headers.set(
                'Authorization',
                'Bearer ' + authService.accessToken
            ),
            withCredentials: true,
        });
    }

    // Response
    return next(newReq).pipe(
        catchError((error) => {
            if (error instanceof HttpErrorResponse && error.status === 401) {
                console.error('erorr 401')
                authService.cerrarSesion();
                // location.reload();
            }
            return throwError(() => error);
        })
    );
};
