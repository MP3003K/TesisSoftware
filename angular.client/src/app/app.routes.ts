import { Route } from '@angular/router';
import { initialDataResolver } from 'app/app.resolvers';
import { AuthGuard } from 'app/core/auth/guards/auth.guard';
import { NoAuthGuard } from 'app/core/auth/guards/noAuth.guard';
import { LayoutComponent } from 'app/layout/layout.component';
import { classroomsRoutes } from './modules/admin/classrooms/classrooms.routes';
import { RoleGuard } from './core/auth/guards/role.guard';


// Definición de las rutas
export const appRoutes: Route[] = [
    { path: '', pathMatch: 'full', redirectTo: 'home' },
    { path: 'signed-in-redirect', pathMatch: 'full', redirectTo: 'home' },
    {
        path: '',
        canActivate: [NoAuthGuard],
        component: LayoutComponent,
        data: { layout: 'empty' },
        children: [
            {
                path: 'sign-in',
                loadChildren: () =>
                    import('app/modules/auth/sign-in/sign-in.routes'),
            },
            {
                path: 'sign-up',
                loadChildren: () =>
                    import('app/modules/auth/sign-up/sign-up.routes'),
            },
        ],
    },

    // Auth routes for authenticated users
    {
        path: '',
        canActivate: [AuthGuard],
        canActivateChild: [AuthGuard],
        component: LayoutComponent,
        data: {
            layout: 'empty',
        },
        children: [
            {
                path: 'sign-out',
                loadChildren: () =>
                    import('app/modules/auth/sign-out/sign-out.routes'),
            },
        ],
    },
    {
        path: '',
        canActivate: [AuthGuard, RoleGuard],
        component: LayoutComponent,
        resolve: { initialData: initialDataResolver },
        children: [
            {
                path: 'home',
                loadChildren: () => import('app/modules/admin/home/home.routes'),
            },
            {
                path: 'evaluation',
                loadChildren: () => import('app/modules/admin/evaluation/evaluation.routes'), data: { preload: true }
            },
            {
                path: 'classrooms',
                loadChildren: () => classroomsRoutes,
                data: {
                    preload: true
                }
            }
        ],
    },
];

export class AppRoutingModule { }
