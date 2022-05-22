import {Component, EventEmitter} from '@angular/core';
import {DialogComponent} from '@prism/common';
import {TestService} from '../../../services/test.service';
import {MessageService} from 'primeng/api';
import {Correction} from '../../../domain/Correction';
import {PersonCorrection} from '../../../domain/PersonCorrection';

@Component({
  selector: 'app-correction',
  templateUrl: './correction.component.html',
  styleUrls: ['./correction.component.css'],
  providers: [TestService]
})
export class CorrectionComponent extends DialogComponent {
  correction: Correction = new Correction();
  title = 'Корректировка ';
  json: any;


  personCorrection = [{}];

  result = new EventEmitter<any>();


  constructor(
    private testService: TestService,
    private messageService: MessageService) {
    super();
  }

  async init() {
    try {
      this.loading = true;
      await this.getPersonCorrectionFromApi();
      //this.json = this.testService.getTestConfig();
      //this.correction.title = JSON.stringify(this.json);
    } catch (e) {
      this.messageService.add({severity: 'error', summary: 'Ошибка', detail: e.error?.message || 'Ошибка запроса'});
    } finally {
      this.loading = false;
    }
  }

  correct() {
    this.result.emit(this.json);
  }

  async getPersonCorrection(): Promise<PersonCorrection[]> {
    return [
      {id: 0, name: 'Вася', shortName: 'Вася', archive: false},
      {id: 0, name: 'Петя', shortName: 'Петя', archive: false},
      {id: 0, name: 'Лена', shortName: 'Лена', archive: false},
      {id: 0, name: 'Оля', shortName: 'Оля', archive: false},
    ];
  }

  async getPersonCorrectionFromApi() {
    this.personCorrection = [];
    const personCorrection: PersonCorrection[] = await this.getPersonCorrection();
    this.personCorrection = personCorrection.map(type => ({
      label: type.name,
      value: type
    }));
  }

}
