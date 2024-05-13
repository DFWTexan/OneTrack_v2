import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AgentInfo,
  CompanyRequirementsHistory,
  EmploymentHistory,
  EmploymentJobTitleHistory,
  TransferHistory,
} from '../../../_Models';
import {
  AgentComService,
  AgentDataService,
  AppComService,
  DropdownDataService,
  MiscDataService,
  ModalService,
  UserAcctInfoDataService,
} from '../../../_services';

@Component({
  selector: 'app-tm-emptrans-history',
  templateUrl: './tm-emptrans-history.component.html',
  styleUrl: './tm-emptrans-history.component.css',
})
@Injectable()
export class TmEmptransHistoryComponent implements OnInit, OnDestroy {
  agentInfo: AgentInfo = {} as AgentInfo;
  subscribeAgentInfo: Subscription;

  employmentHistory: EmploymentHistory[] = [];
  transferHistory: TransferHistory[] = [];
  companyRequirementsHistory: CompanyRequirementsHistory[] = [];
  employmentJobTitleHistory: EmploymentJobTitleHistory[] = [];

  constructor(
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    protected modalService: ModalService,
    public appComService: AppComService,
    public userInfoDataService: UserAcctInfoDataService
  ) {
    this.subscribeAgentInfo = new Subscription();
  }

  ngOnInit(): void {
    this.subscribeAgentInfo = this.agentDataService.agentInfoChanged.subscribe(
      (agentInfo: any) => {
        // this.agentInfo = agentInfo;
        this.employmentHistory = agentInfo.employmentHistory;
        this.transferHistory = agentInfo.transferHistory;
        this.companyRequirementsHistory = agentInfo.compayRequirementsHistory;
        this.employmentJobTitleHistory = agentInfo.employmentJobTitleHistory;
      }
    );
  }

  // onNgOnChanges(): void {
  //   this.agentInfo = this.agentDataService.getAgentInfo();
  //   this.employmentHistory = this.agentInfo.employmentHistory;
  //   this.transferHistory = this.agentInfo.transferHistory;
  //   this.companyRequirementsHistory = this.agentInfo.compayRequirementsHistory;
  //   this.employmentJobTitleHistory = this.agentInfo.employmentJobTitleHistory;
  // }

  deleteEmploymentHistory(empItem: any): void {
    this.agentDataService
      .deleteEmploymentHistItem({
        employmentID: this.agentDataService.agentInformation.employmentID,
        employmentHistoryID: empItem.employmentHistoryID,
        userSOEID: this.userInfoDataService.userAcctInfo.soeid,
      })
      .subscribe({
        next: (response) => {
          // console.log(
          //   'EMFTEST (app-tm-emptrans-history: deleteEmploymentHistory) - COMPLETED DELETE response => \n',
          //   response
          // );
        },
        error: (error) => {
          console.error(error);
          // handle the error here
        },
      });
  }

  ngOnDestroy(): void {
    this.subscribeAgentInfo.unsubscribe();
  }
}
