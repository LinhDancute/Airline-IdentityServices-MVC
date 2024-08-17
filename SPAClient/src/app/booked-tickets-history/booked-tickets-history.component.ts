import { Component } from '@angular/core';
import {AppComponent} from "../app.component";
import {LandingpageComponent} from "../landingpage/landingpage.component";
import {FooterComponent} from "../footer/footer.component";

@Component({
  selector: 'app-booked-tickets-history',
  standalone: true,
  imports: [
    AppComponent,
    LandingpageComponent,
    FooterComponent
  ],
  templateUrl: './booked-tickets-history.component.html',
  styleUrl: './booked-tickets-history.component.css'
})
export class BookedTicketsHistoryComponent {

}
