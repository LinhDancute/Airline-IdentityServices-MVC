import { InjectionToken } from '@angular/core';

export const NGX_STRIPE_VERSION = new InjectionToken<string>('NGX_STRIPE_VERSION');
export const STRIPE_OPTIONS = new InjectionToken<any>('STRIPE_OPTIONS');

export const stripeConfig = {
  apiKey: 'pk_test_51PltgFCq8iYfRHUTleV24cicXgJ3kPee3yCriEXH68FlMbLB3rPb8w0bSlGIaHbFxQSwA2WAGqhONZWLDIrHH1yS005SpEfVGX'
};
