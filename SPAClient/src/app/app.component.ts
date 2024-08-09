// app.component.ts
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FooterComponent } from "./footer/footer.component";
import { LandingpageComponent } from "./landingpage/landingpage.component";
import { PageContentComponent } from "./page-content/page-content.component";
import { StripeService } from 'ngx-stripe';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FooterComponent, LandingpageComponent, PageContentComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'SPAClient';
}
