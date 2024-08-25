import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AuthUtils } from 'app/core/auth/auth.utils';
import { UserService } from 'app/core/user/user.service';
import { HttpResponse } from 'app/shared/interfaces';
import { environment } from 'environments/environment';
import { catchError, Observable, of, switchMap, throwError } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private _authenticated: boolean = false;
    private _httpClient = inject(HttpClient);
    private _userService = inject(UserService);

    constructor() {
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Setter & getter for access token
     */

    set accessToken(token: string) {
        try {
            localStorage.setItem('accessToken', token);
            // Verificar si el token se guardó correctamente
            if (localStorage.getItem('accessToken') !== token) {
                // Manejar el error
                console.error('Error al guardar el token de acceso en el almacenamiento local.');
            } else {
                // Comprobar si el token está disponible en el almacenamiento local
                let attempts = 0;
                const checkToken = setInterval(() => {
                    attempts++;
                    if (localStorage.getItem('accessToken') === token || attempts > 5) {
                        clearInterval(checkToken);
                        if (attempts > 5) {
                            console.error('El token de acceso no se guardó en el almacenamiento local después de 5 intentos.');
                        }
                    }
                }, 1000);
            }
        } catch (error) {
            console.error('No se pudo guardar el token en el almacenamiento local:', error);
        }
    }

    get accessToken(): string {
        let accessToken: string = localStorage.getItem('accessToken');
        if (accessToken && accessToken.trim() !== '') {
            return accessToken;
        }

        return null;
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
    signIn(credentials: { email: string; password: string }): Observable<any> {
        // Throw error, if the user is already logged in
        if (this._authenticated) {
            return throwError(() => new Error('User is already logged in.'));
        }

        return this._httpClient
            .post<HttpResponse<any>>(
                `${environment.baseURL}/auth/login`,
                credentials
            )
            .pipe(
                switchMap((response) => {
                    if (response.succeeded) {
                        console.log(response);
                        // Set the authenticated flag to true
                        this._authenticated = true;

                        // Store the access token in the local storage
                        this.accessToken = response.data?.accessToken;

                        // Store the user on the user service
                        this._userService.user = response.data?.user;

                        // Return a new observable with the response
                        return of(response.data);
                    }
                })
            );
    }

    /**
     * Sign in using the access token
     */
    signInUsingToken(): Observable<any> {
        // Sign in using the token
        return this._httpClient
            .get(`${environment.baseURL}/auth/profile`, {
                headers: {
                    Authorization: `Bearer ${this.accessToken}`,
                },
            })
            .pipe(
                catchError(() =>
                    // Return false
                    of(false)
                ),
                switchMap((response: any) => {
                    if (response.succeeded) {
                        this.accessToken = response.data?.accessToken;

                        // Set the authenticated flag to true
                        this._authenticated = true;

                        // Store the user on the user service
                        this._userService.user = response.data?.user;

                        // Return true
                        return of(true);
                    }
                })
            );
    }

    /**
     * Sign out
     */
    signOut(): Observable<boolean> {
        return this._httpClient
            .post<HttpResponse<any>>(`${environment.baseURL}/auth/signOut`, {})
            .pipe(
                switchMap((response) => {
                    if (response.succeeded) {
                        // Remove the access token from the local storage
                        localStorage.removeItem('accessToken');

                        // Set the authenticated flag to false
                        this._authenticated = false;

                        // Return the observable
                        return of(response.data);
                    }
                })
            );
    }


    cerrarSesion() {
        localStorage.removeItem('accessToken');

        // Set the authenticated flag to false
        this._authenticated = false;
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
        // Check if the user is logged in
        if (this._authenticated) {
            return of(true);
        }

        // Check the access token availability
        if (!this.accessToken) {
            return of(false);
        }

        // Check the access token expire date
        if (AuthUtils.isTokenExpired(this.accessToken)) {
            return of(false);
        }

        // If the access token exists, and it didn't expire, sign in using it
        return this.signInUsingToken();
    }
}
