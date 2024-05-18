import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';

import {
  AgentInfo,
  CompanyRequirementsHistory,
  EmploymentHistory,
  EmploymentJobTitleHistory,
  TransferHistory,
} from '../../../_Models';
import {
  AgentComService,
  AgentDataService,
  AppComService,
  DropdownDataService,
  MiscDataService,
  ModalService,
  UserAcctInfoDataService,
} from '../../../_services';
import { ConfirmDialogComponent } from '../../../_components';

@Component({
  selector: 'app-tm-emptrans-history',
  templateUrl: './tm-emptrans-history.component.html',
  styleUrl: './tm-emptrans-history.component.css',
})
@Injectable()
export class TmEmptransHistoryComponent implements OnInit, OnDestroy {
  agentInfo: AgentInfo = {} as AgentInfo;
  eventAction: string = '';
  vObject: any = {};
  subscribeAgentInfo: Subscription;

  employmentHistory: EmploymentHistory[] = [];
  transferHistory: TransferHistory[] = [];
  companyRequirementsHistory: CompanyRequirementsHistory[] = [];
  employmentJobTitleHistory: EmploymentJobTitleHistory[] = [];

  constructor(
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    protected modalService: ModalService,
    public appComService: AppComService,
    public userInfoDataService: UserAcctInfoDataService,
    public dialog: MatDialog
  ) {
    this.subscribeAgentInfo = new Subscription();
  }

  ngOnInit(): void {
    this.subscribeAgentInfo = this.agentDataService.agentInfoChanged.subscribe(
      (agentInfo: any) => {
        // this.agentInfo = agentInfo;
        this.employmentHistory = agentInfo.employmentHistory;
        this.transferHistory = agentInfo.transferHistory;
        this.companyRequirementsHistory = agentInfo.compayRequirementsHistory;
        this.employmentJobTitleHistory = agentInfo.employmentJobTitleHistory;
      }
    );
  }

  // onNgOnChanges(): void {
  //   this.agentInfo = this.agentDataService.getAgentInfo();
  //   this.employmentHistory = this.agentInfo.employmentHistory;
  //   this.transferHistory = this.agentInfo.transferHistory;
  //   this.companyRequirementsHistory = this.agentInfo.compayRequirementsHistory;
  //   this.employmentJobTitleHistory = this.agentInfo.employmentJobTitleHistory;
  // }

  openConfirmDialog(eventAction: string, msg: string, vObject: any): void {
    this.eventAction = eventAction;
    this.vObject = vObject;
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: 'Confirm Action',
        message: 'You are about to DELETE ' + msg,
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      // console.log('The dialog was closed');
      // console.log('Confirmation result:', result);
      if (result) {
        switch (this.eventAction) {
          case 'deleteEmploymentHistory':
            this.deleteEmploymentHistory(vObject);
            break;
          case 'deleteTransferHistory':
            this.deleteTransferHistory(vObject);
            break;
            case 'deleteCoRequirementItem':
              this.deleteCompanyRequirement(vObject);
              break;
          default:
            break;
        }
      }
    });
  }

  deleteEmploymentHistory(empItem: any): void {
    this.agentDataService
      .deleteEmploymentHistItem({
        employmentID: this.agentDataService.agentInformation.employmentID,
        employmentHistoryID: empItem.employmentHistoryID,
        userSOEID: this.userInfoDataService.userAcctInfo.soeid,
      })
      .subscribe({
        next: (response) => {
          // console.log(
          //   'EMFTEST (app-tm-emptrans-history: deleteEmploymentHistory) - COMPLETED DELETE response => \n',
          //   response
          // );
        },
        error: (error) => {
          console.error(error);
          // handle the error here
        },
      });
  }

  deleteTransferHistory(transferItem: any): void {
    this.agentDataService
      .deleteTransferHistItem({
        employmentID: this.agentDataService.agentInformation.employmentID,
        transferHistoryID: transferItem.transferHistoryID,
        userSOEID: this.userInfoDataService.userAcctInfo.soeid,
      })
      .subscribe({
        next: (response) => {
          // console.log(
          //   'EMFTEST (app-tm-emptrans-history: deleteTransferHistory) - COMPLETED DELETE response => \n',
          //   response
          // );
        },
        error: (error) => {
          console.error(error);
          // handle the error here
        },
      });
  }

  deleteCompanyRequirement(coReqItem: any): void {
    this.agentDataService
      .deleteCompanyRequirementsHistItem({
        employmentID: this.agentDataService.agentInformation.employmentID,
        companyRequirementID: coReqItem.companyRequirementID,
        userSOEID: this.userInfoDataService.userAcctInfo.soeid,
      })
      .subscribe({
        next: (response) => {
          // console.log(
          //   'EMFTEST (app-tm-emptrans-history: deleteCompanyRequirement) - COMPLETED DELETE response => \n',
          //   response
          // );
        },
        error: (error) => {
          console.error(error);
          // handle the error here
        },
      });
  }

  ngOnDestroy(): void {
    this.subscribeAgentInfo.unsubscribe();
  }
}
