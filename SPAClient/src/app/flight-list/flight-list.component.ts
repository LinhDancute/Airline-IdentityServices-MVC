import { Component } from '@angular/core';
import {NgForOf} from "@angular/common";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-flight-list',
  standalone: true,
  imports: [
    NgForOf,
    RouterLink
  ],
  templateUrl: './flight-list.component.html',
  styleUrl: './flight-list.component.css'
})
export class FlightListComponent {
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
    },
    {
      departureTime: '06:00',
      arrivalTime: '08:15',
      departureAirport: 'HAN',
      arrivalAirport: 'SGN',
      terminal: '1',
      duration: '2h 15min',
      flightNumber: '243',
      economyPrice: '99.10',
      businessPrice: '147.70',
      seatsLeft: 5
    },
    {
      departureTime: '06:30',
      arrivalTime: '08:40',
      departureAirport: 'HAN',
      arrivalAirport: 'SGN',
      terminal: '1',
      duration: '2h 10min',
      flightNumber: '7253',
      economyPrice: '91.50',
      businessPrice: '230.80',
      seatsLeft: 3
    },
    {
      departureTime: '07:00',
      arrivalTime: '09:15',
      departureAirport: 'HAN',
      arrivalAirport: 'SGN',
      terminal: '1',
      duration: '2h 15min',
      flightNumber: '207',
      economyPrice: '99.10',
      businessPrice: '230.80',
      seatsLeft: 7
    }
  ];
}
