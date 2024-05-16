import { Component, OnInit } from '@angular/core';
import { MiscDataService, WorkListDataService } from '../_services';
import { LicenseTech } from '../_Models';

@Component({
  selector: 'app-work-list',
  templateUrl: './work-list.component.html',
  styleUrl: './work-list.component.css',
})
export class WorkListComponent implements OnInit {
  loading: boolean = false;
  worklistData: any[] = [];
  worklistNames: string[] = ['Loading...'];
  licenseTechs: LicenseTech[] = [];

  workListName: string = 'Agent Address Change';
  licenseTech: string = 'T9999999';
  date: string | null = null;

  selectedWorkListName = 'Agent Address Change';
  selectedLicenseTech = 'T9999999';
  selectedDate: string | null;

  constructor(
    public workListDataService: WorkListDataService,
    public miscDataService: MiscDataService
  ) {
    // this.selectedLicenseTech = null;
    this.selectedDate = null;
  }

  ngOnInit(): void {
    this.miscDataService.fetchWorkListNames().subscribe((worklistNames) => {
      this.worklistNames = worklistNames;
      this.selectedWorkListName = worklistNames[0];
    });
    this.miscDataService.fetchLicenseTechs().subscribe((licenseTechs) => {
      this.licenseTechs = licenseTechs;
      this.selectedLicenseTech = licenseTechs[0].soeid;
    });
    this.fetchWorkListData();
  }

  fetchWorkListData(): void {

    console.log('EMFTEST (app-work-list) - selectedWorkListName: ', this.selectedWorkListName);
    console.log('EMFTEST (app-work-list) - selectedDate: ', this.selectedDate);
    console.log('EMFTEST (app-work-list) - selectedLicenseTech: ', this.selectedLicenseTech);

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

  changeDate(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedDate = value;
    this.fetchWorkListData();
  }

  changeLicenseTech(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedLicenseTech = value;
    this.fetchWorkListData();
  }

  onSubmit(): void {
    const formData = {
      workListName: this.selectedWorkListName,
      licenseTech: this.selectedLicenseTech,
      date: this.selectedDate,
    };
    console.log(formData);
  }
}
