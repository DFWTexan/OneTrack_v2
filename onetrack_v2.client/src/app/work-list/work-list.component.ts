import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  AgentComService,
  AgentDataService,
  EmployeeDataService,
  ErrorMessageService,
  MiscDataService,
  UserAcctInfoDataService,
  WorkListDataService,
} from '../_services';
import { LicenseTech } from '../_Models';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';

import { ConfirmDialogComponent, InfoDialogComponent } from '../_components';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-work-list',
  templateUrl: './work-list.component.html',
  styleUrl: './work-list.component.css',
})
export class WorkListComponent implements OnInit, OnDestroy {
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
  eventAction: string = '';
  vObject: any = {};

  private subscriptions = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    public workListDataService: WorkListDataService,
    private agentDataService: AgentDataService,
    public miscDataService: MiscDataService,
    public dialog: MatDialog,
    private router: Router,
    public agentComService: AgentComService,
    public employeeDataService: EmployeeDataService,
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

  selectRow(
    event: MouseEvent,
    rowDataEmployeeID: string,
    rowDataTmNumberID: string
  ) {
    event.preventDefault();

    if (this.selectedWorkListName === 'In Process Status') {
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
  }

  onSubmit(): void {
    const formData = {
      workListName: this.selectedWorkListName,
      licenseTech: this.selectedLicenseTech,
      date: this.selectedDate,
    };
    console.log(formData);
  }

  openConfirmDialog(
    eventAction: string,
    msg: string,
    vObject: any,
    checkbox: HTMLInputElement
  ): void {
    this.eventAction = eventAction;
    this.vObject = vObject;
    this.vObject.UserSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;
    switch (eventAction) {
      case 'closeWorklistItem':
        const dialogRef = this.dialog.open(ConfirmDialogComponent, {
          width: '250px',
          data: {
            title: 'Confirm Action',
            message:
              'You want to close Worklist Item (' +
              this.vObject.WorkListDataID +
              ')\n',
            // ' ' +
            // this.agentInfo.lastName,
          },
        });

        dialogRef.afterClosed().subscribe((result) => {
          if (result) {
            this.subscriptions.add(
              this.agentDataService
                .closeWorklistItem({
                  WorkListDataID: this.vObject.WorkListDataID,
                  UserSOEID: this.userAcctInfoDataService.userAcctInfo.soeid,
                })
                .subscribe({
                  next: (response) => {
                    // this.callParentRefreshData.emit();
                  },
                  error: (error) => {
                    if (error.error && error.error.errMessage) {
                      this.errorMessageService.setErrorMessage(
                        error.error.errMessage
                      );
                    }
                  },
                })
            );
          } else {
            // Uncheck the checkbox if the user selects "No"
            checkbox.checked = false;
          }
        });
        break;
      // case 'deleteLicAppt':
      //   const dialogRef_Appt = this.dialog.open(ConfirmDialogComponent, {
      //     width: '250px',
      //     data: {
      //       title: 'Confirm Action',
      //       message:
      //         'You are about to DELETE license appointment ' +
      //         vObject.state + '-' + vObject.status + '-' + vObject.loa + '-' + vObject.coAbv +
      //         '. Do you want to proceed?',
      //     },
      //   });

      //   dialogRef_Appt.afterClosed().subscribe((result) => {
      //     if (result) {
      //       this.subscriptions.add(
      //         this.licenseIncentiveInfoDataService
      //           .deleteLicenseAppointment({
      //             employeeAppointmentID: vObject.employeeAppointmentID,
      //             employeeLicenseID: vObject.employeeLicenseID,
      //             userSOEID: this.userInfoDataService.userAcctInfo.soeid,
      //           })
      //           .subscribe({
      //             next: (response) => {
      //               alert('License Appointment Deleted');
      //             },
      //             error: (error) => {
      //               console.error(error);
      //               // handle the error here
      //             },
      //           })
      //       );
      //     }
      //   });
      //   break;
      // case 'deleteLicense':
      //   const dialogRef_License = this.dialog.open(ConfirmDialogComponent, {
      //     width: '250px',
      //     data: {
      //       title: 'Confirm Action',
      //       message:
      //         'You are about to DELETE license (' +
      //         vObject.employeeLicenseID + ') ' +
      //         vObject.licenseName +
      //         '. Do you want to proceed?',
      //     },
      //   });
      //   dialogRef_License.afterClosed().subscribe((result) => {
      //     if (result) {
      //       this.subscriptions.add(
      //         this.agentDataService
      //         .deleteAgentLicense({
      //           employmentID: this.vObject.employmentID,
      //           employeeLicenseID: this.vObject.employeeLicenseID,
      //           userSOEID: this.userInfoDataService.userAcctInfo.soeid,
      //         })
      //         .subscribe({
      //           next: (response) => {
      //             this.router
      //               .navigateByUrl('/', { skipLocationChange: true })
      //               .then(() => {
      //                 this.router.navigate([
      //                   'team/agent-info',
      //                   this.agentDataService.agentInformation.employeeID,
      //                   'tm-license-mgmt',
      //                 ]);
      //               });
      //           },
      //           error: (error) => {
      //             console.error(error);
      //             // handle the error here
      //           },
      //         })
      //       );
      //     }
      //   });
      //   break;
      default:
        break;
    }
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
