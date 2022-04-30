import {Injectable} from '@angular/core';
import {BaseService} from './BaseService';
import {ApplicationUser} from '../dto/ApplicationUser';

@Injectable()
export class ApplicationUserService extends BaseService {
  private url = this.baseUrl + '/users';

  public async createUser(user: ApplicationUser): Promise<ApplicationUser> {
    const result = await this.http.post<ApplicationUser>(this.url + '/insert-or-update', user).toPromise();
    console.log('user created');
    return result;
  }

  public async updateUser(user: ApplicationUser): Promise<ApplicationUser> {
    const result = await this.http.put<ApplicationUser>(this.url +  + '/insert-or-update', user).toPromise();
    console.log('user updated');
    return result;
  }

  public async deleteUser(id: number): Promise<void> {
    await this.http.delete(this.url + '/' + id).toPromise();
    console.log('user deleted');
  }

  public async getUser(id: number): Promise<ApplicationUser> {
    const result = await this.http.get<ApplicationUser>(this.url + '/' + id).toPromise();
    console.log('user fetched');
    return result;
  }

  public async getUsers(query): Promise<ApplicationUser[]> {
    const result = await this.http.post<ApplicationUser[]>(this.url + '/' + 'all', query).toPromise();
    console.log('users fetched');
    return result;
  }

  public async auth(user: ApplicationUser): Promise<ApplicationUser> {
    // const result = await this.http.post<ApplicationUser>(this.url + '/auth', user).toPromise();
    console.log('auth success');
    const newUser = new ApplicationUser();
    newUser.id = 1;
    newUser.firstName = '1';
    newUser.email = '';
    return /*result*/ newUser;
  }
}
