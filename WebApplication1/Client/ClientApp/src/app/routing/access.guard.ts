import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {AuthService} from '@prism/common';

@Injectable({providedIn: 'root'})
export class AccessGuard implements CanActivate {

  private ROUTES = {
    'plantasks/matrix-item-deleted': 'matrix_item_deleted_view',
    'workmatrix': 'workmatrix_view',
    'plantask-requests': 'plantask_requests_view',
    'priced-plantask-help': 'priced_plantask_view',
    'tasks/:groupCorrectionNumber': (route, state) => {
      return !this.authService.user.roles['plantask_min_access_role'];
    },
    'protocol-valuation': 'protool_valuation_view',
    'plantask-correction-stage': 'admin'
  };

  constructor(private authService: AuthService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot)
    : Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    // если в зачением в роут передали функцию-предикат - то выполняем ее и возвращаем ее значение
    const routeHandle: (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => boolean | string = this.ROUTES[route.routeConfig.path];
    return true;
    if (typeof routeHandle === 'function') {
      return (routeHandle as (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => boolean)(route, state);
    }
    // если описывается строка, то это имя разрешающей роли
    /*if (typeof routeHandle === 'string') {
      if (this.authService.user.roles[routeHandle]) {
        return true;
      }
    }*/
    // по умолчанию роут закрыт и пользователь будет разлогинен, для обновления токена
    this.authService.logout();
    return false;
  }
}
