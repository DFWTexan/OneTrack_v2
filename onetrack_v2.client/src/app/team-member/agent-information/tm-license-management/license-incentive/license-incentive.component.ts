import { Component, OnInit, Injectable } from '@angular/core';

import {
  // AgentLicApplicationInfo,
  AgentLicenseAppointments,
} from '../../../../_Models';
import {
  AgentComService,
  AgentDataService,
  ModalService,
} from '../../../../_services';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-license-incentive',
  templateUrl: './license-incentive.component.html',
  styleUrl: './license-incentive.component.css',
})
@Injectable()
export class LicenseIncentiveComponent implements OnInit {
  modeEdit: boolean = false;
  licenseMgmtData: AgentLicenseAppointments[] = [];
  currentIndex: number = 0;
  panelOpenState = false;

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
  }

  onEditToggle() {
    this.modeEdit = !this.modeEdit;
  }

  onSubmit(form: NgForm) {
    this.modeEdit = false;
    console.log(form);
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
}
