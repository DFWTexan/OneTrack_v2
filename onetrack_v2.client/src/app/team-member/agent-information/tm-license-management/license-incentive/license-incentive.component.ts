import { Component, OnInit, Injectable, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { Subscription, forkJoin, switchMap } from 'rxjs';

import {
  AgentInfo,
  // AgentLicApplicationInfo,
  AgentLicenseAppointments,
  LicenseIncentiveInfo,
} from '../../../../_Models';
import {
  AgentComService,
  AgentDataService,
  ConstantsDataService,
  DropdownDataService,
  ErrorMessageService,
  LicIncentiveInfoDataService,
  ModalService,
  UserAcctInfoDataService,
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
  isPageDirty: boolean = false;
  incentiveUpdateForm: FormGroup;
  employeeLicenseID: number = 0;
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

  private subscriptions = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public agentDataService: AgentDataService,
    private conService: ConstantsDataService,
    public agentComService: AgentComService,
    private drpdwnDataService: DropdownDataService,
    public licIncentiveInfoDataService: LicIncentiveInfoDataService,
    protected modalService: ModalService,
    private userAcctInfoDataService: UserAcctInfoDataService,
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

  ngOnInit(): void {
    this.currentIndex = this.agentDataService.licenseMgmtDataIndex;
    this.subscriptions.add(
      this.agentDataService.licenseMgmtDataIndexChanged.subscribe(
        (index) => (this.currentIndex = index)
      )
    );

    this.licenseMgmtData =
      this.agentDataService.agentInformation.agentLicenseAppointments;
    this.employeeLicenseID =
      this.licenseMgmtData[this.currentIndex].employeeLicenseId;
    // this.getData();
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

    // console.log('EMFTEST (LicenseIncentiveComponent: ngOnInit()) licenseMgmtData => \n', this.licenseMgmtData);

    this.subscriptions.add(
      this.licIncentiveInfoDataService
        .fetchLicIncentiveInfo(
          this.licenseMgmtData[this.currentIndex].employeeLicenseId
          // this.employeeLicenseID
        )
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
              ? formatDate(
                  licenseIncentiveInfo.tmSentDate,
                  'yyyy-MM-dd',
                  'en-US'
                )
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
            TMOMSApprtoSendToHRDate:
              licenseIncentiveInfo.tmomsApprtoSendToHRDate
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
            TMOkToSellSentBySOEID:
              licenseIncentiveInfo.tmOkToSellSentBy || null,
            Notes: licenseIncentiveInfo.notes,
          });
        })
    );
  }

  // getData() {
  //   const fetchDropdownData$ =
  //     this.drpdwnDataService.fetchDropdownData('GetRollOutGroups');
  //   const fetchDMManagers$ = this.licIncentiveInfoDataService.fetchDMManagers();
  //   const fetchBMManagers$ = this.licIncentiveInfoDataService.fetchBMManagers();
  //   const fectchLicenseTeches$ =
  //     this.licIncentiveInfoDataService.fetchLicenseTeches();

  //   this.subscriptions.add(
  //     forkJoin([
  //       fetchDropdownData$,
  //       fetchDMManagers$,
  //       fetchBMManagers$,
  //       fectchLicenseTeches$,
  //     ]).subscribe(([rolloutGroups, dmManagers, bmManagers, licenseTeches]) => {
  //       this.rolloutGroups = [
  //         { value: 'OneTrak', label: 'OneTrak' },
  //         ...rolloutGroups,
  //       ];
  //       this.dmManagers = dmManagers;
  //       this.bmManagers = bmManagers;
  //       this.licenseTeches = licenseTeches;

  //       this.subscriptions.add(
  //         this.licIncentiveInfoDataService
  //           .fetchLicIncentiveInfo(
  //             // this.licenseMgmtData[this.currentIndex].employeeLicenseId
  //             this.employeeLicenseID
  //           )
  //           .subscribe((licenseIncentiveInfo: LicenseIncentiveInfo) => {
  //             this.licenseIncentiveInfo = licenseIncentiveInfo;
  //             this.employmentLicenseIncentiveID =
  //               licenseIncentiveInfo.employmentLicenseIncentiveID;

  //             // Set the dropdown values
  //             this.dmEmploymentIDValue = this.dmManagers.find(
  //               (mgr) => mgr.label === licenseIncentiveInfo.dmMgrName
  //             )?.value;
  //             this.ccdBMEmploymentIDValue = this.bmManagers.find(
  //               (mgr) => mgr.label === licenseIncentiveInfo.cCdBRMgrName
  //             )?.value;
  //             let dmSentByValue = this.licenseTeches.find(
  //               (tech) => tech.label === licenseIncentiveInfo.dmSentBy
  //             )?.value;
  //             let dM10DaySentByValue = this.licenseTeches.find(
  //               (tech) => tech.label === licenseIncentiveInfo.dM10DaySentBy
  //             )?.value;
  //             let dM20DaySentByValue = this.licenseTeches.find(
  //               (tech) => tech.label === licenseIncentiveInfo.dM20DaySentBy
  //             )?.value;
  //             let ccd2BmEmploymentIdValue = this.bmManagers.find(
  //               (mgr) => mgr.label === licenseIncentiveInfo.cCd2BRMgrName
  //             )?.value;
  //             let tmSentByValue = this.licenseTeches.find(
  //               (tech) => tech.label === licenseIncentiveInfo.tmSentBy
  //             )?.value;
  //             let tmOkToSellSentByValue = this.licenseTeches.find(
  //               (tech) => tech.label === licenseIncentiveInfo.tmOkToSellSentBy
  //             )?.value;
  //             let ccOkToSellBmEmploymentIdValue = this.bmManagers.find(
  //               (mgr) => mgr.label === licenseIncentiveInfo.cCdOkToSellBRMgrName
  //             )?.value;

  //             this.incentiveUpdateForm.patchValue({
  //               RollOutGroup: licenseIncentiveInfo.rollOutGroup,
  //               DMEmploymentID: this.dmEmploymentIDValue || 0,
  //               CCdBMEmploymentID: this.ccdBMEmploymentIDValue || 0,
  //               DMSentDate: licenseIncentiveInfo.dmSentDate
  //                 ? formatDate(
  //                     licenseIncentiveInfo.dmSentDate,
  //                     'yyyy-MM-dd',
  //                     'en-US'
  //                   )
  //                 : null,
  //               DMApprovalDate: licenseIncentiveInfo.dmApprovalDate
  //                 ? formatDate(
  //                     licenseIncentiveInfo.dmApprovalDate,
  //                     'yyyy-MM-dd',
  //                     'en-US'
  //                   )
  //                 : null,
  //               DMDeclinedDate: licenseIncentiveInfo.dmDeclinedDate
  //                 ? formatDate(
  //                     licenseIncentiveInfo.dmDeclinedDate,
  //                     'yyyy-MM-dd',
  //                     'en-US'
  //                   )
  //                 : null,
  //               DM10DaySentDate: licenseIncentiveInfo.dM10DaySentDate
  //                 ? formatDate(
  //                     licenseIncentiveInfo.dM10DaySentDate,
  //                     'yyyy-MM-dd',
  //                     'en-US'
  //                   )
  //                 : null,
  //               DM20DaySentDate: licenseIncentiveInfo.dM20DaySentDate
  //                 ? formatDate(
  //                     licenseIncentiveInfo.dM20DaySentDate,
  //                     'yyyy-MM-dd',
  //                     'en-US'
  //                   )
  //                 : null,
  //               DMComment: licenseIncentiveInfo.dmComment,
  //               DMSentBySOEID: dmSentByValue || 0,
  //               DM10DaySentBySOEID: dM10DaySentByValue || 0,
  //               DM20DaySentBySOEID: dM20DaySentByValue || 0,
  //               TMSentDate: licenseIncentiveInfo.tmSentDate
  //                 ? formatDate(
  //                     licenseIncentiveInfo.tmSentDate,
  //                     'yyyy-MM-dd',
  //                     'en-US'
  //                   )
  //                 : null,
  //               CCd2BMEmploymentID: ccd2BmEmploymentIdValue || 0,
  //               TMApprovalDate: licenseIncentiveInfo.tmApprovalDate
  //                 ? formatDate(
  //                     licenseIncentiveInfo.tmApprovalDate,
  //                     'yyyy-MM-dd',
  //                     'en-US'
  //                   )
  //                 : null,
  //               TMDeclinedDate: licenseIncentiveInfo.tmDeclinedDate
  //                 ? formatDate(
  //                     licenseIncentiveInfo.tmDeclinedDate,
  //                     'yyyy-MM-dd',
  //                     'en-US'
  //                   )
  //                 : null,
  //               TM10DaySentDate: licenseIncentiveInfo.tM10DaySentDate
  //                 ? formatDate(
  //                     licenseIncentiveInfo.tM10DaySentDate,
  //                     'yyyy-MM-dd',
  //                     'en-US'
  //                   )
  //                 : null,
  //               TM45DaySentDate: licenseIncentiveInfo.tM45DaySentDate
  //                 ? formatDate(
  //                     licenseIncentiveInfo.tM45DaySentDate,
  //                     'yyyy-MM-dd',
  //                     'en-US'
  //                   )
  //                 : null,
  //               TMExceptionDate: licenseIncentiveInfo.tmExceptionDate
  //                 ? formatDate(
  //                     licenseIncentiveInfo.tmExceptionDate,
  //                     'yyyy-MM-dd',
  //                     'en-US'
  //                   )
  //                 : null,
  //               TMSentBySOEID: tmSentByValue || 0,
  //               TMComment: licenseIncentiveInfo.tmComment,
  //               TMException: licenseIncentiveInfo.tmException,
  //               TMOkToSellSentDate: licenseIncentiveInfo.tmOkToSellSentDate
  //                 ? formatDate(
  //                     licenseIncentiveInfo.tmOkToSellSentDate,
  //                     'yyyy-MM-dd',
  //                     'en-US'
  //                   )
  //                 : null,
  //               CCOkToSellBMEmploymentID: ccOkToSellBmEmploymentIdValue || 0,
  //               TMOMSApprtoSendToHRDate:
  //                 licenseIncentiveInfo.tmomsApprtoSendToHRDate
  //                   ? formatDate(
  //                       licenseIncentiveInfo.tmomsApprtoSendToHRDate,
  //                       'yyyy-MM-dd',
  //                       'en-US'
  //                     )
  //                   : null,
  //               IncetivePeriodDate: licenseIncentiveInfo.incetivePeriodDate
  //                 ? formatDate(
  //                     licenseIncentiveInfo.incetivePeriodDate,
  //                     'yyyy-MM-dd',
  //                     'en-US'
  //                   )
  //                 : null,
  //               IncentiveStatus: licenseIncentiveInfo.incentiveStatus,
  //               TMOkToSellSentBySOEID: tmOkToSellSentByValue || 0,
  //               Notes: licenseIncentiveInfo.notes,
  //             });
  //           })
  //       );
  //     })
  //   );
  // }

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
        // console.log('EMFTEST (LicenseIncentiveComponent: onSubmit()) Update successful => \n', response);

        // this.agentDataService
        //   .fetchAgentInformation(
        //     this.agentDataService.agentInformation.employeeID
        //   )
        //   .subscribe((agentInfo: AgentInfo) => {
        //     this.agentDataService.agentInfoChanged.next(agentInfo);
        //   });

        this.refreshData(incentiveUpdateItem);
      })
      .catch((error) => {
        if (error.error && error.error.errMessage) {
          this.errorMessageService.setErrorMessage(error.error.errMessage);
        }
      });
  }

  refreshData(licenseIncentiveUpd: any) {
    this.licenseIncentiveInfo.rollOutGroup = licenseIncentiveUpd.RollOutGroup;
    this.licenseIncentiveInfo.dmMgrName =
      this.dmManagers.find(
        (mgr) => mgr.value === licenseIncentiveUpd.DMEmploymentID
      )?.label || '';
    this.licenseIncentiveInfo.cCdBRMgrName =
      this.bmManagers.find(
        (mgr) => mgr.value === licenseIncentiveUpd.CCdBMEmploymentID
      )?.label || '';
    this.licenseIncentiveInfo.dmSentDate = licenseIncentiveUpd.DMSentDate;
    this.licenseIncentiveInfo.dmApprovalDate =
      licenseIncentiveUpd.DMApprovalDate;
    this.licenseIncentiveInfo.dmDeclinedDate =
      licenseIncentiveUpd.DMDeclinedDate;
    this.licenseIncentiveInfo.dM10DaySentDate =
      licenseIncentiveUpd.DM10DaySentDate;
    this.licenseIncentiveInfo.dM20DaySentDate =
      licenseIncentiveUpd.DM20DaySentDate;
    this.licenseIncentiveInfo.dmComment = licenseIncentiveUpd.DMComment;
    this.licenseIncentiveInfo.dmSentBy =
      this.licenseTeches.find(
        (tech) => tech.value === licenseIncentiveUpd.DMSentBySOEID
      )?.label || '';
    this.licenseIncentiveInfo.dM10DaySentBy =
      this.licenseTeches.find(
        (tech) => tech.value === licenseIncentiveUpd.DM10DaySentBySOEID
      )?.label || '';
    this.licenseIncentiveInfo.dM20DaySentBy =
      this.licenseTeches.find(
        (tech) => tech.value === licenseIncentiveUpd.DM20DaySentBySOEID
      )?.label || '';
    this.licenseIncentiveInfo.tmSentDate = licenseIncentiveUpd.TMSentDate;
    this.licenseIncentiveInfo.cCd2BRMgrName = this.bmManagers.find(
      (mgr) => mgr.value === licenseIncentiveUpd.CCd2BMEmploymentID
    )?.label;
    this.licenseIncentiveInfo.tmApprovalDate =
      licenseIncentiveUpd.TMApprovalDate;
    this.licenseIncentiveInfo.tmDeclinedDate =
      licenseIncentiveUpd.TMDeclinedDate;
    this.licenseIncentiveInfo.tM10DaySentDate =
      licenseIncentiveUpd.TM10DaySentDate;
    this.licenseIncentiveInfo.tM45DaySentDate =
      licenseIncentiveUpd.TM45DaySentDate;
    this.licenseIncentiveInfo.tmExceptionDate =
      licenseIncentiveUpd.TMExceptionDate;
    this.licenseIncentiveInfo.tmSentBy =
      this.licenseTeches.find(
        (tech) => tech.value === licenseIncentiveUpd.TMSentBySOEID
      )?.label || '';
    this.licenseIncentiveInfo.tmComment = licenseIncentiveUpd.TMComment;
    this.licenseIncentiveInfo.tmException = licenseIncentiveUpd.TMException;
    this.licenseIncentiveInfo.tmOkToSellSentDate =
      licenseIncentiveUpd.TMOkToSellSentDate;
    this.licenseIncentiveInfo.cCdOkToSellBRMgrName = this.bmManagers.find(
      (mgr) => mgr.value === licenseIncentiveUpd.CCOkToSellBMEmploymentID
    )?.label;
    this.licenseIncentiveInfo.tmomsApprtoSendToHRDate =
      licenseIncentiveUpd.TMOMSApprtoSendToHRDate;
    this.licenseIncentiveInfo.incetivePeriodDate =
      licenseIncentiveUpd.IncetivePeriodDate;
    this.licenseIncentiveInfo.incentiveStatus =
      licenseIncentiveUpd.IncentiveStatus;
    this.licenseIncentiveInfo.tmOkToSellSentBy =
      this.licenseTeches.find(
        (tech) => tech.value === licenseIncentiveUpd.TMOkToSellSentBySOEID
      )?.label || '';
    this.licenseIncentiveInfo.notes = licenseIncentiveUpd.Notes;

    this.modeEdit = false;
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
