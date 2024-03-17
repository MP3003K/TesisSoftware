import { inject } from '@angular/core';
import { CanActivateChildFn, CanActivateFn, Router } from '@angular/router';

export const ModulesGuard: CanActivateFn | CanActivateChildFn = (
    { data },
    state
) => {
    const router: Router = inject(Router);

    let module: any[] = JSON.parse(localStorage.getItem('datos_modulos'));

    const haveAccess = module
        .map((element) =>
            element.children.some((e: any) => e.title == data.module)
        )
        .some((e) => e == true);
    if (haveAccess) {
        return true;
    } else {
        alert('no tienes permiso para acceder a este modulo');
        router.navigate(['/']);
        return false;
    }
};
