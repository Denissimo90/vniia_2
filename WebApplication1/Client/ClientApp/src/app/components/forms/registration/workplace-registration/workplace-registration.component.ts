import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { DialogComponent } from '@prism/common';
import { MessageService } from 'primeng';
import { LoadService } from '../../../../services/load.service';
import { Workplace } from '../../../../domain/Workplace';

@Component({
  selector: 'app-workplace-registration',
  templateUrl: './workplace-registration.component.html',
  styleUrls: ['./workplace-registration.component.css']
})
export class WorkplaceRegistrationComponent  extends DialogComponent implements OnInit {

  @Output() result = new EventEmitter();
  workplace: Workplace;
  isEditMode: boolean;

  constructor(
    private loadService: LoadService,
    private messageService: MessageService) { super(); }

  ngOnInit(): void {
  }
  
  async init(workplace?: Workplace) {
    this.loading = true;
if (!!workplace) {
  this.workplace = workplace;
}
else{
  this.workplace = new Workplace();
  this.workplace.id = 0;
  this.workplace.designation = "1111";
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
        detail: 'Рабочее место сохранёно'
      });
      this.result.emit(this.workplace);
      this.visible = false;
    } finally {
      this.loading = false;
    }
  }


}

