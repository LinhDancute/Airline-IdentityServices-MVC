import { TestBed, ComponentFixture } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { PageContentComponent } from './page-content.component';
import { Router } from '@angular/router';

describe('PageContentComponent', () => {
  let component: PageContentComponent;
  let fixture: ComponentFixture<PageContentComponent>;
  let httpMock: HttpTestingController;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PageContentComponent, HttpClientTestingModule, FormsModule],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PageContentComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
    router = TestBed.inject(Router);
    spyOn(router, 'navigateByUrl');
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch cities on initialization', () => {
    const mockCities = ['Sydney Kingsford Smith', 'Toronto Pearson', 'Incheon'];

    component.fetchCities();

    const req = httpMock.expectOne('https://localhost:7003/api/Airport/simplified-names');
    expect(req.request.method).toBe('GET');
    req.flush(mockCities);

    expect(component.cities).toEqual(mockCities);
  });

  it('should handle search flights', () => {
    const mockFlights = [
      { flightSector: 'SYD-YYZ', details: 'Flight details 1' },
      { flightSector: 'YYZ-SYD', details: 'Flight details 2' }
    ];

    component.searchFlightObj = {
      departureAddress: 'Sydney Kingsford Smith',
      arrivalAddress: 'Toronto Pearson',
      fromDate: '2024-07-18',
      toDate: '2024-07-19'
    };

    component.onSearchingFlight();

    const req = httpMock.expectOne('https://localhost:7003/api/Flight/search');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(component.searchFlightObj);
    req.flush(mockFlights);

    expect(component.flightSectors.length).toBe(2);
    expect(component.flightsOneWay.length).toBe(1);
    expect(component.flightsRoundTrip.length).toBe(1);
    expect(router.navigateByUrl).toHaveBeenCalledWith('/flight-search', {
      state: {
        flightsOneWay: component.flightsOneWay,
        flightsRoundTrip: component.flightsRoundTrip,
        searchFlightObj: component.searchFlightObj
      }
    });
  });

  it('should show alert if no flights found', () => {
    spyOn(window, 'alert');

    component.searchFlightObj = {
      departureAddress: 'Sydney Kingsford Smith',
      arrivalAddress: 'Toronto Pearson',
      fromDate: '2024-07-21',
      toDate: '2024-07-22'
    };

    component.onSearchingFlight();

    const req = httpMock.expectOne('https://localhost:7003/api/Flight/search');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(component.searchFlightObj);

    req.flush([]);
    expect(window.alert).toHaveBeenCalledWith('Không tìm thấy chuyến bay phù hợp.');
  });
});
