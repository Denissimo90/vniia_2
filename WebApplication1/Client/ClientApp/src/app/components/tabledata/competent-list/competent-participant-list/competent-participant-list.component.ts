import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { ConfigurationService, DialogService, Table2Component } from '@prism/common';
import { GridApi } from 'ag-grid-community';
import { MessageService } from 'primeng';
import { LoadService } from '../../../../services/load.service';
import { Participant } from '../../../../domain/Participant';
import { ParticipantSelectorComponent } from '../../../../components/forms/participant-selector/participant-selector.component';

@Component({
  selector: 'app-competent-participant-list',
  templateUrl: './competent-participant-list.component.html',
  styleUrls: ['./competent-participant-list.component.css']
})
export class CompetentParticipantListComponent implements OnInit {

  loading = false;
  title: string;
  filter = false;
  total = 0;
  gridApi: GridApi;

  items: Participant[];
  @Input() data: any;
  @Input() isGridDetail = true;
  @ViewChild('t') htmlTable: Table2Component;
  @Output() onInit = new EventEmitter<any>();

  getRowId = (row: Participant) => row.id;

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
      this.items = await this.data.participants;
      // (this.data.workshopPlanId);
    } catch (e) {
      this.messageService.add({severity: 'error', summary: 'Ошибка', detail: e.error?.message || 'Ошибка запроса'});
    } finally {
      this.loading = false;
    }
  }
  
  async addParticipant() {
    const dialog = this.dialogService.createDialog(ParticipantSelectorComponent);
    await dialog.init();
    dialog.result.subscribe((result) => {
      result.forEach(r => this.htmlTable.addRow(r));
    });
  }


  async deleteParticipant() {
    //this.blockingMask = true;
    try {
      //await this.loadService.deleteParticipants(this.selectedParticipants);
      this.htmlTable.removeRowsById(this.items.map(c => c.id), 'Участник удален');
      this.items = [];
      this.messageService.add({
        severity: 'success', summary: 'Выполнено',
        detail: this.items.length > 1 ? 'Участники удалены' : 'Участник удален'
      });
    } finally {
      //this.blockingMask = false;
    }
  }

}
