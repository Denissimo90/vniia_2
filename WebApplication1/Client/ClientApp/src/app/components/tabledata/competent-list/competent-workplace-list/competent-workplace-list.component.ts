import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { ConfigurationService, DialogService, Table2Component } from '@prism/common';
import { GridApi } from 'ag-grid-community';
import { MessageService } from 'primeng';
import { LoadService } from '../../../../services/load.service';
import { Workplace } from '../../../../domain/Workplace';
import { WorkplaceSelectorComponent } from '../../../../components/forms/workplace-selector/workplace-selector.component';

@Component({
  selector: 'app-competent-workplace-list',
  templateUrl: './competent-workplace-list.component.html',
  styleUrls: ['./competent-workplace-list.component.css']
})
export class CompetentWorkplaceListComponent implements OnInit {

  loading = false;
  title: string;
  filter = false;
  total = 0;
  gridApi: GridApi;

  items: Workplace[];
  @Input() data: any;
  @Input() isGridDetail = true;
  @ViewChild('t') htmlTable: Table2Component;
  @Output() onInit = new EventEmitter<any>();

  getRowId = (row: Workplace) => row.id;

  constructor(
    private dialogService: DialogService,
    private messageService: MessageService,
    private configService: ConfigurationService,
    private loadService: LoadService) { }
  
  async ngOnInit() {
  }

  async update() {
    if (this.data) {
      setTimeout(async () =>{
        await this.loadItems();
      }, 50);
  } else {
    this.items = [];
  }
  }

  async ngOnChanges(changes: SimpleChanges) {
    if (!!changes?.data) {
      await this.update();
    }
  }

  async loadItems() {
    if (! this.data?.id) {
      return;
    }

    try {
      this.loading = true;
      this.items = await this.data.workplaces;
      // (this.data.workshopPlanId);
    } catch (e) {
      this.messageService.add({severity: 'error', summary: 'Ошибка', detail: e.error?.message || 'Ошибка запроса'});
    } finally {
      this.loading = false;
    }
  }
  
  async addWorkplace() {
    const dialog = this.dialogService.createDialog(WorkplaceSelectorComponent);
    await dialog.init();
    dialog.result.subscribe((result) => {
      result.forEach(r => this.htmlTable.addRow(r));
    });
  }

  async deleteWorkplace() {
    //this.blockingMask = true;
    try {
      //await this.loadService.deleteWorkplaces(this.selectedWorkplaces);
      this.htmlTable.removeRowsById(this.items.map(c => c.id), 'Рабочее место удалено');
      this.items = [];
      this.messageService.add({
        severity: 'success', summary: 'Выполнено',
        detail: this.items.length > 1 ? 'Рабочие места удалены' : 'Рабочее место удалено'
      });
    } finally {
      //this.blockingMask = false;
    }
  }

}
