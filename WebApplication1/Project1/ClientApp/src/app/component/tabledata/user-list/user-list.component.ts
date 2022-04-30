import {Component, OnInit, ViewChild} from '@angular/core';
import {ApplicationUser} from '../../../dto/ApplicationUser';
import {ApplicationUserService} from '../../../service/ApplicationUserService';
import {MessageService} from 'primeng/api';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
  providers: [ApplicationUserService, MessageService]
})
export class UserListComponent implements OnInit {
  blockingMask = false;
  users: ApplicationUser[] = [];
  selectedUser: ApplicationUser;
  userDialogVisible = false;
  editMode = false;

  @ViewChild('dialog') dialog;

  constructor(
    private applicationUserService: ApplicationUserService,
    private messageService: MessageService) {
  }

  ngOnInit() {
    this.onLoad();
  }

  async onLoad() {
    try {
      this.users = await this.applicationUserService.getUsers(null);
    } catch (e) {
      throw e;
    }
  }

  async startCreateUser() {
    this.userDialogVisible = true;
    this.editMode = false;
    this.dialog.init();
  }

  async startUpdateUser() {
    this.userDialogVisible = true;
    this.editMode = true;
    this.dialog.init(this.selectedUser.id);
  }

  async deleteUser() {
    this.blockingMask = true;
    try {
      await this.applicationUserService.deleteUser(this.selectedUser.id);
      this.messageService.add({severity: 'info', summary: 'Пользователь удален', detail: '', life: 10});
      await this.onLoad();
    } finally {
      this.blockingMask = false;
    }
  }
}
