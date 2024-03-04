import { Component, OnInit, Injectable } from '@angular/core';

import { AgentLicenseAppointments } from '../../../../_Models';
import { AgentDataService } from '../../../../_services';

@Component({
  selector: 'app-license-info',
  templateUrl: './license-info.component.html',
  styleUrl: './license-info.component.css',
})
@Injectable()
export class LicenseInfoComponent implements OnInit {
  licenseMgmtData: AgentLicenseAppointments[] = [];
  currentIndex: number = 0;

  constructor(private agentDataService: AgentDataService) {}

  ngOnInit() {
    this.currentIndex = this.agentDataService.licenseMgmtDataIndex;
    this.licenseMgmtData =
      this.agentDataService.agentInformation.agentLicenseAppointments;
  }

  nextPage() {
    if (this.currentIndex < this.licenseMgmtData.length - 1) {
      this.currentIndex++;
    }
  }

  previousPage() {
    if (this.currentIndex > 0) {
      this.currentIndex--;
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
