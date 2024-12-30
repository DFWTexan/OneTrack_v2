import { Component } from '@angular/core';

import { EmployeeDataService } from '../../_services';

@Component({
  selector: 'app-quick-find',
  templateUrl: './quick-find.component.html',
  styleUrl: './quick-find.component.css',
})
export class QuickFindComponent {
  tmNumber: string = '';
  agentName: string = '';

  isByTMDisabled: boolean = false;
  isByAgentNameDisabled: boolean = false;

  constructor(public employeeDataService: EmployeeDataService) {}

  onChangeByTmNumber() {
    this.isByTMDisabled = false;
    if (this.tmNumber.length > 0) {
      this.isByAgentNameDisabled = true;
    } else {
      this.isByAgentNameDisabled = false;
    }
  }

  onChangeByAgentName() {
    this.isByTMDisabled = true;
    if (this.agentName.length > 0) {
      this.isByTMDisabled = true;
    } else {
      this.isByTMDisabled = false;
    }
  }

  onSearch() {
    if(this.tmNumber.length > 0) {
      this.employeeDataService.fetchEmployeeByTmNumber(this.tmNumber).subscribe((response) => {
        console.log('Search by TM Number: ', response);
      });
    } else if(this.agentName.length > 0) {
      this.employeeDataService.fetchEmployeeByAgentName(this.agentName).subscribe((response) => {
        console.log('Search by Agent Name: ', response);
      });
    }
  }
}
