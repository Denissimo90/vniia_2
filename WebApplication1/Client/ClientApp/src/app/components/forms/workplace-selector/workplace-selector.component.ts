import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { DialogComponent } from '@prism/common';
import { MessageService } from 'primeng';
import { Team } from '../../../domain/Team';
import { Workplace } from '../../../domain/Workplace';
import { LoadService } from '../../../services/load.service';

@Component({
  selector: 'app-workplace-selector',
  templateUrl: './workplace-selector.component.html',
  styleUrls: ['./workplace-selector.component.css']
})
export class WorkplaceSelectorComponent  extends DialogComponent implements OnInit {

  @Output() result = new EventEmitter();
  workplaces: Workplace[] = [];
  workplace: Workplace;
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
        this.workplace = await this.loadService
          .updateWorkplace(this.workplace.id, this.workplace);
      } else {
        this.workplace = await this.loadService
          .createWorkplace(this.workplace);
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

  onWorkplaceChange() {
  }

}