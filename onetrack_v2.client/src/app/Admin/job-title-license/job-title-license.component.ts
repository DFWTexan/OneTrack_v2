import { Component, Injectable, OnInit } from '@angular/core';

import {
  AdminComService,
  AdminDataService,
  ModalService,
} from '../../_services';

@Component({
  selector: 'app-job-title-license',
  templateUrl: './job-title-license.component.html',
  styleUrl: './job-title-license.component.css',
})
@Injectable()
export class JobTitleLicenseComponent {
  loading: boolean = false;
  licenseLevels: any[] = [];
  licenseIncentives: any[] = [];
  jobTitles: any[] = [];

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.loading = true;
    this.adminDataService.fetchLicenseLevels().subscribe((response) => {
      this.licenseLevels = response;
    });
    this.adminDataService.fetchLicenseIncentives().subscribe((response) => {
      this.licenseIncentives = response;
    });
    this.adminDataService.fetchJobTitles().subscribe((response) => {
      this.jobTitles = response;
      this.loading = false;
    });
  }

  saveRow(row: any) {
    this.loading = true;
    // this.adminDataService.saveJobTitle(row).subscribe((response) => {
    //   this.loading = false;
    //   this.jobTitles = response;
    // });
  }
}
