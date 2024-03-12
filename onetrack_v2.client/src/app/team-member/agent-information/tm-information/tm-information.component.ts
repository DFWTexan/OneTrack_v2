import { Component, OnInit, Injectable, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { AgentComService, AgentDataService } from '../../../_services';
import { AgentInfo } from '../../../_Models';
import { ModalService } from '../../../_services';

@Component({
  selector: 'app-tm-information',
  templateUrl: './tm-information.component.html',
  styleUrl: './tm-information.component.css',
})
@Injectable()
export class TmInformationComponent implements OnInit, OnDestroy {
  subscribeAgentInfo: Subscription;
  subscribeAgentLicenseAppointments: Subscription;
  agentInfo: AgentInfo = {} as AgentInfo;

  constructor(
    private agentDataService: AgentDataService,
    public agentComService: AgentComService,
    protected modalService: ModalService
  ) {
    this.subscribeAgentInfo = new Subscription();
    this.subscribeAgentLicenseAppointments = new Subscription();
  }

  ngOnInit() {
    this.subscribeAgentInfo = this.agentDataService.agentInfoChanged.subscribe(
      (agentInfo: any) => {
        this.agentInfo = agentInfo;
      }
    );
  }

  setModeLicenseMgmt(mode: string) {
    this.agentComService.modeLicenseMgmtModal(mode);
  }

  storeLicAppointment(appointment: any) {
    this.agentDataService.storeLicenseAppointment(appointment);
  }

  toggleLicenseMgmt(index: number) {
    this.agentDataService.storeLicenseMgmtDataIndex(index);
    this.agentComService.showLicenseMgmt();
  }

  ngOnDestroy() {
    this.subscribeAgentInfo.unsubscribe();
    this.subscribeAgentLicenseAppointments.unsubscribe();
  }
}
