import { Routes } from '@angular/router';
import { ClassroomsComponent } from './classrooms.component';
import { EditarEstudianteComponent } from './editar-estudiante/editar-estudiante.component';
import { RegistrarEstudiantesComponent } from './registrar-estudiantes/registrar-estudiantes.component';
import { AsignarEstudiantesComponent } from './asignar-estudiantes/asignar-estudiantes.component';

export const classroomsRoutes: Routes = [
    {
        path: '',
        component: ClassroomsComponent,
        children: [
            { path: 'editar-estudiante', component: EditarEstudianteComponent },
            { path: 'registrar-estudiantes', component: RegistrarEstudiantesComponent },
            { path: 'asignar-estudiantes', component: AsignarEstudiantesComponent }
        ]
    }
];
