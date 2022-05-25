import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { ConfigurationService, DialogService, Table2Component } from '@prism/common';
import { GridApi } from 'ag-grid-community';
import { MessageService } from 'primeng';
import { LoadService } from '../../../../services/load.service';
import { Participant } from '../../../../domain/Participant';

@Component({
  selector: 'app-team-participant-list',
  templateUrl: './team-participant-list.component.html',
  styleUrls: ['./team-participant-list.component.css']
})
export class TeamParticipantListComponent implements OnInit {

  loading = false;
  title: string;
  filter = false;
  total = 0;
  gridApi: GridApi;

  items: Participant[];
  @Input() data: any;
  @Input() isGridDetail = true;
  @ViewChild('t') htmlTable: Table2Component;


  getRowId = (row: Participant) => row.id;
  @Output() onInit = new EventEmitter<any>();

  constructor(
    private dialogService: DialogService,
    private messageService: MessageService,
    private configService: ConfigurationService,
    private loadService: LoadService) { }

  ngOnInit() {
    console.log('test');
    this.update();
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

  async loadItems() {
    if (! this.data?.id) {
      return;
    }

    try {
      this.loading = true;
      this.items = this.deepCopy(this.data.participants);
      // (this.data.workshopPlanId);
    } catch (e) {
      this.messageService.add({severity: 'error', summary: 'Ошибка', detail: e.error?.message || 'Ошибка запроса'});
    } finally {
      this.loading = false;
    }
  }

  onGridReady(event:any){
    this.gridApi = event;
    this.onInit.emit(event);
  }

  deepCopy(obj): any {
    let copy;

    // Handle the 3 simple types, and null or undefined
    if (null === obj || 'object' !== typeof obj) {
      return obj;
    }

    // Handle Date
    if (obj instanceof Date) {
        copy = new Date();
        copy.setTime(obj.getTime());
        return copy;
    }

    // Handle Array
    if (obj instanceof Array) {
        copy = [];
        for (let i = 0, len = obj.length; i < len; i++) {
            copy[i] = this.deepCopy(obj[i]);
        }
        return copy;
    }

    // Handle Object
    if (obj instanceof Object) {
        copy = {};
        for (const attr in obj) {
            if (obj.hasOwnProperty(attr)) {
              copy[attr] = this.deepCopy(obj[attr]);
            }
        }
        return copy;
    }

    throw new Error('Unable to copy obj! Its type isnt supported.');
}
/*
async addParticipant() {
  const dialog = this.dialogService.createDialog(ParticipantSelectorComponent);
  await dialog.init();
  dialog.result.subscribe((result) => {
    result.forEach(r => this.htmlTable.addRow(r));
  });
}
*/
async deleteParticipant() {
  //this.blockingMask = true;
  try {
    //await this.loadService.deleteParticipants(this.selectedParticipants);
    this.htmlTable.removeRowsById(this.items.map(c => c.id), 'Участник удален');
    this.messageService.add({
      severity: 'success', summary: 'Выполнено',
      detail: 'Участник удален'
    });
  } finally {
    //this.blockingMask = false;
  }
}

}
