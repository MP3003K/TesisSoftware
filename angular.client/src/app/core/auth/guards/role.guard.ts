import { CanActivateChildFn, CanActivateFn, Router } from '@angular/router';
import _ from 'lodash';
import { RoleService } from '../role.service';
import { inject } from '@angular/core';
import { of, switchMap } from 'rxjs';

export const RoleGuard: CanActivateFn | CanActivateChildFn = (route, state) => {
    const _service: RoleService = inject(RoleService);
    const router: Router = inject(Router);

    return _service.validateAccess(state.url).pipe(
        switchMap(({ succeeded, data }) => {
            console.log(data, state.url);

            if (succeeded) {
                if (data) {
                    return of(data);
                } else {
                    return of(router.parseUrl(''));
                }
            } else {
                return of(false);
            }
        })
    );
    return true;
};
