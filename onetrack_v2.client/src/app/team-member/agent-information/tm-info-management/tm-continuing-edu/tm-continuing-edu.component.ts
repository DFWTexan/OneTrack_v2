import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AgentComService,
  AgentDataService,
  AppComService,
  ModalService,
  UserAcctInfoDataService,
} from '../../../../_services';
import { AgentInfo } from '../../../../_Models';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../../../_components';

@Component({
  selector: 'app-tm-continuing-edu',
  templateUrl: './tm-continuing-edu.component.html',
  styleUrl: './tm-continuing-edu.component.css',
})
@Injectable()
export class TmContinuingEduComponent implements OnInit, OnDestroy {
  agentInfo: AgentInfo = {} as AgentInfo;
  vObject: any = {};
  sumHoursRequired: number = 0;
  sumHoursTaken: number = 0;
  totalHoursRemaining: number = 0;

  private subscriptions = new Subscription();

  constructor(
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    protected modalService: ModalService,
    public appComService: AppComService,
    public userInfoDataService: UserAcctInfoDataService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.subscriptions.add(
      this.agentDataService.agentInfoChanged.subscribe((agentInfo: any) => {
        this.agentInfo = agentInfo;

        this.sumHoursRequired = this.agentInfo.contEduRequiredItems.reduce(
          (total, item) => total + (item.requiredCreditHours || 0),
          0
        );
        this.sumHoursTaken = this.agentInfo.contEduCompletedItems.reduce(
          (total, item) => total + (item.creditHoursTaken || 0),
          0
        );
        this.totalHoursRemaining = this.sumHoursRequired - this.sumHoursTaken;
      })
    );
  }

  onOpenConfirmDialog( msg: string, vObject: any): void {
    this.vObject = vObject;
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: 'Confirm Action',
        message: 'You are about to DELETE ' + msg,
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.subscriptions.add(
          this.agentDataService
            .deleteContEduHoursTaken({
              contEducationRequirementID: vObject.contEducationRequirementID,
              employeeEducationID: vObject.employeeEducationID,
              userSOEID: this.userInfoDataService.userAcctInfo.soeid,
            })
            .subscribe({
              next: (response: any) => {
                // console.log(
                //   'EMFTEST (app-tm-emptrans-history: deleteEmploymentHistory) - COMPLETED DELETE response => \n',
                //   response
                // );
              },
              error: (error: any) => {
                console.error(error);
                // handle the error here
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
