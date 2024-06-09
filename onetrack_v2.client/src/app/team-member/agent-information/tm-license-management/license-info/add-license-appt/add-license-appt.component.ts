import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import {
  AgentComService,
  AgentDataService,
  AppComService,
  ConstantsDataService,
  DropdownDataService,
  ErrorMessageService,
  LicIncentiveInfoDataService,
  UserAcctInfoDataService,
} from '../../../../../_services';

@Component({
  selector: 'app-add-license-appt',
  templateUrl: './add-license-appt.component.html',
  styleUrl: './add-license-appt.component.css',
})
export class AddLicenseApptComponent implements OnInit, OnDestroy {
  isFormSubmitted: boolean = false;
  licenseApptForm: FormGroup;
  companyAbbreviations: { value: number; label: string }[] = [];
  @Input() employeeID: number = 0;
  @Input() employeeLicenseID: number = 0;
  appointmentStatuses: any[] = [
    'Select',
    ...this.conService.getAppointmentStatuses(),
  ];
  companyNames: any[] = [];

  private subscriptions = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    public appComService: AppComService,
    private licIncentiveInfoDataService: LicIncentiveInfoDataService,
    private userInfoDataService: UserAcctInfoDataService,
    private dropdownDataService: DropdownDataService,
    private fb: FormBuilder
  ) {
    this.licenseApptForm = this.fb.group({
      employeeID: [0],
      employeeLicenseID: [0],
      appointmentStatus: ['Select'],
      companyID: [0],
      carrierDate: [null],
      appointmentEffectiveDate: [null],
      appointmentExpireDate: [null],
      appointmentTerminationDate: [null],
    });
  }

  ngOnInit(): void {
    this.subscriptions.add(
      this.dropdownDataService
        .fetchDropdownNumericData(
          'GetCoAbvByLicenseID',
          this.agentDataService.agentLicApptLicenseID
        )
        .subscribe((response) => {
          this.companyAbbreviations = [
            { value: 0, label: 'Select' },
            ...response,
          ];
        })
    );

    // this.subscriptions.add(
    //   this.agentDataService.agentLicApptLicenseIDChanged.subscribe(
    //     (agentLicApptLicenseID: any) => {
    //     }
    //   )
    // );
    this.licenseApptForm.patchValue({
      companyID: 0,
    });
  }

  onSubmit() {
    this.isFormSubmitted = true;

    let licenseApptItem: any = this.licenseApptForm.value;
    licenseApptItem.employeeID =
      this.agentDataService.agentInformation.employeeID;
    licenseApptItem.employeeLicenseID = this.employeeLicenseID;
    licenseApptItem.UserSOEID = this.userInfoDataService.userAcctInfo.soeid;

    if (licenseApptItem.appointmentStatus === 'Select') {
      licenseApptItem.appointmentStatus = '';
    }

    if (this.licenseApptForm.invalid) {
      this.licenseApptForm.setErrors({ invalid: true });
      return;
    }

    this.subscriptions.add(
      this.licIncentiveInfoDataService
        .addLicenseAppointment(licenseApptItem)
        .subscribe({
          next: (response) => {
            const modalDiv = document.getElementById('modal-add-license-appt');
            if (modalDiv != null) {
              modalDiv.style.display = 'none';
            }
            // handle the response here
            // console.log(
            //   'EMFTEST () - Agent License added successfully response => \n ',
            //   response
            // );
          },
          error: (error) => {
            if (error.error && error.error.errMessage) {
              this.errorMessageService.setErrorMessage(error.error.errMessage);
            }
            const modalDiv = document.getElementById('modal-add-license-appt');
            if (modalDiv != null) {
              modalDiv.style.display = 'none';
            }
          },
        })
    );
  }

  onCloseModal() {
    // const modalDiv = document.getElementById('modal-add-license-appt');
    // if (modalDiv != null) {
    //   modalDiv.style.display = 'none';
    // }

    if (this.licenseApptForm.dirty && !this.isFormSubmitted) {
      if (
        confirm('You have unsaved changes. Are you sure you want to close?')
      ) {
        const modalDiv = document.getElementById('modal-add-license-appt');
        if (modalDiv != null) {
          modalDiv.style.display = 'none';
        }
        this.licenseApptForm.reset();
        // this.licenseForm.patchValue({
        //   jobTitleID: 0,
        //   isCurrent: false,
        // });
      }
    } else {
      this.isFormSubmitted = false;
      const modalDiv = document.getElementById('modal-add-license-appt');
      if (modalDiv != null) {
        modalDiv.style.display = 'none';
      }
    }
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
