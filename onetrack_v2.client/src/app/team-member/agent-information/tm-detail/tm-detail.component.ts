import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { AgentDataService, ModalService } from '../../../_services';
import { AgentInfo } from '../../../_Models';

@Component({
  selector: 'app-tm-detail',
  templateUrl: './tm-detail.component.html',
  styleUrl: './tm-detail.component.css',
})
@Injectable()
export class TmDetailComponent implements OnInit, OnDestroy {
  agentInfo: AgentInfo = {} as AgentInfo;
  subscribeAgentInfo: Subscription;

  constructor(
    private agentDataService: AgentDataService,
    protected modalService: ModalService
  ) {
    this.subscribeAgentInfo = new Subscription();
  }

  ngOnInit(): void {
    this.subscribeAgentInfo = this.agentDataService.agentInfoChanged.subscribe(
      (agentInfo: any) => {
        this.agentInfo = agentInfo;
      }
    );
  }

  ngOnDestroy(): void {
    this.subscribeAgentInfo.unsubscribe();
  }
}
