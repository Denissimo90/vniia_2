import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfigurationService, DialogService, LazyLoadEvent, UserService } from '@prism/common';
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
  competents: Competent[] = [];
  blockingMask = false;
  timeouts = {};
  alfaUrl = this.configService.config['alfaApi'];
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
      this.competents = await this.loadService.searchCompetents();
      this.selectCompetentByUrl();
    } catch (e) {
      this.messageService.add({ severity: 'error', summary: 'Ошибка', detail: e.error?.message || 'Ошибка запроса' });
    } finally {
      this.isFirstLoad = false;
    }
  }

  selectCompetentByUrl() {
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
    dialog.result.subscribe((Competent) => {
      this.htmlTable.updateRows(Array.isArray(Competent) ? Competent : [Competent]);
      Object.assign(this.selectedCompetents[0], Array.isArray(Competent) ? Competent[0] : Competent);
    });
  }

  async deleteCompetent() {
    this.blockingMask = true;
    try {
      //await this.loadService.deleteCompetents(this.selectedCompetents);
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
