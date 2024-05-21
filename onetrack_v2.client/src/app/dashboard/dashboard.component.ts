import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatExpansionModule } from '@angular/material/expansion';

import {
  AppComService,
  DashboardDataService,
  MiscDataService,
  UserAcctInfoDataService,
  WorkListDataService,
} from '../_services';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent implements OnInit, OnDestroy {
  loading: boolean = false;
  panelOpenState = false;
  subscriptionAdBankerImportInfo: Subscription = new Subscription();
  subscriptionWorklist: Subscription = new Subscription();
  subscriptionAuditLog: Subscription = new Subscription();

  adBankerImportInfo: any = {};
  worklistData: any[] = [];
  worklistNames: string[] = ['Loading...'];
  defaultWorkListName: string = 'Loading...';

  workListName: string = 'Agent Address Change';
  licenseTech: string = 'T9999999';
  worklistdate: string = new Date().toISOString().split('T')[0];

  selectedWorkListName = 'Loading...';
  selectedLicenseTech = 'T9999999';
  selectedDate: string | null;
  isTechActive: boolean = true;

  adBankerData: any[] = [];
  adBankerStartDate: string = new Date(
    new Date().setDate(new Date().getDate() - 1)
  )
    .toISOString()
    .split('T')[0];
  adBankerEndDate: string = new Date(
    new Date().setDate(new Date().getDate() + 1)
  )
    .toISOString()
    .split('T')[0];
  adBankerImportStatus: string = 'All';
  adBankerImportStatuses: string[] = ['All', 'Success', 'Failed'];

  modifiedBy: string[] = ['Loading...'];
  modifiedBySelected: string = 'Select Tech';
  startDate: string = new Date(new Date().setDate(new Date().getDate()))
    .toISOString()
    .split('T')[0];
  endDate: string = new Date(new Date().setDate(new Date().getDate() + 1))
    .toISOString()
    .split('T')[0];
  auditLogData: any[] = [];

  constructor(
    public appComService: AppComService,
    public workListDataService: WorkListDataService,
    public miscDataService: MiscDataService,
    public dashboardDataService: DashboardDataService,
    public userAcctInfoDataService: UserAcctInfoDataService
  ) {
    this.selectedDate = null;
  }

  ngOnInit(): void {
    this.subscriptionWorklist = this.miscDataService
      .fetchWorkListNames()
      .subscribe((worklistNames) => {
        this.worklistNames = worklistNames;
        this.selectedWorkListName = worklistNames[0];
      });
    this.subscriptionAdBankerImportInfo = this.dashboardDataService
      .fetchAdBankerImportInfo()
      .subscribe((adBankerImportInfo) => {
        this.adBankerImportInfo = adBankerImportInfo;
      });
    this.dashboardDataService
      .fetchAuditModifiedBy(this.isTechActive)
      .subscribe((modifiedBy) => {
        this.modifiedBy = ['Select Tech', ...modifiedBy];
      });
    this.subscriptionAuditLog = this.dashboardDataService
      .fetchAuditLog(this.startDate, this.endDate, null)
      .subscribe((auditLogData) => {
        this.auditLogData = auditLogData;
      });
      this.getAdBankerData();
  }

  // WORKLIST
  fetchWorkListData(): void {
    this.workListDataService
      .fetchWorkListData(
        this.selectedWorkListName,
        this.selectedDate,
        this.userAcctInfoDataService.userAcctInfo.soeid
      )
      .subscribe((worklistData) => {
        this.worklistData = worklistData;
      });
  }

  changeWorkListName(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedWorkListName = value;
    this.fetchWorkListData();
  }

  changeImportDate(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedDate = value;
    this.fetchWorkListData();
  }

  // ADBANKER
  getAdBankerData(): void {
    this.dashboardDataService
      .fetchADBankerData(
        this.adBankerStartDate,
        this.adBankerEndDate,
        this.adBankerImportStatus == 'All'
          ? null
          : this.adBankerImportStatus == 'Success'
          ? true
          : false
      )
      .subscribe((data) => {
        this.adBankerData = data;
      });
  }

  changeADBankerStartDate(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.startDate = value;

    // this.dashboardDataService
    //   .fetchADBankerData(
    //     this.startDate,
    //     this.endDate,
    //     this.adBankerImportStatus == 'All' ? null : this.adBankerImportStatus == 'Success' ? true : false
    //   )
    //   .subscribe((data) => {
    //     this.adBankerData = data;
    //   });
    this.getAdBankerData();
  }
  changeADBankerEndDate(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.endDate = value;
    // this.dashboardDataService
    //   .fetchADBankerData(
    //     this.startDate,
    //     this.endDate,
    //     this.adBankerImportStatus == 'All' ? null : this.adBankerImportStatus == 'Success' ? true : false
    //   )
    //   .subscribe((data) => {
    //     this.adBankerData = data;
    //   });
    this.getAdBankerData();
  }
  changeImportStaus(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.adBankerImportStatus = value;
    // this.dashboardDataService
    //   .fetchADBankerData(
    //     this.startDate,
    //     this.endDate,
    //     value == 'All' ? null : value == 'Success' ? true : false
    //   )
    //   .subscribe((data) => {
    //     this.adBankerData = data;
    //   });
    this.getAdBankerData();
  }

  // AUDIT LOG
  changesStartDate(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.startDate = value;

    this.dashboardDataService
      .fetchAuditLog(
        this.startDate,
        this.endDate,
        this.modifiedBySelected == 'Select Tech'
          ? null
          : this.modifiedBySelected
      )
      .subscribe((auditLogData) => {
        this.auditLogData = auditLogData;
      });
  }
  changesEndDate(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.endDate = value;
    this.dashboardDataService
      .fetchAuditLog(
        this.startDate,
        this.endDate,
        this.modifiedBySelected == 'Select Tech'
          ? null
          : this.modifiedBySelected
      )
      .subscribe((auditLogData) => {
        this.auditLogData = auditLogData;
      });
  }
  changeModifiedBy(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.modifiedBySelected = value;
    this.dashboardDataService
      .fetchAuditLog(
        this.startDate,
        this.endDate,
        value == 'Select Tech' ? null : value
      )
      .subscribe((auditLogData) => {
        this.auditLogData = auditLogData;
      });
  }
  ngOnDestroy(): void {
    this.subscriptionWorklist.unsubscribe();
    this.subscriptionAuditLog.unsubscribe();
  }
}
