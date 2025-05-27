import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  ErrorMessageService,
  ModalService,
  PaginationComService,
  UserAcctInfoDataService,
} from '../../_services';
import { JobTitle } from '../../_Models';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-job-title-license',
  templateUrl: './job-title-license.component.html',
  styleUrl: './job-title-license.component.css',
})
@Injectable()
export class JobTitleLicenseComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  filterJobTitle: string | null = null;
  activeFilterVal = '0';
  selectedFilterIsActive: boolean | null = null;
  selectedFilterLicLevel: string | null = null;
  selectedFilterLicIncentive: string | null = null;
  licenseLevels: any[] = [];
  licenseIncentives: any[] = [];
  jobTitles: any[] = [];
  selectChanged = false;
  // subscribeDataJobTitles: Subscription = new Subscription();
  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public modalService: ModalService,
    public appComService: AppComService,
    public paginationComService: PaginationComService,
    public userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.subscriptionData.add(
      this.adminDataService.fetchLicenseLevels().subscribe((response) => {
        this.licenseLevels = response;
      })
    );

    this.subscriptionData.add(
      this.adminDataService.fetchLicenseIncentives().subscribe((response) => {
        this.licenseIncentives = response;
      })
    );

    this.initJobTitle();
  }

  private initJobTitle() {
    this.subscriptionData.add(
      this.adminDataService.fetchJobTitles().subscribe((response) => {
        this.jobTitles = response;
        this.paginationComService.updateDisplayItems(this.jobTitles);
        this.paginationComService.updatePaginatedResults();
        this.isLoading = false;
      })
    );
  }

  setDirty(job: JobTitle) {
    job.isDirty = true;
  }

  filterByJobTitles(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;

    this.filterJobTitle = value;
    this.adminDataService.filterJobTitleData(
      value === '' ? null : value,
      this.selectedFilterIsActive,
      this.selectedFilterLicLevel,
      this.selectedFilterLicIncentive
    );
    this.getFilterData();
  }

  filterByActive(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.activeFilterVal = value;
    switch (value) {
      case '1':
        this.selectedFilterIsActive = true;
        break;
      case '2':
        this.selectedFilterIsActive = false;
        break;
      default:
        this.selectedFilterIsActive = null;
        break;
    }

    this.adminDataService.filterJobTitleData(
      this.filterJobTitle === '' ? null : this.filterJobTitle,
      this.selectedFilterIsActive,
      this.selectedFilterLicLevel,
      this.selectedFilterLicIncentive
    );
    this.getFilterData();
  }

  filterByLicenseLevel(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;

    this.selectedFilterLicLevel = value;
    this.adminDataService.filterJobTitleData(
      this.filterJobTitle === '' ? null : this.filterJobTitle,
      this.selectedFilterIsActive,
      value,
      this.selectedFilterLicIncentive
    );
    this.getFilterData();
  }

  filterByLicenseIncentive(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;

    this.selectedFilterLicIncentive = value;
    this.adminDataService.filterJobTitleData(
      this.filterJobTitle === '' ? null : this.filterJobTitle,
      this.selectedFilterIsActive,
      this.selectedFilterLicLevel,
      value
    );
    this.getFilterData();
  }

  private getFilterData() {
    this.jobTitles = this.adminDataService.jobTitlesFilter;
    this.subscriptionData.add(
      this.adminDataService.jobTitlesFilterChanged.subscribe((response) => {
        this.jobTitles = response;
      })
    );
    this.paginationComService.changeCurrentPage(1);
    this.paginationComService.updateDisplayItems(this.jobTitles);
    this.paginationComService.updatePaginatedResults();
  }

  onScrollTop() {
    const yourDiv = document.getElementById('job-title-table');
    if (yourDiv !== null) {
      yourDiv.scrollTop = 0;
    }
  }

  clearfilter() {
    this.onScrollTop();
    this.filterJobTitle = null;
    this.activeFilterVal = '0';
    this.selectedFilterIsActive = null;
    this.selectedFilterLicLevel = null;
    this.selectedFilterLicIncentive = null;
    this.adminDataService.filterJobTitleData(null, null, null, null);
    this.initJobTitle();
  }

  onChildCallRefreshData() {
    this.initJobTitle();
  }

  onUpdate(job: JobTitle) {
    job.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;
    this.subscriptionData.add(
      this.adminDataService.upsertJobTitle(job).subscribe({
        next: (response) => {
          // this.callParentRefreshData.emit();
          alert('Job Title Updated Successfully');
          this.forceCloseModal();
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
            this.forceCloseModal();
          }
        },
      })
    );
  }

  forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-job-title');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  closeModal() {
    this.forceCloseModal();
    // if (this.employmentHistoryForm.dirty && !this.isFormSubmitted) {
    //   if (
    //     confirm('You have unsaved changes. Are you sure you want to close?')
    //   ) {
    //     const modalDiv = document.getElementById('modal-edit-emp-history');
    //     if (modalDiv != null) {
    //       modalDiv.style.display = 'none';
    //     }
    //     this.employmentHistoryForm.reset();
    //     this.employmentHistoryForm.patchValue({
    //       backgroundCheckStatus: 'Pending',
    //       isCurrent: true,
    //     });
    //   }
    // } else {
    //   this.isFormSubmitted = false;
    //   const modalDiv = document.getElementById('modal-edit-emp-history');
    //   if (modalDiv != null) {
    //     modalDiv.style.display = 'none';
    //   }
    // }
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
