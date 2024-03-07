import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AgentDataService } from '../../../../_services';
import { AgentInfo } from '../../../../_Models';

@Component({
  selector: 'app-edit-tm-detail',
  templateUrl: './edit-tm-detail.component.html',
  styleUrl: './edit-tm-detail.component.css'
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

  agentInfo: AgentInfo = {} as AgentInfo;
  subscribeAgentInfo: Subscription;

  constructor(private agentService: AgentDataService) {
    this.subscribeAgentInfo = new Subscription();
  }

  ngOnInit(): void {
      this.subscribeAgentInfo = this.agentService.agentInfoChanged.subscribe(
      (agentInfo: any) => {
        this.agentInfo = agentInfo;
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
          dateOfBirth: agentInfo.dateOfBirth,
          licenseLevel: agentInfo.licenseLevel,
          licenseIncentive: agentInfo.licenseIncentive,
          isLicenseincentiveSecondChance: agentInfo.isLicenseincentiveSecondChance,
        });
      }
    );
    //   this.form.patchValue({
    //   employeeID: this.agentService.agentInformation.employeeID ? this.agentService.agentInformation.employeeID.toString() : '',
    //   lastName: this.agentService.agentInformation.lastName,
    //   firstName: this.agentService.agentInformation.firstName,
    //   middleName: this.agentService.agentInformation.middleName,
    //   employeeSSN: this.agentService.agentInformation.employeeSSN,
    //   geid: this.agentService.agentInformation.geid,
    //   address1: this.agentService.agentInformation.address1,
    //   address2: this.agentService.agentInformation.address2,
    //   city: this.agentService.agentInformation.city,
    //   state: this.agentService.agentInformation.state,
    //   zip: this.agentService.agentInformation.zip,
    //   phone: this.agentService.agentInformation.phone,
    //   email: this.agentService.agentInformation.email,
    //   dateOfBirth: this.agentService.agentInformation.dateOfBirth,
    //   licenseLevel: this.agentService.agentInformation.licenseLevel,
    //   licenseIncentive: this.agentService.agentInformation.licenseIncentive,
    //   isLicenseincentiveSecondChance: this.agentService.agentInformation.isLicenseincentiveSecondChance ? 'true' : 'false',
    // });
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
