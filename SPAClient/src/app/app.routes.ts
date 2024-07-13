import { Routes } from '@angular/router';

import {LoginComponent} from "./login/login.component";
import {RegisterComponent} from "./register/register.component";
import {MainComponent} from "./main/main.component";
import {FlightListComponent} from "./flight-list/flight-list.component";

export const routes: Routes = [
  { path: '', redirectTo: '/main', pathMatch: 'full' },
  {
    path: 'main',
    component: MainComponent
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'flight-list', component: FlightListComponent },
];
