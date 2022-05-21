import {Component, OnDestroy, OnInit} from '@angular/core';
import {UserService} from '@prism/common';
import {MessageService} from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: []
})

export class AppComponent implements OnInit, OnDestroy {

  constructor(public user: UserService) {
  }

  async ngOnInit() {
  }

  ngOnDestroy() {
  }
}
