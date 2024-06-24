import { CanActivateChildFn, CanActivateFn, Router, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import _ from 'lodash';
import { RoleService } from '../role.service';
import { inject } from '@angular/core';
import { of, switchMap, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

export const RoleGuard: CanActivateFn | CanActivateChildFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> => {
    const _service: RoleService = inject(RoleService);
    const router: Router = inject(Router);

    return _service.validateAccess(state.url).pipe(
        switchMap(({ succeeded, data }) => {
            if (succeeded) {
                if (data) {
                    return of(data);
                } else {
                    return of(router.parseUrl(''));
                }
            } else {
                return of(false);
            }
        }), catchError((error) => {
            console.error('Ocurrió un error role.guard:', error);
            if (error.status == 401) {
                console.error('No autorizado (validateAccess): ', error);
                router.navigate(['/sign-in']); // Redirige al usuario a la página de inicio de sesión
                return of(false);
            } else {
                console.error('Ocurrió un error:', error);
                return of(false);
            }
        })
    );
};
