import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlightBookingDetailComponent } from './flight-booking-detail.component';

describe('FlightBookingDetailComponent', () => {
  let component: FlightBookingDetailComponent;
  let fixture: ComponentFixture<FlightBookingDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FlightBookingDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FlightBookingDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
