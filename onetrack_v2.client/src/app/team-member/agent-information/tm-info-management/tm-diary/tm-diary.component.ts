import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { AgentInfo, DiaryItem } from '../../../../_Models';
import {
  AgentComService,
  AgentDataService,
  AppComService,
  ErrorMessageService,
  ModalService,
  PaginationComService,
  UserAcctInfoDataService,
} from '../../../../_services';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../../../_components';

@Component({
  selector: 'app-tm-diary',
  templateUrl: './tm-diary.component.html',
  styleUrl: './tm-diary.component.css',
})
@Injectable()
export class TmDiaryComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  agentInfo: AgentInfo = {} as AgentInfo;
  vObject: any = {};
  diaryItems: DiaryItem[] = [];

  private subscriptions = new Subscription();

  constructor(
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    public appComService: AppComService,
    protected modalService: ModalService,
    public paginationComService: PaginationComService,
    public userInfoDataService: UserAcctInfoDataService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.subscriptions.add(
      this.agentDataService.agentInfoChanged.subscribe((agentInfo: any) => {
        this.agentInfo = agentInfo;
      })
    );

    this.subscriptions.add(
      this.agentDataService.diaryItemsChanged.subscribe((diaryData: any) => {
        this.diaryItems = diaryData;
        this.paginationComService.updateDisplayItems(this.diaryItems);
        this.paginationComService.updatePaginatedResults();
        this.isLoading = false;
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
            .deleteDiaryEntry({
              diaryID: vObject.diaryID,
              employmentID: this.agentDataService.agentInformation.employmentID,
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
