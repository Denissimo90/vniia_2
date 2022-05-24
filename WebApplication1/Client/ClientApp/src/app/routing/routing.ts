import {NgModule} from '@angular/core';
import {Route, RouterModule, Routes} from '@angular/router';
import {MenusquareComponent} from '../components/menu/menusquare/menusquare.component';
import {LoginGuard} from '@prism/common';
import {AccessGuard} from './access.guard';
import { HomeComponent } from '../home/home.component';
import { SettingsTabComponent } from '../components/forms/settingsTab/settingsTab.component';

const appRoutes: Routes = [
  {
    path: '',
    component: MenusquareComponent,
    canActivate: [LoginGuard]
  },
  {
    path: 'app-home',
    component: HomeComponent,
    canActivate: [LoginGuard, AccessGuard]
  },
  {
    path: 'app-settingsTab',
    component: SettingsTabComponent,
    canActivate: [LoginGuard, AccessGuard]
  },
  {
    path: '**',
    redirectTo: ''
  }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
