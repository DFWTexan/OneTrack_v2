import { Component, OnInit, Injectable, OnDestroy } from '@angular/core';

import { AgentLicenseAppointments } from '../../../../_Models';
import {
  AgentComService,
  AgentDataService,
  AppComService,
  LicIncentiveInfoDataService,
  ModalService,
  UserAcctInfoDataService,
} from '../../../../_services';
import { ConfirmDialogComponent } from '../../../../_components';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-license-info',
  templateUrl: './license-info.component.html',
  styleUrl: './license-info.component.css',
})
@Injectable()
export class LicenseInfoComponent implements OnInit, OnDestroy {
  licenseMgmtData: AgentLicenseAppointments[] =
    [] as AgentLicenseAppointments[];
  subscriptionAgentInfo: Subscription = new Subscription();
  currentIndex: number = 0;
  eventAction: string = '';
  vObject: any = {};

  private subscriptions = new Subscription();

  constructor(
    protected agentDataService: AgentDataService,
    public agentComService: AgentComService,
    protected modalService: ModalService,
    public appComService: AppComService,
    private userInfoDataService: UserAcctInfoDataService,
    private licenseIncentiveInfoDataService: LicIncentiveInfoDataService,
    public dialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit() {
    this.currentIndex = this.agentDataService.licenseMgmtDataIndex;
    
    this.licenseMgmtData =
      this.agentDataService.agentInformation.agentLicenseAppointments;

    this.subscriptions.add(
      (this.subscriptionAgentInfo =
        this.agentDataService.agentLicenseAppointmentsChanged.subscribe(
          (agentLicenseAppointments) => {
            this.licenseMgmtData = agentLicenseAppointments;
          }
        ))
    );
  }

  // Pagination
  nextPage() {
    if (this.currentIndex < this.licenseMgmtData.length - 1) {
      this.currentIndex++;
      this.agentDataService.licenseMgmtDataIndexChanged.next(this.currentIndex);
    }
  }

  previousPage() {
    if (this.currentIndex > 0) {
      this.currentIndex--;
      this.agentDataService.licenseMgmtDataIndexChanged.next(this.currentIndex);
    }
  }

  getPageInfo(): string {
    return `${this.currentIndex + 1} of ${this.licenseMgmtData.length}`;
  }

  isDisplayPrevious(): boolean {
    return this.currentIndex > 0;
  }

  isDisplayNext(): boolean {
    return this.currentIndex < this.licenseMgmtData.length - 1;
  }

  setModeLicenseMgmt(mode: string) {
    this.agentComService.modeLicenseMgmtModal(mode);
  }

  openConfirmDialog(eventAction: string, msg: string, vObject: any): void {
    this.eventAction = eventAction;
    this.vObject = vObject;

    switch (eventAction) {
      case 'deleteLicense':
        const dialogRef = this.dialog.open(ConfirmDialogComponent, {
          width: '250px',
          data: {
            title: 'Confirm Action',
            message:
              'You are about to DELETE agent license ' +
              this.vObject.licenseName +
              ' - (' +
              this.vObject.employeeLicenseID +
              ')',
          },
        });
    
        dialogRef.afterClosed().subscribe((result) => {
          if (result) {
            this.agentDataService
              .deleteAgentLicense({
                employmentID: this.vObject.employmentID,
                employeeLicenseID: this.vObject.employeeLicenseID,
                userSOEID: this.userInfoDataService.userAcctInfo.soeid,
              })
              .subscribe({
                next: (response) => {
                  this.router
                    .navigateByUrl('/', { skipLocationChange: true })
                    .then(() => {
                      this.router.navigate([
                        'team/agent-info',
                        this.agentDataService.agentInformation.employeeID,
                        'tm-license-mgmt',
                      ]);
                    });
                },
                error: (error) => {
                  console.error(error);
                  // handle the error here
                },
              });
          }
        });
        break;
        case 'deleteLicAppt':
          const dialogRef_LicAppt = this.dialog.open(ConfirmDialogComponent, {
            width: '250px',
            data: {
              title: 'Confirm Action',
              message:
                'You are about to DELETE license appointment ' +
                vObject.employeeAppointmentID +
                '. Do you want to proceed?'
            },
          });
      
          dialogRef_LicAppt.afterClosed().subscribe((result) => {
            if (result) {
              this.licenseIncentiveInfoDataService
                .deleteLicenseAppointment({
                  employmentID: this.vObject.employmentID,
                  employeeLicenseID: this.vObject.employeeLicenseID,
                  userSOEID: this.userInfoDataService.userAcctInfo.soeid,
                })
                .subscribe({
                  next: (response) => {
                    // this.router
                    //   .navigateByUrl('/', { skipLocationChange: true })
                    //   .then(() => {
                    //     this.router.navigate([
                    //       'team/agent-info',
                    //       this.agentDataService.agentInformation.employeeID,
                    //       'tm-license-mgmt',
                    //     ]);
                    //   });
                  },
                  error: (error) => {
                    console.error(error);
                    // handle the error here
                  },
                });
            }
          });
          break;
      default:
        break;
    }
    
  }

  storeLicApptLicenseID(licenseID: number) {
    this.agentDataService.storeLicApptLicenseID(licenseID);
  }

  ngOnDestroy() {
    this.subscriptionAgentInfo.unsubscribe();
  }
}
