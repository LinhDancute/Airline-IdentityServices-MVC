import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NgIf } from '@angular/common';
import { FooterComponent } from "../footer/footer.component";
import { LandingpageComponent } from "../landingpage/landingpage.component";

@Component({
  selector: 'app-success',
  standalone: true,
  imports: [NgIf, FooterComponent, LandingpageComponent],
  templateUrl: './success.component.html',
  styleUrls: ['./success.component.css']
})
export class SuccessComponent implements OnInit {
  sessionId: string | null = null;
  paymentStatus: string = 'in_process';
  amount: number = 0;
  token: string = '';

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.sessionId = params['session_id'] || this.sessionId;
      this.amount = parseFloat(params['amount']) || 0;
      this.token = params['token'] || this.token;
      this.handleStatus();
    });
  }

  handleStatus(): void {
    if (this.sessionId) {
      this.verifyPayment(this.sessionId);
    }
  }

  verifyPayment(sessionId: string): void {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`);
    this.http.get(`https://localhost:7004/api/Payment/verify/${sessionId}`, { headers })
      .subscribe(
        (response: any) => {
          if (response.success) {
            console.log('Payment verified successfully.');
            this.paymentStatus = 'Succeeded';
          } else {
            console.error('Payment verification failed.');
            this.paymentStatus = 'Cancelled';
          }
        },
        (error) => {
          console.error('HTTP Error during payment verification:', error);
          this.paymentStatus = 'failed';
        }
      );
  }

  navigateToBookedTicketsHistory(): void {
    this.router.navigate(['/booked-tickets-history']);
  }
}
