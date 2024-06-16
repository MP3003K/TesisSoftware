import { Routes } from '@angular/router';
import { ClassroomsComponent } from './classrooms.component';
import {
    EditarEstudianteComponent, RegistrarEstudiantesComponent, ListarEstudiantesComponent, AsignarEstudianteComponent,
    ReporteSalonComponent,
    ReporteEstudianteComponent
} from './components';

export const classroomsRoutes: Routes = [
    {
        path: '',
        component: ClassroomsComponent,
        children: [
            { path: 'editar-estudiante', component: EditarEstudianteComponent },
            { path: 'registrar-estudiantes', component: RegistrarEstudiantesComponent },
            { path: 'asignar-estudiantes', component: AsignarEstudianteComponent },
            { path: 'listar-estudiantes', component: ListarEstudiantesComponent },
            {
                path: 'reporte-salon', component: ReporteSalonComponent,
                children: [
                    { path: 'reporte-estudiante', component: ReporteEstudianteComponent }]
            },
        ]
    }
];
