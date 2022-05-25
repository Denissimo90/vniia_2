import {environment} from '../environments/environment';
import {CommonModule, InputNumberModule} from '@prism/common';
import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';

import {SidebarModule as SidebarModuleAngular} from 'ng-sidebar';
import {LoggerModule} from 'ngx-logger';
import {PrimeNgCalendarMaskModule} from 'racoon-mask-primeng';
import {AgGridModule} from 'ag-grid-angular';

import {AccordionModule} from 'primeng/accordion';
import {MessageService} from 'primeng/api';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {BlockUIModule} from 'primeng/blockui';
import {ButtonModule} from 'primeng/button';
import {CalendarModule} from 'primeng/calendar';
import {CardModule} from 'primeng/card';
import {CheckboxModule} from 'primeng/checkbox';
import {ColorPickerModule} from 'primeng/colorpicker';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import {DataViewModule} from 'primeng/dataview';
import {DialogModule} from 'primeng/dialog';
import {DropdownModule} from 'primeng/dropdown';
import {DialogService, DynamicDialogModule, DynamicDialogRef} from 'primeng/dynamicdialog';
import {FieldsetModule} from 'primeng/fieldset';
import {FileUploadModule} from 'primeng/fileupload';
import {InputMaskModule} from 'primeng/inputmask';
import {InputSwitchModule} from 'primeng/inputswitch';
import {InputTextModule} from 'primeng/inputtext';
import {KeyFilterModule} from 'primeng/keyfilter';
import {MenuModule} from 'primeng/menu';
import {MenubarModule} from 'primeng/menubar';
import {MessageModule} from 'primeng/message';
import {MessagesModule} from 'primeng/messages';
import {MultiSelectModule} from 'primeng/multiselect';
import {OrganizationChartModule} from 'primeng/organizationchart';
import {OverlayPanelModule} from 'primeng/overlaypanel';
import {PaginatorModule} from 'primeng/paginator';
import {PanelModule} from 'primeng/panel';
import {PickListModule} from 'primeng/picklist';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import {RadioButtonModule} from 'primeng/radiobutton';
import {ScrollPanelModule} from 'primeng/scrollpanel';
import {SelectButtonModule} from 'primeng/selectbutton';
import {SidebarModule} from 'primeng/sidebar';
import {SpinnerModule} from 'primeng/spinner';
import {SplitButtonModule} from 'primeng/splitbutton';
import {TableModule} from 'primeng/table';
import {TabViewModule} from 'primeng/tabview';
import {TieredMenuModule} from 'primeng/tieredmenu';
import {ToastModule} from 'primeng/toast';
import {ToggleButtonModule} from 'primeng/togglebutton';
import {ToolbarModule} from 'primeng/toolbar';
import {TooltipModule} from 'primeng/tooltip';
import {TreeTableModule} from 'primeng/treetable';

import {SidebarModule as NgSidebarModule} from 'ng-sidebar';

import {AppComponent} from './app.component';
import {MenusquareComponent} from './components/menu/menusquare/menusquare.component';
import {ChartModule, InputTextareaModule, SliderModule} from 'primeng';
import {AppRoutingModule} from './routing';
import {CorrectionComponent} from './components/forms/correction/correction.component';
import {SettingsTabComponent} from './components/forms/settingsTab/settingsTab.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { PdfViewerComponent } from './pdf-viewer/pdf-viewer.component';
import { ParticipantListComponent } from './components/tabledata/participant-list/participant-list.component';
import { RoleListComponent } from './components/tabledata/role-list/role-list.component';
import { TeamListComponent } from './components/tabledata/team-list/team-list.component';
import { CompetentListComponent } from './components/tabledata/competent-list/competent-list.component';
import { CompetentRegistrationComponent } from './components/forms/registration/competent-registration/competent-registration.component';
import { TeamRegistrationComponent } from './components/forms/registration/team-registration/team-registration.component';
import { ParticipantRegistrationComponent } from './components/forms/registration/participant-registration/participant-registration.component';
import { CompetentParticipantListComponent } from './components/tabledata/competent-list/competent-participant-list/competent-participant-list.component';
import { InputSelectorComponent } from './components/forms/input-selector/input-selector.component';
import { WorkplaceRegistrationComponent } from './components/forms/registration/workplace-registration/workplace-registration.component';
import { DropdownCellEditorComponent } from './components/dropdown-cell-editor.component';
import { WorkplaceListComponent } from './components/tabledata/workplace-list/workplace-list.component';
import { TeamParticipantListComponent } from './components/tabledata/team-list/team-participant-list/team-participant-list.component';
import { ParticipantSelectorComponent } from './components/forms/participant-selector/participant-selector.component';
import { CompetentWorkplaceListComponent } from './components/tabledata/competent-list/competent-workplace-list/competent-workplace-list.component';
import { WorkplaceSelectorComponent } from './components/forms/workplace-selector/workplace-selector.component';
import { GroupWorkplaceListComponent } from './components/tabledata/group-workplace-list/group-workplace-list.component';
import { GroupWorkplaceRegistrationComponent } from './components/forms/registration/group-workplace-registration/group-workplace-registration.component';
import { CompetentionTeamListComponent } from './components/tabledata/competention-team-list/competention-team-list.component';
import { CompetentionTeamDetailListComponent } from './components/tabledata/competention-team-list/competention-team-detail-list/competention-team-detail-list.component';


@NgModule({
  declarations: [
    AppComponent,
    MenusquareComponent,
    CorrectionComponent,
    SettingsTabComponent,
    NavMenuComponent,
    PdfViewerComponent,
    ParticipantListComponent,
    RoleListComponent,
    TeamListComponent,
    CompetentListComponent,
    CompetentRegistrationComponent,
    TeamRegistrationComponent,
    ParticipantRegistrationComponent,
    InputSelectorComponent,
    CompetentParticipantListComponent,
    WorkplaceRegistrationComponent,
    WorkplaceListComponent,
    TeamParticipantListComponent,
    ParticipantSelectorComponent,
    CompetentWorkplaceListComponent,
    WorkplaceSelectorComponent,
    GroupWorkplaceListComponent,
    GroupWorkplaceRegistrationComponent,
    CompetentionTeamListComponent,
    CompetentionTeamDetailListComponent
  ],
  imports: [
    CommonModule.forRoot(environment.configName),
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CardModule,
    InputTextModule,
    InputTextareaModule,
    CalendarModule,
    AccordionModule,
    TableModule,
    DropdownModule,
    ButtonModule,
    SplitButtonModule,
    CheckboxModule,
    PanelModule,
    ScrollPanelModule,
    DialogModule,
    FileUploadModule,
    TooltipModule,
    SpinnerModule,
    InputMaskModule,
    InputSwitchModule,
    DataViewModule,
    TabViewModule,
    KeyFilterModule,
    MessagesModule,
    MessageModule,
    FieldsetModule,
    ToolbarModule,
    TreeTableModule,
    MultiSelectModule,
    PickListModule,
    MenubarModule,
    DataViewModule,
    MenuModule,
    RadioButtonModule,
    PaginatorModule,
    ConfirmDialogModule,
    AppRoutingModule,
    OverlayPanelModule,
    TieredMenuModule,
    ProgressSpinnerModule,
    BlockUIModule,
    AutoCompleteModule,
    ToastModule,
    OrganizationChartModule,
    ToggleButtonModule,
    ScrollPanelModule,
    DynamicDialogModule,
    LoggerModule.forRoot(null),
    SidebarModule,
    NgSidebarModule.forRoot(),
    ColorPickerModule,
    SelectButtonModule,
    AgGridModule.withComponents([DropdownCellEditorComponent]),
    PrimeNgCalendarMaskModule,
    SliderModule,
    InputNumberModule
  ],
  providers: [
    MessageService,
    DialogService,
    DynamicDialogRef
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
