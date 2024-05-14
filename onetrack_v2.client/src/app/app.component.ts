import { Component, ViewChild, OnInit } from '@angular/core';
import { BreakpointObserver } from '@angular/cdk/layout';
import { MatSidenav } from '@angular/material/sidenav';

import { AppComService } from './_services';
// import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from './_components/confirm-dialog/confirm-dialog.component';

// interface WeatherForecast {
//   date: string;
//   temperatureC: number;
//   temperatureF: number;
//   summary: string;
// }

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  // template: `<app-side-menu-nav></app-side-menu-nav>`
})
export class AppComponent implements OnInit {
  // public forecasts: WeatherForecast[] = [];
  // title = 'material-responsive-sidenav';

  // @ViewChild(MatSidenav)
  // sidenav!: MatSidenav;
  // isMobile = true;
  // isCollapsed = true;

  constructor(public appComService: AppComService 
    // public dialog: MatDialog
  ) {}

  ngOnInit() {}

  title = 'onetrack_v2';

  // openConfirmDialog(): void {
  //   const dialogRef = this.dialog.open(ConfirmDialogComponent, {
  //     width: '250px',
  //     data: {
  //       title: 'Confirm Action',
  //       message: 'Are you sure you want to do this?',
  //     },
  //   });

  //   dialogRef.afterClosed().subscribe((result) => {
  //     console.log('The dialog was closed');
  //     console.log('Confirmation result:', result);
  //   });
  // }
}
