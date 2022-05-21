import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AppConfig} from '../app.config';

@Injectable()
export class BaseService {
  // protected apiServer = AppConfig.settings.serviceConnection.value;
  // Путь к контроллерам на сервере
  public baseUrl = 'https://localhost:5001';

  constructor(protected http: HttpClient) {
  }
}
