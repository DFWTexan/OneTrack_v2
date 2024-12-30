import { Component } from '@angular/core';
import { Router } from '@angular/router';

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

  constructor(
    public employeeDataService: EmployeeDataService,
    private router: Router
  ) {}

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
    if (this.tmNumber.length > 0) {
      this.employeeDataService
        .fetchEmployeeByTmNumber(this.tmNumber)
        .subscribe((response) => {
          if (response === null) {
            alert('Employee not found');
          } else {
            this.tmNumber = '';
            this.isByAgentNameDisabled = false;
            this.router.navigate([
              '../../team/agent-info',
              response.employeeId,
              'tm-info-mgmt',
            ]);
          }
        });
    } else if (this.agentName.length > 0) {
      this.employeeDataService
        .fetchEmployeeByAgentName(this.agentName)
        .subscribe((response) => {
          console.log('Search by Agent Name: ', response);
        });
    }
  }
}
