import { CanActivate, Router } from "../../../../node_modules/@angular/router";
import { UserService } from "../services/user.service";
import { Injectable } from "../../../../node_modules/@angular/core";

@Injectable({
    providedIn: 'root'
})
export class IsLoggedGuard implements CanActivate {
    constructor(
        private userService: UserService,
        private router: Router
    ) { }
    canActivate(): boolean {
        if (!this.userService.isLoggedIn) {
            this.router.navigate(['login']);
            return false;
        }
        return true;
    }
}