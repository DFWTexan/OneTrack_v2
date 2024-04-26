import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  AgentDataService,
  ConstantsDataService,
  DropdownDataService,
} from '../../../../_services';

@Component({
  selector: 'app-insert-incentive-license',
  templateUrl: './insert-incentive-license.component.html',
  styleUrl: './insert-incentive-license.component.css',
})
export class InsertIncentiveLicenseComponent implements OnInit, OnDestroy {
  incentiveLicenseForm!: FormGroup;
  defaultLicenseStatus = 'Incentive';
  licenseStates: string[] = [];
  licenseNames: { value: string; label: string }[] = [];
  agentName: string = '';
  subscriptionData: Subscription = new Subscription();

  constructor(
    private fb: FormBuilder,
    private conService: ConstantsDataService,
    private drpdwnDataService: DropdownDataService,
    private agentDataService: AgentDataService
  ) {
    this.createForm();
  }

  createForm() {
    this.incentiveLicenseForm = this.fb.group({
      preferredName: [''],
      licenseState: ['', Validators.required],
      licenseName: [''],
      licenseStatus: [{ value: 'Incentive', disabled: true }],
      note: [''],
    });
  }

  ngOnInit(): void {
    this.licenseStates = ['Select', ...this.conService.getStates()];
    this.drpdwnDataService
      .fetchDropdownData('GetLicenseNames')
      .subscribe((licenseNames: { value: string; label: string }[]) => {
        this.licenseNames = [{ value: '0', label: 'Select' }, ...licenseNames];
      });
    this.subscriptionData = this.agentDataService.agentInfoChanged.subscribe(
      (data) => {
        this.agentName = data.lastName + ', ' + data.firstName;
      }
    );
  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      // Your submission logic here
      console.log('Form Submitted!', this.incentiveLicenseForm);
    } else {
      // Handle the invalid form case
      console.error('Form is not valid!');
    }
  }

  cancel() {
    // Your cancel logic here
    console.log('Form Cancellation initiated');
  }

  ngOnDestroy() {
    this.subscriptionData.unsubscribe();
  }
}
