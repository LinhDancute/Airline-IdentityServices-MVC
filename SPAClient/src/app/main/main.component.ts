import { Component } from '@angular/core';
import {LandingpageComponent} from "../landingpage/landingpage.component";
import {PageContentComponent} from "../page-content/page-content.component";
import {FooterComponent} from "../footer/footer.component";

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [
    LandingpageComponent,
    PageContentComponent,
    FooterComponent
  ],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent {

}
