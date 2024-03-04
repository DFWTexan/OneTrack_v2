import { Component, OnInit, Injectable, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { AgentDataService } from '../../../_services';
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
  bodyText = 'This text can be updated in modal 1';

  constructor(
    private agentService: AgentDataService,
    protected modalService: ModalService
  ) {
    this.subscribeAgentInfo = new Subscription();
    this.subscribeAgentLicenseAppointments = new Subscription();
  }

  ngOnInit() {
    this.subscribeAgentInfo = this.agentService.agentInfoChanged.subscribe(
      (agentInfo: any) => {
        this.agentInfo = agentInfo;
      }
    );
  }

  storeLicAppointment(appointment: any) {
    console.log(
      'EMFTEST - (app-tm-information) storeLicAppointment => \n',
      appointment
    );

    this.agentService.storeLicenseAppointment(appointment);
  }

  openModal() {
    const modalDiv = document.getElementById('myModal');
    if (modalDiv != null) {
      modalDiv.style.display = 'block';
    }
  }
  closeModal() {
    const modalDiv = document.getElementById('myModal');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy() {
    this.subscribeAgentInfo.unsubscribe();
    this.subscribeAgentLicenseAppointments.unsubscribe();
  }
}
