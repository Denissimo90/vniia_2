import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';
import { APP_INITIALIZER } from '@angular/core';
import { AppConfig } from './app.config';

export function initializeApp(appConfig: AppConfig) {
  return () => appConfig.load();
}

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {UserListComponent} from './component/tabledata/user-list/user-list.component';
import {UserRegistrationComponent} from './component/forms/registration/user-registration/user-registration.component';
import {ManufactureRegistrationComponent} from './component/forms/registration/manufacture-registration/manufacture-registration.component';
import {ManufactureListComponent} from './component/tabledata/manufacture-list/manufacture-list.component';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ButtonModule} from "primeng/button";
import {TooltipModule} from "primeng/tooltip";
import {ConfirmDialogModule} from "primeng/confirmdialog";
import {ToolbarModule} from "primeng/toolbar";
import {DialogModule} from "primeng/dialog";
import {InputTextModule} from "primeng/inputtext";
import {ContextMenuModule} from 'primeng/contextmenu';
import {MenuItem} from 'primeng/api';
import {DropdownModule} from "primeng/dropdown";
import {PaginatorModule} from "primeng/paginator";
import {FileUploadModule} from "primeng/fileupload";
import {InputMaskModule} from "primeng/inputmask";
import {TableModule} from "primeng/table";
import {CalendarModule} from "primeng/calendar";
import {TabViewModule} from "primeng/tabview";
import {KeyFilterModule} from "primeng/keyfilter";
import {MessageModule} from "primeng/message";
import {ScrollPanelModule} from "primeng/scrollpanel";
import {CheckboxModule} from "primeng/checkbox";
import {MessagesModule} from "primeng/messages";
import {ToastModule} from "primeng/toast";
import {LoginPageComponent} from "./component/authorization/login-page/login-page.component";
import {PdfViewerComponent} from './pdf-viewer/pdf-viewer.component';
import { MainReportComponent } from './component/forms/report/main-report/main-report.component';
import {ChartModule} from 'primeng/chart';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    UserListComponent,
    UserRegistrationComponent,
    ManufactureRegistrationComponent,
    ManufactureListComponent,
    LoginPageComponent,
    PdfViewerComponent,
    MainReportComponent
  ],
  imports: [
    ContextMenuModule,
    DropdownModule,
    FileUploadModule,
    PaginatorModule,
    DialogModule,
    InputMaskModule,
    TableModule,
    CalendarModule,
    TabViewModule,
    KeyFilterModule,
    MessageModule,
    MessagesModule,
    ScrollPanelModule,
    CheckboxModule,
    ToastModule,
    ButtonModule,
    ToolbarModule,
    BrowserModule,
    BrowserAnimationsModule,
    TooltipModule,
    HttpClientModule,
    InputTextModule,
    BrowserModule,
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: 'app-main-report', component: MainReportComponent, pathMatch: 'full'},
      {path: 'app-user-list', component: UserListComponent/*, canActivate: [AuthorizeGuard]*/},
      {path: 'app-user-registration', component: UserRegistrationComponent/*, canActivate: [AuthorizeGuard]*/},
      {path: 'app-manufacture-list', component: ManufactureListComponent/*, canActivate: [AuthorizeGuard]*/},
      {path: 'app-manufacture-registration', component: ManufactureRegistrationComponent/*, canActivate: [AuthorizeGuard]*/},
    ]),
    ConfirmDialogModule,
    ButtonModule,
    TooltipModule,
    ConfirmDialogModule,
    ToolbarModule,
    DialogModule,
    InputTextModule,
    ChartModule
  ],
  providers: [
    AppConfig,
    { provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [AppConfig], multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
