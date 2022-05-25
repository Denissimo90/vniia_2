import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfigurationService, DialogService, UserService } from '@prism/common';
import { MessageService } from 'primeng';
import { LoadService } from '../../../services/load.service';
import { Competent } from '../../../domain/Competent';
import { Team } from '../../../domain/Team';
import { TeamRegistrationComponent } from '../../forms/registration/team-registration/team-registration.component';
import { GridApi, ValueGetterParams } from 'ag-grid-community';

@Component({
  selector: 'app-competention-team-list',
  templateUrl: './competention-team-list.component.html',
  styleUrls: ['./competention-team-list.component.css']
})
export class CompetentionTeamListComponent implements OnInit {
  filter: boolean;
  selectedTeams: Team[] = [];
  nodes: Team[] = [];
  selectedCompetent;
  componentModes = [];
  competents: Competent[] = [];
  blockingMask = false;
  timeouts = {};
  isOnlyActual = false;
  isFirstLoad = true;
  title: string;
  total = 0;
  totalPositions = 0;
  pinnedRow = [];
  summaryItemsMap = new Map<string, boolean>();

  treeMode: false;
  deltaRowDataMode = true;
  loading = false;

  workshopsForCurrentItems = [];

  gridApi: GridApi;
  detailGridApi: GridApi;
  albumsOnly = false;

  @Input() tabHeader: String;

  @ViewChild('t') htmlTable;

  getRowId = (row) => '' + row.id;
  getItemId = (row: Competent) => row.id;

  constructor(
    public user: UserService,
    private loadService: LoadService,
    private dialogService: DialogService,
    private messageService: MessageService,
    private configService: ConfigurationService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.onLoad();
  }

  async onLoad() {
    try {
      this.competents = await this.loadService.searchCompetents();
      this.componentModes = [];
      this.competents.forEach(element => {
        this.componentModes.push({ label: element.title, value: element});
      });
      if (this.competents.length > 0)
      {
        this.selectedCompetent = this.competents[0];

      this.nodes = await this.selectedCompetent.teams;
      console.log(this.nodes);
      }
    } catch (e) {
      this.messageService.add({ severity: 'error', summary: 'Ошибка', detail: e.error?.message || 'Ошибка запроса' });
    } finally {
      this.isFirstLoad = false;
    }
  }


async onComponentModeChange() {  
  }

  onFilterChanged(event: any) {
    this.totalPositions = this.gridApi.getDisplayedRowCount();
    const filteredRows = [];
    this.htmlTable.gridApi.forEachNodeAfterFilter(node => filteredRows.push(node.data));
  }
  
  onRowSelected(event) {
  }

  onGridReady(e) {
    this.gridApi = e;
        setInterval(() => {
          this.gridApi.refreshCells({force: true});
        }, 1000);
  }


  async onDetailInit(event: any) {
    this.detailGridApi = event.gridApi;
  }

  
  async startCreateTeam() {
    const dialog = this.dialogService.createDialog(TeamRegistrationComponent);
    await dialog.init(null);
    dialog.result.subscribe((result) => {
      result.forEach(r => this.htmlTable.addRow(r));
    });
  }

  async startUpdateTeam() {
    const dialog = this.dialogService.createDialog(TeamRegistrationComponent);
    await dialog.init(this.selectedTeams[0]);
    dialog.result.subscribe((Team) => {
      this.htmlTable.updateRows(Array.isArray(Team) ? Team : [Team]);
      Object.assign(this.selectedTeams[0], Array.isArray(Team) ? Team[0] : Team);
    });
  }

  async deleteTeam() {
    this.blockingMask = true;
    try {
      //await this.loadService.deleteTeams(this.selectedTeams);
      this.htmlTable.removeRowsById(this.selectedTeams.map(c => c.id), 'Участник удален');
      this.selectedTeams = [];
      this.messageService.add({
        severity: 'success', summary: 'Выполнено',
        detail: this.selectedTeams.length > 1 ? 'Участники удалены' : 'Участник удален'
      });
    } finally {
      this.blockingMask = false;
    }
  }

    onGetTimerStyle(event: any) {
        if (event.data ) {
          const hours = new Date(new Date().getTime() - event.data.time?.getTime() || this.generateDate()).getHours();
          if (hours > 6) {
            event.style = { 'background': 'rgba(255,0,0,0.28) !important' };
          } else {
            event.style = { 'background': 'rgba(0,255,0,0.28) !important' };
          }
    
        }
      }

      toTwo(s: string): string {
          return s.length < 2 ? ('0' + s) : s;
        }
      
        getTimer = (params: ValueGetterParams) => {
          const date = new Date((new Date().getTime() - params.data.time?.getTime() || this.generateDate()));
          const hours = this.toTwo(date.getHours().toString());
          const minutes = this.toTwo(date.getMinutes().toString());
          const seconds = this.toTwo(date.getSeconds().toString());
          return hours + ':' + minutes + ':' + seconds;
      
        }

        generateDate(){
          const time = new Date();
      time.setHours(time.getHours() - Math.random() * 5 + 1);
      time.setMinutes(time.getHours() - Math.random() * 60 + 1);
      return time;
        }

}
