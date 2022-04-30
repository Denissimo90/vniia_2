import {Injectable} from '@angular/core';
import {BaseService} from './BaseService';
import {Manufacture} from '../dto/Manufacture';

@Injectable()
export class ManufactureService extends BaseService {
  private url = this.baseUrl + '/manufactures';

  public async createManufacture(user: Manufacture): Promise<Manufacture> {
    const result = await this.http.post<Manufacture>(this.url, user).toPromise();
    console.log('user created');
    return result;
  }

  public async deleteManufacture(id: number): Promise<void> {
    await this.http.delete(this.url + '/' + id).toPromise();
    console.log('user deleted');
  }

  public async getManufactures(): Promise<Manufacture[]> {
    const result = await this.http.post<Manufacture[]>(this.url + '/' + 'all', null).toPromise();
    console.log('users fetched');
    return result;
  }
}
