import { Component } from '@angular/core';
import {FooterComponent} from "../footer/footer.component";
import {LandingpageComponent} from "../landingpage/landingpage.component";
import {FlightListComponent} from "../flight-list/flight-list.component";

@Component({
  selector: 'app-flight-search',
  standalone: true,
  imports: [
    FooterComponent,
    LandingpageComponent,
    FlightListComponent
  ],
  templateUrl: './flight-search.component.html',
  styleUrl: './flight-search.component.css'
})
export class FlightSearchComponent {

}
