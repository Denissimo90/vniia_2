import {Component, OnInit} from '@angular/core';
import {ApplicationUser} from './dto/ApplicationUser';
import {Router} from '@angular/router';
import {UserService} from './service/UserService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';
  loading = false;
  user = null;

  constructor(
    private router: Router,
    private userService: UserService) {
  }

  ngOnInit() {
    console.log('client started');
  }

  onSetUser(user: ApplicationUser) {
    this.user = user;
    console.log(this.user);
    // Сохраняем пользователя в глобалньый сервис чтобы в любом компоненте иметь к нему доступ
    this.userService.user = user;
    // После авторизации можно направить приложение на любую страничку
    this.router.navigate(['app-main-report']);
  }
}
