import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { DialogComponent } from '@prism/common';
import { MessageService } from 'primeng';
import { Team } from '../../../domain/Team';
import { Participant } from '../../../domain/Participant';
import { LoadService } from '../../../services/load.service';

@Component({
  selector: 'app-participant-selector',
  templateUrl: './participant-selector.component.html',
  styleUrls: ['./participant-selector.component.css']
})
export class ParticipantSelectorComponent  extends DialogComponent implements OnInit {

  @Output() result = new EventEmitter();
  participants: Participant[] = [];
  participant: Participant;
  team: Team;

  
  constructor(
    private loadService: LoadService,
    private messageService: MessageService) { super(); }

  ngOnInit(): void {
  }
  
  async init() {
    this.loading = true;
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
        this.participant = await this.loadService
          .updateParticipant(this.participant.id, this.participant);
      } else {
        this.participant = await this.loadService
          .createParticipant(this.participant);
      }*/
      this.messageService.add({
        severity: 'success',
        summary: 'Выполнено',
        detail: 'Участник сохранён'
      });
      this.result.emit(this.participant);
      this.visible = false;
    } finally {
      this.loading = false;
    }
  }

  onParticipantChange() {
  }

}