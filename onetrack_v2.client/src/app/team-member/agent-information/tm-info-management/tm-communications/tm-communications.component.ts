import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AgentComService,
  AgentDataService,
  AppComService,
  EmployeeDataService,
  ModalService,
} from '../../../../_services';
import { AgentInfo } from '../../../../_Models';

@Component({
  selector: 'app-tm-communications',
  templateUrl: './tm-communications.component.html',
  styleUrl: './tm-communications.component.css',
})
@Injectable()
export class TmCommunicationsComponent implements OnInit, OnDestroy {
  agentInfo: AgentInfo = {} as AgentInfo;
  selectedCommunicationItem: any;

  private subscriptions = new Subscription();

  constructor(
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    private appComService: AppComService,
    private employeeDataService: EmployeeDataService,
    protected modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.subscriptions.add(
      this.agentDataService.agentInfoChanged.subscribe((agentInfo: any) => {
        this.agentInfo = agentInfo;
      })
    );
  }

  onViewCommunication(vCommunicationItem: any) {
    // console.log('EMFTEST (tm-communications.component: onViewCommunication) - vCommunicationItem => \n', vCommunicationItem);

    this.appComService.updateSelectedEmploymentCommunicationID(
      vCommunicationItem.employmentCommunicationID
    );
    this.subscriptions.add(
      this.employeeDataService
        .fetchEmployeeCommunicationByID(
          vCommunicationItem.employmentCommunicationID
        )
        .subscribe((response) => {
          // this.employeeDataService.employmentCommunicationItem = response;
          // this.employeeDataService.employmentCommunicationItemChanged.next(this.employeeDataService.employmentCommunicationItem);
          this.selectedCommunicationItem = vCommunicationItem;
          this.modalService.open('modal-view-employee-communication');
        })
    );
  }

  // onViewEmployeeCommunication(vCommunicationItem: any) {

  // }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
