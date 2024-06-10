import { Component, OnInit, Injectable, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AgentLicApplicationInfo,
  AgentLicenseAppointments,
} from '../../../../_Models';
import {
  AgentComService,
  AgentDataService,
  AppComService,
  ModalService,
} from '../../../../_services';

@Component({
  selector: 'app-license-application',
  templateUrl: './license-application.component.html',
  styleUrl: './license-application.component.css',
})
@Injectable()
export class LicenseApplicationComponent implements OnInit, OnDestroy {
  licenseMgmtData: AgentLicenseAppointments[] = [];
  currentIndex: number = 0;
  panelOpenState = false;
  agentLicApplicationInfo: AgentLicApplicationInfo =
    {} as AgentLicApplicationInfo;

    private subscriptions = new Subscription();

  constructor(
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    public appComService: AppComService,
    protected modalService: ModalService
  ) {
    this.agentDataService.agentLicApplicationInfo.licenseApplicationItems = [];
    this.agentDataService.agentLicApplicationInfo.licensePreEducationItems = [];
    this.agentDataService.agentLicApplicationInfo.licensePreExamItems = [];
  }

  ngOnInit(): void {
    this.currentIndex = this.agentDataService.licenseMgmtDataIndex;
    this.licenseMgmtData =
      this.agentDataService.agentInformation.agentLicenseAppointments;
    
      this.getData();
      
  }

  onChildCallGetData(): void {
    this.getData();
  }

  getData() {
    this.subscriptions.add(
      this.agentDataService
      .fetchAgentLicApplicationInfo(
        this.licenseMgmtData[this.currentIndex].employeeLicenseId
      )
      .subscribe((agentLicApplicationInfo: AgentLicApplicationInfo) => {
        this.agentLicApplicationInfo = agentLicApplicationInfo;
      })
    );
  }

  // Pagination
  nextPage() {
    if (this.currentIndex < this.licenseMgmtData.length - 1) {
      this.currentIndex++;
      this.agentDataService.licenseMgmtDataIndexChanged.next(this.currentIndex);
    }
  }

  previousPage() {
    if (this.currentIndex > 0) {
      this.currentIndex--;
      this.agentDataService.licenseMgmtDataIndexChanged.next(this.currentIndex);
    }
  }

  getPageInfo(): string {
    return `${this.currentIndex + 1} of ${this.licenseMgmtData.length}`;
  }

  isDisplayPrevious(): boolean {
    return this.currentIndex > 0;
  }

  isDisplayNext(): boolean {
    return this.currentIndex < this.licenseMgmtData.length - 1;
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
