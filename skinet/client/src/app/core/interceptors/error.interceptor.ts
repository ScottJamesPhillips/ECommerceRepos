import {HttpInterceptor, HttpRequest, HttpHandler, HttpEvent} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Router, NavigationExtras } from '@angular/router';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

// Must be injectabel to be usable
@Injectable()

export class ErrorInterceptor implements HttpInterceptor {
    constructor(private router: Router, private toastr: ToastrService){

    }

    // intercept HttpRequest(outgoing) and HttpHandler(incoming/response) - aim to catch response errors and handle
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError(error => {
                if (error){
                    if (error.status === 400){
                        if (error.error.errors)
                        {
                            throw error.error;
                        }else{
                            this.toastr.error(error.error.message, error.error.status);
                        }
                    }
                    if (error.status === 401){
                        this.toastr.error(error.error.message, error.error.status);
                    }
                    if (error.status === 404){
                        this.router.navigateByUrl('/not-found');
                    }
                    if (error.status === 404){
                        this.router.navigateByUrl('/not-found');
                    }
                    if (error.status === 500){
                        //121
                        const navigationExtras: NavigationExtras = {state: {error: error.error}};
                        this.router.navigateByUrl('/server-error', navigationExtras);
                    }
                }
                return throwError(error);
            })
        );
    }
}
