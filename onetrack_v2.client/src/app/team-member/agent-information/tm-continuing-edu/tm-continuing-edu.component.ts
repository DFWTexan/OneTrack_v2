import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { AgentComService, AgentDataService, ModalService } from '../../../_services';
import { AgentInfo } from '../../../_Models';

@Component({
  selector: 'app-tm-continuing-edu',
  templateUrl: './tm-continuing-edu.component.html',
  styleUrl: './tm-continuing-edu.component.css',
})
@Injectable()
export class TmContinuingEduComponent implements OnInit, OnDestroy {
  agentInfo: AgentInfo = {} as AgentInfo;
  subscribeAgentInfo: Subscription;
  sumHoursRequired: number = 0;
  sumHoursTaken: number = 0;
  totalHoursRemaining: number = 0;

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
        this.agentInfo = agentInfo;

        this.sumHoursRequired = this.agentInfo.contEduRequiredItems.reduce(
          (total, item) => total + (item.requiredCreditHours || 0),
          0
        );
        this.sumHoursTaken = this.agentInfo.contEduCompletedItems.reduce(
          (total, item) => total + (item.creditHoursTaken || 0),
          0
        );
        this.totalHoursRemaining = this.sumHoursRequired - this.sumHoursTaken;
      }
    );
  }

  ngOnDestroy(): void {
    this.subscribeAgentInfo.unsubscribe();
  }
}
