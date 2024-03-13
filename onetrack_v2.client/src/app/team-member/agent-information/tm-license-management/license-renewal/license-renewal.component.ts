import { Component, OnInit, Injectable } from '@angular/core';

import { AgentLicenseAppointments } from '../../../../_Models';
import { AgentComService, AgentDataService, ModalService } from '../../../../_services';

@Component({
  selector: 'app-license-renewal',
  templateUrl: './license-renewal.component.html',
  styleUrl: './license-renewal.component.css'
})
@Injectable()
export class LicenseRenewalComponent implements OnInit {
  licenseMgmtData: AgentLicenseAppointments[] = [];
  currentIndex: number = 0;

  constructor(
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    protected modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.currentIndex = this.agentDataService.licenseMgmtDataIndex;
    this.licenseMgmtData =
      this.agentDataService.agentInformation.agentLicenseAppointments;
    this.agentDataService
      .fetchAgentLicApplicationInfo(
        this.licenseMgmtData[this.currentIndex].employeeLicenseId
      )
      // .subscribe((agentLicApplicationInfo: AgentLicApplicationInfo) => {
      //   this.agentLicApplicationInfo = agentLicApplicationInfo;
      // });
  }

}
