import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import {
  AgentComService,
  AgentDataService,
  AppComService,
  ConstantsDataService,
  ErrorMessageService,
  LicIncentiveInfoDataService,
  UserAcctInfoDataService,
} from '../../../../../_services';
import { AgentLicenseAppointments } from '../../../../../_Models';

@Component({
  selector: 'app-edit-lic-appl-info',
  templateUrl: './edit-lic-appl-info.component.html',
  styleUrl: './edit-lic-appl-info.component.css',
})
@Injectable()
export class EditLicApplInfoComponent implements OnInit, OnDestroy {
  isFormSubmitted: boolean = false;
  licApplicationForm!: FormGroup;
  applicationStatuses: string[] = [
    'Select',
    ...this.constDataService.getApplicationStatuses(),
  ];
  employeeLicenseID: number = 0;
  licenseMgmtData: AgentLicenseAppointments[] =
    [] as AgentLicenseAppointments[];
  currentIndex: number = 0;

  private subscriptions = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    private constDataService: ConstantsDataService,
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    public appComService: AppComService,
    private licApplicationDataService: LicIncentiveInfoDataService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.licApplicationForm = new FormGroup({
      licenseApplicationID: new FormControl(0),
      employeeLicenseID: new FormControl(null),
      sentToAgentDate: new FormControl(null),
      recFromAgentDate: new FormControl(null),
      sentToStateDate: new FormControl(null),
      recFromStateDate: new FormControl(null),
      applicationStatus: new FormControl(null),
      applicationType: new FormControl(null),
      renewalDate: new FormControl(null),
      renewalMethod: new FormControl(null),
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
      this.agentComService.modeLicAppInfoChanged.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.subscriptions.add(
            this.agentDataService.licenseApplicationItemChanged.subscribe(
              (licApplication: any) => {
                this.employeeLicenseID = licApplication.employeeLicenseID;
                this.licApplicationForm.patchValue({
                  licenseApplicationID: licApplication.licenseApplicationID,
                  sentToAgentDate: formatDate(
                    licApplication.sentToAgentDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  recFromAgentDate: formatDate(
                    licApplication.recFromAgentDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  sentToStateDate: formatDate(
                    licApplication.sentToStateDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  recFromStateDate: formatDate(
                    licApplication.recFromStateDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  applicationStatus: licApplication.applicationStatus,
                  applicationType: licApplication.applicationType,
                });
              }
            )
          );
        } else {
          this.licApplicationForm.reset();
          this.licApplicationForm.patchValue({
            applicationStatus: 'Select',
            applicationType: 'Select',
          });
        }
      })
    );
  }

  onSubmit() {
    let licApplicationItem: any = this.licApplicationForm.value;
    licApplicationItem.employeeLicenseID =
      this.licenseMgmtData[this.currentIndex].employeeLicenseId;
    licApplicationItem.applicationType = 'Initial Application';
    licApplicationItem.UserSOEID =
      this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.licApplicationForm.invalid) {
      this.licApplicationForm.setErrors({ invalid: true });
      return;
    }

    if (this.agentComService.modeLicAppInfo === 'INSERT') {
      licApplicationItem.licenseApplicationID = 0;
    }

    this.subscriptions.add(
      this.licApplicationDataService
        .upsertLicenseApplicationItem(licApplicationItem)
        .subscribe({
          next: (response) => {
            this.isFormSubmitted = true;
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
    const modalDiv = document.getElementById('modal-edit-lic-appl-info');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  onCloseModal() {
    // const modalDiv = document.getElementById('modal-edit-lic-appl-info');
    // if (modalDiv != null) {
    //   modalDiv.style.display = 'none';
    // }
    if (this.licApplicationForm.dirty && !this.isFormSubmitted) {
      if (
        confirm('You have unsaved changes. Are you sure you want to close?')
      ) {
        this.forceCloseModal();
        this.licApplicationForm.reset();
        this.licApplicationForm.patchValue({
          applicationStatus: 'Select',
          applicationType: 'Select',
        });
      }
    } else {
      this.isFormSubmitted = false;
      this.forceCloseModal();
    }
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
