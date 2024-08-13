import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { Subscription, switchMap } from 'rxjs';

import {
  AgentComService,
  AgentDataService,
  ConstantsDataService,
  DropdownDataService,
  ErrorMessageService,
  LicIncentiveInfoDataService,
  UserAcctInfoDataService,
} from '../../../../../_services';
import { LicenseAppointment } from '../../../../../_Models';
import { formatDate } from '@angular/common';

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
  appointmentStatuses: any[] = [
    'Select',
    ...this.conService.getAppointmentStatuses(),
  ];
  employeeAppointmentID: number = 0;
  form = this.fb.group({
    licenseID: new FormControl({ value: 0, disabled: true }),
    employeeAppointmentID: new FormControl({ value: 0, disabled: true }),
    appointmentStatus: new FormControl(''),
    companyID: new FormControl(0),
    carrierDate: new FormControl(''),
    appointmentEffectiveDate: new FormControl(''),
    appointmentExpireDate: new FormControl(''),
    appointmentTerminationDate: new FormControl(''),
  });

  private subscriptions = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    private agentDataService: AgentDataService,
    private agentComService: AgentComService,
    private licIncentiveInfoDataService: LicIncentiveInfoDataService,
    private userInfoDataService: UserAcctInfoDataService,
    private dropdownDataService: DropdownDataService,
    private fb: FormBuilder
  ) {}

  //   ngOnInit(): void {
  //     this.subscriptions.add(
  //       this.agentComService.modeLicenseApptChanged.subscribe((mode: string) => {
  //         if (mode === 'EDIT') {
  // console.log('EMFTEST (modeLicenseApptChanged: EDIT)');

  //           this.subscriptions.add(
  //             this.agentDataService.licenseAppointmentChanged.subscribe(
  //               (licenseAppointment: any) => {
  //                 this.subscriptions.add(
  //                   this.agentDataService.agentLicApptLicenseIDChanged.subscribe(
  //                     (agentLicApptLicenseID: any) => {
  //                       this.subscriptions.add(
  //                         this.dropdownDataService
  //                           .fetchDropdownNumericData(
  //                             'GetCoAbvByLicenseID',
  //                             agentLicApptLicenseID
  //                           )
  //                           .subscribe((response) => {
  //                             this.companyAbbreviations = response;

  //                             this.licenseAppointment = licenseAppointment;
  //                             this.employeeAppointmentID =
  //                               licenseAppointment.employeeAppointmentID;

  // console.log('EMFTEST (licenseAppointmentChanged: Appt...) - licenseAppointment => \n', licenseAppointment);

  //                             this.form.reset({
  //                               licenseID: licenseAppointment.licenseID,
  //                               employeeAppointmentID:
  //                                 licenseAppointment.employeeAppointmentID,
  //                               appointmentStatus:
  //                                 licenseAppointment.appointmentStatus,
  //                               companyID: licenseAppointment.companyID,
  //                               carrierDate: licenseAppointment.carrierDate
  //                                 ? formatDate(
  //                                     licenseAppointment.carrierDate,
  //                                     'yyyy-MM-dd',
  //                                     'en-US'
  //                                   )
  //                                 : null,
  //                               appointmentEffectiveDate:
  //                                 licenseAppointment.appointmentEffectiveDate
  //                                   ? formatDate(
  //                                       licenseAppointment.appointmentEffectiveDate,
  //                                       'yyyy-MM-dd',
  //                                       'en-US'
  //                                     )
  //                                   : null,
  //                               appointmentExpireDate:
  //                                 licenseAppointment.appointmentExpireDate
  //                                   ? formatDate(
  //                                       licenseAppointment.appointmentExpireDate,
  //                                       'yyyy-MM-dd',
  //                                       'en-US'
  //                                     )
  //                                   : null,
  //                               appointmentTerminationDate:
  //                                 licenseAppointment.appointmentTerminationDate
  //                                   ? formatDate(
  //                                       licenseAppointment.appointmentTerminationDate,
  //                                       'yyyy-MM-dd',
  //                                       'en-US'
  //                                     )
  //                                   : null,
  //                             });
  //                           })
  //                       );
  //                     }
  //                   )
  //                 );

  //                 // this.licenseAppointment = licenseAppointment;
  //                 // this.employeeAppointmentID = licenseAppointment.employeeAppointmentID;
  //                 // this.form.patchValue({
  //                 //   licenseID: licenseAppointment.licenseID,
  //                 //   employeeAppointmentID: licenseAppointment.employeeAppointmentID,
  //                 //   appointmentStatus: licenseAppointment.appointmentStatus,
  //                 //   companyID: licenseAppointment.companyID,
  //                 //   carrierDate: licenseAppointment.carrierDate
  //                 //     ? formatDate(
  //                 //         licenseAppointment.carrierDate,
  //                 //         'yyyy-MM-dd',
  //                 //         'en-US'
  //                 //       )
  //                 //     : null,
  //                 //   appointmentEffectiveDate:
  //                 //     licenseAppointment.appointmentEffectiveDate
  //                 //       ? formatDate(
  //                 //           licenseAppointment.appointmentEffectiveDate,
  //                 //           'yyyy-MM-dd',
  //                 //           'en-US'
  //                 //         )
  //                 //       : null,
  //                 //   appointmentExpireDate: licenseAppointment.appointmentExpireDate
  //                 //     ? formatDate(
  //                 //         licenseAppointment.appointmentExpireDate,
  //                 //         'yyyy-MM-dd',
  //                 //         'en-US'
  //                 //       )
  //                 //     : null,
  //                 //   appointmentTerminationDate:
  //                 //     licenseAppointment.appointmentTerminationDate
  //                 //       ? formatDate(
  //                 //           licenseAppointment.appointmentTerminationDate,
  //                 //           'yyyy-MM-dd',
  //                 //           'en-US'
  //                 //         )
  //                 //       : null,
  //                 //   // employeeLicenseID: licenseAppointment.employeeLicenseID,
  //                 //   // retentionDate: licenseAppointment.retentionDate,
  //                 //   // companyAbbr: 'TBD...',
  //                 // });
  //               }
  //             )
  //           );
  //         } else {
  //           this.subscriptions.add(
  //             this.agentDataService.agentLicApptLicenseIDChanged.subscribe(
  //               (agentLicApptLicenseID: any) => {
  //                 this.subscriptions.add(
  //                   this.dropdownDataService
  //                     .fetchDropdownNumericData(
  //                       'GetCoAbvByLicenseID',
  //                       agentLicApptLicenseID
  //                     )
  //                     .subscribe((response) => {
  //                       this.companyAbbreviations = [
  //                         { value: 0, label: 'Select' },
  //                         ...response,
  //                       ];
  //                       this.form.reset({
  //                         companyID: 0,
  //                         appointmentStatus: 'Select',
  //                       });
  //                     })
  //                 );
  //               }
  //             )
  //           );
  //         }
  //       })
  //     );
  //   }
  //////////////////////////////////////////////////////////////////////////////////////////////////////////
  ngOnInit(): void {
    this.subscriptions.add(
      this.agentComService.modeLicenseApptChanged
        .pipe(
          switchMap((mode: string) => {
            if (mode === 'EDIT') {
console.log('EMFTEST (modeLicenseApptChanged: EDIT)');
              // MODE: EDIT
              return this.agentDataService.licenseAppointmentChanged.pipe(
                switchMap((licenseAppointment: any) => {
                  this.licenseAppointment = licenseAppointment;
                  this.employeeAppointmentID =
                    licenseAppointment.employeeAppointmentID;
console.log('EMFTEST (licenseAppointmentChanged: Appt...) - licenseAppointment => \n', licenseAppointment);
                  return this.agentDataService.agentLicApptLicenseIDChanged.pipe(
                    switchMap((agentLicApptLicenseID: any) => {
                      return this.dropdownDataService.fetchDropdownNumericData(
                        'GetCoAbvByLicenseID',
                        agentLicApptLicenseID
                      );
                    })
                  );
                })
              );
            } else {
              // MODE: INSERT
              return this.agentDataService.agentLicApptLicenseIDChanged.pipe(
                switchMap((agentLicApptLicenseID: any) => {
                  return this.dropdownDataService.fetchDropdownNumericData(
                    'GetCoAbvByLicenseID',
                    agentLicApptLicenseID
                  );
                })
              );
            }
          })
        )
        .subscribe((response) => {
          if (this.licenseAppointment) {
            this.companyAbbreviations = response;
            this.form.patchValue({
              licenseID: this.licenseAppointment.licenseID,
              employeeAppointmentID:
                this.licenseAppointment.employeeAppointmentID,
              appointmentStatus: this.licenseAppointment.appointmentStatus,
              companyID: this.licenseAppointment.companyID,
              carrierDate: this.licenseAppointment.carrierDate
                ? formatDate(
                    this.licenseAppointment.carrierDate,
                    'yyyy-MM-dd',
                    'en-US'
                  )
                : null,
              appointmentEffectiveDate: this.licenseAppointment
                .appointmentEffectiveDate
                ? formatDate(
                    this.licenseAppointment.appointmentEffectiveDate,
                    'yyyy-MM-dd',
                    'en-US'
                  )
                : null,
              appointmentExpireDate: this.licenseAppointment
                .appointmentExpireDate
                ? formatDate(
                    this.licenseAppointment.appointmentExpireDate,
                    'yyyy-MM-dd',
                    'en-US'
                  )
                : null,
              appointmentTerminationDate: this.licenseAppointment
                .appointmentTerminationDate
                ? formatDate(
                    this.licenseAppointment.appointmentTerminationDate,
                    'yyyy-MM-dd',
                    'en-US'
                  )
                : null,
            });
          } else {
            this.companyAbbreviations = [
              { value: 0, label: 'Select' },
              ...response,
            ];
            this.form.reset({
              companyID: 0,
              appointmentStatus: 'Select',
            });
          }
        })
    );
  }

  onSubmit() {
    this.isFormSubmitted = true;

    let licenseApptItem: any = this.form.value;
    licenseApptItem.employeeID =
      this.agentDataService.agentInformation.employeeID;
    licenseApptItem.employeeAppointmentID = this.employeeAppointmentID;
    licenseApptItem.UserSOEID = this.userInfoDataService.userAcctInfo.soeid;

    if (licenseApptItem.appointmentStatus === 'Select') {
      licenseApptItem.appointmentStatus = '';
    }

    if (licenseApptItem.carrierDate === '') {
      licenseApptItem.carrierDate = null;
    }

    if (licenseApptItem.appointmentEffectiveDate === '') {
      licenseApptItem.appointmentEffectiveDate = null;
    }

    if (licenseApptItem.appointmentExpireDate === '') {
      licenseApptItem.appointmentExpireDate = null;
    }

    if (licenseApptItem.appointmentTerminationDate === '') {
      licenseApptItem.appointmentTerminationDate = null;
    }

    if (licenseApptItem.companyID === 0 || licenseApptItem.companyID === null) {
      this.form.controls['companyID'].setErrors({ required: true });
    }

    if (this.form.invalid) {
      this.form.setErrors({ invalid: true });
      return;
    }

    this.subscriptions.add(
      this.licIncentiveInfoDataService
        .updateLicenseAppointment(licenseApptItem)
        .subscribe({
          next: (response) => {
            // const modalDiv = document.getElementById('modal-edit-license-appt');
            // if (modalDiv != null) {
            //   modalDiv.style.display = 'none';
            // }
            this.forceCloseModal();
          },
          error: (error) => {
            if (error.error && error.error.errMessage) {
              this.errorMessageService.setErrorMessage(error.error.errMessage);
            }
            this.forceCloseModal();
          },
        })
    );
  }

  forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-license-appt');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  onCancel() {
    // const modalDiv = document.getElementById('modal-edit-license-appt');
    // if (modalDiv != null) {
    //   modalDiv.style.display = 'none';
    // }
    this.forceCloseModal();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
