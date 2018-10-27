import { HttpInterceptor, HttpEvent, HttpRequest, HttpHandler } from "../../../../node_modules/@angular/common/http";
import { Observable } from "../../../../node_modules/rxjs";
import { Injectable } from "../../../../node_modules/@angular/core";
import { UserService } from "../services/user.service";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

    constructor(
        private readonly userService: UserService
    ) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        var token = this.userService.accessToken;
        if (token) {
            req = req.clone({
                setHeaders: {
                    Authorization: `Bearer ${token}`
                }
            });
        }
        return next.handle(req);
    }
}