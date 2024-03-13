import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, of, switchMap, throwError } from 'rxjs';
import { AuthUtils } from 'app/core/auth/auth.utils';
import { UserService } from 'app/core/user/user.service';
import { baseURL } from 'environment';
@Injectable()
export class AuthService {
    private _authenticated: boolean = false;

    /**
     * Constructor
     */
    constructor(
        private _httpClient: HttpClient,
        private _userService: UserService
    ) {}

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Setter & getter for access token
     */
    set accessToken(token: string) {
        localStorage.setItem('accessToken', token);
    }

    get accessToken(): string {
        return localStorage.getItem('accessToken') ?? '';
    }
    set localUser(user: any) {
        localStorage.setItem('user', JSON.stringify(user));
    }
    get localUser() {
        return JSON.parse(localStorage.getItem('user') ?? '{}');
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Forgot password
     *
     * @param email
     */
    forgotPassword(email: string): Observable<any> {
        return this._httpClient.post('api/auth/forgot-password', email);
    }

    /**
     * Reset password
     *
     * @param password
     */
    resetPassword(password: string): Observable<any> {
        return this._httpClient.post('api/auth/reset-password', password);
    }

    /**
     * Sign in
     *
     * @param credentials
     */
    signIn({
        email,
        password,
    }: {
        email: string;
        password: string;
    }): Observable<any> {
        if (this._authenticated) {
            return throwError(() => new Error('User is already logged in.'));
        }

        return this._httpClient
            .get(`${baseURL}/usuario/${email}/${password}`)
            .pipe(
                switchMap((response: any) => {
                    const {
                        data: {
                            persona,
                            rol: { id },
                        },
                    } = response;
                    const parsedUser = {
                        id: persona.id,
                        email,
                        name: `${persona.nombres} ${persona.apellidoPaterno} ${persona.apellidoMaterno}`,
                        role: id,
                    };
                    //this.accessToken = response.accessToken;
                    this._authenticated = true;
                    this._userService.user = parsedUser;
                    this.localUser = parsedUser;
                    return of(response);
                })
            );
    }

    /**
     * Sign in using the access token
     */
    signInUsingToken(): Observable<any> {
        // return this._httpClient
        //     .post('api/auth/sign-in-with-token', {
        //         accessToken: this.accessToken,
        //     })
        //     .pipe(
        //         catchError(() => of(false)),
        //         switchMap((response: any) => {
        //             if (response.accessToken) {
        //                 this.accessToken = response.accessToken;
        //             }
        //             this._authenticated = true;
        //             this._userService.user = response.user;
        //             return of(true);
        //         }),
        //     );
        if (Object.keys(this.localUser).length == 0) return of(false);
        this._authenticated = true;
        this._userService.user = this.localUser;
        return of(true);
    }

    /**
     * Sign out
     */
    signOut(): Observable<any> {
        // localStorage.removeItem('accessToken');
        localStorage.removeItem('user');
        this._authenticated = false;
        return of(true);
    }

    /**
     * Sign up
     *
     * @param user
     */
    signUp(user: {
        name: string;
        email: string;
        password: string;
        company: string;
    }): Observable<any> {
        return this._httpClient.post('api/auth/sign-up', user);
    }

    /**
     * Unlock session
     *
     * @param credentials
     */
    unlockSession(credentials: {
        email: string;
        password: string;
    }): Observable<any> {
        return this._httpClient.post('api/auth/unlock-session', credentials);
    }

    /**
     * Check the authentication status
     */
    check(): Observable<boolean> {
        // if (this._authenticated) {
        //     return of(true);
        // }
        // if (!this.accessToken) {
        //     return of(false);
        // }
        // if (AuthUtils.isTokenExpired(this.accessToken)) {
        //     return of(false);
        // }
        return this.signInUsingToken();
    }
}
