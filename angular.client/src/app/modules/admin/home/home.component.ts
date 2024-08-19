import {
    ChangeDetectorRef,
    Component,
    OnDestroy,
    OnInit,
    ViewEncapsulation,
} from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { Router } from '@angular/router';
import { UserService } from 'app/core/user/user.service';
import { User } from 'app/core/user/user.types';
import { SharedModule } from 'app/shared/shared.module';
import { Subject, takeUntil } from 'rxjs';

@Component({
    selector: 'app-home',
    standalone: true,
    templateUrl: './home.component.html',
    encapsulation: ViewEncapsulation.None,
    imports: [MatCardModule, SharedModule],
})
export class HomeComponent implements OnInit, OnDestroy {
    user: User;

    private _unsubscribeAll: Subject<any> = new Subject<any>();
    /**
     * Constructor
     */
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _service: UserService,
        private router: Router
    ) {}
    ngOnInit(): void {
        this._service.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((user: User) => {
                this.user = user;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });
    }
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }
    navigateToEvaluation() {
        this.router.navigate(['/evaluation']); // Cambia la ruta según sea necesario
    }
}
