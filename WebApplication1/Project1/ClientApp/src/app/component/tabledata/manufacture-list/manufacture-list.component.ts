import {Component, OnInit, ViewChild} from '@angular/core';
import {ManufactureService} from '../../../service/ManufactureService';
import {Manufacture} from '../../../dto/Manufacture';
import {MessagesModule} from 'primeng/messages';
import {MessageModule} from 'primeng/message';
import {ManufactureRegistrationComponent} from '../../forms/registration/manufacture-registration/manufacture-registration.component';
import {ApplicationUserService} from '../../../service/ApplicationUserService';
import {MessageService} from 'primeng/api';

@Component({
  selector: 'app-manufacture-list',
  templateUrl: './manufacture-list.component.html',
  styleUrls: ['./manufacture-list.component.css'],
  providers: [ManufactureService, MessageService]
})
export class ManufactureListComponent implements OnInit {
  blockingMask = false;
  filter: boolean;
  userDialogVisible = false;
  msgs: [];
  manufacture: Manufacture;
  manufactures: Manufacture[] = [];
  @ViewChild('t') htmlTable;

  constructor(
    private manufactureService: ManufactureService,
    private messageService: MessageService) {
  }

  ngOnInit(): void {
    this.onLoad();
  }

  async onLoad() {
    try {
      const manufactures = await this.manufactureService.getManufactures();
      this.manufactures = manufactures;
    } catch (e) {
      throw e;
    }
  }

  async startCreateManufacture() {
    this.userDialogVisible = true;
  }

  async deleteManufacture() {
    this.blockingMask = true;
    try {
      await this.manufactureService.deleteManufacture(this.manufacture.id);
      this.messageService.add({severity: 'info', summary: 'Позиция удалена', detail: '', life: 10});
      this.htmlTable.removeRowsById(this.manufacture.id, 'Позиция удалена');
    } finally {
      this.blockingMask = false;
    }
  }

}
