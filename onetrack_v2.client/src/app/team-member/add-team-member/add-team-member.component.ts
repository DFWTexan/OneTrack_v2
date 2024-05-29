import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Subscription, zip } from 'rxjs';

import {
  AgentDataService,
  ConstantsDataService,
  DropdownDataService,
  UserAcctInfoDataService,
} from '../../_services';

@Component({
  selector: 'app-add-team-member',
  templateUrl: './add-team-member.component.html',
  styleUrl: './add-team-member.component.css',
})
export class AddTeamMemberComponent implements OnInit, OnDestroy {
  formSubmitted = false;
  newAgentForm: FormGroup;
  states: any[] = ['Select State', ...this.conService.getStates()];
  employerAgencies: any[] = [{ value: '0', label: 'Loading...' }, 'Loading...'];
  branchCodes: any[] = [{ branchCode: 'Loading...' }];
  jobTitles: any[] = [{ value: 0, label: 'Loading...' }];

  private subscriptions = new Subscription();

  constructor(
    private fb: FormBuilder,
    private conService: ConstantsDataService,
    private drpdwnDataService: DropdownDataService,
    private agentDataService: AgentDataService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {
    this.newAgentForm = this.fb.group({
      FirstName: ['', Validators.required],
      MiddleName: [''],
      LastName: ['', Validators.required],
      preferedName: [''],
      DateOfBirth: ['01/01/0001 00:00:00'],
      EmployeeSSN: [''],
      GEID: [''],
      SOEID: [''],
      NationalProducerNumber: [0],
      CompanyID: ['', Validators.required],
      // employerAgency: ['', [Validators.required, this.employerAgencyValidator]],
      WorkStateAbv: ['Select State'],
      ResStateAbv: ['Select State'],
      JobTitleID: [0],
      HireDate: ['01/01/0001 00:00:00'],
      BranchCode: [''],
      Address1: [''],
      Address2: [''],
      City: [''],
      Zip: [''],
      Email: [''],
      Phone: [''],
      WorkPhone: [''],
      Fax: [''],
    });
  }

  ngOnInit() {
    this.subscriptions.add(
      this.drpdwnDataService
        .fetchDropdownData('GetEmployerAgencies')
        .subscribe((employerAgencies: { value: string; label: string }[]) => {
          this.employerAgencies = [
            { valu: '0', label: 'Select Employer/Agency' },
            ...employerAgencies,
          ];
          this.newAgentForm.get('employerAgency')?.setValue(0);
        })
    );

    this.subscriptions.add(
      this.drpdwnDataService
        .fetchDropdownData('GetJobTitles')
        .subscribe((jobTitles: { value: string; label: string }[]) => {
          this.jobTitles = [
            { value: '0', label: 'Select Job Title' },
            ...jobTitles,
          ];
          this.newAgentForm.get('JobTitleID')?.setValue(0);
        })
    );

    this.subscriptions.add(
      this.agentDataService
        .fetchBranchCodes()
        .subscribe((branchCodes: any[]) => {
          this.branchCodes = [
            { branchCode: 'Select Branch Code' },
            ...branchCodes,
          ];
          this.newAgentForm.get('branchCode')?.setValue('Select Branch Code');
        })
    );
  }

  // VALIDATION
  // employerAgencyValidator(
  //   control: AbstractControl
  // ): { [key: string]: boolean } | null {
  //   if (control.value === 0 || control.value === '') {
  //     return { invalid: true };
  //   }
  //   return null;
  // }

  onSubmit() {
    this.formSubmitted = true;

    let agent: any = this.newAgentForm.value;
    agent.UserSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (agent.employerAgency === 0 || agent.employerAgency === '') {
      this.newAgentForm.controls['employerAgency'].setErrors({ invalid: true });
    }

    if (agent.WorkStateAbv === 'Select State') {
      this.newAgentForm.controls['WorkStateAbv'].setErrors({ invalid: true });
    }

    if (agent.ResStateAbv === 'Select State') {
      this.newAgentForm.controls['ResStateAbv'].setErrors({ invalid: true });
    }

    if (agent.branchCode === 'Select Branch Code') {
      agent.branchCode = '';
      // this.newAgentForm.controls['branchCode'].setErrors({ invalid: true });
    }

    if (agent.HireDate === '01/01/0001 00:00:00') {
      this.newAgentForm.controls['HireDate'].setErrors({ invalid: true });
    }

    if (this.newAgentForm.invalid) {
      this.newAgentForm.setErrors({ invalid: true });
      return;
    }

    this.agentDataService.upsertAgent(agent).subscribe({
      next: (response) => {
        // console.log(response);
        // handle the response here
        // console.log(
        //   'EMFTEST () - Agent added successfully response => \n ',
        //   response
        // );
      },
      error: (error) => {
        console.error(error);
        // handle the error here
      },
    });
  }

  onCancel() {
    this.newAgentForm.reset();
    this.newAgentForm.get('workState')?.setValue('Select State');
    this.newAgentForm.get('resState')?.setValue('Select State');
    this.newAgentForm.get('employerAgency')?.setValue(0);
    this.newAgentForm.get('branchCode')?.setValue('Select Branch Code');
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
