import { Component, OnInit, Injectable, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { Location } from '@angular/common';
import { Router } from '@angular/router';

import {
  AgentComService,
  AgentDataService,
  AppComService,
  ErrorMessageService,
  LicIncentiveInfoDataService,
  UserAcctInfoDataService,
} from '../../../../_services';
import { AgentInfo, LicenseItem } from '../../../../_Models';
import { ModalService } from '../../../../_services';
import {
  ConfirmDialogComponent,
  InfoDialogComponent,
} from '../../../../_components';

@Component({
  selector: 'app-tm-information',
  templateUrl: './tm-information.component.html',
  styleUrl: './tm-information.component.css',
})
@Injectable()
export class TmInformationComponent implements OnInit, OnDestroy {
  isLoading = false;
  isIndex = false;
  eventAction: string = '';
  vObject: any = {};
  agentInfo: AgentInfo = {} as AgentInfo;
  typeTickler: any = {};
  workState: string | null = null;
  displayOpenLicenseID: number = 0;
  isLegacyView = false;
  activeTab: string = 'license';

  sortedLicenseItems: any[] = [];
  sortedAppointmentItems: any[] = [];
  sortColumn: string = '';
  sortDirection: 'asc' | 'desc' = 'asc';

  private subscriptions = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    protected modalService: ModalService,
    public appComService: AppComService,
    private licenseIncentiveInfoDataService: LicIncentiveInfoDataService,
    public userInfoDataService: UserAcctInfoDataService,
    public dialog: MatDialog,
    private router: Router,
    private location: Location
  ) {}

  ngOnInit() {
    this.isLoading = true;

    this.isLegacyView = this.appComService.isLegacyView;
    this.subscriptions.add(
      this.appComService.isLegacyViewChanged.subscribe((isLegacyView) => {
        this.isLegacyView = isLegacyView;
      })
    );

    this.agentInfo = this.agentDataService.agentInformation;
    this.subscriptions.add(
      this.agentDataService.agentInfoChanged
        // .subscribe(
        //   (agentInfo: AgentInfo) => {
        //     this.isLoading = false;
        //     this.agentInfo = agentInfo;
        //   }
        // )
        .subscribe({
          next: (agentInfo: AgentInfo) => {
            this.isLoading = false;
            this.agentInfo = agentInfo;
            this.sortedLicenseItems = [...agentInfo.licenseItems];
            this.sortedAppointmentItems = [...agentInfo.appointmentItems];
          },
          error: (error) => {
            if (error.error && error.error.errMessage) {
              this.errorMessageService.setErrorMessage(error.error.errMessage);
            }
          },
        })
    );
  }

  onToggleView() {
    this.appComService.updateIsLegacyView(!this.isLegacyView);
  }

  onChildCallRefreshData(data: any) {
    this.agentInfo = data;
  }

  openConfirmDialog(eventAction: string, msg: string, vObject: any): void {
    this.eventAction = eventAction;
    this.vObject = vObject;
    switch (eventAction) {
      case 'deleteAgent':
        const dialogRef = this.dialog.open(ConfirmDialogComponent, {
          width: '250px',
          data: {
            title: 'Confirm Action',
            message:
              'You are about to DELETE (' +
              this.agentInfo.geid +
              ')\n' +
              this.agentInfo.firstName +
              ' ' +
              this.agentInfo.lastName,
          },
        });

        dialogRef.afterClosed().subscribe((result) => {
          if (result) {
            this.subscriptions.add(
              this.agentDataService
                .deleteAgent({
                  employeeId: this.agentInfo.employeeID,
                  userSOEID: this.userInfoDataService.userAcctInfo.soeid,
                })
                .subscribe({
                  next: (response) => {
                    this.location.back();
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
          }
        });
        break;
      case 'deleteLicAppt':
        const dialogRef_Appt = this.dialog.open(ConfirmDialogComponent, {
          width: '250px',
          data: {
            title: 'Confirm Action',
            message:
              'You are about to DELETE license appointment ' +
              vObject.state +
              '-' +
              vObject.status +
              '-' +
              vObject.loa +
              '-' +
              vObject.coAbv +
              '. Do you want to proceed?',
          },
        });

        dialogRef_Appt.afterClosed().subscribe((result) => {
          if (result) {
            this.subscriptions.add(
              this.licenseIncentiveInfoDataService
                .deleteLicenseAppointment({
                  employeeAppointmentID: vObject.employeeAppointmentID,
                  employeeLicenseID: vObject.employeeLicenseID,
                  userSOEID: this.userInfoDataService.userAcctInfo.soeid,
                })
                .subscribe({
                  next: (response) => {
                    // alert('License Appointment Deleted');
                    this.appComService.updateAppMessage(
                      'License Appointment Deleted successfully.' // 'Data submitted successfully.'
                    );

                    this.agentDataService
                      .fetchAgentInformation(
                        this.agentDataService.agentInformation.employeeID
                      )
                      .subscribe((agentInfo: AgentInfo) => {
                        // this.isLoading = false;
                        // this.ticklerCount = agentInfo.ticklerItems.length;
                        // this.worklistCount = agentInfo.worklistItems.length;
                        this.agentInfo = agentInfo;
                        // this.callParentRefreshData.emit(agentInfo);
                      });
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
          }
        });
        break;
      case 'deleteLicense':
        const dialogRef_License = this.dialog.open(ConfirmDialogComponent, {
          width: '250px',
          data: {
            title: 'Confirm Action',
            message:
              'You are about to DELETE license (' +
              vObject.employeeLicenseID +
              ') ' +
              vObject.licenseName +
              '. Do you want to proceed?',
          },
        });

        dialogRef_License.afterClosed().subscribe((result) => {
          if (result) {
            this.subscriptions.add(
              this.agentDataService
                .deleteAgentLicense({
                  employmentID: this.vObject.employmentID,
                  employeeLicenseID: this.vObject.employeeLicenseID,
                  userSOEID: this.userInfoDataService.userAcctInfo.soeid,
                })
                .subscribe({
                  next: (response) => {
                    this.appComService.updateAppMessage(
                      'License Deleted successfully.' // 'Data submitted successfully.'
                    );
                    // this.router
                    //   .navigateByUrl('/', { skipLocationChange: true })
                    //   .then(() => {
                    //     this.router.navigate([
                    //       'team/agent-info',
                    //       this.agentDataService.agentInformation.employeeID,
                    //       'tm-license-info',
                    //     ]);
                    //   });
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
          }
        });
        break;
      default:
        break;
    }
  }

  setModeLicenseMgmt(mode: string) {
    this.agentComService.modeLicenseMgmtModal(mode);
  }

  storeLicAppointment(appointment: any) {
    this.agentComService.modeLicenseApptModal('EDIT');
    this.agentDataService.storeLicenseAppointment(appointment);
  }

  storeLicApptLicenseID(licenseID: number) {
    this.agentDataService.storeLicApptLicenseID(licenseID);
  }

  storeEmployeeLicenseID(EmployeeLicenseID: number) {
    this.agentDataService.storeEmployeeLicenseID(EmployeeLicenseID);
  }

  toggleLicenseMgmt(index: number) {
    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading Agent License Mgmt...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.agentDataService.storeLicenseMgmtDataIndex(index);
      this.agentComService.showLicenseMgmt();

      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate([
          'team/agent-info',
          this.agentDataService.agentInformation.employeeID,
          'tm-license-mgmt',
        ]);

        dialogRef.close(); // Close the dialog
      });
    }, 500); // Adjust the delay as needed
  }

  // onSetTypeTickler(typeTickler: any) {
  //   this.typeTickler = typeTickler;
  // }

  onSetWorkState(workState: string | null) {
    this.agentDataService.storeWorkState(workState);
  }

  onSetOpenLicenseID(displayOpenLicenseID: number) {
    this.displayOpenLicenseID = displayOpenLicenseID;
  }

  onSortData(column: string, type: string) {
    if (this.sortColumn === column) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortColumn = column;
      this.sortDirection = 'asc';
    }

    const compareFunction = (a: LicenseItem, b: LicenseItem) => {
      const valueA = a[column as keyof LicenseItem];
      const valueB = b[column as keyof LicenseItem];

      if (valueA === undefined && valueB === undefined) {
        return 0;
      } else if (valueA === undefined) {
        return this.sortDirection === 'asc' ? -1 : 1;
      } else if (valueB === undefined) {
        return this.sortDirection === 'asc' ? 1 : -1;
      } else if (valueA < valueB) {
        return this.sortDirection === 'asc' ? -1 : 1;
      } else if (valueA > valueB) {
        return this.sortDirection === 'asc' ? 1 : -1;
      } else {
        return 0;
      }
    };

    if (type === 'appointment') {
      this.sortedAppointmentItems.sort(compareFunction);
    } else {
      this.sortedLicenseItems.sort(compareFunction);
    }
  }

   openModal() {
    const modalDiv = document.getElementById('myModal');
    if (modalDiv != null) {
      modalDiv.style.display = 'block';
    }
  }
  closeModal() {
    const modalDiv = document.getElementById('myModal');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
