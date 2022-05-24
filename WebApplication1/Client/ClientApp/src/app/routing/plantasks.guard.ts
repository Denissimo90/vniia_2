import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {UserService} from '@prism/common';

@Injectable({providedIn: 'root'})
export class plantasksGuard implements CanActivate {
  constructor(private user: UserService, private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot)
    : Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (this.user.roles.view_plantasks) {
      return true;
    }
    this.router.navigate(['/']);
    return false;
  }
}
