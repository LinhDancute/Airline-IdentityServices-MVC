import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormsModule } from "@angular/forms";
import { NgIf } from "@angular/common";
import {LandingpageComponent} from "../landingpage/landingpage.component";
import {FooterComponent} from "../footer/footer.component";

declare var Stripe: any;

@Component({
  selector: 'app-flight-booking-detail',
  templateUrl: './flight-booking-detail.component.html',
  standalone: true,
  imports: [
    FormsModule,
    NgIf,
    LandingpageComponent,
    FooterComponent,
  ],
  styleUrls: ['./flight-booking-detail.component.css'],
})
export class FlightBookingDetailComponent implements OnInit {
  selectedOneWayFlight: any;
  selectedRoundTripFlight: any;
  selectedOneWayFlightSeatClass: string = '';
  selectedRoundTripFlightSeatClass: string = '';
  searchFlightObj: any;
  flightSectorOneWay: string = '';
  flightSectorRoundTrip: string = '';
  user: any;
  editPhoneNumber: boolean = false;
  token: string = '';
  private stripe: any;
  sessionId: any;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private http: HttpClient
  ) {
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras.state) {
      this.selectedOneWayFlight = navigation.extras.state['selectedOneWayFlight'];
      this.selectedRoundTripFlight = navigation.extras.state['selectedRoundTripFlight'];
      this.selectedOneWayFlightSeatClass = navigation.extras.state['selectedOneWayFlightSeatClass'];
      this.selectedRoundTripFlightSeatClass = navigation.extras.state['selectedRoundTripFlightSeatClass'];
      this.flightSectorOneWay = navigation.extras.state['flightSectorOneWay'];
      this.flightSectorRoundTrip = navigation.extras.state['flightSectorRoundTrip'];
      this.searchFlightObj = navigation.extras.state['searchFlightObj'];
      this.user = navigation.extras.state['user'];
      this.token = navigation.extras.state['token'];
    }
  }


  ngOnInit(): void {
    this.loadStripeScript();
  }

  loadStripeScript(): void {
    if (document.getElementById('stripe-js')) {
      this.initializeStripe();
      return;
    }
    const script = document.createElement('script');
    script.id = 'stripe-js';
    script.src = 'https://js.stripe.com/v3/';
    script.onload = () => this.initializeStripe();
    script.onerror = () => console.error('Failed to load Stripe.js');
    document.head.appendChild(script);
  }

  initializeStripe(): void {
    if (window.Stripe) {
      this.stripe = Stripe('pk_test_51PltgFCq8iYfRHUTleV24cicXgJ3kPee3yCriEXH68FlMbLB3rPb8w0bSlGIaHbFxQSwA2WAGqhONZWLDIrHH1yS005SpEfVGX');
    } else {
      console.error('Stripe.js not loaded.');
      setTimeout(() => this.initializeStripe(), 1000);
    }
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

  navigateToLogin(): void {
    this.router.navigate(['/login'], {
      state: {
        returnUrl: this.router.url,
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

  toggleEditPhoneNumber(event?: MouseEvent): void {
    if (event) {
      event.preventDefault();
    }
    this.editPhoneNumber = !this.editPhoneNumber;
  }

  updatePhoneNumber(): void {
    if (!this.token) {
      console.error('No token found.');
      return;
    }

    const updateUrl = `https://localhost:7002/api/Auth/updatePhoneNumber/${this.user.id}`;
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`);

    this.http.put(updateUrl, { phoneNumber: this.user.phoneNumber }, { headers }).subscribe(
      (response: any) => {
        if (response.flag) {
          alert('Phone number updated successfully.');
          this.editPhoneNumber = false;
        } else {
          alert('Failed to update phone number.');
        }
      },
      (error) => {
        console.error('Error updating phone number:', error);
        alert('An error occurred while updating the phone number.');
      }
    );
  }

  paymentProcessing(): void {
    if (!this.token) {
      console.error('No token found.');
      return;
    }

    const totalPrice = parseFloat(this.getTotalPrice());
    this.initiatePayment(totalPrice);
  }

  initiatePayment(totalPrice: number): void {
    if (!this.stripe) {
      console.error('Stripe is not initialized.');
      return;
    }

    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${this.token}`)
      .set('Content-Type', 'application/json');

    this.http.post('https://localhost:7004/api/Payment/create-checkout-session', { Amount: totalPrice }, { headers })
      .subscribe(
        (response: any) => {
          console.log('Response:', response);
          this.createTickets();
          this.stripe.redirectToCheckout({ sessionId: response.id })
            .then((result: any) => {
              if (result.error) {
                console.error('Stripe Error:', result.error);
                alert(result.error.message);
              } else {
                this.createTickets();
              }
            })
            .catch((error: any) => {
              console.error('Stripe Redirect Error:', error);
              alert('An error occurred during the payment process.');
            });
        },
        (error) => {
          console.error('HTTP Error:', error);
          alert('An error occurred while creating the checkout session.');
        }
      );
  }

  createTickets(): void {
    if (!this.token) {
      console.error('No token found.');
      return;
    }

    const ticketObj = [];

    if (this.selectedOneWayFlight) {
      ticketObj.push({
        id: this.user.id,
        passengerName: this.user.userName,
        passengerPhoneNumber: this.user.phoneNumber,
        itinerary: this.flightSectorOneWay,
        flightNumber: this.selectedOneWayFlight.flightNumber,
        date: this.selectedOneWayFlight.date,
        departureTime: this.selectedOneWayFlight.departureTime,
        seat: "",
        class: this.selectedOneWayFlightSeatClass,
        pnr: "",
        mealRequest: [],
        baggageType: [],
        usd: this.selectedOneWayFlight.price.toFixed(2),
        vnd: "",
        status: 1
      });
    }

    if (this.selectedRoundTripFlight) {
      ticketObj.push({
        id: this.user.id,
        passengerName: this.user.userName,
        passengerPhoneNumber: this.user.phoneNumber,
        itinerary: this.flightSectorRoundTrip,
        flightNumber: this.selectedRoundTripFlight.flightNumber,
        date: this.selectedRoundTripFlight.date,
        departureTime: this.selectedRoundTripFlight.departureTime,
        seat: "",
        class: this.selectedRoundTripFlightSeatClass,
        pnr: "",
        mealRequest: [],
        baggageType: [],
        usd: this.selectedRoundTripFlight.price.toFixed(2),
        vnd: "",
        status: 1
      });
    }

    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${this.token}`)
      .set('Content-Type', 'application/json');

    this.http.post('https://localhost:7004/api/Ticket/bulk', ticketObj, { headers }).subscribe(
      (response: any) => {
        console.log('Ticket creation response:', response);
        alert('Tickets created successfully.');
        this.router.navigate(['/booked-tickets-history']);
      },
      (error) => {
        console.error('Error creating tickets:', error);
        alert('An error occurred while creating the tickets.');
      }
    );
  }
}
