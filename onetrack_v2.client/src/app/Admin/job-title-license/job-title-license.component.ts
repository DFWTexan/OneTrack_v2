import { Component, Injectable, OnInit } from '@angular/core';

import {
  AdminComService,
  AdminDataService,
  ModalService,
  PaginationComService,
} from '../../_services';
import { JobTitle } from '../../_Models';

@Component({
  selector: 'app-job-title-license',
  templateUrl: './job-title-license.component.html',
  styleUrl: './job-title-license.component.css',
})
@Injectable()
export class JobTitleLicenseComponent implements OnInit {
  isLoading: boolean = false;
  licenseLevels: any[] = [];
  licenseIncentives: any[] = [];
  jobTitles: any[] = [];
  selectChanged = false;

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public modalService: ModalService,
    public paginationComService: PaginationComService
  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.adminDataService.fetchLicenseLevels().subscribe((response) => {
      this.licenseLevels = response;
    });
    this.adminDataService.fetchLicenseIncentives().subscribe((response) => {
      this.licenseIncentives = response;
    });
    this.adminDataService.fetchJobTitles().subscribe((response) => {
      this.jobTitles = response;
      this.paginationComService.updateDisplayItems(this.jobTitles);
      this.paginationComService.updatePaginatedResults();
      this.isLoading = false;
    });
  }

  setDirty(job: JobTitle) {
    job.isDirty = true;
  }
}
