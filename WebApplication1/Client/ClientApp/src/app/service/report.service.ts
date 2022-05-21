import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BaseService} from './BaseService';
import {ReportEntity} from '../dto/ReportEntity';
import {ApplicationUser} from '../dto/ApplicationUser';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Injectable()
export class ReportService extends BaseService {

  private url = this.baseUrl + '/report';

  constructor(
    protected httpClient: HttpClient) {
     super(httpClient);
  }

  async createPersonalIncomeTaxReport() {
    /*const file = await this.http.get(`/${this.restPath}/createPersonalIncomeTaxReport`, {
      observe: 'response',
      responseType: 'blob',
      params: new HttpParams()
        .append('login', login)
        .append('password', password)
        .append('year', year.toString())
        .append('claimIds', claims.map(c => c.id).toString())
    }).toPromise();

    this.fileViewerService.showOrSaveFile(file, ServeFileType.SAVE);*/
  }

  async UpdateReportValues() {
    await this.http.get<any>(this.url).toPromise();
  }

  async getYearReport(date: Date): Promise<ReportEntity[]> {
    const result = await this.http.post<ReportEntity[]>(this.url + '/' + 'year', null).toPromise();
    console.log('users fetched');
    return result;
  }

}
