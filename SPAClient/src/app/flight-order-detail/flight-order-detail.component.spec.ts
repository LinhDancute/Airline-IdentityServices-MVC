import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlightOrderDetailComponent } from './flight-order-detail.component';

describe('FlightOrderDetailComponent', () => {
  let component: FlightOrderDetailComponent;
  let fixture: ComponentFixture<FlightOrderDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FlightOrderDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FlightOrderDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
