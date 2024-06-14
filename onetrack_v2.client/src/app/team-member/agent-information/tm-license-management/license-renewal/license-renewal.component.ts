import { Component, OnInit, Injectable, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AgentLicApplicationInfo,
  AgentLicenseAppointments,
} from '../../../../_Models';
import {
  AgentComService,
  AgentDataService,
  ModalService,
} from '../../../../_services';

@Component({
  selector: 'app-license-renewal',
  templateUrl: './license-renewal.component.html',
  styleUrl: './license-renewal.component.css',
})
@Injectable()
export class LicenseRenewalComponent implements OnInit, OnDestroy {
  licenseMgmtData: AgentLicenseAppointments[] = [];
  currentIndex: number = 0;
  agentLicApplicationInfo: AgentLicApplicationInfo =
    {} as AgentLicApplicationInfo;

  private subscriptions = new Subscription();

  constructor(
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    protected modalService: ModalService
  ) {
    this.agentDataService.agentLicApplicationInfo.licenseRenewalItems = [];
  }

  ngOnInit(): void {
    this.currentIndex = this.agentDataService.licenseMgmtDataIndex;
    this.subscriptions.add(
      this.agentDataService.licenseMgmtDataIndexChanged.subscribe(
        (index: number) => {
          this.currentIndex = index;
        }
      )
    );

    this.licenseMgmtData =
      this.agentDataService.agentInformation.agentLicenseAppointments;
    this.subscriptions.add(
      this.agentDataService.licenseMgmtDataChanged.subscribe(
        (licenseMgmtData: AgentLicenseAppointments) => {
          this.licenseMgmtData = [licenseMgmtData];
        }
      )
    );

    // this.subscriptions.add(
    //   this.agentDataService
    //   .fetchAgentLicApplicationInfo(
    //     this.licenseMgmtData[this.currentIndex].employeeLicenseId
    //   )
    //   .subscribe((agentLicApplicationInfo: AgentLicApplicationInfo) => {
    //     this.agentLicApplicationInfo = agentLicApplicationInfo;
    //   })
    // );
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
  // nextPage() {
  //   if (this.currentIndex < this.licenseMgmtData.length - 1) {
  //     this.currentIndex++;
  //     this.agentDataService.licenseMgmtDataIndexChanged.next(this.currentIndex);
  //   }
  // }

  // previousPage() {
  //   if (this.currentIndex > 0) {
  //     this.currentIndex--;
  //     this.agentDataService.licenseMgmtDataIndexChanged.next(this.currentIndex);
  //   }
  // }

  // getPageInfo(): string {
  //   return `${this.currentIndex + 1} of ${this.licenseMgmtData.length}`;
  // }

  // isDisplayPrevious(): boolean {
  //   return this.currentIndex > 0;
  // }

  // isDisplayNext(): boolean {
  //   return this.currentIndex < this.licenseMgmtData.length - 1;
  // }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  } 
}
