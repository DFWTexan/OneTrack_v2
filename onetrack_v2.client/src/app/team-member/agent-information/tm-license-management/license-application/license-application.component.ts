import { Component, OnInit, Injectable } from '@angular/core';

import { AgentLicenseAppointments } from '../../../../_Models';
import { AgentComService, AgentDataService, ModalService } from '../../../../_services';

@Component({
  selector: 'app-license-application',
  templateUrl: './license-application.component.html',
  styleUrl: './license-application.component.css'
})
@Injectable()
export class LicenseApplicationComponent implements OnInit {
  licenseMgmtData: AgentLicenseAppointments[] = [];
  currentIndex: number = 0;

  constructor(
    protected agentDataService: AgentDataService,
    public agentComService: AgentComService,
    protected modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.currentIndex = this.agentDataService.licenseMgmtDataIndex;
    this.licenseMgmtData =
      this.agentDataService.agentInformation.agentLicenseAppointments;
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
