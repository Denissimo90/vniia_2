
import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { ConfigurationService, DialogService, Table2Component } from '@prism/common';
import { GridApi, ValueGetterParams } from 'ag-grid-community';
import { MessageService } from 'primeng';
import { LoadService } from '../../../../services/load.service';
import { Participant } from '../../../../domain/Participant';
import { TeamRegistrationComponent } from '../../../forms/registration/team-registration/team-registration.component';



@Component({
  selector: 'app-competention-team-detail-list',
  templateUrl: './competention-team-detail-list.component.html',
  styleUrls: ['./competention-team-detail-list.component.css']
})
export class CompetentionTeamDetailListComponent implements OnInit {
  
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

  
  async play(){

  }

  async pause(){

  }


  toTwo(s: string): string {
      return s.length < 2 ? ('0' + s) : s;
    }
  
    getTimer = (params: ValueGetterParams) => {
      const date = new Date((new Date().getTime() - params.data.time?.getTime() || 0));
      const hours = this.toTwo(date.getHours().toString());
      const minutes = this.toTwo(date.getMinutes().toString());
      const seconds = this.toTwo(date.getSeconds().toString());
      return hours + ':' + minutes + ':' + seconds;
  
    }
    
  onGridReady(e) {
    this.gridApi = e;
        setInterval(() => {
          this.gridApi.refreshCells({force: true});
        }, 1000);
  }
  onGetTimerStyle(event: any) {
      if (event.data ) {
        const hours = new Date(new Date().getTime() - event.data.time?.getTime() || 0).getHours();
        if (hours > 6) {
          event.style = { 'background': 'rgba(255,0,0,0.28) !important' };
        } else {
          event.style = { 'background': 'rgba(0,255,0,0.28) !important' };
        }
  
      }
    }
  
}
