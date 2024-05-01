import { Component } from '@angular/core';
import {MatExpansionModule} from '@angular/material/expansion';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  panelOpenState = false;
}
