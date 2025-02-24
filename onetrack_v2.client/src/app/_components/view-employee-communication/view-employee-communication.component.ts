import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { AppComService, EmployeeDataService } from '../../_services';

@Component({
  selector: 'app-view-employee-communication',
  templateUrl: './view-employee-communication.component.html',
  styleUrl: './view-employee-communication.component.css',
})
export class ViewEmployeeCommunicationComponent implements OnInit, OnDestroy {
  empCommID: number = 0;
  empCommItem: any;
  attachments: any[] = [];

  private subscriptions = new Subscription();

  constructor(
    public appComService: AppComService,
    private employeeDataService: EmployeeDataService
  ) {}

  ngOnInit(): void {
    this.empCommID = this.appComService.selectedEmploymentCommunicationID;
    this.subscriptions.add(
      this.employeeDataService.employmentCommunicationItemChanged.subscribe(
        (response) => {
          this.empCommItem = response;
  
          // Split the emailAttachments string by '|' and assign to this.attachments
          if (response.emailAttachments) {
            this.attachments = response.emailAttachments.split('|');
          }
  
console.log('EMFTEST (view-employee-communication.component: ngOnInit) - this.empCommItem => \n', this.empCommItem);
console.log('Attachments:', this.attachments);
        }
      )
    );
  }

  // getData() {
  //   this.subscriptions.add(
  //     this.employeeDataService
  //       .fetchEmployeeCommunicationByID(this.empCommID)
  //       .subscribe(
  //         (response) => {
  //           this.empCommItem = response;
  //         },
  //         (error) => {
  //           console.log(
  //             'EMFTEST (view-employee-communication.component: getData) - error => \n',
  //             error
  //           );
  //         }
  //       )
  //   );
  // }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
