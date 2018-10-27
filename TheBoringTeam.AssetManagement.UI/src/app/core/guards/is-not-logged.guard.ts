import { CanActivate } from "../../../../node_modules/@angular/router";
import { UserService } from "../services/user.service";
import { Injectable } from "../../../../node_modules/@angular/core";

@Injectable({
    providedIn: 'root'
})
export class IsNotLoggedGuard implements CanActivate {
    constructor(
        private userService: UserService
    ) { }
    canActivate(): boolean {
        if (this.userService.isLoggedIn) {
            return false;
        }
        return true;
    }
}