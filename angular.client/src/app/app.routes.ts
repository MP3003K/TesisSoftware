import { Route } from '@angular/router';
import { LayoutComponent } from 'app/layout/layout.component';
import { initialDataResolver } from './app.resolvers';
import { NoAuthGuard } from './core/auth/guards/noAuth.guard';
import { AuthGuard } from './core/auth/guards/auth.guard';

export const appRoutes: Route[] = [
    { path: '', pathMatch: 'full', redirectTo: 'dashboards/project' },
    {
        path: '',
        canMatch: [NoAuthGuard],
        component: LayoutComponent,
        data: {
            layout: 'empty',
        },
        children: [
            {
                path: 'confirmation-required',
                loadChildren: () =>
                    import(
                        'app/modules/auth/confirmation-required/confirmation-required.routes'
                    ),
            },
            {
                path: 'forgot-password',
                loadChildren: () =>
                    import(
                        'app/modules/auth/forgot-password/forgot-password.routes'
                    ),
            },
            {
                path: 'reset-password',
                loadChildren: () =>
                    import(
                        'app/modules/auth/reset-password/reset-password.routes'
                    ),
            },
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
    {
        path: '',
        canMatch: [AuthGuard],
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
            {
                path: 'unlock-session',
                loadChildren: () =>
                    import(
                        'app/modules/auth/unlock-session/unlock-session.routes'
                    ),
            },
        ],
    },

    {
        path: '',
        canMatch: [AuthGuard],
        component: LayoutComponent,
        resolve: {
            initialData: initialDataResolver,
        },
        children: [
            {
                path: 'dashboards',
                children: [
                    {
                        path: 'evaluation',
                        loadChildren: () =>
                            import(
                                'app/modules/admin/dashboards/evaluation/evaluation.routes'
                            ),
                    },
                    {
                        path: 'reports',
                        loadChildren: () =>
                            import(
                                'app/modules/admin/dashboards/reports/reports.routes'
                            ),
                    },
                ],
            },
            {
                path: 'pages',
                children: [
                    {
                        path: 'error',
                        children: [
                            {
                                path: '404',
                                loadChildren: () =>
                                    import(
                                        'app/modules/admin/pages/error/error-404/error-404.module'
                                    ).then((m) => m.Error404Module),
                            },
                            {
                                path: '500',
                                loadChildren: () =>
                                    import(
                                        'app/modules/admin/pages/error/error-500/error-500.module'
                                    ).then((m) => m.Error500Module),
                            },
                        ],
                    },
                ],
            },

            {
                path: '404-not-found',
                pathMatch: 'full',
                loadChildren: () =>
                    import(
                        'app/modules/admin/pages/error/error-404/error-404.module'
                    ).then((m) => m.Error404Module),
            },
            { path: '**', redirectTo: '404-not-found' },
        ],
    },
];
