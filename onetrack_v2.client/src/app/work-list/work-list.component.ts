import { Component, OnInit } from '@angular/core';
import { MiscDataService, UserAcctInfoDataService, WorkListDataService } from '../_services';
import { LicenseTech } from '../_Models';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { InfoDialogComponent } from '../_components';

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
  selectedRowIndex: number | null = null;
  selectedElement: string | null = null;

  workListName: string = 'Agent Address Change';
  licenseTech: string = 'T9999999';
  date: string | null = null;

  selectedWorkListName = 'Agent Address Change';
  selectedLicenseTech = 'T9999999';
  selectedDate: string | null;

  constructor(
    public workListDataService: WorkListDataService,
    public miscDataService: MiscDataService,
    public dialog: MatDialog,
    private router: Router,
    public userAcctInfoDataService: UserAcctInfoDataService
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

  selectRow(event: MouseEvent, rowDataEmployeeID: string) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading Agent Info..' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate([
          '../../team/agent-info',
          rowDataEmployeeID,
          'tm-info-mgmt',
        ]);
      });
      dialogRef.close();
    }, 100);
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
