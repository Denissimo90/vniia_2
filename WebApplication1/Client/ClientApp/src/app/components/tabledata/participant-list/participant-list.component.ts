import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfigurationService, DialogService, LazyLoadEvent, UserService } from '@prism/common';
import { MessageService } from 'primeng';
import { Participant } from '../../../domain/Participant';
import { LoadService } from '../../../services/load.service';
import { ParticipantRegistrationComponent } from '../../forms/registration/participant-registration/participant-registration.component';

@Component({
  selector: 'app-participant-list',
  templateUrl: './participant-list.component.html',
  styleUrls: ['./participant-list.component.css']
})
export class ParticipantListComponent implements OnInit {
  filter: boolean;
  selectedParticipants: Participant[] = [];
  participants: Participant[] = [];
  blockingMask = false;
  timeouts = {};
  isOnlyActual = false;
  isFirstLoad = true;

  @ViewChild('t') htmlTable;

  getRowId = (row) => '' + row.id;

  constructor(
    public user: UserService,
    private loadService: LoadService,
    private dialogService: DialogService,
    private messageService: MessageService,
    private configService: ConfigurationService,
    private route: ActivatedRoute,
    private router: Router
  ) {
  }

  async ngOnInit() {
    await this.onLoad();
  }

  onCellClicked(event) {
    this.htmlTable.grid.api.copySelectedRowsToClipboard(false, [event.column.colId]);
  }

  async onLoad() {
    try {
      this.participants = await this.loadService.searchParticipants();
      this.selectParticipantByUrl();
    } catch (e) {
      this.messageService.add({ severity: 'error', summary: 'Ошибка', detail: e.error?.message || 'Ошибка запроса' });
    } finally {
      this.isFirstLoad = false;
    }
  }

  selectParticipantByUrl() {
    if (!this.isFirstLoad) {
      return;
    }
    // Выделение нужной строки если id в url
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      const node = this.htmlTable.grid.api.getRowNode(id);
      this.htmlTable.gridApi.ensureNodeVisible(node);
      if (node) {
        node.setSelected(true);
        this.htmlTable.gridApi.ensureNodeVisible(node, 'middle');
      } else {
        this.router.navigate(['/participants']);
      }
    }
  }

  async startCreateParticipant() {
    const dialog = this.dialogService.createDialog(ParticipantRegistrationComponent);
    await dialog.init(null);
    dialog.result.subscribe((result) => {
      result.forEach(r => this.htmlTable.addRow(r));
    });
  }

  async startUpdateParticipant() {
    const dialog = this.dialogService.createDialog(ParticipantRegistrationComponent);
    await dialog.init(this.selectedParticipants[0]);
    dialog.result.subscribe((Participant) => {
      this.htmlTable.updateRows(Array.isArray(Participant) ? Participant : [Participant]);
      Object.assign(this.selectedParticipants[0], Array.isArray(Participant) ? Participant[0] : Participant);
    });
  }

  async deleteParticipant() {
    this.blockingMask = true;
    try {
      //await this.loadService.deleteParticipants(this.selectedParticipants);
      this.htmlTable.removeRowsById(this.selectedParticipants.map(c => c.id), 'Участник удален');
      this.selectedParticipants = [];
      this.messageService.add({
        severity: 'success', summary: 'Выполнено',
        detail: this.selectedParticipants.length > 1 ? 'Участники удалены' : 'Участник удален'
      });
    } finally {
      this.blockingMask = false;
    }
  }

}
