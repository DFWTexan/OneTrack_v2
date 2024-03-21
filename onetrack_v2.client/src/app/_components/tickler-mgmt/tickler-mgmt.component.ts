import { Component } from '@angular/core';

@Component({
  selector: 'app-tickler-mgmt',
  templateUrl: './tickler-mgmt.component.html',
  styleUrl: './tickler-mgmt.component.css'
})
export class TicklerMgmtComponent {
  tabs = [
    { link: 'tic-info', label: 'Tickler' },
    { link: 'tic-search', label: 'Search' },
    // Add more tabs here...
  ];

}
