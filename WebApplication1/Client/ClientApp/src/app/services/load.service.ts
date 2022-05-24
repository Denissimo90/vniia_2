import {Injectable} from '@angular/core';
import {BaseService} from './base.service';
import {HttpClient} from '@angular/common/http';
import {AuthService, EventBusService, FileViewerService} from '@prism/common';
import {Competent} from '../domain/Competent';
import { Role } from '../domain/Role';
import { Team } from '../domain/Team';
import { Participant } from '../domain/Participant';


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
    const contentts = new Competent();
    contentts.id = 1;
    contentts.title = "Компет 1"

    let team = new Team();
    team.id = 1;
    team.name = "Team1";
    let team2 = new Team();
    team2.id = 2;
    team2.name = "Team2";

    const p1 = new Participant();
    p1.id = 1;
    p1.firstName = "111";
    const p2 = new Participant();
    p2.id = 2;
    p2.firstName = "222";

    contentts.teams = [team, team2];
    contentts.participants = [p1,p2];

    const contentts2 = new Competent();
    contentts2.id = 2;
    contentts2.title = "Компет 2"

    let team3 = new Team();
    team3.id = 3;
    team3.name = "Team3";
    let team4 = new Team();
    team4.id = 4;
    team4.name = "Team4";

    const p11 = new Participant();
    p11.id = 3;
    p11.firstName = "444";
    const p21 = new Participant();
    p21.id = 4;
    p21.firstName = "333";

    contentts2.teams = [team3, team4];
    contentts2.participants = [p11,p21];
    return [contentts, contentts2];
  }

  async searchRoles(): Promise<any> {
    return new Role();
  }

  async searchTeams(): Promise<any> {
    return new Team();
  }

  async searchParticipants(): Promise<any> {
    const p = new Participant();
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
