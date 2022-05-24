import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfigurationService, DialogService, LazyLoadEvent, UserService } from '@prism/common';
import { MessageService } from 'primeng';
import { Role } from '../../../domain/Role';
import { LoadService } from '../../../services/load.service';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.css']
})
export class RoleListComponent implements OnInit {
  filter: boolean;
  selectedRoles: Role[] = [];
  roles: Role[] = [];
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
      this.roles = await this.loadService.searchRoles();
      this.selectRoleByUrl();
    } catch (e) {
      this.messageService.add({ severity: 'error', summary: 'Ошибка', detail: e.error?.message || 'Ошибка запроса' });
    } finally {
      this.isFirstLoad = false;
    }
  }

  selectRoleByUrl() {
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
        this.router.navigate(['/roles']);
      }
    }
  }
/*
  async startCreateRole() {
    const dialog = this.dialogService.createDialog(RoleRegistrationComponent);
    await dialog.init(null);
    dialog.result.subscribe((result) => {
      result.forEach(r => this.htmlTable.addRow(r));
    });
  }

  async startUpdateRole() {
    const dialog = this.dialogService.createDialog(RoleRegistrationComponent);
    await dialog.init(this.selectedRoles[0]);
    dialog.result.subscribe((Role) => {
      this.htmlTable.updateRows(Array.isArray(Role) ? Role : [Role]);
      Object.assign(this.selectedRoles[0], Array.isArray(Role) ? Role[0] : Role);
    });
  }

  async deleteRole() {
    this.blockingMask = true;
    try {
      //await this.loadService.deleteRoles(this.selectedRoles);
      this.htmlTable.removeRowsById(this.selectedRoles.map(c => c.id), 'Заявка удалена');
      this.selectedRoles = [];
      this.messageService.add({
        severity: 'success', summary: 'Выполнено',
        detail: this.selectedRoles.length > 1 ? 'Заявки удалены' : 'Заявка удалена'
      });
    } finally {
      this.blockingMask = false;
    }
  }*/

}
