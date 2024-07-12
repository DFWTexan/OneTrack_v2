import { Component, OnInit, Injectable, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';

import {
  AgentLicApplicationInfo,
  AgentLicenseAppointments,
} from '../../../../_Models';
import {
  AgentComService,
  AgentDataService,
  ErrorMessageService,
  LicIncentiveInfoDataService,
  ModalService,
  UserAcctInfoDataService,
} from '../../../../_services';
import { ConfirmDialogComponent } from '../../../../_components';

@Component({
  selector: 'app-license-renewal',
  templateUrl: './license-renewal.component.html',
  styleUrl: './license-renewal.component.css',
})
@Injectable()
export class LicenseRenewalComponent implements OnInit, OnDestroy {
  licenseMgmtData: AgentLicenseAppointments[] = [];
  currentIndex: number = 0;
  agentLicApplicationInfo: AgentLicApplicationInfo =
    {} as AgentLicApplicationInfo;
  eventAction: string = '';
  vObject: any = {};

  private subscriptions = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    private licenseDataService: LicIncentiveInfoDataService,
    protected modalService: ModalService,
    private userInfoDataService: UserAcctInfoDataService,
    public dialog: MatDialog
  ) {
    this.agentDataService.agentLicApplicationInfo.licenseRenewalItems = [];
  }

  ngOnInit(): void {
    this.currentIndex = this.agentDataService.licenseMgmtDataIndex;
    this.subscriptions.add(
      this.agentDataService.licenseMgmtDataIndexChanged.subscribe(
        (index: number) => {
          this.currentIndex = index;
          this.getData();
        }
      )
    );

    this.licenseMgmtData =
      this.agentDataService.agentInformation.agentLicenseAppointments;
    this.subscriptions.add(
      this.agentDataService.licenseMgmtDataChanged.subscribe(
        (licenseMgmtData: AgentLicenseAppointments) => {
          this.licenseMgmtData = [licenseMgmtData];
          this.getData();
        }
      )
    );

    // this.subscriptions.add(
    //   this.agentDataService
    //   .fetchAgentLicApplicationInfo(
    //     this.licenseMgmtData[this.currentIndex].employeeLicenseId
    //   )
    //   .subscribe((agentLicApplicationInfo: AgentLicApplicationInfo) => {
    //     this.agentLicApplicationInfo = agentLicApplicationInfo;
    //   })
    // );
    this.getData();
  }

  onChildCallGetData(): void {
    this.getData();
  }

  getData() {
    this.subscriptions.add(
      this.agentDataService
        .fetchAgentLicApplicationInfo(
          this.licenseMgmtData[this.currentIndex].employeeLicenseId
        )
        .subscribe((agentLicApplicationInfo: AgentLicApplicationInfo) => {
          this.agentLicApplicationInfo = agentLicApplicationInfo;
        })
    );
  }

  // Pagination
  // nextPage() {
  //   if (this.currentIndex < this.licenseMgmtData.length - 1) {
  //     this.currentIndex++;
  //     this.agentDataService.licenseMgmtDataIndexChanged.next(this.currentIndex);
  //   }
  // }

  // previousPage() {
  //   if (this.currentIndex > 0) {
  //     this.currentIndex--;
  //     this.agentDataService.licenseMgmtDataIndexChanged.next(this.currentIndex);
  //   }
  // }

  // getPageInfo(): string {
  //   return `${this.currentIndex + 1} of ${this.licenseMgmtData.length}`;
  // }

  // isDisplayPrevious(): boolean {
  //   return this.currentIndex > 0;
  // }

  // isDisplayNext(): boolean {
  //   return this.currentIndex < this.licenseMgmtData.length - 1;
  // }

  openConfirmDialog(eventAction: string, msg: string, vObject: any): void {
    this.eventAction = eventAction;
    this.vObject = vObject;

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: 'Confirm Action',
        message: `You are about to DELETE ${msg} - (${vObject.licenseApplicationID}). Do you want to proceed?`,
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.subscriptions.add(
          this.licenseDataService
            .deleteLicenseApplicationItem({
              licenseApplicationID: this.vObject.licenseApplicationID,
              employeeLicenseID: this.vObject.employeeLicenseID,
              userSOEID: this.userInfoDataService.userAcctInfo.soeid,
            })
            .subscribe({
              next: (response) => {
                this.getData();
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
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
