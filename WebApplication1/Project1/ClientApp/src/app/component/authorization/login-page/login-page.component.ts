import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {ApplicationUser} from "../../../dto/ApplicationUser";
import {ApplicationUserService} from "../../../service/ApplicationUserService";

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
  providers: [ApplicationUserService]
})
export class LoginPageComponent implements OnInit {
  loading = false;
  user: ApplicationUser = new ApplicationUser();

  @Output() setUser = new EventEmitter<ApplicationUser>();

  constructor(private applicationUserService: ApplicationUserService) { }

  ngOnInit(): void {
    console.log('login-page init');
  }

  async auth() {
    this.loading = true;
    try {
      if (!this.user.username || !this.user.password) {
        console.log('Для авторизации необходимо имя пользователя/email и пароль');
      } else {
        this.user = await this.applicationUserService.auth(this.user);
        this.login();
      }
    } catch (e) {
      console.log('Ошибка при авторизации: ' + e);
    } finally {
      this.loading = false;
    }
  }

  login() {
    this.setUser.emit(this.user);
  }

}
