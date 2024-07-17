import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-flight-booking-detail',
  templateUrl: './flight-booking-detail.component.html',
  standalone: true,
  imports: [
    NgIf
  ],
  styleUrls: ['./flight-booking-detail.component.css']
})
export class FlightBookingDetailComponent implements OnInit {
  selectedOneWayFlight: any;
  selectedRoundTripFlight: any;
  searchFlightObj: any;

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras.state) {
      this.selectedOneWayFlight = navigation.extras.state['selectedOneWayFlight'];
      this.selectedRoundTripFlight = navigation.extras.state['selectedRoundTripFlight'];
      this.searchFlightObj = navigation.extras.state['searchFlightObj'];
    }
    console.log('received flight search info at check order: ', this.searchFlightObj);
    console.log('price 1: ', this.selectedOneWayFlight?.price);
    console.log('price 2: ', this.selectedRoundTripFlight?.price);

  }

  ngOnInit(): void {}

  getPriceDisplay(price: number): string {
    return price ? price.toFixed(2) : 'N/A';
  }

  getDayOfWeek(date: string): string {
    const days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    const day = new Date(date).getDay();
    return days[day];
  }

  getTotalPrice(): string {
    let totalPrice = 0;
    if (this.selectedOneWayFlight) {
      totalPrice += this.selectedOneWayFlight.price;
    }
    if (this.selectedRoundTripFlight) {
      totalPrice += this.selectedRoundTripFlight.price;
    }
    return totalPrice.toFixed(2);
  }
}
