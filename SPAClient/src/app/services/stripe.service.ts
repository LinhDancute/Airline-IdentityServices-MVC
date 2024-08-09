import { Injectable } from '@angular/core';
import { loadStripe, Stripe } from '@stripe/stripe-js';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StripeService {
  private stripe: Stripe | null = null;

  constructor(private http: HttpClient) {
    this.initializeStripe();
  }

  private async initializeStripe() {
    this.stripe = await loadStripe(environment.stripePublishableKey);
  }

  getStripe(): Stripe | null {
    return this.stripe;
  }

  createPaymentIntent(amount: number, currency: string): Observable<any> {
    return this.http.post('/api/create-payment-intent', { amount, currency });
  }

  redirectToCheckout(sessionId: string): Observable<any> {
    if (!this.stripe) {
      throw new Error('Stripe not initialized');
    }
    // @ts-ignore
    return this.stripe.redirectToCheckout({ sessionId });
  }
}
