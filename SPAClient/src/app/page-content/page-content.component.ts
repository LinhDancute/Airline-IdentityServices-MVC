import { Component } from '@angular/core';
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-page-content',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './page-content.component.html',
  styleUrls: ['./page-content.component.css', '../../assets/css/styles.css']
})
export class PageContentComponent {

}
