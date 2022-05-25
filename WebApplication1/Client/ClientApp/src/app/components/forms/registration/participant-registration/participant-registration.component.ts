import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { DialogComponent } from '@prism/common';
import { MessageService } from 'primeng';
import { LoadService } from '../../../../services/load.service';
import { Participant } from '../../../../domain/Participant';

@Component({
  selector: 'app-participant-registration',
  templateUrl: './participant-registration.component.html',
  styleUrls: ['./participant-registration.component.css']
})
export class ParticipantRegistrationComponent  extends DialogComponent implements OnInit {

  @Output() result = new EventEmitter();
  participant: Participant;

  isEditMode: boolean;
  constructor(
    private loadService: LoadService,
    private messageService: MessageService) { super(); }

  ngOnInit(): void {
  }
  
  async init(participant?: Participant) {
    this.loading = true;
if (!!participant) {
  this.participant = participant;
  this.isEditMode = true;
}
else{
  this.participant = new Participant();
  this.participant.firstName = "1111";
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


}
