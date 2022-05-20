import {Injectable} from '@angular/core';
import {BaseService} from './base.service';
import {HttpClient} from '@angular/common/http';
import {AuthService, EventBusService, FileViewerService} from '@prism/common';


@Injectable({
  providedIn: 'root'
})
export class TestService extends BaseService {
  private restPath = '/dialog';

  constructor(
    protected httpClient: HttpClient,
    protected authService: AuthService,
    protected bus: EventBusService,
    protected fileViewerService: FileViewerService) {
    super(httpClient, authService, bus);
  }

  async getTestConfig(): Promise<any> {
    return await this.http.get<any>(`/${this.restPath}/oauth/weblogin?response_type=code&client_id=1&redirect_uri=https%3A%2F%2Fsample.com%2Fauth&state=123abc`, this.httpOptions)
      .toPromise();
  }

  async updateOrCreatePlantaskContracts(id: number, count: number): Promise<any> {
    const contract = await this.http.post<any>(
      `/${this.restPath}/plantask/${id}/updateOrCreate/${count}`, {})
      .toPromise();
    return contract;
  }
}
