/* eslint-disable */
import { FuseNavigationItem } from '@fuse/components/navigation';

export const defaultNavigation: FuseNavigationItem[] = [
    {
        id: 'dashboards',
        title: 'Panel',
        subtitle: 'Evaluaciones',
        type: 'group',
        icon: 'heroicons_outline:home',
        children: [
            {
                id: 'dashboards.evaluations',
                title: 'Evaluaciones',
                type: 'basic',
                icon: 'heroicons_outline:clipboard-check',
                link: '/dashboards/evaluation',
            },
            {
                id: 'dashboards.reports',
                title: 'Reportes',
                type: 'basic',
                icon: 'heroicons_outline:chart-pie',
                link: '/dashboards/reports',
            },
        ],
    },
];
