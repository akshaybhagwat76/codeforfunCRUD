import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AccountService } from '../services/account.service';

@Injectable()
export class AuthGuard implements CanActivate {
    isAccess;
    role;
    constructor(private authService: AccountService, private router: Router) {
    }

    canActivate(route: ActivatedRouteSnapshot): any {
        this.role = route.data.role;
        this.authService.checkRole(this.role).subscribe(x => {
            this.isAccess = x;
            if (!this.isAccess) {
                this.router.navigate(['']);
            }
        })
        return true;

    }

}
