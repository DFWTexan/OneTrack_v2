import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { ModalService, TicklerMgmtComService, TicklerMgmtDataService } from '../../../_services';
import { StockTickler, TicklerInfo } from '../../../_Models';

@Component({
  selector: 'app-tickler-info',
  templateUrl: './tickler-info.component.html',
  styleUrl: './tickler-info.component.css',
})
@Injectable()
export class TicklerInfoComponent implements OnInit, OnDestroy {
  loading: boolean = false;
  stockTicklerItems: StockTickler[] = [];
  licenseTechItems: any = [];
  subscribeStockTicklerItemsData: Subscription;
  selectedLicenseTechID: number = 0;
  ticklerInfoItems: TicklerInfo[] = [];

  constructor(
    public ticklerMgmtDataService: TicklerMgmtDataService,
    public ticklerMgmtComService: TicklerMgmtComService,
    protected modalService: ModalService
  ) {
    this.subscribeStockTicklerItemsData = new Subscription();
  }

  ngOnInit(): void {
    this.loading = true;
    this.ticklerMgmtDataService
      .fetchStockTickler()
      .subscribe((stockTicklerItems: any) => {
        this.stockTicklerItems = stockTicklerItems;
      });

    this.ticklerMgmtDataService
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
  }

  changeStockTicklerItems(event: Event): void {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;

    this.selectedLicenseTechID = +value;

    this.ticklerMgmtDataService
      .fetchTicklerInfo(0, +value, 0)
      .subscribe((ticklerInfoItems: any) => {
        this.ticklerInfoItems = ticklerInfoItems;
        this.loading = false;
      });
  }

  ngOnDestroy(): void {
    this.subscribeStockTicklerItemsData.unsubscribe();
  }
}
