import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AgentComService,
  AgentDataService,
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
    // this.agentComService.viewCommunication(communication);
    this.selectedCommunicationItem = vCommunicationItem;
    this.modalService.open('modal-view-employee-communication');
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
