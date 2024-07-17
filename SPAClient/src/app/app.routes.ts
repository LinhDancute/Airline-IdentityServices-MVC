import { Routes } from '@angular/router';

import {LoginComponent} from "./login/login.component";
import {RegisterComponent} from "./register/register.component";
import {MainComponent} from "./main/main.component";
import {FlightListComponent} from "./flight-list/flight-list.component";
import {FlightSearchComponent} from "./flight-search/flight-search.component";
import {FlightBookingDetailComponent} from "./flight-booking-detail/flight-booking-detail.component";
import {CheckOrderComponent} from "./check-order/check-order.component";

export const routes: Routes = [
  { path: '', redirectTo: '/main', pathMatch: 'full' },
  {
    path: 'main',
    component: MainComponent
  },
  {
    path: 'flight-search',
    component: FlightSearchComponent
  },
  {
    path: 'check-order',
    component: CheckOrderComponent
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'flight-list', component: FlightListComponent },
  { path: 'flight-booking-detail', component: FlightBookingDetailComponent}
];
