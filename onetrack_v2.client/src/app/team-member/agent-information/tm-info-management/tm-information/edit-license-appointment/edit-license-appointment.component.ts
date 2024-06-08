import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  AgentDataService,
  DropdownDataService,
  ErrorMessageService,
  LicIncentiveInfoDataService,
  UserAcctInfoDataService,
} from '../../../../../_services';
import { LicenseAppointment } from '../../../../../_Models';

@Component({
  selector: 'app-edit-license-appointment',
  templateUrl: './edit-license-appointment.component.html',
  styleUrl: './edit-license-appointment.component.css',
})
@Injectable()
export class EditLicenseAppointmentComponent implements OnInit, OnDestroy {
  isFormSubmitted: boolean = false;
  companyAbbreviations: { value: number; label: string }[] = [];
  licenseAppointment: LicenseAppointment = {} as LicenseAppointment;
  form = this.fb.group({
    licenseID: new FormControl({ value: '', disabled: true }),
    employeeAppointmentID: new FormControl({ value: '', disabled: true }),
    appointmentStatus: new FormControl(''),
    companyID: new FormControl(''),
    carrierDate: new FormControl(''),
    appointmentEffectiveDate: new FormControl(''),
    appointmentExpireDate: new FormControl(''),
    appointmentTerminationDate: new FormControl(''),
  });
  
  private subscriptions = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    private agentDataService: AgentDataService,
    private licIncentiveInfoDataService: LicIncentiveInfoDataService,
    private userInfoDataService: UserAcctInfoDataService,
    private dropdownDataService: DropdownDataService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.subscriptions.add(
      this.agentDataService.licenseAppointmentChanged.subscribe(
        (licenseAppointment: any) => {
          this.subscriptions.add(
            this.agentDataService.agentLicApptLicenseIDChanged.subscribe(
              (agentLicApptLicenseID: any) => {
                this.subscriptions.add(
                  this.dropdownDataService
                    .fetchDropdownNumericData(
                      'GetCoAbvByLicenseID',
                      agentLicApptLicenseID
                    )
                    .subscribe((response) => {
                      this.companyAbbreviations = response;
                    })
                );
              }
            )
          );

          this.licenseAppointment = licenseAppointment;
          this.form.patchValue({
            licenseID: licenseAppointment.licenseID,
            employeeAppointmentID: licenseAppointment.employeeAppointmentID,
            appointmentStatus: licenseAppointment.appointmentStatus,
            companyID: licenseAppointment.companyID,
            carrierDate: licenseAppointment.carrierDate,
            appointmentEffectiveDate:
              licenseAppointment.appointmentEffectiveDate,
            appointmentExpireDate: licenseAppointment.appointmentExpireDate,
            appointmentTerminationDate:
              licenseAppointment.appointmentTerminationDate,
            // employeeLicenseID: licenseAppointment.employeeLicenseID,
            // retentionDate: licenseAppointment.retentionDate,
            // companyAbbr: 'TBD...',
          });
        }
      )
    );
  }

  onSubmit() {
    // ERROR: TBD
    // if (error.error && error.error.errMessage) {
    //   this.errorMessageService.setErrorMessage(error.error.errMessage);
    // }
    this.isFormSubmitted = true;

    let licenseApptItem: any = this.form.value;
    licenseApptItem.employeeID =
      this.agentDataService.agentInformation.employeeID;
    // licenseApptItem.employeeLicenseID = this.employeeLicenseID;
    licenseApptItem.UserSOEID = this.userInfoDataService.userAcctInfo.soeid;

    if (licenseApptItem.appointmentStatus === 'Select') {
      licenseApptItem.appointmentStatus = '';
    }

    if (this.form.invalid) {
      this.form.setErrors({ invalid: true });
      return;
    }

    console.log(
      'EMFTEST (app-add-license-appt: onSubmit) - licenseApptItem => \n ',
      licenseApptItem
    );

    this.subscriptions.add(
      this.licIncentiveInfoDataService
        .updateLicenseAppointment(licenseApptItem)
        .subscribe({
          next: (response) => {
            const modalDiv = document.getElementById('modal-edit-license-appt');
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
            const modalDiv = document.getElementById('modal-edit-license-appt');
            if (modalDiv != null) {
              modalDiv.style.display = 'none';
            }
          },
        })
    );
  }

  onCancel() {
    const modalDiv = document.getElementById('modal-edit-license-appt');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
