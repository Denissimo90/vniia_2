import {enableProdMode} from '@angular/core';
import {platformBrowserDynamic} from '@angular/platform-browser-dynamic';

import {AppModule} from './app/app.module';
import {environment} from './environments/environment';
import 'hammerjs';
import {LicenseManager} from 'ag-grid-enterprise';

if (environment.production) {
  enableProdMode();
}

LicenseManager.setLicenseKey('Softmap_LLC__on_behalf_of_FGUP_VNIIA_Prism_1Devs6_March_2020__MTU4MzQ1MjgwMDAwMA==0212dff765d5249b4b356c0c25888b1f');

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.log(err));
