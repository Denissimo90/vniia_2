import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {AuthService, EventBusService} from '@prism/common';

@Injectable()
export class BaseService {

  public static httpOptions = {
    headers: new HttpHeaders({
      'Content-type': 'application/json'
    }),
  };

  public get httpOptions(): Object {
    return BaseService.httpOptions;
  }

  constructor(
    protected http: HttpClient,
    protected authService: AuthService,
    protected bus: EventBusService
  ) {
  }

  protected handleError<T>(error: any, operation = 'operation', result?: T) {
    // console.error(error);contracts.service.ts
    // console.log(`${operation} failed: ${error.message}`);

    switch (operation) {
      default:
        // In this case we can catch HTTPP errors by operation described on each of them like "getDocs" etc,
        // or by error.status but wey we catch in RequestInterceptorService
        break;
    }

    return result;
  }
}
