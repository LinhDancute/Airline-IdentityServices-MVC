import { Component, Input, OnInit } from '@angular/core';
import { Router, NavigationEnd, RouterLink } from '@angular/router';
import {CurrencyPipe, NgClass, NgForOf, NgIf} from "@angular/common";
import {LandingpageComponent} from "../landingpage/landingpage.component";
import {FooterComponent} from "../footer/footer.component";

@Component({
  selector: 'app-flight-list',
  templateUrl: './flight-list.component.html',
  standalone: true,
  imports: [
    RouterLink,
    NgForOf,
    NgIf,
    NgClass,
    CurrencyPipe,
    LandingpageComponent,
    FooterComponent
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

  // @ts-ignore
  selectedOneWayFlightSeatClass: string;
  // @ts-ignore
  selectedRoundTripFlightSeatClass: string;
  // @ts-ignore
  flightSectorOneWay: string;
  // @ts-ignore
  flightSectorRoundTrip: string;

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
    });
  }

  ngOnInit(): void {}

  getRandomPrice(prices: { price: number }[]): number {
    const randomIndex = Math.floor(Math.random() * prices.length);
    return prices[randomIndex].price;
  }

  assignRandomPrices(flights: any[]): void {
    flights.forEach(flight => {
      flight.randomEconomyPrice = this.getRandomPrice(this.economyPrices.map(p => ({ price: p.economyPrice })));
      flight.randomBusinessPrice = this.getRandomPrice(this.businessPrices.map(p => ({ price: p.businessPrice })));
    });
  }

  selectOneWayFlight(flight: any, seatClass: string): void {
    this.selectedOneWayFlight = flight;
    this.selectedOneWayFlight.price = seatClass === 'Economy' ? flight.randomEconomyPrice : flight.randomBusinessPrice;
    this.selectedOneWayFlightSeatClass = seatClass;
    this.flightSectorOneWay = flight.flightSector;
    this.showRoundTrip = true;
  }

  selectRoundTripFlight(flight: any, seatClass: string): void {
    this.selectedRoundTripFlight = flight;
    this.selectedRoundTripFlight.price = seatClass === 'Economy' ? flight.randomEconomyPrice : flight.randomBusinessPrice;
    this.selectedRoundTripFlightSeatClass = seatClass;
    this.flightSectorRoundTrip = flight.flightSector;
  }

  navigateToBookingDetail(): void {
    this.router.navigate(['/flight-booking-detail'], {
      state: {
        selectedOneWayFlight: this.selectedOneWayFlight,
        selectedRoundTripFlight: this.selectedRoundTripFlight,
        selectedOneWayFlightSeatClass: this.selectedOneWayFlightSeatClass,
        selectedRoundTripFlightSeatClass: this.selectedRoundTripFlightSeatClass,
        searchFlightObj: this.searchFlightObj,
        flightSectorOneWay: this.flightSectorOneWay,
        flightSectorRoundTrip: this.flightSectorRoundTrip
      }
    });
  }
}
