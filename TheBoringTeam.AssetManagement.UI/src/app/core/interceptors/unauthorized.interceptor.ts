import { Router } from "../../../../node_modules/@angular/router";
import { HttpInterceptor, HttpErrorResponse, HttpRequest, HttpHandler, HttpEvent } from "../../../../node_modules/@angular/common/http";
import { Observable } from "../../../../node_modules/rxjs";
import { Injectable } from "../../../../node_modules/@angular/core";
import { catchError } from "../../../../node_modules/rxjs/operators";

@Injectable()
export class UnauthorizedInterceptor implements HttpInterceptor {
    constructor(private router: Router) { }

    private handleAuthError(err: HttpErrorResponse): Observable<any> {
        if (err.status === 401 || err.status === 403) {
            this.router.navigateByUrl(`/error?code=1`);
            throw err;
        }
        throw err;
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError((err) => this.handleAuthError(err))
        );
    }
}