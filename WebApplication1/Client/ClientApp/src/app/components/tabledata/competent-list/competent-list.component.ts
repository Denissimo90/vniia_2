import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfigurationService, DetailCellRenderer, DialogService, LazyLoadEvent, UserService } from '@prism/common';
import { GridApi } from 'ag-grid-community';
import { MessageService } from 'primeng';
import { Competent } from '../../../domain/Competent';
import { LoadService } from '../../../services/load.service';
import { CompetentRegistrationComponent } from '../../forms/registration/competent-registration/competent-registration.component';

@Component({
  selector: 'app-competent-list',
  templateUrl: './competent-list.component.html',
  styleUrls: ['./competent-list.component.css']
})
export class CompetentListComponent implements OnInit {
  filter: boolean;
  selectedCompetents: Competent[] = [];
  nodes: Competent[] = [];
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

  DetailCellRenderer = DetailCellRenderer;

  leftovers: { [productId: number]: number };

  workshopsForCurrentItems = [];

  gridApi: GridApi;
  detailGridApi: GridApi;
  albumsOnly = false;

  @Input() tabHeader: String;

  @ViewChild('t') htmlTable;

  getRowId = (row: Competent) => row.id;

  constructor(
    public user: UserService,
    private loadService: LoadService,
    private dialogService: DialogService,
    private messageService: MessageService,
    private configService: ConfigurationService,
    private route: ActivatedRoute,
    private router: Router
  ) {
  }

  async ngOnInit() {
    await this.onLoad();
  }

  onCellClicked(event) {
    this.htmlTable.grid.api.copySelectedRowsToClipboard(false, [event.column.colId]);
  }

  async onLoad() {
    try {
      this.nodes = await this.loadService.searchCompetents();
      this.selectCompetentByUrl();
    } catch (e) {
      this.messageService.add({ severity: 'error', summary: 'Ошибка', detail: e.error?.message || 'Ошибка запроса' });
    } finally {
      this.isFirstLoad = false;
    }
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


  selectCompetentByUrl() {
    return;
    if (!this.isFirstLoad) {
      return;
    }
    // Выделение нужной строки если id в url
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      const node = this.htmlTable.grid.api.getRowNode(id);
      this.htmlTable.gridApi.ensureNodeVisible(node);
      if (node) {
        node.setSelected(true);
        this.htmlTable.gridApi.ensureNodeVisible(node, 'middle');
      } else {
        this.router.navigate(['/competents']);
      }
    }
  }

  async startCreateCompetent() {
    const dialog = this.dialogService.createDialog(CompetentRegistrationComponent);
    await dialog.init(null);
    dialog.result.subscribe((result) => {
      result.forEach(r => this.htmlTable.addRow(r));
    });
  }

  async startUpdateCompetent() {
    const dialog = this.dialogService.createDialog(CompetentRegistrationComponent);
    await dialog.init(this.selectedCompetents[0]);
    dialog.result.subscribe((competent): void => {
      this.htmlTable.updateRows(Array.isArray(competent) ? competent : [competent]);
      Object.assign(this.selectedCompetents[0], Array.isArray(competent) ? competent[0] : competent);
    });
  }

  async deleteCompetent() {
    this.blockingMask = true;
    try {
      // await this.loadService.deleteCompetents(this.selectedCompetents);
      this.htmlTable.removeRowsById(this.selectedCompetents.map(c => c.id), 'Заявка удалена');
      this.selectedCompetents = [];
      this.messageService.add({
        severity: 'success', summary: 'Выполнено',
        detail: this.selectedCompetents.length > 1 ? 'Заявки удалены' : 'Заявка удалена'
      });
    } finally {
      this.blockingMask = false;
    }
  }

}
