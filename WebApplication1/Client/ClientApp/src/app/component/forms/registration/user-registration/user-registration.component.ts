import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {MessageService} from 'primeng/api';
import {ApplicationUser} from '../../../../dto/ApplicationUser';
import {ApplicationUserService} from '../../../../service/ApplicationUserService';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css'],
  providers: [ApplicationUserService, MessageService]
})
export class UserRegistrationComponent implements OnInit {
  user: ApplicationUser = new ApplicationUser();
  loading = false;

  @Input() private _visible = false;
  get visible(): boolean {
    return this._visible;
  }

  set visible(value: boolean) {
    if (value) {
      this.init(this.userId);
    } else {
      this.hide.emit();
    }
    this._visible = value;
  }

  userId: any;
  @Input() editMode;
  @Output() result = new EventEmitter();
  @Output() hide = new EventEmitter();

  constructor(
    private applicationUserService: ApplicationUserService,
    private messageService: MessageService
  ) {
  }

  ngOnInit() {
  }

  async init(userId: number) {
    this.loading = true;
    this.userId = userId;
    if (this.userId != null && this.editMode) {
      this.user = await this.applicationUserService.getUser(this.userId);
      console.log(this.user);
    } else {
      this.user = new ApplicationUser();
    }
    this.loading = false;
  }

  saveUser() {
    this.loading = true;
    try {
      if (this.userId) {
        this.applicationUserService.updateUser(this.user);
      } else {
        this.applicationUserService.createUser(this.user);
      }
      this.hide.emit();
      this.messageService.add({severity: 'success', summary: 'Выполнено', detail: 'Пользователь сохранён'});
      this.result.emit(this.user);
      this.visible = false;
    } finally {
      this.loading = false;
    }
  }
}
