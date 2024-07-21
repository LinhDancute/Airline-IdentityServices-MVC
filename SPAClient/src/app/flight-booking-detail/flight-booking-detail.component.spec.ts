import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FlightBookingDetailComponent } from './flight-booking-detail.component';
import { Router } from '@angular/router';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
import { of } from 'rxjs';
import {HttpHeaders} from "@angular/common/http";

describe('FlightBookingDetailComponent', () => {
  let component: FlightBookingDetailComponent;
  let fixture: ComponentFixture<FlightBookingDetailComponent>;
  let httpTestingController: HttpTestingController;
  let mockRouter: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    mockRouter = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      imports: [
        FlightBookingDetailComponent,
        HttpClientTestingModule,
        FormsModule,
        NgIf
      ],
      providers: [
        { provide: Router, useValue: mockRouter }
      ]
    }).compileComponents();

    httpTestingController = TestBed.inject(HttpTestingController);
    fixture = TestBed.createComponent(FlightBookingDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize correctly with router state', () => {
    const mockState = {
      selectedOneWayFlight: { flightNumber: '123', price: 99.10 },
      selectedRoundTripFlight: { flightNumber: '456', price: 199.20 },
      selectedOneWayFlightSeatClass: 'Economy',
      selectedRoundTripFlightSeatClass: 'Business',
      searchFlightObj: { departureAddress: 'A', arrivalAddress: 'B', fromDate: '2024-07-01', toDate: '2024-07-10' },
      flightSectorOneWay: 'A-B',
      flightSectorRoundTrip: 'B-A',
      user: { id: 1, userName: 'John Doe', phoneNumber: '1234567890', email: 'john.doe@example.com' },
      token: 'mock-token'
    };

    // Set state directly
    component.selectedOneWayFlight = mockState.selectedOneWayFlight;
    component.selectedRoundTripFlight = mockState.selectedRoundTripFlight;
    component.selectedOneWayFlightSeatClass = mockState.selectedOneWayFlightSeatClass;
    component.selectedRoundTripFlightSeatClass = mockState.selectedRoundTripFlightSeatClass;
    component.searchFlightObj = mockState.searchFlightObj;
    component.flightSectorOneWay = mockState.flightSectorOneWay;
    component.flightSectorRoundTrip = mockState.flightSectorRoundTrip;
    component.user = mockState.user;
    component.token = mockState.token;

    fixture.detectChanges();

    expect(component.selectedOneWayFlight).toEqual(mockState.selectedOneWayFlight);
    expect(component.selectedRoundTripFlight).toEqual(mockState.selectedRoundTripFlight);
    expect(component.selectedOneWayFlightSeatClass).toEqual(mockState.selectedOneWayFlightSeatClass);
    expect(component.selectedRoundTripFlightSeatClass).toEqual(mockState.selectedRoundTripFlightSeatClass);
    expect(component.searchFlightObj).toEqual(mockState.searchFlightObj);
    expect(component.flightSectorOneWay).toEqual(mockState.flightSectorOneWay);
    expect(component.flightSectorRoundTrip).toEqual(mockState.flightSectorRoundTrip);
    expect(component.user).toEqual(mockState.user);
    expect(component.token).toEqual(mockState.token);
  });

  it('should call updatePhoneNumber and handle success', () => {
    spyOn(component, 'updatePhoneNumber').and.callThrough();
    component.token = 'mock-token';
    component.user = { id: 1, phoneNumber: '1234567890' };

    const mockResponse = { flag: true };
    const mockHeaders = new HttpHeaders().set('Authorization', `Bearer ${component.token}`);
    const mockUpdateUrl = `https://localhost:7002/api/Auth/updatePhoneNumber/${component.user.id}`;

    component.updatePhoneNumber();

    const req = httpTestingController.expectOne(mockUpdateUrl);
    expect(req.request.method).toBe('PUT');
    expect(req.request.headers.get('Authorization')).toEqual(`Bearer ${component.token}`);
    req.flush(mockResponse);

    expect(component.updatePhoneNumber).toHaveBeenCalled();
    expect(component.editPhoneNumber).toBeFalse();
  });

  it('should call createTickets and handle success', () => {
    spyOn(component, 'createTickets').and.callThrough();
    component.token = 'mock-token';
    component.user = { id: 1, userName: 'John Doe', phoneNumber: '1234567890' };

    const mockTicketObj = [
      { id: 1, passengerName: 'John Doe', passengerPhoneNumber: '1234567890', itinerary: 'A-B', flightNumber: '123', date: '2024-07-01', departureTime: '12:00', seat: '', class: 'Economy', pnr: '', mealRequest: [], baggageType: [], usd: '99.10', vnd: '', status: 0 },
      { id: 1, passengerName: 'John Doe', passengerPhoneNumber: '1234567890', itinerary: 'B-A', flightNumber: '456', date: '2024-07-10', departureTime: '14:00', seat: '', class: 'Business', pnr: '', mealRequest: [], baggageType: [], usd: '199.20', vnd: '', status: 0 }
    ];

    const mockResponse = { success: true };
    const mockHeaders = new HttpHeaders().set('Authorization', `Bearer ${component.token}`).set('Content-Type', 'application/json');

    component.createTickets();

    const req = httpTestingController.expectOne('https://localhost:7004/api/Ticket/bulk');
    expect(req.request.method).toBe('POST');
    expect(req.request.headers.get('Authorization')).toEqual(`Bearer ${component.token}`);
    expect(req.request.body).toEqual(mockTicketObj);
    req.flush(mockResponse);

    expect(component.createTickets).toHaveBeenCalled();
  });

  afterEach(() => {
    httpTestingController.verify();
  });
});
