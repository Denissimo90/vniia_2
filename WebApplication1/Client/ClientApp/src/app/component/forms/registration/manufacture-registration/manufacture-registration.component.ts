import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {MessageService} from 'primeng/api';
import {Manufacture} from '../../../../dto/Manufacture';
import {ManufactureService} from '../../../../service/ManufactureService';

@Component({
  selector: 'app-manufacture-registration',
  templateUrl: './manufacture-registration.component.html',
  styleUrls: ['./manufacture-registration.component.css'],
  providers: [ManufactureService, MessageService]
})
export class ManufactureRegistrationComponent implements OnInit {
  manufacture: Manufacture;
  loading = false;

  @Input() private _visible = false;
  get visible(): boolean {
    return this._visible;
  }

  set visible(value: boolean) {
    if (value) {
    } else {
      this.hide.emit();
    }
    this._visible = value;
  }

  @Input() userId;
  @Input() editMode;
  @Output() result = new EventEmitter();
  @Output() hide = new EventEmitter();

  constructor(
    private manufactureService: ManufactureService,
    private messageService: MessageService) {
  }

  ngOnInit(): void {
  }

  async saveManufacture() {
    this.loading = true;
    try {
      await this.manufactureService.createManufacture(this.manufacture);
      this.messageService.add({severity: 'success', summary: 'Выполнено', detail: 'Предприятие сохранёно'});
      this.result.emit(this.manufacture);
      this.visible = false;
    } finally {
      this.loading = false;
    }
  }
}
