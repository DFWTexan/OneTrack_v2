import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Subscription } from 'rxjs';

import {
  AgentComService,
  AgentDataService,
  ModalService,
  TicklerMgmtComService,
  TicklerMgmtDataService,
  UserAcctInfoDataService,
} from '../../../_services';
import {
  AgentInfo,
  StockTickler,
  TicklerInfo,
  UserAcctInfo,
} from '../../../_Models';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-tickler-info',
  templateUrl: './tickler-info.component.html',
  styleUrl: './tickler-info.component.css',
})
@Injectable()
export class TicklerInfoComponent implements OnInit, OnDestroy {
  loading: boolean = false;
  stockTicklerItems: StockTickler[] = [];
  licenseTechItems: any = ['Loading...'];
  selectedLicenseTechID: number = 0;
  ticklerInfoItems: TicklerInfo[] = [];
  userAcctInfo: UserAcctInfo = {} as UserAcctInfo;
  eventAction: string = '';
  vObject: any = {};

  subscriptionData: Subscription = new Subscription();

  constructor(
    public ticklerMgmtDataService: TicklerMgmtDataService,
    public ticklerMgmtComService: TicklerMgmtComService,
    private agentDataService: AgentDataService,
    public agentComService: AgentComService,
    public router: Router,
    public dialog: MatDialog,
    protected modalService: ModalService,
    private userInfoDataService: UserAcctInfoDataService
  ) {
    this.userAcctInfo = this.userInfoDataService.userAcctInfo;
    this.stockTicklerItems = this.ticklerMgmtDataService.stockTicklerItems;
    this.licenseTechItems = this.ticklerMgmtDataService.licenseTechItems;
  }

  ngOnInit(): void {
    this.loading = true;

    this.subscriptionData.add(
      this.userInfoDataService.userAcctInfoChanged.subscribe(
        (userAcctInfo: UserAcctInfo) => {
          this.userAcctInfo = userAcctInfo;
        }
      )
    );

    this.subscriptionData.add(
      this.ticklerMgmtDataService.licenseTechItemsChanged.subscribe(
        (licenseTechItems: any) => {
          this.licenseTechItems = licenseTechItems;
        }
      )
    );

    this.subscriptionData.add(
      this.ticklerMgmtDataService.stockTicklerItemsChanged.subscribe(
        (stockTicklerItems: StockTickler[]) => {
          this.stockTicklerItems = stockTicklerItems;
        }
      )
    );

    if (this.router.url.startsWith('/team/search-members')) {
      this.subscriptionData.add(
        this.ticklerMgmtDataService
          .fetchLicenseTech(0, null)
          .subscribe((licenseTechItems: any) => {
            this.licenseTechItems = licenseTechItems;
            this.selectedLicenseTechID = licenseTechItems[0].licenseTechId;

            this.ticklerMgmtDataService
              .fetchTicklerInfo(0, licenseTechItems[0].licenseTechId, 0)
              .subscribe((ticklerInfoItems: TicklerInfo[]) => {
                this.loading = false;
                this.ticklerInfoItems = ticklerInfoItems;
              });
          })
      );
    } else {
      this.subscriptionData.add(
        this.ticklerMgmtDataService
          .fetchTicklerInfo(
            0,
            0,
            this.agentDataService.agentInformation.employmentID
          )
          .subscribe((ticklerInfoItems: TicklerInfo[]) => {
            this.loading = false;
            this.ticklerInfoItems = ticklerInfoItems;
          })
      );
    }
  }

  onChildCallRefreshData(): void {
    this.fetchTicklerInfo();
  }

  private fetchTicklerInfo(): void {
    this.subscriptionData.add(
      this.ticklerMgmtDataService
        .fetchTicklerInfo(0, this.selectedLicenseTechID, 0)
        .subscribe((ticklerInfoItems: TicklerInfo[]) => {
          this.ticklerInfoItems = ticklerInfoItems;
          this.loading = false;
        })
    );
  }

  onChangeStockTicklerItems(event: Event): void {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;

    this.selectedLicenseTechID = +value;

    // this.subscriptionData.add(
    //   this.ticklerMgmtDataService
    //     .fetchTicklerInfo(0, +value, 0)
    //     .subscribe((ticklerInfoItems: any) => {
    //       this.ticklerInfoItems = ticklerInfoItems;
    //       this.loading = false;
    //     })
    // );
    this.fetchTicklerInfo();
  }

  onCloseTicklerItem(ticklerInfo: TicklerInfo): void {

console.log('EMFTEST (onCloseTicklerItem) - Tickler info in onCloseTicklerItem:', JSON.stringify(ticklerInfo));

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: 'Confirm Action',
        message:
          'You are about to CLOSE Tickler Item (' +
          ticklerInfo.ticklerId +
          ') - ' +
          ticklerInfo.lkpValue +
          '. Do you want to proceed?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.ticklerMgmtDataService
          .closeTicklerItem({
            TicklerID: ticklerInfo.ticklerId,
            TicklerCloseByLicenseTechID: ticklerInfo.licenseTechId,
            UserSOEID: this.userInfoDataService.userAcctInfo.soeid,
          })
          .subscribe({
            next: (response) => {
              this.fetchTicklerInfo();
            },
            error: (error) => {
              console.error(error);
              // handle the error here
            },
          });
      }
    });
  }

  onDeleteTicklerItem(ticklerInfo: TicklerInfo): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: 'Confirm Action',
        message:
          'You are about to DELETE Tickler Item (' +
          ticklerInfo.ticklerId +
          ') - ' +
          ticklerInfo.lkpValue +
          '. Do you want to proceed?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.ticklerMgmtDataService
          .deleteTicklerItem({
            TicklerID: ticklerInfo.ticklerId,
            UserSOEID: this.userInfoDataService.userAcctInfo.soeid,
          })
          .subscribe({
            next: (response) => {
              this.fetchTicklerInfo();
            },
            error: (error) => {
              console.error(error);
              // handle the error here
            },
          });
      }
    });
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
