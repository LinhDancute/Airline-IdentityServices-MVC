import { Component, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { FormsModule } from "@angular/forms";
import { Router, RouterLink } from "@angular/router";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule,
    RouterLink
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginObj: any = {
    email: "",
    password: ""
  };

  http = inject(HttpClient);
  // @ts-ignore
  returnUrl: string;
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

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras.state) {
      this.returnUrl = navigation.extras.state['returnUrl'] || '/';
      this.selectedOneWayFlight = navigation.extras.state['selectedOneWayFlight'];
      this.selectedRoundTripFlight = navigation.extras.state['selectedRoundTripFlight'];
      this.selectedOneWayFlightSeatClass = navigation.extras.state['selectedOneWayFlightSeatClass'];
      this.selectedRoundTripFlightSeatClass = navigation.extras.state['selectedRoundTripFlightSeatClass'];
      this.searchFlightObj = navigation.extras.state['searchFlightObj'];
      this.flightSectorOneWay = navigation.extras.state['flightSectorOneWay'];
      this.flightSectorRoundTrip = navigation.extras.state['flightSectorRoundTrip'];
    }
  }

  onLogin() {
    debugger;
    this.http.post("https://localhost:7002/api/Auth/login", this.loginObj).subscribe((response: any) => {
      if (response.token) {
        const token = response.token;
        console.log("Login successful. Token:", token);
        alert("Đăng nhập thành công");

        // Use the token to fetch the user data
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        this.http.get("https://localhost:7002/api/Auth/currentUser", { headers }).subscribe((userResponse: any) => {
          console.log("User data:", userResponse);

          // Navigate to flight-booking-detail with state
          this.router.navigate([this.returnUrl], {
            state: {
              token: token,  // Pass the token to flight-booking-detail
              user: userResponse, // User data response
              selectedOneWayFlight: this.selectedOneWayFlight,
              selectedRoundTripFlight: this.selectedRoundTripFlight,
              selectedOneWayFlightSeatClass: this.selectedOneWayFlightSeatClass,
              selectedRoundTripFlightSeatClass: this.selectedRoundTripFlightSeatClass,
              searchFlightObj: this.searchFlightObj,
              flightSectorOneWay: this.flightSectorOneWay,
              flightSectorRoundTrip: this.flightSectorRoundTrip
            }
          });
        });
      } else {
        alert("Sai email hoặc mật khẩu");
      }
    });
  }
}
