import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  newAgentForm: FormGroup;
  states: any[] = ['Select State', 'Loading...'];
  employerAgencies: any[] = [
    { valu: '0', label: 'Select Employer Agency' },
    'Loading...',
  ];
  branchCodes: any[] = [{branchCode: 'Loading...'}];

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
          { valu: '0', label: 'Select Employer Agency' },
          ...employerAgencies,
        ];
        this.newAgentForm.get('employerAgency')?.setValue(0);
      });
    this.agentDataService.fetchBranchCodes().subscribe((branchCodes: any[]) => {
      this.branchCodes = [{ branchCode: 'Select Branch Code' }, ...branchCodes];
      this.newAgentForm.get('branchCode')?.setValue('Select Branch Code');
    });
  }

  onSubmit() {
    // Handle form submission
  }

  onCancel() {
    this.newAgentForm.reset();
  }

  ngOnDestroy() {
    // Clean up component
  }
}
