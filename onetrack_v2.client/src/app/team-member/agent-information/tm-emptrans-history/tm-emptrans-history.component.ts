import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { AgentInfo, CompanyRequirementsHistory, EmploymentHistory, EmploymentJobTitleHistory, TransferHistory } from '../../../_Models';
import { AgentComService, AgentDataService, ModalService } from '../../../_services';

@Component({
  selector: 'app-tm-emptrans-history',
  templateUrl: './tm-emptrans-history.component.html',
  styleUrl: './tm-emptrans-history.component.css'
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
    protected modalService: ModalService
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

  ngOnDestroy(): void {
    this.subscribeAgentInfo.unsubscribe();
  }
}
