import {Component, inject} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {FormsModule} from "@angular/forms";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-page-content',
  standalone: true,
  imports: [
    RouterLink,
    FormsModule
  ],
  templateUrl: './page-content.component.html',
  styleUrls: ['./page-content.component.css', '../../assets/css/styles.css']
})
export class PageContentComponent {
  searchFlightObj:any = {
    "departureAddress": "",
    "arrivalAddress": "",
    "fromDate": "",
    "toDate": ""
  };

  http = inject(HttpClient)
  constructor(private router: Router) {

  }
  onSearchingFlight() {
    this.router.navigateByUrl("/flight-search");

    // debugger;
    // this.http.post("https://localhost:7003/api/Flight/search", this.searchFlightObj).subscribe((response: any) => {
    //   if (response.flag) {
    //     this.router.navigateByUrl("/flight-search");
    //   } else {
    //     alert("Chuyến bay không hợp lệ.");
    //   }
    // });
  }
}
