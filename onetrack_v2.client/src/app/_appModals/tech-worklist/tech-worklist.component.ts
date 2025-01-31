import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { TechWorklistData } from '../../_Models';
import {
  AgentComService,
  AppComService,
  DashboardDataService,
  EmployeeDataService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../_services';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent, InfoDialogComponent } from '../../_components';

@Component({
  selector: 'app-tech-worklist',
  templateUrl: './tech-worklist.component.html',
  styleUrl: './tech-worklist.component.css',
})
export class TechWorklistComponent implements OnInit, OnDestroy {
  techWorklistItems: TechWorklistData[] = [];
  selectedRowIndex: number | null = null;
  selectedElement: string | null = null;

  subscriptions: Subscription = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    private router: Router,
    public dialog: MatDialog,
    public appComService: AppComService,
    public agentComService: AgentComService,
    public dashboardDataService: DashboardDataService,
    public employeeDataService: EmployeeDataService,
    public userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.subscriptions.add(
      this.appComService.techWorklistItemsChanged.subscribe(
        (techWorklistItems: TechWorklistData[]) => {
          this.techWorklistItems = techWorklistItems;
        }
      )
    );
  }

  selectRow(
    event: MouseEvent,
    worklistName: string,
    rowDataEmployeeID: string,
    rowDataTmNumberID: string
  ) {
    event.preventDefault();

    if (worklistName === 'In Process Status') {
      rowDataTmNumberID = rowDataTmNumberID.replace(/^T/, '');

      this.employeeDataService
        .fetchEmployeeByTmNumber(rowDataTmNumberID.trim())
        .subscribe((response) => {
          this.agentComService.updateShowLicenseMgmt(false);
          if (response === null) {
            alert('Employee not found');
          } else {
            this.router.navigate([
              '../../team/agent-info',
              response.employeeId,
              'tm-info-mgmt',
            ]);
          }
        });
    } else {
      const dialogRef = this.dialog.open(InfoDialogComponent, {
        data: { message: 'Loading Agent Info..' },
      });

      // Delay the execution of the blocking operation
      setTimeout(() => {
        this.router
          .navigateByUrl('/', { skipLocationChange: true })
          .then(() => {
            this.router.navigate([
              '../../team/agent-info',
              rowDataEmployeeID,
              'tm-info-mgmt',
            ]);
          });
        dialogRef.close();
      }, 100);
    }
    this.forceCloseModal();
  }

  getWorkListDataField(workListData: string, index: number): string {
    const fields = workListData.split('|');
    return fields[index] || '';
  }

  onCloseWorklistItem(worklistDataID: TechWorklistData): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: 'Confirm Action',
        message:
          'You are about to CLOSE Worklist Item (' +
          worklistDataID +
          '). Do you want to proceed?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.dashboardDataService
          .closeWorklistItem({
            WorkListDataID: worklistDataID,
            UserSOEID: this.userAcctInfoDataService.userAcctInfo.soeid,
          })
          .subscribe({
            next: (response) => {
              // this.fetchWorkListData();
              this.fetchTechWorkListData();
            },
            error: (error) => {
              if (error.error && error.error.errMessage) {
                this.errorMessageService.setErrorMessage(
                  error.error.errMessage
                );
              }
            },
          });
      }
    });
  }

  fetchTechWorkListData(): void {
    this.subscriptions.add(
      this.dashboardDataService
        .fetchTechWorklistData(this.userAcctInfoDataService.userAcctInfo.soeid)
        .subscribe((techWrklistData) => {
          this.techWorklistItems = techWrklistData;
          this.appComService.updateTechWorklistItems(techWrklistData);
          this.appComService.updateOpenTechWorklistCount(techWrklistData.length);
          // this.cdr.detectChanges();
        })
    );
  }

  forceCloseModal() {
    // this.isFormSubmitted = false;
    // if (this.modeCloseModal === 'EDIT') {
    //   const modalDiv = document.getElementById('modal-edit-license-info');
    //   if (modalDiv != null) {
    //     modalDiv.style.display = 'none';
    //   }
    // } else {
    const modalDiv = document.getElementById('modal-tech-work-list');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
    // }
  }

  // onCloseModal() {
  //   if (this.licenseForm.dirty && !this.isFormSubmitted) {
  //     if (
  //       confirm('You have unsaved changes. Are you sure you want to close?')
  //     ) {
  //       // const modalDiv = document.getElementById('modal-edit-license-info');
  //       // if (modalDiv != null) {
  //       //   modalDiv.style.display = 'none';
  //       // }
  //       this.forceCloseModal();
  //       this.licenseForm.reset();
  //       // this.licenseForm.patchValue({
  //       //   jobTitleID: 0,
  //       //   isCurrent: false,
  //       // });
  //     }
  //   } else {
  //     this.isFormSubmitted = false;
  //     // const modalDiv = document.getElementById('modal-edit-license-info');
  //     // if (modalDiv != null) {
  //     //   modalDiv.style.display = 'none';
  //     // }
  //     this.forceCloseModal();
  //   }
  // }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
