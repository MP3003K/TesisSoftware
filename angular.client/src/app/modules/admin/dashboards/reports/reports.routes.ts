import { Routes } from "@angular/router";
import { StudentReportComponent } from "./student-report/student-report.component";
import { ClassroomReportComponent } from "./classroom-report/classroom-report.component";
import { ReportsComponent } from "./reports.component";

export default [
    {
        path: '',
        component: ReportsComponent,
        children: [
            {
                path: '',
                component: ClassroomReportComponent,
            },
            {
                path: ':id',
                component: StudentReportComponent,
            },
        ],
    },
] as Routes