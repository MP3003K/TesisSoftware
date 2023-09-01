import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, ReplaySubject, tap } from 'rxjs';
import { Navigation } from 'app/core/navigation/navigation.types';
import { UserService } from '../user/user.service';
import { AuthService } from '../auth/auth.service';

@Injectable({
    providedIn: 'root',
})
export class NavigationService {
    private roles = {
        1: ['dashboards.reports'],
        2: ['dashboards.evaluations'],
    };
    private _navigation: ReplaySubject<Navigation> =
        new ReplaySubject<Navigation>(1);

    /**
     * Constructor
     */
    constructor(
        private _httpClient: HttpClient,
        private authService: AuthService,
    ) {}

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Getter for navigation
     */
    get navigation$(): Observable<Navigation> {
        return this._navigation.asObservable();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Get all navigation data
     */
    get(): Observable<Navigation> {
        return this._httpClient.get<Navigation>('api/common/navigation').pipe(
            tap(
                ({
                    default: {
                        0: { children, ...rest },
                    },
                }) => {
                    const { role } = this.authService.localUser;
                    this._navigation.next({
                        default: [
                            {
                                children: children.filter((e) =>
                                    this.roles[role].includes(e.id),
                                ),
                                ...rest,
                            },
                        ],
                    });
                },
            ),
        );
    }
}
