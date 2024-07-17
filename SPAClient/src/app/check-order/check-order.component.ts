import { Component } from '@angular/core';
import {FlightListComponent} from "../flight-list/flight-list.component";
import {FooterComponent} from "../footer/footer.component";
import {LandingpageComponent} from "../landingpage/landingpage.component";
import {FlightBookingDetailComponent} from "../flight-booking-detail/flight-booking-detail.component";

@Component({
  selector: 'app-check-order',
  standalone: true,
  imports: [
    FlightListComponent,
    FooterComponent,
    LandingpageComponent,
    FlightBookingDetailComponent
  ],
  templateUrl: './check-order.component.html',
  styleUrl: './check-order.component.css'
})
export class CheckOrderComponent {

}
