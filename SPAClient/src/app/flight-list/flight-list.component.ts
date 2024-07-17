import { Component, Input, OnInit } from '@angular/core';
import { Router, NavigationEnd, RouterLink } from '@angular/router';
import {NgClass, NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-flight-list',
  templateUrl: './flight-list.component.html',
  standalone: true,
  imports: [
    RouterLink,
    NgForOf,
    NgIf,
    NgClass
  ],
  styleUrls: ['./flight-list.component.css']
})
export class FlightListComponent implements OnInit {
  @Input() flightsOneWay: any[] = [];
  @Input() flightsRoundTrip: any[] = [];

  selectedOneWayFlight: any;
  selectedRoundTripFlight: any;
  showRoundTrip: boolean = false;
  searchFlightObj: any;

  economyPrices: { economyPrice: number }[] = [
    { economyPrice: 99.10 },
    { economyPrice: 99.10 },
    { economyPrice: 91.50 },
    { economyPrice: 99.10 },
    { economyPrice: 105.80 },
    { economyPrice: 115.25 },
    { economyPrice: 88.90 },
    { economyPrice: 97.45 },
    { economyPrice: 102.75 },
    { economyPrice: 93.20 }
  ];

  businessPrices: { businessPrice: number }[] = [
    { businessPrice: 147.70 },
    { businessPrice: 147.70 },
    { businessPrice: 230.80 },
    { businessPrice: 230.80 },
    { businessPrice: 215.50 },
    { businessPrice: 198.25 },
    { businessPrice: 225.40 },
    { businessPrice: 210.60 },
    { businessPrice: 205.90 },
    { businessPrice: 189.75 }
  ];

  //receive data from page-content (flight-search info)
  constructor(private router: Router) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        const navigation = this.router.getCurrentNavigation();
        if (navigation?.extras.state) {
          this.flightsOneWay = navigation.extras.state['flightsOneWay'] || [];
          this.flightsRoundTrip = navigation.extras.state['flightsRoundTrip'] || []
          this.searchFlightObj = navigation.extras.state['searchFlightObj'];
          this.assignRandomPrices(this.flightsOneWay);
          this.assignRandomPrices(this.flightsRoundTrip);
        }
      }
      // console.log('received one way flights: ', this.flightsOneWay);
      // console.log('received round-trip flights: ', this.flightsRoundTrip);
      // console.log('received flight search info: ', this.searchFlightObj);
    });

  }

  ngOnInit(): void {}

  //get random in list
  getRandomPrice(prices: { price: number }[]): number {
    const randomIndex = Math.floor(Math.random() * prices.length);
    return prices[randomIndex].price;
  }

  //set random prices for seat
  assignRandomPrices(flights: any[]): void {
    flights.forEach(flight => {
      flight.randomEconomyPrice = this.getRandomPrice(this.economyPrices.map(p => ({ price: p.economyPrice })));
      flight.randomBusinessPrice = this.getRandomPrice(this.businessPrices.map(p => ({ price: p.businessPrice })));
    });
  }

  //select one way flight
  selectOneWayFlight(flight: any): void {
    this.selectedOneWayFlight = flight;
    this.selectedOneWayFlight.price = flight.randomEconomyPrice;
    this.showRoundTrip = true;
  }

  //select round trip flight
  selectRoundTripFlight(flight: any): void {
    this.selectedRoundTripFlight = flight;
    this.selectedRoundTripFlight.price = flight.randomEconomyPrice;
  }

  //pass data to flight-booking-detail (2 flights have been choosen, flight-search info)
  navigateToBookingDetail(): void {
    this.router.navigate(['/check-order'], {
      state: {
        selectedOneWayFlight: this.selectedOneWayFlight,
        selectedRoundTripFlight: this.selectedRoundTripFlight,
        searchFlightObj: this.searchFlightObj
      }
    });
    // console.log('selected one way flight:', this.selectedOnWayFlight);
    // console.log('selected round trip flight:', this.selectedRoundTripFlight);
    // console.log('pass flight search info:', this.searchFlightObj);
  }
}
