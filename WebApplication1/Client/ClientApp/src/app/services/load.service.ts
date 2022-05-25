import {Injectable} from '@angular/core';
import {BaseService} from './base.service';
import {HttpClient} from '@angular/common/http';
import {AuthService, EventBusService, FileViewerService} from '@prism/common';
import {Competent} from '../domain/Competent';
import { Role } from '../domain/Role';
import { Team } from '../domain/Team';
import { Participant } from '../domain/Participant';
import { Workplace } from '../domain/Workplace';


@Injectable({
  providedIn: 'root'
})
export class LoadService extends BaseService {
  private restPath = '/dto';

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

  async searchCompetents(): Promise<any> {
    const result = [];
    for (let i = 1; i < 100; i++) {
      const competent = new Competent();
      competent.id = i;
      competent.title = 'Titile ' + i;
      competent.teams = [];
      competent.participants = [];

      for (let j = 1; j < Math.random() * 50 + 1; j++) {
        const team = new Team();
        team.id = j * i;
        team.name = 'Name ' + team.id;
        competent.teams.push(team);
      }

      for (let j = 1; j < Math.random() * 50 + 1; j++) {
        const participant = new Participant();
        participant.id = (j * i).toString();
        participant.firstName = 'FirstName ' + participant.id;
        participant.middleName = 'SecondName ' + participant.id;
        participant.lastName = 'ThirdName ' + participant.id;
        competent.participants.push(participant);
      }
      result.push(competent);
    }

    return result;
  }

  async searchRoles(): Promise<any> {
    return new Role();
  }

  async searchTeams(): Promise<any> {
    return new Team();
  }

  async searchParticipants(): Promise<any> {
    const p = new Participant();
    p.id = "1"; 
    return [p];
  }

  async searchWorkplaces(): Promise<any> {
    const p = new Workplace();
    p.id = 1;
    return [p];
  }

  async updateOrCreatePlantaskContracts(id: number, count: number): Promise<any> {
    const contract = await this.http.post<any>(
      `/${this.restPath}/plantask/${id}/updateOrCreate/${count}`, {})
      .toPromise();
    return contract;
  }
}
