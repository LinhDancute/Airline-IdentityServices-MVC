import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-page-content',
  templateUrl: './page-content.component.html',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule
  ],
  styleUrls: ['./page-content.component.css']
})
export class PageContentComponent implements OnInit {

  searchFlightObj: any = {
    departureAddress: "",
    arrivalAddress: "",
    fromDate: "",
    toDate: ""
  };

  cities: string[] = [];
  flightSectors: string[] = [];
  flightsOneWay: any[] = [];
  flightsRoundTrip: any[] = [];

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.fetchCities();
  }

  fetchCities(): void {
    this.http.get<string[]>("https://localhost:7003/api/Airport/simplified-names")
      .subscribe(
        (response) => {
          console.log('Fetched cities:', response);
          this.cities = response;
        },
        (error) => {
          console.error('Error fetching cities:', error);
        }
      );
  }

  onSearchingFlight(): void {
    this.http.post<any[]>('https://localhost:7003/api/Flight/search', this.searchFlightObj).subscribe(
      (response: any[]) => {
        if (Array.isArray(response) && response.length > 0) {
          this.flightSectors = [];
          this.flightsOneWay = [];
          this.flightsRoundTrip = [];

          //extract flightSector
          this.flightSectors = Array.from(new Set(response.map(flight => flight.flightSector)));

          //filter flights into two arrays based on flightSector
          response.forEach(flight => {
            if (flight.flightSector === this.flightSectors[0]) {
              this.flightsOneWay.push(flight);
            } else if (flight.flightSector === this.flightSectors[1]) {
              this.flightsRoundTrip.push(flight);
            }
          });

          this.router.navigateByUrl('/flight-search', {
            //pass response data as state to flight-list
            state: {
              flightsOneWay: this.flightsOneWay,
              flightsRoundTrip: this.flightsRoundTrip,
              searchFlightObj: this.searchFlightObj
            }
          });
          console.log('Flights One Way:', this.flightsOneWay);
          console.log('Flights Round Trip:', this.flightsRoundTrip);
          console.log('Flight search info:', this.searchFlightObj);
        } else {
          alert('Không tìm thấy chuyến bay phù hợp.');
        }
      },
      (error) => {
        console.error('Error searching flight:', error);
      }
    );
  }
}
