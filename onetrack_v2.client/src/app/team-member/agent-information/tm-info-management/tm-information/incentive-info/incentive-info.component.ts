import { Component, Injectable, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { formatDate } from '@angular/common';
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
import {
  AgentLicenseAppointments,
  LicenseIncentiveInfo,
} from '../../../../../_Models';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../../../../_components';

@Component({
  selector: 'app-incentive-info',
  templateUrl: './incentive-info.component.html',
  styleUrl: './incentive-info.component.css',
})
@Injectable()
export class IncentiveInfoComponent implements OnInit, OnDestroy {
  isFormSubmitted: boolean = false;
  isPageDirty: boolean = false;
  incentiveUpdateForm: FormGroup;
  employeeLicenseID: number = 0;
  licenseInfo: any = {};
  employmentLicenseIncentiveID: number = 0;
  modeEdit: boolean = false;
  licenseMgmtData: AgentLicenseAppointments[] = [];
  licenseIncentiveInfo: LicenseIncentiveInfo = {} as LicenseIncentiveInfo;
  currentIndex: number = 0;
  panelOpenState = false;
  rolloutGroups: { value: string; label: string }[] = [];
  dmManagers: { value: number; label: string }[] = [];
  dmEmploymentIDValue: any = 0;
  bmManagers: { value: number; label: string }[] = [];
  ccdBMEmploymentIDValue: any = 0;
  licenseTeches: { value: string; label: string }[] = [];
  incentiveStatuses: string[] = this.conService.getIncentiveStatuses();
  eventAction: string = '';
  vObject: any = {};

  private subscriptions = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public agentDataService: AgentDataService,
    private conService: ConstantsDataService,
    public agentComService: AgentComService,
    private drpdwnDataService: DropdownDataService,
    public licIncentiveInfoDataService: LicIncentiveInfoDataService,
    public dialog: MatDialog,
    public userAcctInfoDataService: UserAcctInfoDataService,
    public appComService: AppComService,
    private fb: FormBuilder
  ) {
    this.incentiveUpdateForm = this.fb.group({
      RollOutGroup: ['Select'],
      DMEmploymentID: [0],
      CCdBMEmploymentID: [0],
      DMSentDate: [null],
      DMApprovalDate: [null],
      DMDeclinedDate: [null],
      DM10DaySentDate: [null],
      DM20DaySentDate: [null],
      DMComment: [null],
      DMSentBySOEID: [null],
      DM10DaySentBySOEID: [null],
      DM20DaySentBySOEID: [null],
      TMSentDate: [null],
      CCd2BMEmploymentID: [0],
      TMApprovalDate: [null],
      TMDeclinedDate: [null],
      TM10DaySentDate: [null],
      TM45DaySentDate: [null],
      TMExceptionDate: [null],
      TMSentBySOEID: [null],
      TMComment: [null],
      TMException: [null],
      TMOkToSellSentDate: [null],
      CCOkToSellBMEmploymentID: [0],
      TMOMSApprtoSendToHRDate: [null],
      IncetivePeriodDate: [null],
      IncentiveStatus: [null],
      TMOkToSellSentBySOEID: [null],
      Notes: [null],
    });
  }

  ngOnInit() {
    this.isFormSubmitted = false;
    this.isPageDirty = false;

    this.licenseInfo = this.agentDataService.licenseInfo;
    this.employeeLicenseID = this.agentDataService.agentEmployeeLicenseID;
    if (this.employeeLicenseID === 0) {
      this.errorMessageService.setErrorMessage(
        'Employee License ID is not set.'
      );
      return;
    }

    this.licIncentiveInfoDataService
      .fetchLicIncentiveInfo(this.employeeLicenseID)
      .subscribe((licenseIncentiveInfo: LicenseIncentiveInfo) => {
        this.licenseIncentiveInfo = licenseIncentiveInfo;
        this.employmentLicenseIncentiveID =
          licenseIncentiveInfo.employmentLicenseIncentiveID;

        // Set the dropdown values
        this.dmEmploymentIDValue = this.dmManagers.find(
          (mgr) => mgr.label === licenseIncentiveInfo.dmMgrName
        )?.value;
        this.ccdBMEmploymentIDValue = this.bmManagers.find(
          (mgr) => mgr.label === licenseIncentiveInfo.cCdBRMgrName
        )?.value;
        let dmSentByValue = this.licenseTeches.find(
          (tech) => tech.label === licenseIncentiveInfo.dmSentBy
        )?.value;
        let dM10DaySentByValue = this.licenseTeches.find(
          (tech) => tech.label === licenseIncentiveInfo.dM10DaySentBy
        )?.value;
        let dM20DaySentByValue = this.licenseTeches.find(
          (tech) => tech.label === licenseIncentiveInfo.dM20DaySentBy
        )?.value;
        let ccd2BmEmploymentIdValue = this.bmManagers.find(
          (mgr) => mgr.label === licenseIncentiveInfo.cCd2BRMgrName
        )?.value;
        let tmSentByValue = this.licenseTeches.find(
          (tech) => tech.label === licenseIncentiveInfo.tmSentBy
        )?.value;
        let tmOkToSellSentByValue = this.licenseTeches.find(
          (tech) => tech.label === licenseIncentiveInfo.tmOkToSellSentBy
        )?.value;
        let ccOkToSellBmEmploymentIdValue = this.bmManagers.find(
          (mgr) => mgr.label === licenseIncentiveInfo.cCdOkToSellBRMgrName
        )?.value;

        this.incentiveUpdateForm.patchValue({
          RollOutGroup: licenseIncentiveInfo.rollOutGroup,
          DMEmploymentID: this.dmEmploymentIDValue || 0,
          CCdBMEmploymentID: this.ccdBMEmploymentIDValue || 0,
          DMSentDate: licenseIncentiveInfo.dmSentDate
            ? formatDate(licenseIncentiveInfo.dmSentDate, 'yyyy-MM-dd', 'en-US')
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
          DM10DaySentDate: licenseIncentiveInfo.dM10DaySentDate
            ? formatDate(
                licenseIncentiveInfo.dM10DaySentDate,
                'yyyy-MM-dd',
                'en-US'
              )
            : null,
          DM20DaySentDate: licenseIncentiveInfo.dM20DaySentDate
            ? formatDate(
                licenseIncentiveInfo.dM20DaySentDate,
                'yyyy-MM-dd',
                'en-US'
              )
            : null,
          DMComment: licenseIncentiveInfo.dmComment,
          // DMSentBySOEID: dmSentByValue || null,
          DMSentBySOEID: licenseIncentiveInfo.dmSentBy || null,
          DM10DaySentBySOEID: licenseIncentiveInfo.dM10DaySentBy || null,
          DM20DaySentBySOEID: licenseIncentiveInfo.dM20DaySentBy || null,
          TMSentDate: licenseIncentiveInfo.tmSentDate
            ? formatDate(licenseIncentiveInfo.tmSentDate, 'yyyy-MM-dd', 'en-US')
            : null,
          CCd2BMEmploymentID: ccd2BmEmploymentIdValue || 0,
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
          TM10DaySentDate: licenseIncentiveInfo.tM10DaySentDate
            ? formatDate(
                licenseIncentiveInfo.tM10DaySentDate,
                'yyyy-MM-dd',
                'en-US'
              )
            : null,
          TM45DaySentDate: licenseIncentiveInfo.tM45DaySentDate
            ? formatDate(
                licenseIncentiveInfo.tM45DaySentDate,
                'yyyy-MM-dd',
                'en-US'
              )
            : null,
          TMExceptionDate: licenseIncentiveInfo.tmExceptionDate
            ? formatDate(
                licenseIncentiveInfo.tmExceptionDate,
                'yyyy-MM-dd',
                'en-US'
              )
            : null,
          TMSentBySOEID: licenseIncentiveInfo.tmSentBy || null,
          TMComment: licenseIncentiveInfo.tmComment,
          TMException: licenseIncentiveInfo.tmException,
          TMOkToSellSentDate: licenseIncentiveInfo.tmOkToSellSentDate
            ? formatDate(
                licenseIncentiveInfo.tmOkToSellSentDate,
                'yyyy-MM-dd',
                'en-US'
              )
            : null,
          CCOkToSellBMEmploymentID: ccOkToSellBmEmploymentIdValue || 0,
          TMOMSApprtoSendToHRDate: licenseIncentiveInfo.tmomsApprtoSendToHRDate
            ? formatDate(
                licenseIncentiveInfo.tmomsApprtoSendToHRDate,
                'yyyy-MM-dd',
                'en-US'
              )
            : null,
          IncetivePeriodDate: licenseIncentiveInfo.incetivePeriodDate
            ? formatDate(
                licenseIncentiveInfo.incetivePeriodDate,
                'yyyy-MM-dd',
                'en-US'
              )
            : null,
          IncentiveStatus: licenseIncentiveInfo.incentiveStatus,
          TMOkToSellSentBySOEID: licenseIncentiveInfo.tmOkToSellSentBy || null,
          Notes: licenseIncentiveInfo.notes,
        });
      });

    //     this.subscriptions.add(
    //       this.agentDataService.agentEmployeeLicenseIDChanged.subscribe(
    //         (employeeLicenseID: number) => {
    //           this.employeeLicenseID = employeeLicenseID;

    // console.log(
    //             'EMFTEST (IncentiveInfo - ngOnInit) - IncentiveInfoComponent - employeeLicenseIDChanged: ',
    //             employeeLicenseID
    //           );

    //           if (this.employeeLicenseID === 0) {
    //             this.errorMessageService.setErrorMessage(
    //               'Employee License ID is not set.'
    //             );
    //           } else {
    //             // Fetch the license incentive info for the new employee license ID
    //             this.licIncentiveInfoDataService
    //               .fetchLicIncentiveInfo(
    //                 employeeLicenseID
    //               )
    //               .subscribe((licenseIncentiveInfo: LicenseIncentiveInfo) => {
    //                 this.licenseIncentiveInfo = licenseIncentiveInfo;
    //                 this.employmentLicenseIncentiveID =
    //                   licenseIncentiveInfo.employmentLicenseIncentiveID;

    //                 // Set the dropdown values
    //                 this.dmEmploymentIDValue = this.dmManagers.find(
    //                   (mgr) => mgr.label === licenseIncentiveInfo.dmMgrName
    //                 )?.value;
    //                 this.ccdBMEmploymentIDValue = this.bmManagers.find(
    //                   (mgr) => mgr.label === licenseIncentiveInfo.cCdBRMgrName
    //                 )?.value;
    //                 let dmSentByValue = this.licenseTeches.find(
    //                   (tech) => tech.label === licenseIncentiveInfo.dmSentBy
    //                 )?.value;
    //                 let dM10DaySentByValue = this.licenseTeches.find(
    //                   (tech) => tech.label === licenseIncentiveInfo.dM10DaySentBy
    //                 )?.value;
    //                 let dM20DaySentByValue = this.licenseTeches.find(
    //                   (tech) => tech.label === licenseIncentiveInfo.dM20DaySentBy
    //                 )?.value;
    //                 let ccd2BmEmploymentIdValue = this.bmManagers.find(
    //                   (mgr) => mgr.label === licenseIncentiveInfo.cCd2BRMgrName
    //                 )?.value;
    //                 let tmSentByValue = this.licenseTeches.find(
    //                   (tech) => tech.label === licenseIncentiveInfo.tmSentBy
    //                 )?.value;
    //                 let tmOkToSellSentByValue = this.licenseTeches.find(
    //                   (tech) => tech.label === licenseIncentiveInfo.tmOkToSellSentBy
    //                 )?.value;
    //                 let ccOkToSellBmEmploymentIdValue = this.bmManagers.find(
    //                   (mgr) =>
    //                     mgr.label === licenseIncentiveInfo.cCdOkToSellBRMgrName
    //                 )?.value;

    //                 this.incentiveUpdateForm.patchValue({
    //                   RollOutGroup: licenseIncentiveInfo.rollOutGroup,
    //                   DMEmploymentID: this.dmEmploymentIDValue || 0,
    //                   CCdBMEmploymentID: this.ccdBMEmploymentIDValue || 0,
    //                   DMSentDate: licenseIncentiveInfo.dmSentDate
    //                     ? formatDate(
    //                         licenseIncentiveInfo.dmSentDate,
    //                         'yyyy-MM-dd',
    //                         'en-US'
    //                       )
    //                     : null,
    //                   DMApprovalDate: licenseIncentiveInfo.dmApprovalDate
    //                     ? formatDate(
    //                         licenseIncentiveInfo.dmApprovalDate,
    //                         'yyyy-MM-dd',
    //                         'en-US'
    //                       )
    //                     : null,
    //                   DMDeclinedDate: licenseIncentiveInfo.dmDeclinedDate
    //                     ? formatDate(
    //                         licenseIncentiveInfo.dmDeclinedDate,
    //                         'yyyy-MM-dd',
    //                         'en-US'
    //                       )
    //                     : null,
    //                   DM10DaySentDate: licenseIncentiveInfo.dM10DaySentDate
    //                     ? formatDate(
    //                         licenseIncentiveInfo.dM10DaySentDate,
    //                         'yyyy-MM-dd',
    //                         'en-US'
    //                       )
    //                     : null,
    //                   DM20DaySentDate: licenseIncentiveInfo.dM20DaySentDate
    //                     ? formatDate(
    //                         licenseIncentiveInfo.dM20DaySentDate,
    //                         'yyyy-MM-dd',
    //                         'en-US'
    //                       )
    //                     : null,
    //                   DMComment: licenseIncentiveInfo.dmComment,
    //                   // DMSentBySOEID: dmSentByValue || null,
    //                   DMSentBySOEID: licenseIncentiveInfo.dmSentBy || null,
    //                   DM10DaySentBySOEID:
    //                     licenseIncentiveInfo.dM10DaySentBy || null,
    //                   DM20DaySentBySOEID:
    //                     licenseIncentiveInfo.dM20DaySentBy || null,
    //                   TMSentDate: licenseIncentiveInfo.tmSentDate
    //                     ? formatDate(
    //                         licenseIncentiveInfo.tmSentDate,
    //                         'yyyy-MM-dd',
    //                         'en-US'
    //                       )
    //                     : null,
    //                   CCd2BMEmploymentID: ccd2BmEmploymentIdValue || 0,
    //                   TMApprovalDate: licenseIncentiveInfo.tmApprovalDate
    //                     ? formatDate(
    //                         licenseIncentiveInfo.tmApprovalDate,
    //                         'yyyy-MM-dd',
    //                         'en-US'
    //                       )
    //                     : null,
    //                   TMDeclinedDate: licenseIncentiveInfo.tmDeclinedDate
    //                     ? formatDate(
    //                         licenseIncentiveInfo.tmDeclinedDate,
    //                         'yyyy-MM-dd',
    //                         'en-US'
    //                       )
    //                     : null,
    //                   TM10DaySentDate: licenseIncentiveInfo.tM10DaySentDate
    //                     ? formatDate(
    //                         licenseIncentiveInfo.tM10DaySentDate,
    //                         'yyyy-MM-dd',
    //                         'en-US'
    //                       )
    //                     : null,
    //                   TM45DaySentDate: licenseIncentiveInfo.tM45DaySentDate
    //                     ? formatDate(
    //                         licenseIncentiveInfo.tM45DaySentDate,
    //                         'yyyy-MM-dd',
    //                         'en-US'
    //                       )
    //                     : null,
    //                   TMExceptionDate: licenseIncentiveInfo.tmExceptionDate
    //                     ? formatDate(
    //                         licenseIncentiveInfo.tmExceptionDate,
    //                         'yyyy-MM-dd',
    //                         'en-US'
    //                       )
    //                     : null,
    //                   TMSentBySOEID: licenseIncentiveInfo.tmSentBy || null,
    //                   TMComment: licenseIncentiveInfo.tmComment,
    //                   TMException: licenseIncentiveInfo.tmException,
    //                   TMOkToSellSentDate: licenseIncentiveInfo.tmOkToSellSentDate
    //                     ? formatDate(
    //                         licenseIncentiveInfo.tmOkToSellSentDate,
    //                         'yyyy-MM-dd',
    //                         'en-US'
    //                       )
    //                     : null,
    //                   CCOkToSellBMEmploymentID: ccOkToSellBmEmploymentIdValue || 0,
    //                   TMOMSApprtoSendToHRDate:
    //                     licenseIncentiveInfo.tmomsApprtoSendToHRDate
    //                       ? formatDate(
    //                           licenseIncentiveInfo.tmomsApprtoSendToHRDate,
    //                           'yyyy-MM-dd',
    //                           'en-US'
    //                         )
    //                       : null,
    //                   IncetivePeriodDate: licenseIncentiveInfo.incetivePeriodDate
    //                     ? formatDate(
    //                         licenseIncentiveInfo.incetivePeriodDate,
    //                         'yyyy-MM-dd',
    //                         'en-US'
    //                       )
    //                     : null,
    //                   IncentiveStatus: licenseIncentiveInfo.incentiveStatus,
    //                   TMOkToSellSentBySOEID:
    //                     licenseIncentiveInfo.tmOkToSellSentBy || null,
    //                   Notes: licenseIncentiveInfo.notes,
    //                 });
    //               });
    //           }
    //         }
    //       )
    //     );

    this.rolloutGroups = this.drpdwnDataService.rollOutGroups;
    this.subscriptions.add(
      this.drpdwnDataService.rollOutGroupsChanged.subscribe(
        (rolloutGroups: { value: string; label: string }[]) => {
          this.rolloutGroups = rolloutGroups;
        }
      )
    );
    this.dmManagers = this.licIncentiveInfoDataService.dmManagers;
    this.subscriptions.add(
      this.licIncentiveInfoDataService.dmManagersChanged.subscribe(
        (dmManagers: { value: number; label: string }[]) => {
          this.dmManagers = dmManagers;
        }
      )
    );
    this.bmManagers = this.licIncentiveInfoDataService.bmManagers;
    this.subscriptions.add(
      this.licIncentiveInfoDataService.bmManagersChanged.subscribe(
        (bmManagers: { value: number; label: string }[]) => {
          this.bmManagers = bmManagers;
        }
      )
    );
    this.licenseTeches = this.licIncentiveInfoDataService.licenseTeches;
    this.subscriptions.add(
      this.licIncentiveInfoDataService.licenseTechesChanged.subscribe(
        (licenseTeches: { value: string; label: string }[]) => {
          this.licenseTeches = licenseTeches;
        }
      )
    );

    // this.subscriptions.add(
    //       this.licIncentiveInfoDataService
    //         .fetchLicIncentiveInfo(
    //           this.employeeLicenseID
    //           // this.employeeLicenseID
    //         )
    //         .subscribe((licenseIncentiveInfo: LicenseIncentiveInfo) => {
    //           this.licenseIncentiveInfo = licenseIncentiveInfo;
    //           this.employmentLicenseIncentiveID =
    //             licenseIncentiveInfo.employmentLicenseIncentiveID;

    //           // Set the dropdown values
    //           this.dmEmploymentIDValue = this.dmManagers.find(
    //             (mgr) => mgr.label === licenseIncentiveInfo.dmMgrName
    //           )?.value;
    //           this.ccdBMEmploymentIDValue = this.bmManagers.find(
    //             (mgr) => mgr.label === licenseIncentiveInfo.cCdBRMgrName
    //           )?.value;
    //           let dmSentByValue = this.licenseTeches.find(
    //             (tech) => tech.label === licenseIncentiveInfo.dmSentBy
    //           )?.value;
    //           let dM10DaySentByValue = this.licenseTeches.find(
    //             (tech) => tech.label === licenseIncentiveInfo.dM10DaySentBy
    //           )?.value;
    //           let dM20DaySentByValue = this.licenseTeches.find(
    //             (tech) => tech.label === licenseIncentiveInfo.dM20DaySentBy
    //           )?.value;
    //           let ccd2BmEmploymentIdValue = this.bmManagers.find(
    //             (mgr) => mgr.label === licenseIncentiveInfo.cCd2BRMgrName
    //           )?.value;
    //           let tmSentByValue = this.licenseTeches.find(
    //             (tech) => tech.label === licenseIncentiveInfo.tmSentBy
    //           )?.value;
    //           let tmOkToSellSentByValue = this.licenseTeches.find(
    //             (tech) => tech.label === licenseIncentiveInfo.tmOkToSellSentBy
    //           )?.value;
    //           let ccOkToSellBmEmploymentIdValue = this.bmManagers.find(
    //             (mgr) => mgr.label === licenseIncentiveInfo.cCdOkToSellBRMgrName
    //           )?.value;

    //           this.incentiveUpdateForm.patchValue({
    //             RollOutGroup: licenseIncentiveInfo.rollOutGroup,
    //             DMEmploymentID: this.dmEmploymentIDValue || 0,
    //             CCdBMEmploymentID: this.ccdBMEmploymentIDValue || 0,
    //             DMSentDate: licenseIncentiveInfo.dmSentDate
    //               ? formatDate(
    //                   licenseIncentiveInfo.dmSentDate,
    //                   'yyyy-MM-dd',
    //                   'en-US'
    //                 )
    //               : null,
    //             DMApprovalDate: licenseIncentiveInfo.dmApprovalDate
    //               ? formatDate(
    //                   licenseIncentiveInfo.dmApprovalDate,
    //                   'yyyy-MM-dd',
    //                   'en-US'
    //                 )
    //               : null,
    //             DMDeclinedDate: licenseIncentiveInfo.dmDeclinedDate
    //               ? formatDate(
    //                   licenseIncentiveInfo.dmDeclinedDate,
    //                   'yyyy-MM-dd',
    //                   'en-US'
    //                 )
    //               : null,
    //             DM10DaySentDate: licenseIncentiveInfo.dM10DaySentDate
    //               ? formatDate(
    //                   licenseIncentiveInfo.dM10DaySentDate,
    //                   'yyyy-MM-dd',
    //                   'en-US'
    //                 )
    //               : null,
    //             DM20DaySentDate: licenseIncentiveInfo.dM20DaySentDate
    //               ? formatDate(
    //                   licenseIncentiveInfo.dM20DaySentDate,
    //                   'yyyy-MM-dd',
    //                   'en-US'
    //                 )
    //               : null,
    //             DMComment: licenseIncentiveInfo.dmComment,
    //             // DMSentBySOEID: dmSentByValue || null,
    //             DMSentBySOEID: licenseIncentiveInfo.dmSentBy || null,
    //             DM10DaySentBySOEID: licenseIncentiveInfo.dM10DaySentBy || null,
    //             DM20DaySentBySOEID: licenseIncentiveInfo.dM20DaySentBy || null,
    //             TMSentDate: licenseIncentiveInfo.tmSentDate
    //               ? formatDate(
    //                   licenseIncentiveInfo.tmSentDate,
    //                   'yyyy-MM-dd',
    //                   'en-US'
    //                 )
    //               : null,
    //             CCd2BMEmploymentID: ccd2BmEmploymentIdValue || 0,
    //             TMApprovalDate: licenseIncentiveInfo.tmApprovalDate
    //               ? formatDate(
    //                   licenseIncentiveInfo.tmApprovalDate,
    //                   'yyyy-MM-dd',
    //                   'en-US'
    //                 )
    //               : null,
    //             TMDeclinedDate: licenseIncentiveInfo.tmDeclinedDate
    //               ? formatDate(
    //                   licenseIncentiveInfo.tmDeclinedDate,
    //                   'yyyy-MM-dd',
    //                   'en-US'
    //                 )
    //               : null,
    //             TM10DaySentDate: licenseIncentiveInfo.tM10DaySentDate
    //               ? formatDate(
    //                   licenseIncentiveInfo.tM10DaySentDate,
    //                   'yyyy-MM-dd',
    //                   'en-US'
    //                 )
    //               : null,
    //             TM45DaySentDate: licenseIncentiveInfo.tM45DaySentDate
    //               ? formatDate(
    //                   licenseIncentiveInfo.tM45DaySentDate,
    //                   'yyyy-MM-dd',
    //                   'en-US'
    //                 )
    //               : null,
    //             TMExceptionDate: licenseIncentiveInfo.tmExceptionDate
    //               ? formatDate(
    //                   licenseIncentiveInfo.tmExceptionDate,
    //                   'yyyy-MM-dd',
    //                   'en-US'
    //                 )
    //               : null,
    //             TMSentBySOEID: licenseIncentiveInfo.tmSentBy || null,
    //             TMComment: licenseIncentiveInfo.tmComment,
    //             TMException: licenseIncentiveInfo.tmException,
    //             TMOkToSellSentDate: licenseIncentiveInfo.tmOkToSellSentDate
    //               ? formatDate(
    //                   licenseIncentiveInfo.tmOkToSellSentDate,
    //                   'yyyy-MM-dd',
    //                   'en-US'
    //                 )
    //               : null,
    //             CCOkToSellBMEmploymentID: ccOkToSellBmEmploymentIdValue || 0,
    //             TMOMSApprtoSendToHRDate:
    //               licenseIncentiveInfo.tmomsApprtoSendToHRDate
    //                 ? formatDate(
    //                     licenseIncentiveInfo.tmomsApprtoSendToHRDate,
    //                     'yyyy-MM-dd',
    //                     'en-US'
    //                   )
    //                 : null,
    //             IncetivePeriodDate: licenseIncentiveInfo.incetivePeriodDate
    //               ? formatDate(
    //                   licenseIncentiveInfo.incetivePeriodDate,
    //                   'yyyy-MM-dd',
    //                   'en-US'
    //                 )
    //               : null,
    //             IncentiveStatus: licenseIncentiveInfo.incentiveStatus,
    //             TMOkToSellSentBySOEID:
    //               licenseIncentiveInfo.tmOkToSellSentBy || null,
    //             Notes: licenseIncentiveInfo.notes,
    //           });
    //         })
    //     );
  }

  setDirty() {
    this.isPageDirty = true;
  }

  onEditToggle() {
    this.modeEdit = !this.modeEdit;
  }

  onSubmit() {
    this.isFormSubmitted = true;

    let incentiveUpdateItem: any = this.incentiveUpdateForm.value;
    incentiveUpdateItem.employeeLicenseID = this.employeeLicenseID;
    incentiveUpdateItem.EmploymentLicenseIncentiveID =
      this.employmentLicenseIncentiveID;
    incentiveUpdateItem.userSOEID =
      this.userAcctInfoDataService.userAcctInfo.soeid;

    // Ensure nullable string fields are set to null if empty
    const nullableFields = [
      'DMSentBySOEID',
      'DM10DaySentBySOEID',
      'DM20DaySentBySOEID',
      'TMSentBySOEID',
      'TMOkToSellSentBySOEID',
    ];

    nullableFields.forEach((field) => {
      if (!incentiveUpdateItem[field] || incentiveUpdateItem[field] === '0') {
        incentiveUpdateItem[field] = null;
      }
    });

    if (incentiveUpdateItem.DMSentDate == '') {
      incentiveUpdateItem.DMSentDate = null;
    }
    if (incentiveUpdateItem.DMApprovalDate == '') {
      incentiveUpdateItem.DMApprovalDate = null;
    }
    if (incentiveUpdateItem.DMDeclinedDate == '') {
      incentiveUpdateItem.DMDeclinedDate = null;
    }
    if (incentiveUpdateItem.DM10DaySentDate == '') {
      incentiveUpdateItem.DM10DaySentDate = null;
    }
    if (incentiveUpdateItem.DM20DaySentDate == '') {
      incentiveUpdateItem.DM20DaySentDate = null;
    }

    if (incentiveUpdateItem.TMSentDate == '') {
      incentiveUpdateItem.TMSentDate = null;
    }
    if (incentiveUpdateItem.TMApprovalDate == '') {
      incentiveUpdateItem.TMApprovalDate = null;
    }
    if (incentiveUpdateItem.TMDeclinedDate == '') {
      incentiveUpdateItem.TMDeclinedDate = null;
    }
    if (incentiveUpdateItem.TM10DaySentDate == '') {
      incentiveUpdateItem.TM10DaySentDate = null;
    }
    if (incentiveUpdateItem.TM45DaySentDate == '') {
      incentiveUpdateItem.TM45DaySentDate = null;
    }
    if (incentiveUpdateItem.TMExceptionDate == '') {
      incentiveUpdateItem.TMExceptionDate = null;
    }
    if (incentiveUpdateItem.TMOkToSellSentDate == '') {
      incentiveUpdateItem.TMOkToSellSentDate = null;
    }
    if (incentiveUpdateItem.TMOMSApprtoSendToHRDate == '') {
      incentiveUpdateItem.TMOMSApprtoSendToHRDate = null;
    }

    // Handle default date for IncetivePeriodDate
    if (!incentiveUpdateItem.IncetivePeriodDate) {
      incentiveUpdateItem.IncetivePeriodDate = '1900-01-01';
    }

    if (this.incentiveUpdateForm.invalid) {
      this.incentiveUpdateForm.setErrors({ invalid: true });
      return;
    }

    this.licIncentiveInfoDataService
      .updateLicenseIncentiveInfo(incentiveUpdateItem)
      .then((response) => {
        this.licIncentiveInfoDataService
          .fetchLicIncentiveInfo(this.employeeLicenseID)
          .subscribe((licenseIncentiveUpd: LicenseIncentiveInfo) => {
            this.licenseIncentiveInfo = licenseIncentiveUpd;
            this.employmentLicenseIncentiveID =
              licenseIncentiveUpd.employmentLicenseIncentiveID;
            // this.licIncentiveInfoDataService.licenseIncentiveInfoChanged.next(
            //   this.licenseIncentiveInfo
            // );
            // this.refreshData(licenseIncentiveUpd);
            this.isFormSubmitted = false;
            this.appComService.updateAppMessage(
              'Data submitted successfully.' // 'Data submitted successfully.'
            );
            this.onEditToggle();
          });
      })
      .catch((error) => {
        if (error.error && error.error.errMessage) {
          this.errorMessageService.setErrorMessage(error.error.errMessage);
        }
      });
  }

  openConfirmDialog(eventAction: string, msg: string, vType?: string): void {
      this.eventAction = eventAction;
      // this.vObject = vObject;
  
      const dialogRef = this.dialog.open(ConfirmDialogComponent, {
        width: '250px',
        data: {
          title: 'Confirm Action',
          message: 'You are about to DELETE \n' + msg + '\n\nAre you sure?',
        },
      });
  
      dialogRef.afterClosed().subscribe((result) => {
        if (result) {
          // this.subscriptions.add(
          //   this.adminDataService
          //     .deleteCompany({
          //       companyId: vObject.companyId,
          //       addressId: vObject.addressId,
          //       userSOEID: this.userAcctInfoDataService.userAcctInfo.soeid,
          //     })
          //     .subscribe({
          //       next: (response) => {
          //         // this.location.back();
          //         // this.fetchCompanyItems();
          //       },
          //       error: (error) => {
          //         if (error.error && error.error.errMessage) {
          //           this.errorMessageService.setErrorMessage(
          //             error.error.errMessage
          //           );
          //         }
          //       },
          //     })
          // );
        }
      });
    }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
