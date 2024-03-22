import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { ModalService, TicklerMgmtDataService } from '../../../_services';
import { StockTickler, TicklerInfo, TicklerLicTech } from '../../../_Models';

@Component({
  selector: 'app-tickler-info',
  templateUrl: './tickler-info.component.html',
  styleUrl: './tickler-info.component.css',
})
@Injectable()
export class TicklerInfoComponent implements OnInit, OnDestroy {
  stockTicklerItems: StockTickler[] = [];
  licenseTechItems: any = [];
  subscribeStockTicklerItemsData: Subscription;
  selectedLicenseTechID: number = 0;
  ticklerInfoItems: TicklerInfo[] = [];

  constructor(
    public ticklerMgmtDataService: TicklerMgmtDataService,
    protected modalService: ModalService
  ) {
    this.subscribeStockTicklerItemsData = new Subscription();
  }

  ngOnInit(): void {
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

// console.log(
//           'EMFTEST () - licenseTechItems[0]: ',
//           licenseTechItems[0].licenseTechId
//         );

        this.ticklerMgmtDataService
          .fetchTicklerInfo(0, licenseTechItems[0].licenseTechId, 0)
          .subscribe((ticklerInfoItems: any) => {

//  console.log(
//               'EMFTEST () - stockTicklerItems => \n',
//               stockTicklerItems
//             );

            this.ticklerInfoItems = ticklerInfoItems;
          });
      });
  }

  changeStockTicklerItems(event: Event): void {
    const target = event.target as HTMLInputElement;
    const value = target.value;

    this.selectedLicenseTechID = +value;

    this.ticklerMgmtDataService
      .fetchTicklerInfo(+value, 0, 0)
      .subscribe((stockTicklerItems: any) => {
        this.stockTicklerItems = stockTicklerItems;
      });
  }

  ngOnDestroy(): void {
    this.subscribeStockTicklerItemsData.unsubscribe();
  }
}
