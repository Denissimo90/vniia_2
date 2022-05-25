import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfigurationService, DialogService, UserService } from '@prism/common';
import { MessageService } from 'primeng';
import { LoadService } from '../../../services/load.service';
import { Competent } from '../../../domain/Competent';
import { GroupWorkplace } from '../../../domain/GroupWorkplace';
import { GroupWorkplaceRegistrationComponent } from '../../forms/registration/group-workplace-registration/group-workplace-registration.component';
import { GridApi } from 'ag-grid-community';


@Component({
  selector: 'app-group-workplace-list',
  templateUrl: './group-workplace-list.component.html',
  styleUrls: ['./group-workplace-list.component.css']
})
export class GroupWorkplaceListComponent implements OnInit {

  filter: boolean;
  selectedGroupWorkplaces: GroupWorkplace[] = [];
  nodes: GroupWorkplace[] = [];
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

  
  async startCreateGroupWorkplace() {
    const dialog = this.dialogService.createDialog(GroupWorkplaceRegistrationComponent);
    await dialog.init(null);
    dialog.result.subscribe((result) => {
      result.forEach(r => this.htmlTable.addRow(r));
    });
  }

  async startUpdateGroupWorkplace() {
    const dialog = this.dialogService.createDialog(GroupWorkplaceRegistrationComponent);
    await dialog.init(this.selectedGroupWorkplaces[0]);
    dialog.result.subscribe((GroupWorkplace) => {
      this.htmlTable.updateRows(Array.isArray(GroupWorkplace) ? GroupWorkplace : [GroupWorkplace]);
      Object.assign(this.selectedGroupWorkplaces[0], Array.isArray(GroupWorkplace) ? GroupWorkplace[0] : GroupWorkplace);
    });
  }

  async deleteGroupWorkplace() {
    this.blockingMask = true;
    try {
      //await this.loadService.deleteGroupWorkplaces(this.selectedGroupWorkplaces);
      this.htmlTable.removeRowsById(this.selectedGroupWorkplaces.map(c => c.id), 'Группа удалена');
      this.selectedGroupWorkplaces = [];
      this.messageService.add({
        severity: 'success', summary: 'Выполнено',
        detail: 'Группа удалена'
      });
    } finally {
      this.blockingMask = false;
    }
  }

}
