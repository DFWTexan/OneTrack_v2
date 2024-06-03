import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  AgentDataService,
  ConstantsDataService,
  DropdownDataService,
  ErrorMessageService,
} from '../../../../../_services';

@Component({
  selector: 'app-insert-incentive-license',
  templateUrl: './insert-incentive-license.component.html',
  styleUrl: './insert-incentive-license.component.css',
})
export class InsertIncentiveLicenseComponent implements OnInit, OnDestroy {
  isFormSubmitted: boolean = false;
  incentiveLicenseForm!: FormGroup;
  defaultLicenseStatus = 'Incentive';
  licenseStates: string[] = [];
  licenseNames: { value: string; label: string }[] = [];
  agentName: string = '';

  private subscriptions = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
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
      licenseState: ['Select', Validators.required],
      licenseName: [0, Validators.required],
      licenseStatus: [{ value: 'Incentive', disabled: true }],
      note: [''],
    });
  }

  ngOnInit(): void {
    this.licenseStates = ['Select', ...this.conService.getStates()];
    this.subscriptions.add(
      this.drpdwnDataService
        .fetchDropdownData('GetLicenseNames')
        .subscribe((licenseNames: { value: string; label: string }[]) => {
          this.licenseNames = [
            { value: '0', label: 'Select Name' },
            ...licenseNames,
          ];
        })
    );
    this.subscriptions.add(
      this.agentDataService.agentInfoChanged.subscribe((data) => {
        this.agentName = data.lastName + ', ' + data.firstName;
      })
    );
  }

  onSubmit() {
    // if (form.valid) {
    //   // Your submission logic here
    //   console.log('Form Submitted!', this.incentiveLicenseForm);
    // } else {
    //   // Handle the invalid form case
    //   console.error('Form is not valid!');
    // }

    // ERROR: TBD...
    // if (error.error && error.error.errMessage) {
    //   this.errorMessageService.setErrorMessage(error.error.errMessage);
    // }
  }

  closeModal() {
    // const modalDiv = document.getElementById('modal-insert-incentive-license');
    // if (modalDiv != null) {
    //   modalDiv.style.display = 'none';
    // }

    if (this.incentiveLicenseForm.dirty && !this.isFormSubmitted) {
      if (
        confirm('You have unsaved changes. Are you sure you want to close?')
      ) {
        const modalDiv = document.getElementById(
          'modal-insert-incentive-license'
        );
        if (modalDiv != null) {
          modalDiv.style.display = 'none';
        }
        this.incentiveLicenseForm.reset();
        // this.incentiveLicenseForm.patchValue({
        //   jobTitleID: 0,
        //   isCurrent: false,
        // });
      }
    } else {
      this.isFormSubmitted = false;
      const modalDiv = document.getElementById(
        'modal-insert-incentive-license'
      );
      if (modalDiv != null) {
        modalDiv.style.display = 'none';
      }
    }
  }

  cancel() {
    // Your cancel logic here
    console.log('Form Cancellation initiated');
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
