import { Component, OnInit, Injectable, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  // AgentLicApplicationInfo,
  AgentLicenseAppointments,
  LicenseIncentiveInfo,
} from '../../../../_Models';
import {
  AgentComService,
  AgentDataService,
  DropdownDataService,
  LicIncentiveInfoDataService,
  ModalService,
} from '../../../../_services';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-license-incentive',
  templateUrl: './license-incentive.component.html',
  styleUrl: './license-incentive.component.css',
})
@Injectable()
export class LicenseIncentiveComponent implements OnInit, OnDestroy {
  isFormSubmitted: boolean = false;
  incentiveUpdateForm: FormGroup;
  modeEdit: boolean = false;
  licenseMgmtData: AgentLicenseAppointments[] = [];
  licenseIncentiveInfo: LicenseIncentiveInfo = {} as LicenseIncentiveInfo;
  currentIndex: number = 0;
  panelOpenState = false;
  rolloutGroups: { value: string; label: string }[] = [];
  dmManagers: { value: number; label: string }[] = [];

  private subscriptions = new Subscription();

  constructor(
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    private drpdwnDataService: DropdownDataService,
    public licIncentiveInfoDataService: LicIncentiveInfoDataService,
    protected modalService: ModalService,
    private fb: FormBuilder
  ) {
    this.incentiveUpdateForm = this.fb.group({
      RollOutGroup: ['Select'],
      DMEmploymentID: [''],
      CCdBMEmploymentID: [''],
      DMSentDate: [''],
      DMApprovalDate: [''],
      DMDeclinedDate: [''],
      DM10DaySentDate: [''],
      DM20DaySentDate: [''],
      DMComment: [''],
      DMSentBySOEID: [''],
      DM10DaySentBySOEID: [''],
      DM20DaySentBySOEID: [''],
      TMSentDate: [''],
      CCd2BMEmploymentID: [''],
      TMApprovalDate: [''],
      TMDeclinedDate: [''],
      TM10DaySentDate: [''],
      TM45DaySentDate: [''],
      TMExceptionDate: [''],
      TMSentBySOEID: [''],
      TMComment: [''],
      TMException: [''],
      TMOkToSellSentDate: [''],
      CCOkToSellBMEmploymentID: [''],
      TMOMSApprtoSendToHRDate: [''],
      IncetivePeriodDate: [''],
      IncentiveStatus: [''],
      TMOkToSellSentBySOEID: [''],
      Notes: [''],
    });
  }

  ngOnInit(): void {
    this.currentIndex = this.agentDataService.licenseMgmtDataIndex;
    this.licenseMgmtData =
      this.agentDataService.agentInformation.agentLicenseAppointments;

    this.subscriptions.add(
      this.drpdwnDataService
        .fetchDropdownData('GetRollOutGroups')
        .subscribe((rolloutGroups: { value: string; label: string }[]) => {
          this.rolloutGroups = [{value: 'OneTrak', label: 'OneTrak'}, ...rolloutGroups];
        })
    );

    this.subscriptions.add(
      this.licIncentiveInfoDataService
        .fetchDMManagers()
        .subscribe((dmManagers: { value: number; label: string }[]) => {

console.log('EMFTEST (ngOnInit) - dmManagers => \n', dmManagers);

          this.dmManagers = dmManagers;
        })
    );

    this.subscriptions.add(
      this.licIncentiveInfoDataService
        .fetchLicIncentiveInfo(
          this.licenseMgmtData[this.currentIndex].employeeLicenseId
        )
        .subscribe((licenseIncentiveInfo: LicenseIncentiveInfo) => {
          console.log(
            'EMFTEST (ngOnInit) - licenseIncentiveInfo => \n',
            licenseIncentiveInfo
          );

          this.licenseIncentiveInfo = licenseIncentiveInfo;

          let dmEmploymentIDValue = this.dmManagers.find(mgr => mgr.label === licenseIncentiveInfo.dmMgrName)?.value;
          // if (correspondingValue) {
          //   this.incentiveUpdateForm.patchValue({
          //     DMEmploymentID: correspondingValue
          //   });
          // }

          this.incentiveUpdateForm.patchValue({
            RollOutGroup: licenseIncentiveInfo.rollOutGroup,
            DMEmploymentID: dmEmploymentIDValue || 0,
            // CCdBMEmploymentID: licenseIncentiveInfo.ccdBmEmploymentId,
            DMSentDate: licenseIncentiveInfo.dmSentDate
              ? formatDate(
                  licenseIncentiveInfo.dmSentDate,
                  'yyyy-MM-dd',
                  'en-US'
                )
              : null,
            DMApprovalDate: licenseIncentiveInfo.dmApprovalDate
              ? formatDate(
                  licenseIncentiveInfo.dmApprovalDate,
                  'yyyy-MM-dd',
                  'en-US'
                )
              : null,
            DMDeclinedDate: licenseIncentiveInfo.dmDeclinedDate
              ? formatDate(
                  licenseIncentiveInfo.dmDeclinedDate,
                  'yyyy-MM-dd',
                  'en-US'
                )
              : null,
            // DM10DaySentDate: licenseIncentiveInfo.dm10DaySentDate
            //   ? formatDate(
            //       licenseIncentiveInfo.dm10DaySentDate,
            //       'yyyy-MM-dd',
            //       'en-US'
            //     )
            //   : null,
            // DM20DaySentDate: licenseIncentiveInfo.dm20DaySentDate
            //   ? formatDate(
            //       licenseIncentiveInfo.dm20DaySentDate,
            //       'yyyy-MM-dd',
            //       'en-US'
            //     )
            //   : null,
            DMComment: licenseIncentiveInfo.dmComment,
            // DMSentBySOEID: licenseIncentiveInfo.dmSentBySoeid,
            // DM10DaySentBySOEID: licenseIncentiveInfo.dm10DaySentBySoeid,
            // DM20DaySentBySOEID: licenseIncentiveInfo.dm20DaySentBySoeid,
            TMSentDate: licenseIncentiveInfo.tmSentDate
              ? formatDate(
                  licenseIncentiveInfo.tmSentDate,
                  'yyyy-MM-dd',
                  'en-US'
                )
              : null,
            // CCd2BMEmploymentID: licenseIncentiveInfo.ccd2BmEmploymentId,
            TMApprovalDate: licenseIncentiveInfo.tmApprovalDate
              ? formatDate(
                  licenseIncentiveInfo.tmApprovalDate,
                  'yyyy-MM-dd',
                  'en-US'
                )
              : null,
            TMDeclinedDate: licenseIncentiveInfo.tmDeclinedDate
              ? formatDate(
                  licenseIncentiveInfo.tmDeclinedDate,
                  'yyyy-MM-dd',
                  'en-US'
                )
              : null,
            // TM10DaySentDate: licenseIncentiveInfo.tm10DaySentDate
            //   ? formatDate(
            //       licenseIncentiveInfo.tm10DaySentDate,
            //       'yyyy-MM-dd',
            //       'en-US'
            //     )
            //   : null,
            // TM45DaySentDate: licenseIncentiveInfo.tm45DaySentDate
            //   ? formatDate(
            //       licenseIncentiveInfo.tm45DaySentDate,
            //       'yyyy-MM-dd',
            //       'en-US'
            //     )
            //   : null,
            TMExceptionDate: licenseIncentiveInfo.tmExceptionDate
              ? formatDate(
                  licenseIncentiveInfo.tmExceptionDate,
                  'yyyy-MM-dd',
                  'en-US'
                )
              : null,
            // TMSentBySOEID: licenseIncentiveInfo.tmSentBySoeid,
            TMComment: licenseIncentiveInfo.tmComment,
            TMException: licenseIncentiveInfo.tmException,
            TMOkToSellSentDate: licenseIncentiveInfo.tmOkToSellSentDate
              ? formatDate(
                  licenseIncentiveInfo.tmOkToSellSentDate,
                  'yyyy-MM-dd',
                  'en-US'
                )
              : null,
            // CCOkToSellBMEmploymentID: licenseIncentiveInfo.ccOkToSellBmEmploymentId,
            // TMOMSApprtoSendToHRDate: licenseIncentiveInfo.tmoMsApprToSendToHrDate
            //   ? formatDate(
            //       licenseIncentiveInfo.tmoMsApprToSendToHrDate,
            //       'yyyy-MM-dd',
            //       'en-US'
            //     )
            //   : null,
            // IncetivePeriodDate: licenseIncentiveInfo.incentivePeriodDate
            //   ? formatDate(
            //       licenseIncentiveInfo.incentivePeriodDate,
            //       'yyyy-MM-dd',
            //       'en-US'
            //     )
            //   : null,
            IncentiveStatus: licenseIncentiveInfo.incentiveStatus,
            // TMOkToSellSentBySOEID: licenseIncentiveInfo.tmOkToSellSentBySoeid,
            Notes: licenseIncentiveInfo.notes,
          });
        })
    );
  }

  onEditToggle() {
    this.modeEdit = !this.modeEdit;
  }

  onSubmit() {
    this.modeEdit = false;
    // console.log(form);
  }

  // Pagination
  // nextPage() {
  //   if (this.currentIndex < this.licenseMgmtData.length - 1) {
  //     this.currentIndex++;
  //     this.agentDataService.licenseMgmtDataIndexChanged.next(this.currentIndex);
  //   }
  // }

  // previousPage() {
  //   if (this.currentIndex > 0) {
  //     this.currentIndex--;
  //     this.agentDataService.licenseMgmtDataIndexChanged.next(this.currentIndex);
  //   }
  // }

  // getPageInfo(): string {
  //   return `${this.currentIndex + 1} of ${this.licenseMgmtData.length}`;
  // }

  // isDisplayPrevious(): boolean {
  //   return this.currentIndex > 0;
  // }

  // isDisplayNext(): boolean {
  //   return this.currentIndex < this.licenseMgmtData.length - 1;
  // }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
