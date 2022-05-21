import {Injectable} from '@angular/core';
import {ApplicationUser} from '../dto/ApplicationUser';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private _user: ApplicationUser;

  get user(): ApplicationUser {
    return this._user;
  }

  set user(value: ApplicationUser) {
    this._user = value;
  }

  constructor() {
  }
}
