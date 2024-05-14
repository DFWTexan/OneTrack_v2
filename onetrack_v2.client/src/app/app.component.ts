import { Component, ViewChild, OnInit } from '@angular/core';
import { BreakpointObserver } from '@angular/cdk/layout';
import { MatSidenav } from '@angular/material/sidenav';

import { AppComService } from './_services';

// interface WeatherForecast {
//   date: string;
//   temperatureC: number;
//   temperatureF: number;
//   summary: string;
// }

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
  // template: `<app-side-menu-nav></app-side-menu-nav>`
})
export class AppComponent implements OnInit {
  // public forecasts: WeatherForecast[] = [];
  // title = 'material-responsive-sidenav';
  
  // @ViewChild(MatSidenav)
  // sidenav!: MatSidenav;
  // isMobile = true;
  // isCollapsed = true;

  constructor(public appComService: AppComService) {}

  ngOnInit() {}

  title = 'onetrack_v2';
}
