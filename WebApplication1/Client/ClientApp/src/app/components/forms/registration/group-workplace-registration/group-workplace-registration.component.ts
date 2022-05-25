import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfigurationService, DialogComponent, DialogService, Table2Component } from '@prism/common';
import { MessageService } from 'primeng';
import { Workplace } from '../../../../domain/Workplace';
import { GroupWorkplace } from '../../../../domain/GroupWorkplace';
import { LoadService } from '../../../../services/load.service';
import { WorkplaceSelectorComponent } from '../../workplace-selector/workplace-selector.component';


@Component({
  selector: 'app-group-workplace-registration',
  templateUrl: './group-workplace-registration.component.html',
  styleUrls: ['./group-workplace-registration.component.css']
})
export class GroupWorkplaceRegistrationComponent extends DialogComponent implements OnInit {

  @Output() result = new EventEmitter();
  groupWorkplace: GroupWorkplace;
  filter: boolean;
  selectedWorkplaces: Workplace[] = [];
  blockingMask = false;

  isEditMode: boolean;
  constructor(
    private loadService: LoadService,
    private messageService: MessageService,
    private dialogService: DialogService,
    private configService: ConfigurationService,
    private route: ActivatedRoute,
    private router: Router) { super(); }

    getRowId = (row) => '' + row.id;
    items: Workplace[];
    @ViewChild('t') htmlTable;
    @Output() onInit = new EventEmitter<any>();

  ngOnInit(): void {
  }
  
  async init(groupWorkplace?: GroupWorkplace) {
    this.loading = true;
if (!!groupWorkplace) {
  this.groupWorkplace = groupWorkplace;
  this.isEditMode = true;
}
else{
  this.groupWorkplace = new GroupWorkplace();
  this.groupWorkplace.workplaces= [];
}
    //await this.getPlacesFromApi();

    /*if (authorId == null) {
      this.editMode = false;
    } else {
      this.editMode = true;
      this.author = await this.authorService.getAuthor(authorId);
    }*/
    this.loading = false;
  }
  
  async save() {
    this.loading = true;

    try {
      /*if (this.isEditMode) {
        this.groupWorkplace = await this.loadService
          .updateWorkplace(this.groupWorkplace.id, this.groupWorkplace);
      } else {
        this.groupWorkplace = await this.loadService
          .createWorkplace(this.groupWorkplace);
      }*/
      this.messageService.add({
        severity: 'success',
        summary: 'Выполнено',
        detail: 'Участник сохранён'
      });
      this.result.emit(this.groupWorkplace);
      this.visible = false;
    } finally {
      this.loading = false;
    }
  }

  async startCreateWorkplace() {
    const dialog = this.dialogService.createDialog(WorkplaceSelectorComponent);
    await dialog.init();
    dialog.result.subscribe((result) => {
      result.forEach(r => this.htmlTable.addRow(r));
    });
  }


  async deleteWorkplace() {
    this.blockingMask = true;
    try {
      //await this.loadService.deleteWorkplaces(this.selectedWorkplaces);
      this.htmlTable.removeRowsById(this.selectedWorkplaces.map(c => c.id), 'Участник удален');
      this.selectedWorkplaces = [];
      this.messageService.add({
        severity: 'success', summary: 'Выполнено',
        detail: this.selectedWorkplaces.length > 1 ? 'Участники удалены' : 'Участник удален'
      });
    } finally {
      this.blockingMask = false;
    }
  }

}
