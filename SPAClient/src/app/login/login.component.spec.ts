import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LoginComponent } from './login.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let httpTestingController: HttpTestingController;
  let mockRouter: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    mockRouter = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      imports: [
        LoginComponent,
        HttpClientTestingModule,
        FormsModule,
        RouterTestingModule // Use RouterTestingModule for testing router interactions
      ],
      providers: [
        { provide: Router, useValue: mockRouter }
      ]
    }).compileComponents();

    httpTestingController = TestBed.inject(HttpTestingController);
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should handle login and navigate with state', () => {
    const mockLoginObj = { email: 'test@example.com', password: 'password123' };
    const mockToken = 'mock-token';
    const mockUser = { id: 1, userName: 'John Doe', email: 'test@example.com' };

    component.loginObj = mockLoginObj;

    spyOn(component['router'], 'navigate').and.callThrough();

    const mockLoginResponse = { token: mockToken };
    component.onLogin();

    const loginReq = httpTestingController.expectOne('https://localhost:7002/api/Auth/login');
    expect(loginReq.request.method).toBe('POST');
    expect(loginReq.request.body).toEqual(mockLoginObj);
    loginReq.flush(mockLoginResponse);

    const userReq = httpTestingController.expectOne('https://localhost:7002/api/Auth/currenUser');
    expect(userReq.request.method).toBe('GET');
    expect(userReq.request.headers.get('Authorization')).toEqual(`Bearer ${mockToken}`);
    userReq.flush(mockUser);

    expect(mockRouter.navigate).toHaveBeenCalledWith([component.returnUrl], {
      state: {
        token: mockToken,
        user: mockUser,
        selectedOneWayFlight: component.selectedOneWayFlight,
        selectedRoundTripFlight: component.selectedRoundTripFlight,
        selectedOneWayFlightSeatClass: component.selectedOneWayFlightSeatClass,
        selectedRoundTripFlightSeatClass: component.selectedRoundTripFlightSeatClass,
        searchFlightObj: component.searchFlightObj,
        flightSectorOneWay: component.flightSectorOneWay,
        flightSectorRoundTrip: component.flightSectorRoundTrip
      }
    });
  });

  afterEach(() => {
    httpTestingController.verify();
  });
});
