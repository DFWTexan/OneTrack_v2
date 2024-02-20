import { HttpClient } from '@angular/common/http';
import { Component, ViewChild, OnInit } from '@angular/core';
import { BreakpointObserver } from '@angular/cdk/layout';
import { MatSidenav } from '@angular/material/sidenav';

// interface WeatherForecast {
//   date: string;
//   temperatureC: number;
//   temperatureF: number;
//   summary: string;
// }

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  // public forecasts: WeatherForecast[] = [];
  // title = 'material-responsive-sidenav';
  @ViewChild(MatSidenav)
  sidenav!: MatSidenav;
  isMobile = true;
  isCollapsed = true;

  constructor(private http: HttpClient, private observer: BreakpointObserver) {}

  ngOnInit() {
    // this.getForecasts();
    this.observer.observe(['(max-width: 800px)']).subscribe((screenSize) => {
      if (screenSize.matches) {
        this.isMobile = true;
      } else {
        this.isMobile = false;
      }
    });
  }

  // getForecasts() {
  //   this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
  //     (result) => {
  //       this.forecasts = result;
  //     },
  //     (error) => {
  //       console.error(error);
  //     }
  //   );
  // }
  toggleMenu() {
    this.sidenav.open(); // On desktop/tablet, the menu can never be fully closed
    this.isCollapsed = !this.isCollapsed;
  }

  title = 'onetrack_v2.client';
}
