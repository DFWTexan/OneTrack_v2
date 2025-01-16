import { Component, Injectable, OnInit, OnDestroy, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import {
  AgentComService,
  AgentDataService,
  ConstantsDataService,
  ErrorMessageService,
  LicIncentiveInfoDataService,
  UserAcctInfoDataService,
} from '../../../../../_services';
import { AgentLicenseAppointments } from '../../../../../_Models';

@Component({
  selector: 'app-edit-license-renewal',
  templateUrl: './edit-license-renewal.component.html',
  styleUrl: './edit-license-renewal.component.css',
})
@Injectable()
export class EditLicenseRenewalComponent implements OnInit, OnDestroy {
  @Output() callParentGetData = new EventEmitter<void>();
  isFormSubmitted: boolean = false;
  licRenewalForm!: FormGroup;
  renewalMethods: string[] = [];
  licenseMgmtData: AgentLicenseAppointments[] =
    [] as AgentLicenseAppointments[];
  currentIndex: number = 0;
  licenseApplicationID: number = 0;

  private subscriptions = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    private licApplicationDataService: LicIncentiveInfoDataService,
    private userInfoDataService: UserAcctInfoDataService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.licRenewalForm = this.fb.group({
      licenseApplicationID: [],
      employeeLicenseID: new FormControl({ value: '', disabled: true }),
      renewalMethod: [null],
      sentToAgentDate: [null],
      recFromAgentDate: new FormControl(null),
      sentToStateDate: [null],
      recFromStateDate: new FormControl(null),
      renewalDate: [null],
    });

    this.currentIndex = this.agentDataService.licenseMgmtDataIndex;
    this.subscriptions.add(
      this.agentDataService.licenseMgmtDataIndexChanged.subscribe(
        (index: number) => {
          this.currentIndex = index;
        }
      )
    );

    this.licenseMgmtData =
      this.agentDataService.agentInformation.agentLicenseAppointments;
    this.subscriptions.add(
      this.agentDataService.licenseMgmtDataChanged.subscribe(
        (licenseMgmtData: AgentLicenseAppointments) => {
          this.licenseMgmtData = [licenseMgmtData];
        }
      )
    );

    this.subscriptions.add(
      this.agentComService.modeLicRenewalChanged.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.renewalMethods = this.conService.getLicenseRenewalMethods();

          this.subscriptions.add(
            this.agentDataService.licenseRenewalItemChanged.subscribe(
              (licRenewal: any) => {
                this.licenseApplicationID = licRenewal.licenseApplicationID;
                this.licRenewalForm.patchValue({
                  licenseApplicationID: licRenewal.licenseApplicationID,
                  employeeLicenseID: licRenewal.employeeLicenseID,
                  sentToAgentDate: licRenewal.sentToAgentDate
                    ? formatDate(
                        licRenewal.sentToAgentDate,
                        'yyyy-MM-dd',
                        'en-US'
                      )
                    : null,
                  recFromAgentDate: licRenewal.recFromAgentDate
                    ? formatDate(
                        licRenewal.recFromAgentDate,
                        'yyyy-MM-dd',
                        'en-US'
                      )
                    : null,
                  sentToStateDate: licRenewal.sentToStateDate
                    ? formatDate(
                        licRenewal.sentToStateDate,
                        'yyyy-MM-dd',
                        'en-US'
                      )
                    : null,
                  // recFromStateDate: licRenewal.recFromStateDate
                  //   ? formatDate(
                  //       licRenewal.recFromStateDate,
                  //       'yyyy-MM-dd',
                  //       'en-US'
                  //     )
                  //   : null,
                  renewalDate: licRenewal.renewalDate
                    ? formatDate(licRenewal.renewalDate, 'yyyy-MM-dd', 'en-US')
                    : null,
                  renewalMethod: licRenewal.renewalMethod,
                });
              }
            )
          );
        } else {
          this.renewalMethods = [
            'Select',
            ...this.conService.getLicenseRenewalMethods(),
          ];
          this.licRenewalForm.reset();
          this.licRenewalForm.patchValue({
            renewalMethod: 'Select',
          });
        }
      })
    );
  }

  onSubmit() {
    this.isFormSubmitted = true;
    let licenseRenewalInfo: any = this.licRenewalForm.value;

    licenseRenewalInfo.employeeLicenseId =
      this.licenseMgmtData[this.currentIndex].employeeLicenseId;
    this.agentDataService.agentInformation.employmentID;
    licenseRenewalInfo.applicationType = 'Renewal Application';
    licenseRenewalInfo.UserSOEID = this.userInfoDataService.userAcctInfo.soeid;

    if (this.agentComService.modeLicRenewal == 'EDIT') {
      licenseRenewalInfo.licenseApplicationID = this.licenseApplicationID;
      licenseRenewalInfo.applicationStatus = 'N/A';
    } else {
      licenseRenewalInfo.licenseApplicationID = 0;
    }

    // if (licenseRenewalInfo.licenseID === 0) {
    //   this.licenseForm.controls['licenseID'].setErrors({ invalid: true });
    // }

    // if (licenseInfo.licenseStatus === 'Select') {
    //   this.licenseForm.controls['licenseStatus'].setErrors({ invalid: true });
    // }

    // if (licenseInfo.licenseIssueDate === '01/01/0001 00:00:00') {
    //   this.licenseForm.controls['licenseIssueDate'].setErrors({
    //     invalid: true,
    //   });
    // }

    if (this.licRenewalForm.invalid) {
      this.licRenewalForm.setErrors({ invalid: true });
      return;
    }

    this.subscriptions.add(
      this.licApplicationDataService
        .upsertLicenseApplicationItem(licenseRenewalInfo)
        .subscribe({
          next: (response) => {
            this.callParentGetData.emit();
            this.forceCloseModal();
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
            this.forceCloseModal();
          },
        })
    );
  }

  onCloseModal() {
    if (this.licRenewalForm.dirty && !this.isFormSubmitted) {
      if (
        confirm('You have unsaved changes. Are you sure you want to close?')
      ) {
        this.licRenewalForm.reset();
        this.forceCloseModal();
      }
    } else {
      this.isFormSubmitted = false;
      this.forceCloseModal();
    }
  }

  forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-lic-renewal');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
