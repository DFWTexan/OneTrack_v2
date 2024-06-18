import { Component, OnInit, Injectable, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { Location } from '@angular/common';
import { Router } from '@angular/router';

import {
  AgentComService,
  AgentDataService,
  AppComService,
  LicIncentiveInfoDataService,
  UserAcctInfoDataService,
} from '../../../../_services';
import { AgentInfo } from '../../../../_Models';
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
  eventAction: string = '';
  vObject: any = {};
  agentInfo: AgentInfo = {} as AgentInfo;

  private subscriptions = new Subscription();

  constructor(
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    protected modalService: ModalService,
    public appComService: AppComService,
    private licenseIncentiveInfoDataService: LicIncentiveInfoDataService,
    private userInfoDataService: UserAcctInfoDataService,
    public dialog: MatDialog,
    private router: Router,
    private location: Location
  ) {}

  ngOnInit() {
    this.isLoading = true;

    this.agentInfo = this.agentDataService.agentInformation;
    this.subscriptions.add(
      this.agentDataService.agentInfoChanged.subscribe((agentInfo: any) => {
        this.isLoading = false;
        this.agentInfo = agentInfo;
      })
    );
  }

  onChildCallRefreshData(data: any) {
    // this.agentInfo = data;
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
                    console.error(error);
                    // handle the error here
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
              vObject.employeeAppointmentID +
              '. Do you want to proceed?'
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
                    alert('License Appointment Deleted');
                  },
                  error: (error) => {
                    console.error(error);
                    // handle the error here
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
    this.agentDataService.storeLicenseAppointment(appointment);
  }

  storeLicApptLicenseID(licenseID: number) {
    this.agentDataService.storeLicApptLicenseID(licenseID);
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

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
