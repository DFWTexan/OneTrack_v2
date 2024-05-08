import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatExpansionModule } from '@angular/material/expansion';

import {
  AppComService,
  DashboardDataService,
  MiscDataService,
  WorkListDataService,
} from '../_services';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent implements OnInit, OnDestroy {
  loading: boolean = false;
  panelOpenState = false;

  worklistData: any[] = [];
  worklistNames: string[] = ['Loading...'];
  defaultWorkListName: string = 'Loading...';

  workListName: string = 'Agent Address Change';
  licenseTech: string = 'T9999999';
  date: string | null = null;

  selectedWorkListName = 'Loading...';
  selectedLicenseTech = 'T9999999';
  selectedDate: string | null;

  constructor(
    public appComService: AppComService,
    public workListDataService: WorkListDataService,
    public miscDataService: MiscDataService,
    public dashboardDataService: DashboardDataService
  ) {
    // this.selectedLicenseTech = null;
    this.selectedDate = null;
  }

  ngOnInit(): void {
    this.miscDataService.fetchWorkListNames().subscribe((worklistNames) => {
      this.worklistNames = worklistNames;
      this.selectedWorkListName = worklistNames[0];
    });
    // this.miscDataService.fetchWorkListNames().subscribe((worklistNames) => {
    //   this.worklistNames = worklistNames;
    //   this.selectedWorkListName = worklistNames[0];
    // });
    // this.miscDataService.fetchLicenseTechs().subscribe((licenseTechs) => {
    //   this.licenseTechs = licenseTechs;
    //   this.selectedLicenseTech = licenseTechs[0].soeid;
    // });
    // this.fetchWorkListData();
  }

  fetchWorkListData(): void {
    this.workListDataService
      .fetchWorkListData(
        this.selectedWorkListName,
        this.selectedDate,
        this.selectedLicenseTech
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

  ngOnDestroy(): void {}
}
