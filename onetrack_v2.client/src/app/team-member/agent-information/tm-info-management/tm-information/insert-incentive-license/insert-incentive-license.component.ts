import { Component, Input, OnDestroy, OnInit, input } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  AgentComService,
  AgentDataService,
  ConstantsDataService,
  DropdownDataService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../../../_services';

@Component({
  selector: 'app-insert-incentive-license',
  templateUrl: './insert-incentive-license.component.html',
  styleUrl: './insert-incentive-license.component.css',
})
export class InsertIncentiveLicenseComponent implements OnInit, OnDestroy {
  isFormSubmitted: boolean = false;
  @Input() employeeID: number = 0;
  @Input() employmentID: number = 0;
  @Input() branchState: any | null = null;
  incentiveLicenseForm!: FormGroup;
  defaultLicenseStatus = 'Incentive';
  licenseStates: string[] = [];
  licenseNames: { value: number; label: string }[] = [];
  agent: any = {};
  selectedLicenseState: any | null = null;
  workState: string | null = null;

  private subscriptions = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    private fb: FormBuilder,
    private conService: ConstantsDataService,
    private drpdwnDataService: DropdownDataService,
    private agentDataService: AgentDataService,
    private agentComService: AgentComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {
    this.createForm();
  }

  createForm() {
    this.incentiveLicenseForm = this.fb.group({
      licenseState: ['Select', Validators.required],
      licenseID: [
        { value: 0, disabled: !this.selectedLicenseState },
        Validators.required,
      ],
      licenseStatus: [{ value: 'Incentive', disabled: true }],
      note: [''],
    });
  }

  ngOnInit(): void {
    this.licenseStates = ['Select', ...this.conService.getStates()];

    this.agent = this.agentDataService.agentInformation;
    this.subscriptions.add(
      this.agentDataService.agentInfoChanged.subscribe((data) => {
        this.agent = data;
        this.selectedLicenseState = this.agent.workStateAbv ?? 'Select';
        this.getStateLicenseNames(this.selectedLicenseState);
        this.updateLicenseIDDisabledState();
        this.incentiveLicenseForm.patchValue({
          licenseState: this.agent.workStateAbv ?? 'Select',
          licenseID: 0,
        });
      })
    );
    this.workState = this.agentDataService.workState;
    this.subscriptions.add(
      this.agentDataService.workStateChanged.subscribe(
        (workState: string | null) => {
          this.workState = workState;
        }
      )
    );

    this.getStateLicenseNames(
      this.agentDataService.agentInformation.branchDeptStreetState ?? 'Select'
    );
  }

  getStateLicenseNames(state: string) {
    if (state === 'Select') {
      return;
    }
    this.subscriptions.add(
      this.drpdwnDataService
        .fetchDropdownNumericData('GetLicenseNumericNames', state)
        .subscribe((licenseNames: { value: number; label: string }[]) => {
          this.licenseNames = [{ value: 0, label: 'Select' }, ...licenseNames];
        })
    );
  }

  onSubmit() {
    let incentiveLicenseItem = this.incentiveLicenseForm.value;
    incentiveLicenseItem.employeeLicenseID = 0;
    incentiveLicenseItem.employeeID = this.employeeID;
    incentiveLicenseItem.employmentID = this.employmentID;
    incentiveLicenseItem.userSOEID =
      this.userAcctInfoDataService.userAcctInfo.soeid;

    if (
      incentiveLicenseItem.licenseState === 'Select' ||
      incentiveLicenseItem.licenseState === '' ||
      incentiveLicenseItem.licenseState === null
    ) {
      this.incentiveLicenseForm.controls['licenseState'].setErrors({
        required: true,
      });
    }

    if (
      incentiveLicenseItem.licenseID === 0 ||
      incentiveLicenseItem.licenseID === null ||
      incentiveLicenseItem.licenseID === '' 
    ) {
      this.incentiveLicenseForm.controls['licenseID'].setErrors({
        required: true,
      });
    }

    if (this.incentiveLicenseForm.invalid) {
      this.incentiveLicenseForm.setErrors({ invalid: true });
      return;
    }

    this.subscriptions.add(
      this.agentDataService.upsertAgentLicense(incentiveLicenseItem).subscribe({
        next: (response) => {
          this.isFormSubmitted = true;
          this.incentiveLicenseForm.reset();
          this.incentiveLicenseForm.patchValue({
            licenseState: this.branchState,
            licenseID: 0,
            licenseStatus: 'Incentive',
          });
          this.onCloseModal();
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
          }

          const modalDiv = document.getElementById(
            'modal-insert-incentive-license'
          );
          if (modalDiv != null) {
            modalDiv.style.display = 'none';
          }
        },
      })
    );
  }

  onChangeLicenseState(event: any) {
    // this.incentiveLicenseForm.patchValue({
    //   licenseState: event.target.value,
    // });
    this.selectedLicenseState = event.target.value;
    this.updateLicenseIDDisabledState();

    if (
      event.target.value !== this.agentDataService.agentInformation.workStateAbv
    ) {
      // alert(
      //   'Selection is different than Agent Work State: ' +
      //     this.agentDataService.agentInformation.workStateAbv
      // );
    }

    this.getStateLicenseNames(event.target.value);
  }

  updateLicenseIDDisabledState() {
    if (
      this.selectedLicenseState !== 'Select' &&
      this.selectedLicenseState !== '' &&
      this.selectedLicenseState !== null
    ) {
      this.incentiveLicenseForm.get('licenseID')?.enable();
    } else {
      this.incentiveLicenseForm.get('licenseID')?.disable();
    }
  }

  onCloseModal() {
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
