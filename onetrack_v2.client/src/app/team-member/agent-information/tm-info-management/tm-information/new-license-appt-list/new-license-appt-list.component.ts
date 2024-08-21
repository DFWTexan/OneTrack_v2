import { Component, Injectable, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { AgentInfo } from '../../../../../_Models';
import {
  AgentComService,
  AgentDataService,
  ModalService,
  AppComService,
  LicIncentiveInfoDataService,
  UserAcctInfoDataService,
} from '../../../../../_services';
import { ConfirmDialogComponent, InfoDialogComponent } from '../../../../../_components';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-new-license-appt-list',
  templateUrl: './new-license-appt-list.component.html',
  styleUrl: './new-license-appt-list.component.css'
})
@Injectable()
export class NewLicenseApptListComponent implements OnInit, OnDestroy {
  isLoading = false;
  isIndex = false;
  eventAction: string = '';
  vObject: any = {};
  agentInfo: AgentInfo = {} as AgentInfo;
  typeTickler: any = {};
  workState: string | null = null;
  displayOpenLicenseID: number = 0;
  isLegacyView = false;

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
      this.agentDataService.agentInfoChanged.subscribe(
        (agentInfo: AgentInfo) => {
          this.isLoading = false;
          this.agentInfo = agentInfo;
        }
      )
    );
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
                    // this.location.back();
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

  storeLicApptLicenseID(licenseID: number) {
    this.agentDataService.storeLicApptLicenseID(licenseID);
  }

  storeLicAppointment(appointment: any) {
    this.agentComService.modeLicenseApptModal('EDIT');
    this.agentDataService.storeLicenseAppointment(appointment);
  }

  onSetOpenLicenseID(displayOpenLicenseID: number) {
    this.displayOpenLicenseID = displayOpenLicenseID;
  }

  onSetTypeTickler(typeTickler: any) {
    this.typeTickler = typeTickler;
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
