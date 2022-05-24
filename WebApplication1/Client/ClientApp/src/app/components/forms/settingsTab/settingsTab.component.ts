import {Component, EventEmitter} from '@angular/core';
import {DialogComponent} from '@prism/common';
import {TestService} from '../../../services/test.service';
import {MessageService} from 'primeng/api';
import {Correction} from '../../../domain/Correction';
import {PersonCorrection} from '../../../domain/PersonCorrection';

@Component({
  selector: 'app-settingsTab',
  templateUrl: './settingsTab.component.html',
  styleUrls: ['./settingsTab.component.css']
})
export class SettingsTabComponent {
  tabIndex = 0;


  personCorrection = [{}];

  result = new EventEmitter<any>();


  constructor(
    private messageService: MessageService) {
  }

  async init() {
  }

  refreshTabs() {

  }

}
