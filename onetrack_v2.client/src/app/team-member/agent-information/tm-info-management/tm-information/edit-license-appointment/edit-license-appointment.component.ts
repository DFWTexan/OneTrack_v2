import {
  Component,
  Injectable,
  OnInit,
  OnDestroy,
  ChangeDetectorRef,
  AfterViewInit,
  SimpleChanges,
  OnChanges,
} from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { Subject, Subscription, switchMap, tap } from 'rxjs';

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
export class EditLicenseAppointmentComponent
  implements OnInit, AfterViewInit, OnChanges, OnDestroy
{
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
  private companyIDSubject = new Subject<void>();

  constructor(
    public errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    private agentDataService: AgentDataService,
    private agentComService: AgentComService,
    private licIncentiveInfoDataService: LicIncentiveInfoDataService,
    private userInfoDataService: UserAcctInfoDataService,
    private dropdownDataService: DropdownDataService,
    private fb: FormBuilder,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.subscriptions.add(
      this.agentComService.modeLicenseApptChanged
        .pipe(
          switchMap((mode: string) => {
            if (mode === 'EDIT') {
              // MODE: EDIT
              return this.agentDataService.licenseAppointmentChanged.pipe(
                switchMap((licenseAppointment: any) => {
                  this.licenseAppointment = licenseAppointment;
                  this.employeeAppointmentID =
                    licenseAppointment.employeeAppointmentID;

                  return this.agentDataService.agentLicApptLicenseIDChanged.pipe(
                    switchMap((agentLicApptLicenseID: any) => {
                      return this.dropdownDataService
                        .fetchDropdownNumericData(
                          'GetCoAbvByLicenseID',
                          agentLicApptLicenseID
                        )
                        .pipe(
                          tap((response) => {
                            this.companyAbbreviations = response;
                            this.companyIDSubject.next();
                          })
                        );
                    })
                  );
                })
              );
            } else {
              // MODE: INSERT
              return this.agentDataService.agentLicApptLicenseIDChanged.pipe(
                switchMap((agentLicApptLicenseID: any) => {
                  return this.dropdownDataService
                    .fetchDropdownNumericData(
                      'GetCoAbvByLicenseID',
                      agentLicApptLicenseID
                    )
                    .pipe(
                      tap((response) => {
                        this.companyAbbreviations = [
                          { value: 0, label: 'Select' },
                          ...response,
                        ];
                        this.form.reset({
                          companyID: 0,
                          appointmentStatus: 'Select',
                        });
                        // Trigger change detection
                        this.cdr.detectChanges();
                      })
                    );
                })
              );
            }
          })
        )
        .subscribe()
    );
  }

  ngAfterViewInit(): void {
    this.subscriptions.add(
      this.companyIDSubject.subscribe(() => {
        setTimeout(() => {
          this.updateFormValues();
        }, 0);
      })
    );
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['companyAbbreviations']) {
      this.updateFormValues();
    }
  }

  private updateFormValues(): void {
    this.form.patchValue({
      licenseID: this.licenseAppointment.licenseID,
      employeeAppointmentID: this.licenseAppointment.employeeAppointmentID,
      appointmentStatus: this.licenseAppointment.appointmentStatus,
      companyID: this.licenseAppointment.companyID, // Ensure this value is updated
      carrierDate: this.licenseAppointment.carrierDate
        ? formatDate(this.licenseAppointment.carrierDate, 'yyyy-MM-dd', 'en-US')
        : null,
      appointmentEffectiveDate: this.licenseAppointment.appointmentEffectiveDate
        ? formatDate(
            this.licenseAppointment.appointmentEffectiveDate,
            'yyyy-MM-dd',
            'en-US'
          )
        : null,
      appointmentExpireDate: this.licenseAppointment.appointmentExpireDate
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
    // Force update the form control to trigger change detection
    this.form.get('companyID')?.updateValueAndValidity();
    // Trigger change detection
    this.cdr.detectChanges();
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

console.log('EMFTEST () - Agent License Appt. this.licenseApptForm.invalid:  ', this.form.invalid);       

    if (this.form.invalid) {

console.log('EMFTEST () - Agent License Appt. ERROR response => \n ', licenseApptItem);

      this.form.setErrors({ invalid: true });
      return;
    }

console.log('EMFTEST () - Agent License Appt. added successfully response => \n ', licenseApptItem);    

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
