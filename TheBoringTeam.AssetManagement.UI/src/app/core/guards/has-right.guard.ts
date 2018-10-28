import { CanActivate, Router, ActivatedRoute, ActivatedRouteSnapshot } from "../../../../node_modules/@angular/router";
import { UserService } from "../services/user.service";
import { Injectable } from "../../../../node_modules/@angular/core";

@Injectable({
    providedIn: 'root'
})
export class HasRightGuard implements CanActivate {
    constructor(
        private userService: UserService,
        private router: Router
    ) { }
    canActivate(route: ActivatedRouteSnapshot): boolean {
        const right = route.data["right"];

        if (!this.userService.user || !this.userService.user.role.rights.filter(f => f.name === right).length) {
            this.router.navigate(['error'], { queryParams: { code: 2 } });
            return false;
        }
        return true;
    }
}