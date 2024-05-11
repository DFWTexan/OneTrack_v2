import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { zip } from 'rxjs';

import {
  AgentDataService,
  ConstantsDataService,
  DropdownDataService,
} from '../../_services';

@Component({
  selector: 'app-add-team-member',
  templateUrl: './add-team-member.component.html',
  styleUrl: './add-team-member.component.css',
})
export class AddTeamMemberComponent implements OnInit, OnDestroy {
  formSubmitted = false;
  newAgentForm: FormGroup;
  states: any[] = ['Loading...'];
  employerAgencies: any[] = [{ valu: '0', label: 'Loading...' }, 'Loading...'];
  branchCodes: any[] = [{ branchCode: 'Loading...' }];

  constructor(
    private fb: FormBuilder,
    private conService: ConstantsDataService,
    private drpdwnDataService: DropdownDataService,
    private agentDataService: AgentDataService
  ) {
    this.newAgentForm = this.fb.group({
      firstName: ['', Validators.required],
      middleName: [''],
      lastName: ['', Validators.required],
      preferedName: ['', Validators.required],
      dateOfBirth: [''],
      employeeSSN: [''],
      teamID: [''],
      soeid: [''],
      nationalProducerNumber: [''],
      employerAgency: ['', Validators.required],
      // employerAgency: ['', [Validators.required, this.employerAgencyValidator]],
      workState: ['', Validators.required],
      resState: ['', Validators.required],
      jobTitle: [''],
      hireDate: ['', Validators.required],
      branchCode: [''],
      address1: [''],
      address2: [''],
      city: [''],
      zip: [''],
      email: [''],
      homePhone: [''],
      workPhone: [''],
      fax: [''],
    });
  }

  ngOnInit() {
    this.states = ['Select State', ...this.conService.getStates()];
    if (this.newAgentForm) {
      this.newAgentForm.get('workState')?.setValue('Select State');
      this.newAgentForm.get('resState')?.setValue('Select State');
    }
    this.drpdwnDataService
      .fetchDropdownData('GetEmployerAgencies')
      .subscribe((employerAgencies: { value: string; label: string }[]) => {
        this.employerAgencies = [
          { valu: '0', label: 'Select Employer/Agency' },
          ...employerAgencies,
        ];
        this.newAgentForm.get('employerAgency')?.setValue(0);
      });
    this.agentDataService.fetchBranchCodes().subscribe((branchCodes: any[]) => {
      this.branchCodes = [{ branchCode: 'Select Branch Code' }, ...branchCodes];
      this.newAgentForm.get('branchCode')?.setValue('Select Branch Code');
    });
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
    // agent.soeid = agent.soeid.toUpperCase();
    agent.soeid = 'EMFTEST';

    if (agent.employerAgency === 0 || agent.employerAgency === '') {
      this.newAgentForm.controls['employerAgency'].setErrors({ invalid: true });
    }

    if (agent.workState === 'Select State') {
      this.newAgentForm.controls['workState'].setErrors({ invalid: true });
    }

    if (agent.resState === 'Select State') {
      this.newAgentForm.controls['resState'].setErrors({ invalid: true });
    }

    if (agent.branchCode === 'Select Branch Code') {
      agent.branchCode = '';
      // this.newAgentForm.controls['branchCode'].setErrors({ invalid: true });
    }

    if (!this.newAgentForm.invalid) {
      this.newAgentForm.setErrors({ invalid: true });
      return;
    }

    this.agentDataService.addAgent(agent).subscribe({
      next: (response) => {
        console.log(response);
        // handle the response here
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
    // Clean up component
  }
}
