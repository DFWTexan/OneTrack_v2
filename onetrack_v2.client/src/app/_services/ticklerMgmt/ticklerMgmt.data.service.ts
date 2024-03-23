import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { StockTickler, TicklerInfo, TicklerLicTech } from '../../_Models';
import { TicklerMgmtComService } from './ticklerMgmt.com.service';

@Injectable({
  providedIn: 'root',
})
export class TicklerMgmtDataService {
  private apiUrl: string = environment.apiUrl + 'TicklerMgmt/';

  stockTicklerItems: StockTickler[] = [];
  stockTicklerItemsChanged = new Subject<StockTickler[]>();
  ticklerInfoItems: TicklerInfo[] = [];
  ticklerInfoItemsChanged = new Subject<TicklerInfo[]>();
  licenseTechItems: TicklerLicTech[] = [];
  licenseTechItemsChanged = new Subject<TicklerLicTech[]>();
  ticklerInfo: TicklerInfo = {} as TicklerInfo;
  ticklerInfoChanged = new Subject<TicklerInfo>();

  paramLicenseTech: number = 0;
  paramSOEID: string | null = null;

  constructor(
    private http: HttpClient,
    private ticklerMgmtComService: TicklerMgmtComService
  ) {}

  fetchStockTickler(): Observable<StockTickler[]> {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetStockTickler')
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.stockTicklerItems = response.objData;
            this.stockTicklerItemsChanged.next(this.stockTicklerItems);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  fetchLicenseTech(
    licenseTechID: number | 0,
    soeid: string | null
  ): Observable<TicklerLicTech[]> {
    const queryParams = `?licenseTechID=${licenseTechID}&soeid=${
      soeid ? soeid : ''
    }`;

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(`${this.apiUrl}GetLicenseTech${queryParams}`)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.licenseTechItems = response.objData;
            this.licenseTechItemsChanged.next(this.licenseTechItems);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  fetchTicklerInfo(
    ticklerID: number | 0,
    licenseTechID: number | 0,
    employmentID: number | 0
  ): Observable<TicklerInfo[]> {
    const queryParams = `?ticklerID=${ticklerID}&licenseTechID=${licenseTechID}&soeid=${employmentID}`;

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(`${this.apiUrl}GetTicklerInfo${queryParams}`)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.ticklerInfoItems = response.objData;
            this.ticklerInfoItemsChanged.next(this.ticklerInfoItems);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  storeTicklerInfo(mode: string | '', ticklerInfo: any | null) {
    this.ticklerMgmtComService.modeTicklerMgmtModal(mode);
    this.ticklerInfo = ticklerInfo || {};
    this.ticklerInfoChanged.next(this.ticklerInfo);
  }

  addStockTickler(stockTickler: StockTickler): Observable<StockTickler> {
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'AddStockTickler', stockTickler)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.stockTicklerItems.push(response.objData);
            this.stockTicklerItemsChanged.next(this.stockTicklerItems);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }
}
