import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tickler-mgmt',
  templateUrl: './tickler-mgmt.component.html',
  styleUrl: './tickler-mgmt.component.css'
})
export class TicklerMgmtComponent {
  
  constructor(public router: Router) { }

}
