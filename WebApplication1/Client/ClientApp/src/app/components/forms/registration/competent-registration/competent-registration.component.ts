import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { DialogComponent } from '@prism/common';
import { Competent } from '../../../../domain/Competent';

@Component({
  selector: 'app-competent-registration',
  templateUrl: './competent-registration.component.html',
  styleUrls: ['./competent-registration.component.css']
})
export class CompetentRegistrationComponent extends DialogComponent implements OnInit {

  @Output() result = new EventEmitter();
  competent: Competent;

  constructor() { super(); }

  ngOnInit(): void {
  }
  
  async init(competent?: Competent) {
    this.loading = true;
if (!!competent) {
  this.competent = competent;
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
