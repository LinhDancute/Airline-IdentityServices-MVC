  import { Component, OnInit } from '@angular/core';
  import { Router } from '@angular/router';
  import { HttpClient, HttpHeaders } from '@angular/common/http';
  import { FormsModule } from "@angular/forms";
  import { NgIf } from "@angular/common";

  @Component({
    selector: 'app-flight-booking-detail',
    templateUrl: './flight-booking-detail.component.html',
    standalone: true,
    imports: [
      FormsModule,
      NgIf,
    ],
    styleUrls: ['./flight-booking-detail.component.css'],
  })

  export class FlightBookingDetailComponent implements OnInit {
    selectedOneWayFlight: any;
    selectedRoundTripFlight: any;
    // @ts-ignore
    selectedOneWayFlightSeatClass: string;
    // @ts-ignore
    selectedRoundTripFlightSeatClass: string;
    searchFlightObj: any;
    // @ts-ignore
    flightSectorOneWay: string;
    // @ts-ignore
    flightSectorRoundTrip: string;
    user: any;
    editPhoneNumber: boolean = false;
    // @ts-ignore
    token: string;
    private stripe: any;

    constructor(
      private router: Router,
      private http: HttpClient) {
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
      // @ts-ignore
      console.log('Flight sector one-way:', this.flightSectorOneWay);
      // @ts-ignore
      console.log('Flight sector round-trip:', this.flightSectorRoundTrip);
    }

    ngOnInit(): void {
      this.loadStripeScript();
    }

    loadStripeScript(): void {
      const script = document.createElement('script');
      script.src = 'https://js.stripe.com/v3/';
      script.onload = () => this.initializeStripe();
      script.onerror = () => console.error('Failed to load Stripe.js');
      document.head.appendChild(script);
    }


    initializeStripe(): void {
      debugger;
      if (typeof window !== 'undefined') {
        if (window.Stripe) {
          this.stripe = window.Stripe('pk_test_51PltgFCq8iYfRHUTleV24cicXgJ3kPee3yCriEXH68FlMbLB3rPb8w0bSlGIaHbFxQSwA2WAGqhONZWLDIrHH1yS005SpEfVGX'); // Replace with your Stripe public key
        } else {
          console.error('Stripe.js not loaded.');
          setTimeout(() => this.initializeStripe(), 1000); // Retry after 1 second
        }
      }
    }


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

    createTickets(): void {
      debugger;
      if (!this.token) {
        console.error('No token found.');
        return;
      }
      const ticketObj = [
        {
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
          status: 0
        },
        {
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
          status: 0
        }
      ];

      const headers = new HttpHeaders()
        .set('Authorization', `Bearer ${this.token}`)
        .set('Content-Type', 'application/json');

      this.http.post('https://localhost:7004/api/Ticket/bulk', ticketObj, { headers }).subscribe(
        (response: any) => {
          if (response.success) {
            this.initiatePayment(parseFloat(this.getTotalPrice()));
          } else {
            alert('Failed to create tickets.');
          }
        },
        (error) => {
          console.error('Error creating tickets:', error);
          alert('An error occurred while creating the tickets.');
        }
      );
    }

    initiatePayment(totalPrice: number): void {
      if (!window.Stripe) {
        console.error('Stripe.js not loaded.');
        return;
      }

      this.stripe = window.Stripe('pk_test_51PltgFCq8iYfRHUTleV24cicXgJ3kPee3yCriEXH68FlMbLB3rPb8w0bSlGIaHbFxQSwA2WAGqhONZWLDIrHH1yS005SpEfVGX'); // Replace with your Stripe public key

      this.http.post('https://localhost:7004/api/Payment/create-checkout-session', { Amount: totalPrice }, {
        headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`).set('Content-Type', 'application/json')
      }).subscribe(
        (response: any) => {
          this.stripe.redirectToCheckout({ sessionId: response.id }).then((result: any) => {
            if (result.error) {
              alert(result.error.message);
              this.router.navigate(['/cancel']);
            } else {
              alert('Payment successful!');
              this.router.navigate(['/success?session_id=']);
            }
          });
        },
        (error) => {
          console.error('Error initiating payment:', error);
          alert('An error occurred while initiating payment.');
        }
      );
    }
  }
