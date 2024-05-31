import { Component, OnInit, Injectable, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { Location } from '@angular/common';

import {
  AgentComService,
  AgentDataService,
  AppComService,
  UserAcctInfoDataService,
} from '../../../../_services';
import { AgentInfo } from '../../../../_Models';
import { ModalService } from '../../../../_services';
import { ConfirmDialogComponent } from '../../../../_components';
import { Router } from '@angular/router';
import { InfoDialogComponent } from '../../../../_components/info-dialog/info-dialog.component';

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
    private agentDataService: AgentDataService,
    public agentComService: AgentComService,
    protected modalService: ModalService,
    public appComService: AppComService,
    private userInfoDataService: UserAcctInfoDataService,
    public dialog: MatDialog,
    private router: Router,
    private location: Location
  ) {}

  ngOnInit() {
    this.isLoading = true;

    this.subscriptions.add(
      this.agentDataService.agentInfoChanged.subscribe((agentInfo: any) => {
        this.isLoading = false;
        this.agentInfo = agentInfo;
      })
    );
  }

  openConfirmDialog(eventAction: string, msg: string, vObject: any): void {
    this.eventAction = eventAction;
    this.vObject = vObject;
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
                // console.log(
                //   'EMFTEST (app-tm-emptrans-history: deleteEmploymentHistory) - COMPLETED DELETE response => \n',
                //   response
                // );
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
  }

  setModeLicenseMgmt(mode: string) {
    this.agentComService.modeLicenseMgmtModal(mode);
  }

  storeLicAppointment(appointment: any) {
    this.agentDataService.storeLicenseAppointment(appointment);
  }

  // toggleLicenseMgmt(index: number) {
  //   alert('Plese be patient - Loading Agent License Mgmt...');
  //   // this.showLoadingDialog('toggleLicenseMgmt', 'Loading Agent License Mgmt...', null);
  //   const dialogRef = this.dialog.open(InfoDialogComponent, {
  //     data: { message: 'Loading Agent License Mgmt...' },
  //   });

  //   this.agentDataService.storeLicenseMgmtDataIndex(index);
  //   this.agentComService.showLicenseMgmt();

  //   // this.router.navigateByUrl('/team/agent-license-info');
  //   this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
  //     this.router.navigate([
  //       'team/agent-info',
  //       this.agentDataService.agentInformation.employeeID,
  //       'tm-license-mgmt',
  //     ]);
  //   });
  // }
  toggleLicenseMgmt(index: number) {
    // alert('Please be patient - Loading Agent License Mgmt...');
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

  showLoadingDialog(eventAction: string, msg: string, vObject: any): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { message: msg },
      disableClose: true, // This prevents the user from closing the dialog
    });

    // Close the dialog when a value is set
    this.subscriptions.add(
      this.agentDataService.agentInfoChanged.subscribe((agentInfo: any) => {
        if (agentInfo) {
          dialogRef.close();
        }
      })
    );
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
