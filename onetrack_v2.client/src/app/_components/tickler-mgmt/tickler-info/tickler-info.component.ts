import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

import {
  AgentDataService,
  ModalService,
  TicklerMgmtComService,
  TicklerMgmtDataService,
} from '../../../_services';
import { AgentInfo, StockTickler, TicklerInfo } from '../../../_Models';

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

  // subscriptions
  subscribeStockTicklerData: Subscription = new Subscription();
  subscribeLicenseTechData: Subscription = new Subscription();
  subscribeTicklerInfoData: Subscription = new Subscription();

  constructor(
    public ticklerMgmtDataService: TicklerMgmtDataService,
    public ticklerMgmtComService: TicklerMgmtComService,
    private agentDataService: AgentDataService,
    public router: Router,
    protected modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.loading = true;
    this.subscribeStockTicklerData = this.ticklerMgmtDataService
      .fetchStockTickler()
      .subscribe((stockTicklerItems: any) => {
        this.stockTicklerItems = stockTicklerItems;
      });

    if (this.router.url.startsWith('/team/search-members')) {
      this.subscribeLicenseTechData = this.ticklerMgmtDataService
        .fetchLicenseTech(0, null)
        .subscribe((licenseTechItems: any) => {
          this.licenseTechItems = licenseTechItems;
          this.selectedLicenseTechID = licenseTechItems[0].licenseTechId;
          this.ticklerMgmtDataService
            .fetchTicklerInfo(0, licenseTechItems[0].licenseTechId, 0)
            .subscribe((ticklerInfoItems: any) => {
              this.loading = false;
              this.ticklerInfoItems = ticklerInfoItems;
            });
        });
    } else {
      this.subscribeTicklerInfoData = this.ticklerMgmtDataService
        .fetchTicklerInfo(
          0,
          0,
          this.agentDataService.agentInformation.employmentID
        )
        .subscribe((ticklerInfoItems: any) => {
          this.loading = false;
          this.ticklerInfoItems = ticklerInfoItems;
        });
    }
  }

  onChangeStockTicklerItems(event: Event): void {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;

    this.selectedLicenseTechID = +value;

    this.subscribeTicklerInfoData = this.ticklerMgmtDataService
      .fetchTicklerInfo(0, +value, 0)
      .subscribe((ticklerInfoItems: any) => {
        this.ticklerInfoItems = ticklerInfoItems;
        this.loading = false;
      });
  }

  ngOnDestroy(): void {
    this.subscribeStockTicklerData.unsubscribe();
    this.subscribeLicenseTechData.unsubscribe();
    this.subscribeTicklerInfoData.unsubscribe();
  }
}
