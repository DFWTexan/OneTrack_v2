import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-view-employee-communication',
  templateUrl: './view-employee-communication.component.html',
  styleUrl: './view-employee-communication.component.css'
})
export class ViewEmployeeCommunicationComponent {
 @Input() communicationItem: any;

}
