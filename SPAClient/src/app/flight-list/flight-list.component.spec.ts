import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Router, NavigationEnd } from '@angular/router';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FlightListComponent } from './flight-list.component';
import { CurrencyPipe, NgClass, NgForOf, NgIf } from '@angular/common';
import { RouterLink } from '@angular/router';
import { of, filter } from 'rxjs';

describe('FlightListComponent', () => {
  let component: FlightListComponent;
  let fixture: ComponentFixture<FlightListComponent>;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        NgForOf,
        NgIf,
        NgClass,
        RouterLink,
        CurrencyPipe,
        FlightListComponent
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlightListComponent);
    component = fixture.componentInstance;
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize flight lists and assign random prices on navigation', () => {
    const mockFlightsOneWay = [
      { flightSector: 'SYD-YYZ', economySeat: 10, businessSeat: 5 },
    ];
    const mockFlightsRoundTrip = [
      { flightSector: 'YYZ-SYD', economySeat: 8, businessSeat: 2 },
    ];
    const mockSearchFlightObj = {
      departureAddress: 'Sydney Kingsford Smith',
      arrivalAddress: 'Toronto Pearson',
      fromDate: '2024-07-18',
      toDate: '2024-07-19'
    };

    router.navigate(['/flight-search'], {
      state: {
        flightsOneWay: mockFlightsOneWay,
        flightsRoundTrip: mockFlightsRoundTrip,
        searchFlightObj: mockSearchFlightObj
      }
    });

    router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      fixture.detectChanges();
      expect(component.flightsOneWay).toEqual(mockFlightsOneWay);
      expect(component.flightsRoundTrip).toEqual(mockFlightsRoundTrip);
      expect(component.searchFlightObj).toEqual(mockSearchFlightObj);
      expect(component.flightsOneWay[0].randomEconomyPrice).toBeDefined();
      expect(component.flightsRoundTrip[0].randomBusinessPrice).toBeDefined();
    });
  });

  it('should select a one-way flight and show round trip options', () => {
    const mockFlight = { flightSector: 'SYD-YYZ', economySeat: 10, businessSeat: 5 };
    component.selectOneWayFlight(mockFlight, 'Economy');
    expect(component.selectedOneWayFlight).toEqual(mockFlight);
    expect(component.selectedOneWayFlightSeatClass).toBe('Economy');
    expect(component.showRoundTrip).toBeTrue();
  });

  it('should select a round-trip flight and set selected round trip flight', () => {
    const mockFlight = { flightSector: 'YYZ-SYD', economySeat: 8, businessSeat: 2 };
    component.selectRoundTripFlight(mockFlight, 'Business');
    expect(component.selectedRoundTripFlight).toEqual(mockFlight);
    expect(component.selectedRoundTripFlightSeatClass).toBe('Business');
  });

  it('should navigate to booking detail page with selected flights and details', () => {
    const mockFlightOneWay = { flightSector: 'SYD-YYZ', economySeat: 10, businessSeat: 5 };
    const mockFlightRoundTrip = { flightSector: 'YYZ-SYD', economySeat: 8, businessSeat: 2 };

    component.selectOneWayFlight(mockFlightOneWay, 'Economy');
    component.selectRoundTripFlight(mockFlightRoundTrip, 'Business');

    spyOn(router, 'navigate');

    component.navigateToBookingDetail();

    expect(router.navigate).toHaveBeenCalledWith(['/check-order'], {
      state: {
        selectedOneWayFlight: mockFlightOneWay,
        selectedRoundTripFlight: mockFlightRoundTrip,
        selectedOneWayFlightSeatClass: 'Economy',
        selectedRoundTripFlightSeatClass: 'Business',
        searchFlightObj: component.searchFlightObj,
        flightSectorOneWay: 'SYD-YYZ',
        flightSectorRoundTrip: 'YYZ-SYD'
      }
    });
  });
});
