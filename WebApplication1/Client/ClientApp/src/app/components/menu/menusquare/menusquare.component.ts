import {Component, OnInit} from '@angular/core';
import {AuthService, ConfigurationService, DialogService, MessageServiceKey, UserService} from '@prism/common';
import {MessageService} from 'primeng/api';
import {HttpClient} from '@angular/common/http';
import {DomSanitizer} from '@angular/platform-browser';
import {BaseService} from '../../../services/base.service';
import {Location} from '@angular/common';
import {LauncherMenuItem} from '../../../domain/LauncherMenuItem';
import {LauncherMenuList} from '../../../domain/LauncherMenuList';
import * as Color from 'color';
import {TestService} from '../../../services/test.service';
import {CorrectionComponent} from '../../forms/correction/correction.component';
import { InputSelectorComponent } from '../../forms/input-selector/input-selector.component';
import { NewUserService } from '../../../services/new.user.service';

@Component({
  selector: 'app-menusquare',
  templateUrl: './menusquare.component.html',
  styleUrls: ['./menusquare.component.css'],
  providers: [DialogService]
})
export class MenusquareComponent implements OnInit {


  constructor(
    public user: UserService,
    private dialogService: DialogService,
    private messageService: MessageService,
    private configurationService: ConfigurationService,
    private httpClient: HttpClient,
    private location: Location,
    private sanitizer: DomSanitizer,
    private auth: AuthService,
    private appUserService: NewUserService
  ) {
  }

  menu: any[] = [];
  hasAnyApp = false;
  hovers = {};

  async ngOnInit() {
    if (!!this.configurationService.config['mainMenu']) {
      this.updateMenuFromJson(this.configurationService.config['mainMenu']);
    } else if (!!this.configurationService.config['menuConfigUri']) {
      this.updateMenuFromFile(this.configurationService.config['menuConfigUri']);
    } else {
      this.showMessage('Не найден файл конфигурации меню!');
    }

    console.log(this.user?.user.changePassword, !!this.user?.user.changePassword);
     if (!!this.user?.user.changePassword + "" == "True") 
    {
      const dialog = this.dialogService.createDialog(InputSelectorComponent);
      dialog.init('Смена пароля', 'Пароль', 'Сменить', 'Ваш пароль устарел, требуется обновление',
      null, true, false, null, null, null, null, null, false);
      dialog.result.subscribe(async (secret) => {
        await this.appUserService.changePassword(this.user?.user?.employeeId, secret);
        console.log('await change password');
        this.auth.logout();
      });

    }

    //const config = await this.testService.getTestConfig();
    //console.log(config);
 
   //await this.newDialog();

  }

  async newDialog() {
    const dialog = this.dialogService.createDialog(CorrectionComponent);
    await dialog.init();
    dialog.result.subscribe((res: any) => {
      this.messageService.add({severity: 'success', summary: 'Выполнено', detail: JSON.stringify(res)});
      dialog.visible = false;
    });
  }


  updateMenuFromJson(data: any[]) {
    this.menu = this.generateMenuTree(data);
  }

  onClick(data: any, event: MouseEvent) {
    if (!!data['url']) {
      if (!this.hasRole(data)) {
        this.showMessage('У Вас недостаточно прав, чтобы перейти на страницу');
      }
      if (event.ctrlKey) {
        const open = (url) => {
          window.open(url);
        };
        open(data['url']);
      } else {
        const win = window.open(data['url'], '_self');
      }
    }
  }

  openInNewTab(url: string) {
    Object.assign(document.createElement('a'), {
      target: '_blank',
      url,
    }).click();
  }

  updateMenuFromFile(url: string) {
    let uri = this.configurationService.config['menuConfigUri'];
    if (uri.toLowerCase().indexOf('http') === -1) {
      uri = window.location.origin + this.location.prepareExternalUrl(uri);
    }
    this.httpClient.get(uri, BaseService.httpOptions).toPromise().then(
      (data: any[]) => {
        this.menu = this.generateMenuTree(data);
      },
      (e) => {
        this.showMessage('Невозможно загрузить конфигурацию меню');
      },
    );
  }

  generateMenuTree(data: any[]): any[] {
    const defaultColor = '#3d60a1';
    const defaultGroup = '';
    const menu = [];
    const subListMap = new Map();

    for (let i = 0; i < data.length; i++) {
      if (!this.hasRole(data[i])) {
        continue;
      }

      const menuItem: LauncherMenuItem = Object.assign(new LauncherMenuItem(), data[i]);
      if (!menuItem.color) {
        menuItem.color = defaultColor;
      }
      if (!menuItem.color.startsWith('#')) {
        menuItem.color = '#' + menuItem.color;
      }

      menuItem.hoverColor = new Color(menuItem.color).lighten(0.5).hexString();

      const group = !data[i]['group'] ? defaultGroup : data[i]['group'];
      let list: LauncherMenuList = subListMap.get(group);
      if (!list) {
        list = new LauncherMenuList();
        list.title = group;
        subListMap.set(group, list);
        if (group === defaultGroup) {
          menu.unshift(list);
        } else {
          menu.push(list);
        }
      }
      let subMenu = list.group[list.group.length - 1];
      if (!subMenu || subMenu.length >= 3) {
        subMenu = [];
        list.group.push(subMenu);
      }
      subMenu.push(menuItem);
      if (!this.hasAnyApp && !!menuItem.url) {
        this.hasAnyApp = true;
      }
    }
    // чтобы было красиво, вставим пустишки если строка не полная
    menu.forEach(i => {
        const subMenu = i.group[i.group.length - 1];
        if (subMenu.length === 2) {
          subMenu.splice(1, 0, {});
        }
        if (subMenu.length === 1) {
          subMenu.unshift({});
          subMenu.push({});
        }
      }
    );
    return menu;
  }

  hasRole(item: any): boolean {
    return !(!!item.role && item.role.length > 0 && !this.user.roles[item.role]);
  }

  showMessage(message?: string) {
    this.messageService.add({
      sticky: true, key: MessageServiceKey.OK, severity: 'warn', summary: 'Информация',
      detail: !!message ? message : 'У Вас недостаточно прав для выполнения этого запроса.\n Обратитесь к администратору.'
    });
  }
}
