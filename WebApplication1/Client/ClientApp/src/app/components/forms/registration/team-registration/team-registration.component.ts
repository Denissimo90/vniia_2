import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfigurationService, DialogComponent, DialogService, Table2Component } from '@prism/common';
import { MessageService } from 'primeng';
import { Participant } from '../../../../domain/Participant';
import { Team } from '../../../../domain/Team';
import { LoadService } from '../../../../services/load.service';
import { ParticipantSelectorComponent } from '../../participant-selector/participant-selector.component';

@Component({
  selector: 'app-team-registration',
  templateUrl: './team-registration.component.html',
  styleUrls: ['./team-registration.component.css']
})
export class TeamRegistrationComponent extends DialogComponent implements OnInit {

  @Output() result = new EventEmitter();
  team: Team;
  filter: boolean;
  selectedParticipants: Participant[] = [];
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
    items: Participant[];
    @ViewChild('t') htmlTable;
    @Output() onInit = new EventEmitter<any>();


    ngOnInit(): void {
      this.onLoad();
    }
  
    async onLoad() {
      try {
        this.team = await this.loadService.searchTeams()[0];
      } catch (e) {
        this.messageService.add({ severity: 'error', summary: 'Ошибка', detail: e.error?.message || 'Ошибка запроса' });
      } finally {
      }
    }
  
  
  async init(team?: Team) {
    this.loading = true;
if (!!team) {
  this.team = team;
  this.isEditMode = true;
}
else{
  this.team = new Team();
  this.team.name = "1111";
  this.team.participants= [];
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
        this.team = await this.loadService
          .updateParticipant(this.team.id, this.team);
      } else {
        this.team = await this.loadService
          .createParticipant(this.team);
      }*/
      this.messageService.add({
        severity: 'success',
        summary: 'Выполнено',
        detail: 'Участник сохранён'
      });
      this.result.emit(this.team);
      this.visible = false;
    } finally {
      this.loading = false;
    }
  }

  async startCreateParticipant() {
    const dialog = this.dialogService.createDialog(ParticipantSelectorComponent);
    await dialog.init();
    dialog.result.subscribe((result) => {
      result.forEach(r => this.htmlTable.addRow(r));
    });
  }


  async deleteParticipant() {
    this.blockingMask = true;
    try {
      //await this.loadService.deleteParticipants(this.selectedParticipants);
      this.htmlTable.removeRowsById(this.selectedParticipants.map(c => c.id), 'Участник удален');
      this.selectedParticipants = [];
      this.messageService.add({
        severity: 'success', summary: 'Выполнено',
        detail: this.selectedParticipants.length > 1 ? 'Участники удалены' : 'Участник удален'
      });
    } finally {
      this.blockingMask = false;
    }
  }

}
