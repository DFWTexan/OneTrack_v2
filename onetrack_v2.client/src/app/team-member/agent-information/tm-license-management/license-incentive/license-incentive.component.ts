import { Component, OnInit, Injectable, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { Subscription, forkJoin } from 'rxjs';

import {
  // AgentLicApplicationInfo,
  AgentLicenseAppointments,
  LicenseIncentiveInfo,
} from '../../../../_Models';
import {
  AgentComService,
  AgentDataService,
  ConstantsDataService,
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
  dmEmploymentIDValue: any = 0;
  bmManagers: { value: number; label: string }[] = [];
  ccdBMEmploymentIDValue: any = 0;
  licenseTeches: { value: string; label: string }[] = [];
  incentiveStatuses: string[] = this.conService.getIncentiveStatuses();

  private subscriptions = new Subscription();

  constructor(
    public agentDataService: AgentDataService,
    private conService: ConstantsDataService,
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
    
    const fetchDropdownData$ =
      this.drpdwnDataService.fetchDropdownData('GetRollOutGroups');
    const fetchDMManagers$ = this.licIncentiveInfoDataService.fetchDMManagers();
    const fetchBMManagers$ = this.licIncentiveInfoDataService.fetchBMManagers();
    const fectchLicenseTeches$ = this.licIncentiveInfoDataService.fetchLicenseTeches();

    this.subscriptions.add(
      forkJoin([
        fetchDropdownData$,
        fetchDMManagers$,
        fetchBMManagers$,
        fectchLicenseTeches$,
      ]).subscribe(([rolloutGroups, dmManagers, bmManagers, licenseTeches]) => {
        this.rolloutGroups = [
          { value: 'OneTrak', label: 'OneTrak' },
          ...rolloutGroups,
        ];
        this.dmManagers = dmManagers;
        this.bmManagers = bmManagers;
        this.licenseTeches = licenseTeches;

        this.subscriptions.add(
          this.licIncentiveInfoDataService
            .fetchLicIncentiveInfo(
              this.licenseMgmtData[this.currentIndex].employeeLicenseId
            )
            .subscribe((licenseIncentiveInfo: LicenseIncentiveInfo) => {
              
              this.licenseIncentiveInfo = licenseIncentiveInfo;

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
                DMSentBySOEID: dmSentByValue || 0,
                DM10DaySentBySOEID: dM10DaySentByValue || 0,
                DM20DaySentBySOEID: dM20DaySentByValue || 0,
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
                TMSentBySOEID: tmSentByValue || 0,
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
                TMOkToSellSentBySOEID: tmOkToSellSentByValue || 0,
                Notes: licenseIncentiveInfo.notes,
              });
            })
        );
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
