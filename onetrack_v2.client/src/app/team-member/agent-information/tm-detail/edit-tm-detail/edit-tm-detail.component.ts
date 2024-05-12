import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import { AgentDataService, ConstantsDataService } from '../../../../_services';
import { AgentInfo } from '../../../../_Models';

@Component({
  selector: 'app-edit-tm-detail',
  templateUrl: './edit-tm-detail.component.html',
  styleUrl: './edit-tm-detail.component.css',
})
@Injectable()
export class EditTmDetailComponent implements OnInit, OnDestroy {
  form = new FormGroup({
    employeeID: new FormControl(''),
    lastName: new FormControl(''),
    firstName: new FormControl(''),
    middleName: new FormControl(''),
    employeeSSN: new FormControl(''),
    geid: new FormControl(''),
    address1: new FormControl(''),
    address2: new FormControl(''),
    city: new FormControl(''),
    state: new FormControl(''),
    zip: new FormControl(''),
    phone: new FormControl(''),
    email: new FormControl(''),
    dateOfBirth: new FormControl(''),
    licenseLevel: new FormControl(''),
    licenseIncentive: new FormControl(''),
    isLicenseincentiveSecondChance: new FormControl(''),
  });

  states: any[] = ['Loading...'];
  agentInfo: AgentInfo = {} as AgentInfo;
  subscribeAgentInfo: Subscription;

  constructor(
    private conService: ConstantsDataService,
    private agentService: AgentDataService
  ) {
    this.subscribeAgentInfo = new Subscription();
  }

  ngOnInit(): void {
    this.subscribeAgentInfo = this.agentService.agentInfoChanged.subscribe(
      (agentInfo: any) => {
        this.agentInfo = agentInfo;
        let dateOfBirth = this.agentInfo.dateOfBirth;
        this.form.patchValue({
          employeeID: agentInfo.employeeID,
          lastName: agentInfo.lastName,
          firstName: agentInfo.firstName,
          middleName: agentInfo.middleName,
          employeeSSN: agentInfo.employeeSSN,
          geid: agentInfo.geid,
          address1: agentInfo.address1,
          address2: agentInfo.address2,
          city: agentInfo.city,
          state: agentInfo.state,
          zip: agentInfo.zip,
          phone: agentInfo.phone,
          email: agentInfo.email,
          dateOfBirth: dateOfBirth
            ? formatDate(dateOfBirth, 'yyyy-MM-dd', 'en-US')
            : null,
          licenseLevel: agentInfo.licenseLevel,
          licenseIncentive: agentInfo.licenseIncentive,
          isLicenseincentiveSecondChance:
            agentInfo.isLicenseincentiveSecondChance,
        });
      }
    );
    this.states = ['Select State', ...this.conService.getStates()];
  }

  onSubmit() {
    console.log(this.form.value);
  }

  ngOnDestroy(): void {
    this.subscribeAgentInfo.unsubscribe();
  }
}
function injectable(): (
  target: typeof EditTmDetailComponent
) => void | typeof EditTmDetailComponent {
  throw new Error('Function not implemented.');
}
