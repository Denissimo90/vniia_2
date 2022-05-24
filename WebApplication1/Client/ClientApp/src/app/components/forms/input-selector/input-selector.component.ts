import {Component, EventEmitter, Output} from '@angular/core';
import {DialogComponent} from '@prism/common';

@Component({
  selector: 'app-input-selector',
  templateUrl: './input-selector.component.html',
  styleUrls: ['./input-selector.component.css'],
  providers: []
})

export class InputSelectorComponent extends DialogComponent {

  inputTitle = 'Комментарий: ';
  title = 'Добавьте комментарий';
  okTitle = 'ОК';
  description: string;
  value: any;

  step = 1;
  min: number;
  max: number;
  maxFractionDigits = 0;
  minFractionDigits = 0;
  showButtons = false;
  isInputNumber = false;
  enabled = true;

  constructor() {
    super();
  }

  @Output() result = new EventEmitter<string>();

  init(title?: string, inputTitle?: string, okTitle?: string, description?: string, initValue?: any,
       enabled = true,
       isInputNumber = false,
       step = 1,
       min = null,
       max = null,
       maxFractionDigits = 0,
       minFractionDigits = 0,
       showButtons = false
  ) {

    if (!!title) {
      this.title = title;
    }
    if (!!inputTitle) {
      this.inputTitle = inputTitle;
    }
    if (!!okTitle) {
      this.okTitle = okTitle;
    }
    if (!!description) {
      this.description = description;
    }
    if (!!initValue) {
      this.value = initValue;
    }

    this.enabled = enabled;
    this.isInputNumber = isInputNumber;
    this.step = step;
    this.min = min;
    this.max = max;
    this.maxFractionDigits = maxFractionDigits;
    this.minFractionDigits = minFractionDigits;
    this.showButtons = showButtons;
  }

  saveAndClose() {
    this.result.emit(this.value);
    this.visible = false;
  }
}
