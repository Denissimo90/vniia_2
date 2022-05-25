import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfigurationService, DialogService, UserService } from '@prism/common';
import { MessageService } from 'primeng';
import { LoadService } from '../../../services/load.service';
import { Competent } from '../../../domain/Competent';
import { Team } from '../../../domain/Team';
import { TeamRegistrationComponent } from '../../forms/registration/team-registration/team-registration.component';
import { GridApi } from 'ag-grid-community';

@Component({
  selector: 'app-team-list',
  templateUrl: './team-list.component.html',
  styleUrls: ['./team-list.component.css']
})
export class TeamListComponent implements OnInit {
  filter: boolean;
  selectedTeams: Team[] = [];
  nodes: Team[] = [];
  selectedCompetent: Competent[] = [];
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
    this.nodes = [];
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

}
