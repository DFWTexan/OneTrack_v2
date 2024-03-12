import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import { AgentComService, AgentDataService } from '../../../../../_services';
import { AgentLicenseAppointments } from '../../../../../_Models';

@Component({
  selector: 'app-edit-license-info',
  templateUrl: './edit-license-info.component.html',
  styleUrl: './edit-license-info.component.css',
})
export class EditLicenseInfoComponent implements OnInit, OnDestroy {
  licenseForm: FormGroup;
  @Input()currentIndex: number = 0;
  licenseMgmtData: AgentLicenseAppointments[] = [];
  subscriptionMode: Subscription = new Subscription();
  subscriptionData: Subscription = new Subscription();

  constructor(
    private agentDataService: AgentDataService,
    public agentComService: AgentComService,
    private fb: FormBuilder
  ) {
    this.licenseForm = this.fb.group({
      agentName: [{ value: '', disabled: true }],
      employeeLicenseId: [{ value: '', disabled: true }, Validators.required],
      licenseState: ['', Validators.required],
      licenseName: ['', Validators.required],
      licenseNumber: ['', Validators.required],
      licenseStatus: ['', Validators.required],
      affiliatedLicense: [''],
      originalIssueDate: ['', Validators.required],
      lineOfAuthIssueDate: ['', Validators.required],
      licenseEffectiveDate: ['', Validators.required],
      licenseExpirationDate: ['', Validators.required],
      licenseNotes: [''],
      required: [''],
      reinstatementDate: [''],
      resNoneRes: [''],
      employmentID: ['', Validators.required],
    });
  }

  // ngOnInit(): void {
  //   this.currentIndex = this.agentDataService.licenseMgmtDataIndex;
  //   this.licenseMgmtData =
  //     this.agentDataService.agentInformation.agentLicenseAppointments;
  //     this.licenseForm.patchValue({
  //       agentName: this.agentDataService.agentInformation.lastName + ', ' + this.agentDataService.agentInformation.firstName,
  //       employeeLicenseId: this.licenseMgmtData[this.currentIndex].employeeLicenseId,
  //       licenseState: this.licenseMgmtData[this.currentIndex].licenseState,
  //       licenseName: this.licenseMgmtData[this.currentIndex].employeeLicenseId,
  //       licenseNumber: this.licenseMgmtData[this.currentIndex].licenseNumber,
  //       licenseStatus: this.licenseMgmtData[this.currentIndex].licenseStatus,
  //       affiliatedLicense: 'SELECT-TBD...',
  //       originalIssueDate: this.licenseMgmtData[this.currentIndex].originalIssueDate,
  //       lineOfAuthIssueDate: this.licenseMgmtData[this.currentIndex].lineOfAuthIssueDate,
  //       licenseEffectiveDate: this.licenseMgmtData[this.currentIndex].licenseEffectiveDate,
  //       licenseExpirationDate: this.licenseMgmtData[this.currentIndex].licenseExpirationDate,
  //       licenseNotes: 'TBD...',
  //       required: 'Yes',
  //       reinstatementDate: 'TBD...',
  //       resNoneRes: this.licenseMgmtData[this.currentIndex].resNoneRes,
  //       employmentID: this.licenseMgmtData[this.currentIndex].employmentID,
  //     });
  // }
  ngOnInit(): void {
    this.subscriptionMode =
      this.agentComService.modeLicenseMgmtChanged.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          // this.currentIndex = this.agentDataService.licenseMgmtDataIndex;
          this.subscriptionData =
            this.agentDataService.licenseMgmtDataIndexChanged.subscribe(
              (licenseMgmtDataIndex: any) => {
                this.currentIndex = licenseMgmtDataIndex;
              }
            );

          this.licenseMgmtData =
            this.agentDataService.agentInformation.agentLicenseAppointments;
          let originalIssueDate =
            this.licenseMgmtData[this.currentIndex].originalIssueDate;
          let lineOfAuthIssueDate =
            this.licenseMgmtData[this.currentIndex].lineOfAuthIssueDate;
          let licenseEffectiveDate =
            this.licenseMgmtData[this.currentIndex].licenseEffectiveDate;
          let licenseExpirationDate =
            this.licenseMgmtData[this.currentIndex].licenseExpirationDate;
          this.licenseForm.patchValue({
            agentName:
              this.agentDataService.agentInformation.lastName +
              ', ' +
              this.agentDataService.agentInformation.firstName,
            employeeLicenseId:
              this.licenseMgmtData[this.currentIndex].employeeLicenseId,
            licenseState: this.licenseMgmtData[this.currentIndex].licenseState,
            licenseName:
              this.licenseMgmtData[this.currentIndex].employeeLicenseId,
            licenseNumber:
              this.licenseMgmtData[this.currentIndex].licenseNumber,
            licenseStatus:
              this.licenseMgmtData[this.currentIndex].licenseStatus,
            affiliatedLicense: 'SELECT-TBD...',
            originalIssueDate: originalIssueDate
              ? formatDate(originalIssueDate, 'yyyy-MM-dd', 'en-US')
              : null,
            lineOfAuthIssueDate: lineOfAuthIssueDate
              ? formatDate(lineOfAuthIssueDate, 'yyyy-MM-dd', 'en-US')
              : null,
            licenseEffectiveDate: licenseEffectiveDate
              ? formatDate(licenseEffectiveDate, 'yyyy-MM-dd', 'en-US')
              : null,
            licenseExpirationDate: licenseExpirationDate
              ? formatDate(licenseExpirationDate, 'yyyy-MM-dd', 'en-US')
              : null,
            licenseNotes: 'TBD...',
            required: 'Yes',
            reinstatementDate: 'TBD...',
            resNoneRes: this.licenseMgmtData[this.currentIndex].resNoneRes,
            employmentID: this.licenseMgmtData[this.currentIndex].employmentID,
          });
        } else {
          this.licenseForm.reset();
        }
      });
  }

  onSubmit() {
    console.log(
      'EMFTest - (app-edit-license-info) onSubmit => \n',
      this.licenseForm.value
    );
  }

  ngOnDestroy(): void {
    this.subscriptionMode.unsubscribe();
    this.subscriptionData.unsubscribe();
  }
}
