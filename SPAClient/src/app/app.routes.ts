import { Routes } from '@angular/router';

import { LandingpageComponent } from './landingpage/landingpage.component';
import {LoginComponent} from "./login/login.component";
import {RegisterComponent} from "./register/register.component";
import {PageContentComponent} from "./page-content/page-content.component";
import {FooterComponent} from "./footer/footer.component";

export const routes: Routes = [
  { path: '', redirectTo: '/main', pathMatch: 'full' },
  {
    path: 'main',
    component: LandingpageComponent,
    children: [
      { path: '', component: PageContentComponent },
      { path: 'footer', component: FooterComponent }
    ]
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
];
