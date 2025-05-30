import {
  Component,
  Injectable,
  OnInit,
  OnDestroy,
  ChangeDetectorRef,
  AfterViewInit,
  SimpleChanges,
  OnChanges,
  EventEmitter,
  Output,
} from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { Subject, Subscription, switchMap, tap } from 'rxjs';

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
import { AgentInfo, LicenseAppointment } from '../../../../../_Models';
import { formatDate } from '@angular/common';
import { dateValidator } from '../../../../../_shared';

@Component({
  selector: 'app-edit-license-appointment',
  templateUrl: './edit-license-appointment.component.html',
  styleUrl: './edit-license-appointment.component.css',
})
@Injectable()
export class EditLicenseAppointmentComponent
  implements OnInit, AfterViewInit, OnChanges, OnDestroy
{
  @Output() callParentRefreshData = new EventEmitter<any>();
  isFormSubmitted: boolean = false;
  companyAbbreviations: { value: number; label: string }[] = [];
  licenseAppointment: LicenseAppointment = {} as LicenseAppointment;
  appointmentStatuses: any[] = [
    'Select',
    ...this.conService.getAppointmentStatuses(),
  ];
  employeeAppointmentID: number = 0;
  employeeLicenseID: number = 0;
  form = this.fb.group({
    licenseID: new FormControl({ value: 0, disabled: true }),
    employeeLicenseId: new FormControl(0),
    employeeAppointmentID: new FormControl({ value: 0, disabled: true }),
    appointmentStatus: new FormControl(''),
    companyID: new FormControl(0),
    carrierDate: new FormControl('', dateValidator),
    appointmentEffectiveDate: new FormControl('', dateValidator),
    appointmentExpireDate: new FormControl('', dateValidator),
    appointmentTerminationDate: new FormControl('', dateValidator),
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
    private appComService: AppComService,
    private fb: FormBuilder,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.subscriptions.add(
      this.agentDataService.agentEmployeeLicenseIDChanged.subscribe(
        (employeeLicenseID: number) => {
          this.employeeLicenseID = employeeLicenseID;
        }
      )
    );

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
              this.form.reset({
                licenseID: 0,
                employeeLicenseId: 0,
                employeeAppointmentID: 0,
                appointmentStatus: 'Select',
                companyID: 0,
                carrierDate: null,
                appointmentEffectiveDate: null,
                appointmentExpireDate: null,
                appointmentTerminationDate: null,
              });
              this.companyAbbreviations = [{ value: 0, label: 'Select' }];

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
      employeeLicenseId: this.licenseAppointment.employeeLicenseId,
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
    licenseApptItem.employeeLicenseID = this.employeeLicenseID;
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
        .upsertLicenseAppointment(licenseApptItem)
        .subscribe({
          next: (response) => {
            this.appComService.updateAppMessage(
              'Data submitted successfully.' // 'Data submitted successfully.'
            );
            // const modalDiv = document.getElementById('modal-edit-license-appt');
            // if (modalDiv != null) {
            //   modalDiv.style.display = 'none';
            // }
            this.agentDataService
              .fetchAgentInformation(
                this.agentDataService.agentInformation.employeeID
              )
              .subscribe((agentInfo: AgentInfo) => {
                // this.isLoading = false;
                // this.ticklerCount = agentInfo.ticklerItems.length;
                // this.worklistCount = agentInfo.worklistItems.length;
                // this.agentInfo = agentInfo;
                this.agentDataService.storeLicenseAppt('', null);
                this.agentDataService.storeLicenseInfo('INSERT', null);
                this.callParentRefreshData.emit(agentInfo);
              });

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
