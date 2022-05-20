import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {MenusquareComponent} from '../components/menu/menusquare/menusquare.component';
import {LoginGuard} from '@prism/common';

const appRoutes: Routes = [
      {
        path: '',
        component: MenusquareComponent,
        canActivate: [LoginGuard]
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
export class AppRoutingModule {}

