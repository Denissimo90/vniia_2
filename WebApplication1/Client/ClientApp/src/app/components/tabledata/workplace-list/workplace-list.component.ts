import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfigurationService, DialogService, UserService } from '@prism/common';
import { MessageService } from 'primeng';
import { Workplace } from '../../../domain/Workplace';
import { LoadService } from '../../../services/load.service';
import { WorkplaceRegistrationComponent } from '../../forms/registration/workplace-registration/workplace-registration.component';

@Component({
  selector: 'app-workplace-list',
  templateUrl: './workplace-list.component.html',
  styleUrls: ['./workplace-list.component.css']
})
export class WorkplaceListComponent implements OnInit {
  filter: boolean;
  selectedWorkplaces: Workplace[] = [];
  workPlaces: Workplace[] = [];
  blockingMask = false;
  timeouts = {};
  isOnlyActual = false;
  isFirstLoad = true;

  @ViewChild('t') htmlTable;

  getRowId = (row) => '' + row.id;

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
      this.workPlaces = await this.loadService.searchWorkplaces();
      this.selectWorkplaceByUrl();
    } catch (e) {
      this.messageService.add({ severity: 'error', summary: 'Ошибка', detail: e.error?.message || 'Ошибка запроса' });
    } finally {
      this.isFirstLoad = false;
    }
  }

  selectWorkplaceByUrl() {
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
        this.router.navigate(['/workplaces']);
      }
    }
  }

  async startCreateWorkplace() {
    const dialog = this.dialogService.createDialog(WorkplaceRegistrationComponent);
    await dialog.init(null);
    dialog.result.subscribe((result) => {
      result.forEach(r => this.htmlTable.addRow(r));
    });
  }

  async startUpdateWorkplace() {
    const dialog = this.dialogService.createDialog(WorkplaceRegistrationComponent);
    await dialog.init(this.selectedWorkplaces[0]);
    dialog.result.subscribe((Workplace) => {
      this.htmlTable.updateRows(Array.isArray(Workplace) ? Workplace : [Workplace]);
      Object.assign(this.selectedWorkplaces[0], Array.isArray(Workplace) ? Workplace[0] : Workplace);
    });
  }

  async deleteWorkplace() {
    this.blockingMask = true;
    try {
      //await this.loadService.deleteWorkplaces(this.selectedWorkplaces);
      this.htmlTable.removeRowsById(this.selectedWorkplaces.map(c => c.id), 'Рабочее место удалено');
      this.selectedWorkplaces = [];
      this.messageService.add({
        severity: 'success', summary: 'Выполнено',
        detail: this.selectedWorkplaces.length > 1 ? 'Рабочие места удалены' : 'Рабочее место удален'
      });
    } finally {
      this.blockingMask = false;
    }
  }

}
