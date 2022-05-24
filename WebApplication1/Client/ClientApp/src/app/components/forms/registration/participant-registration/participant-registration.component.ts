import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { DialogComponent } from '@prism/common';
import { Participant } from '../../../../domain/Participant';

@Component({
  selector: 'app-participant-registration',
  templateUrl: './participant-registration.component.html',
  styleUrls: ['./participant-registration.component.css']
})
export class ParticipantRegistrationComponent  extends DialogComponent implements OnInit {

  @Output() result = new EventEmitter();
  participant: Participant;

  constructor() { super(); }

  ngOnInit(): void {
  }
  
  async init(participant?: Participant) {
    this.loading = true;
if (!!participant) {
  this.participant = participant;
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


}
