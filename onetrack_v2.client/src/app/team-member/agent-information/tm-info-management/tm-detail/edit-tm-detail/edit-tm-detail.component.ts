import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import {
  AgentDataService,
  ConstantsDataService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../../../_services';
import { AgentInfo } from '../../../../../_Models';

@Component({
  selector: 'app-edit-tm-detail',
  templateUrl: './edit-tm-detail.component.html',
  styleUrl: './edit-tm-detail.component.css',
})
@Injectable()
export class EditTmDetailComponent implements OnInit, OnDestroy {
  form = new FormGroup({
    employeeID: new FormControl(''),
    lastName: new FormControl('', Validators.required),
    firstName: new FormControl('', Validators.required),
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
    secondChance: new FormControl(''),
  });

  isFormSubmitted = false;
  states: any[] = ['Loading...'];
  licenseLevels: any[] = [];
  licenseIncentives: any[] = [];
  agentInfo: AgentInfo = {} as AgentInfo;

  private subscriptions = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    private agentService: AgentDataService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.subscriptions.add(
      this.agentService.agentInfoChanged.subscribe((agentInfo: any) => {
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
          secondChance: agentInfo.isLicenseincentiveSecondChance,
        });
      })
    );

    this.states = ['Select State', ...this.conService.getStates()];
    this.subscriptions.add(
      this.agentService
        .fetchLicenseLevels()
        .subscribe((licenseLevels: any[]) => {
          this.licenseLevels = [
            { value: 'Select License Level' },
            ...licenseLevels,
          ];
        })
    );

    this.subscriptions.add(
      this.agentService
        .fetchLicenseIncentives()
        .subscribe((licIncentives: any[]) => {
          this.licenseIncentives = [
            { value: 'Select Incentive' },
            ...licIncentives,
          ];
        })
    );
  }

  onSubmit() {
    this.isFormSubmitted = true;
    console.log('EMFTEST () - this.form.value => \n', this.form.value);

    if (!this.form || !this.agentInfo) {
      // Handle the case where this.form or this.agentInfo is null or undefined
      return;
  }
    let agent: any = this.form.value;
    
    agent.employeeSSN = this.agentInfo.employeeSSN.replace(/-/g, '');
    agent.employeeID = this.agentInfo.employeeID;
    //--> agent.lastName = this.agentInfo.lastName.toUpperCase();
    //--> agent.firstName = this.agentInfo.firstName.toUpperCase();
    //--> agent.middleName = this.agentInfo?.middleName?.toUpperCase();
    //--> agent.soeid = this.agentInfo.soeid;
    //--> agent.dateOfBirth = this.agentInfo.dateOfBirth;
    agent.nationalProducerNumber = this.agentInfo.nationalProdercerNumber;
    //--> agent.geid = this.agentInfo.geid;
    //--> agent.alias = this.agentInfo.alias;
    //--> agent.excludeFromRpts = this.agentInfo.excludeFromReports;
    //--> agent.address1 = this.agentInfo.address1;
    //--> agent.address2 = this.agentInfo?.address2;
    //--> agent.city = this.agentInfo.city;
    //--> agent.state = this.agentInfo.state;
    //--> agent.zip = this.agentInfo.zip; 
    //--> agent.phone = this.agentInfo.phone;
    agent.fax = this.agentInfo.branchDeptFax;
    agent.employmentID = this.agentInfo.employmentID;
    //--> agent.email = this.agentInfo.email;
    agent.workPhone = this.agentInfo.phone;
    agent.employeeStatus = this.agentInfo.employeeStatus;
    agent.companyID = this.agentInfo.companyID;
    agent.ceRequired = this.agentInfo.ceRequired;
    agent.licenseLevel = this.agentInfo.licenseLevel;
    agent.licenseIncentive = this.agentInfo.licenseIncentive;
    agent.secondChance = this.agentInfo.isLicenseincentiveSecondChance;
    agent.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    // if (agent.employerAgency === 0 || agent.employerAgency === '') {
    //   this.newAgentForm.controls['employerAgency'].setErrors({ invalid: true });
    // }

    // if (agent.workState === 'Select State') {
    //   this.newAgentForm.controls['workState'].setErrors({ invalid: true });
    // }

    // if (agent.resState === 'Select State') {
    //   this.newAgentForm.controls['resState'].setErrors({ invalid: true });
    // }

    // if (agent.branchCode === 'Select Branch Code') {
    //   agent.branchCode = '';
    //   // this.newAgentForm.controls['branchCode'].setErrors({ invalid: true });
    // }

    if (this.form.invalid) {
      this.form.setErrors({ invalid: true });
      return;
    }

    this.subscriptions.add(
      this.agentService.updateAgent(agent).subscribe({
        next: (response) => {
          this.forceCloseModal();
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
          }
        },
      })
    );
  }

  forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-tm-detail');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  closeModal() {
    // const modalDiv = document.getElementById('modal-edit-tm-detail');
    // if (modalDiv != null) {
    //   modalDiv.style.display = 'none';
    // }
    if (this.form.dirty && !this.isFormSubmitted) {
      if (
        confirm('You have unsaved changes. Are you sure you want to close?')
      ) {
        this.forceCloseModal();
        this.form.reset();
      }
    } else {
      this.isFormSubmitted = false;
      this.forceCloseModal();
    }
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}

function injectable(): (
  target: typeof EditTmDetailComponent
) => void | typeof EditTmDetailComponent {
  throw new Error('Function not implemented.');
}
