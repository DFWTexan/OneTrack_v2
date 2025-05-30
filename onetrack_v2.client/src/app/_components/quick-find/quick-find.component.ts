import { Component, ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';

import {
  AgentComService,
  AppComService,
  EmployeeDataService,
  ModalService,
} from '../../_services';

@Component({
  selector: 'app-quick-find',
  templateUrl: './quick-find.component.html',
  styleUrl: './quick-find.component.css',
})
export class QuickFindComponent {
  // @ViewChild('modalAgentFindInfo') modalAgentFindInfo: ElementRef;
  isLoading: boolean = false;
  tmNumber: string = '';
  agentName: string = '';
  agents: any[] = [];

  isByTMDisabled: boolean = false;
  isByAgentNameDisabled: boolean = false;

  constructor(
    public employeeDataService: EmployeeDataService,
    protected modalService: ModalService,
    public agentComService: AgentComService,
    public appComService: AppComService,
    private router: Router
  ) {}

  onChangeByTmNumber() {
    this.isByTMDisabled = false;
    if (this.tmNumber.length > 0) {
      this.isByAgentNameDisabled = true;
    } else {
      this.isByAgentNameDisabled = false;
    }
  }

  onChangeByAgentName() {
    this.isByTMDisabled = true;
    if (this.agentName.length > 0) {
      this.isByTMDisabled = true;
    } else {
      this.isByTMDisabled = false;
    }
  }

  focusSubmitSearch(event: any) {
    if (event.key === 'Tab') {
      event.preventDefault();
      const submitSearchButton = document.getElementById('submitSearch');
      if (submitSearchButton) {
        submitSearchButton.focus();
      }
    }
  }

  onSearch() {
    this.isByAgentNameDisabled = false;
    this.isByTMDisabled = false;

    if (this.tmNumber.length > 0) {
      this.isLoading = true;

      this.employeeDataService
        .fetchEmployeeByTmNumber(this.tmNumber.trim())
        .subscribe((response) => {
          this.isLoading = false;
          this.tmNumber = '';

          this.isByAgentNameDisabled = false;
          this.isByTMDisabled = false;

          this.agentComService.updateShowLicenseMgmt(false);
          if (response === null) {
            alert('Employee not found');
          } else {
            this.appComService.updateIsAllAgentsSelected(false);
            this.router.navigate([
              '../../team/agent-info',
              response.employeeId,
              'tm-info-mgmt',
            ]);
          }
        });
    } else if (this.agentName.length > 0) {
      this.isLoading = true;

      this.employeeDataService
        .fetchEmployeeByAgentName(this.agentName.trim())
        .subscribe((response) => {
          this.agentName = '';

          this.isByAgentNameDisabled = false;
          this.isByTMDisabled = false;
          this.isLoading = false;

          if (response.length === 0) {
            this.isLoading = false;
            alert('Employee not found');
            return;
          } else if (response.length === 1) {
            // this.isLoading = false;
            // this.agentName = '';
            this.appComService.updateIsAllAgentsSelected(false);
            this.router.navigate([
              '../../team/agent-info',
              response[0].employeeId,
              'tm-info-mgmt',
            ]);
            return;
          } else {
            this.agentComService.updateShowLicenseMgmt(false);
            // this.isLoading = false;
            // this.agentName = '';

            // console.log('Search by Agent Name: ', response);
            this.agents = response;
            this.openModal();
          }
        });
    }
  }

  openModal() {
    this.modalService.open('modal-agent-find-info');
  }

  onSelectAgent(agent: any) {
    this.agentName = '';
    this.isByTMDisabled = false;
    this.onCloseModal();

    this.router.navigate([
      '../../team/agent-info',
      agent.employeeId,
      'tm-info-mgmt',
    ]);
  }

  onCloseModal() {
    this.agentName = '';
    this.isByTMDisabled = false;
    const modalDiv = document.getElementById('modal-agent-find-info');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }
}
