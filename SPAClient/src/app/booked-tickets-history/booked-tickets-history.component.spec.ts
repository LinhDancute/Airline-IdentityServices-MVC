import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookedTicketsHistoryComponent } from './booked-tickets-history.component';

describe('BookedTicketsHistoryComponent', () => {
  let component: BookedTicketsHistoryComponent;
  let fixture: ComponentFixture<BookedTicketsHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookedTicketsHistoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookedTicketsHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
