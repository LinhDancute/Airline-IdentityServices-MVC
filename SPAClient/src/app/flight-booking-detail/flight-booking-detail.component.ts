import { Component } from '@angular/core';
import {NgForOf} from "@angular/common";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-flight-booking-detail',
  standalone: true,
  imports: [
    NgForOf,
    RouterLink
  ],
  templateUrl: './flight-booking-detail.component.html',
  styleUrl: './flight-booking-detail.component.css'
})
export class FlightBookingDetailComponent {
  flights = [
    {
      departureTime: '05:00',
      arrivalTime: '07:15',
      departureAirport: 'HAN',
      arrivalAirport: 'SGN',
      terminal: '1',
      duration: '2h 15min',
      flightNumber: '205',
      economyPrice: '99.10',
      businessPrice: '147.70',
      seatsLeft: 8
    }]
}
