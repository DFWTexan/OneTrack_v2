import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { AgentDataService, ModalService } from '../../../../_services';
import { AgentInfo } from '../../../../_Models';

@Component({
  selector: 'app-tm-detail',
  templateUrl: './tm-detail.component.html',
  styleUrl: './tm-detail.component.css',
})
@Injectable()
export class TmDetailComponent implements OnInit, OnDestroy {
  agentInfo: AgentInfo = {} as AgentInfo;

  private subscriptions = new Subscription();

  constructor(
    private agentDataService: AgentDataService,
    protected modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.subscriptions.add(
      this.agentDataService.agentInfoChanged.subscribe((agentInfo: any) => {
        this.agentInfo = agentInfo;
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
