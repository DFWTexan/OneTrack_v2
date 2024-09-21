import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../_environments/environment';
import { DashboardData } from '../../_Models';

@Injectable({
  providedIn: 'root',
})
export class DashboardDataService {
  private apiUrl: string = environment.apiUrl + 'Dashboard/';

  constructor(private http: HttpClient) {}

  // fetchDashboardData(): Observable<DashboardData> {
  //   return this.http
  //     .get<{
  //       success: boolean;
  //       statusCode: number;
  //       objData: any;
  //       errMessage: string;
  //     }>(this.apiUrl + 'GetDashboardData')
  //     .pipe(
  //       map((response) => {
  //         if (response.success && response.statusCode === 200) {
  //           return response.objData;
  //         } else {
  //           throw new Error(response.errMessage || 'Unknown error');
  //         }
  //       })
  //     );
  // }

  fetchAdBankerImportInfo(): Observable<DashboardData> {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetAdBankerImportStatus')
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  fetchADBankerData(
    startDate: string = new Date().toISOString(),
    endDate: string,
    importStatus: string | null
  ) {

    const queryParams = `?startDate=${startDate}&endDate=${endDate}&importStatus=${importStatus}`;

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(`${this.apiUrl}GetAdBankerImportData${queryParams}`)
      .pipe(
        map((response) => {
          if (response.success && response.objData) {
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  fetchAuditModifiedBy(isActive: boolean): Observable<string[]> {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetAuditModifiedBy', {
        params: { isActive: isActive.toString() },
      })
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  fetchAuditLog(
    startDate: string = new Date().toISOString(),
    endDate: string,
    modifiedBy: string | null
  ) {
    const queryParams = `?startDate=${startDate}&endDate=${endDate}&modifiedBy=${
      modifiedBy ? modifiedBy : ''
    }`;

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(`${this.apiUrl}GetAuditLog${queryParams}`)
      .pipe(
        map((response) => {
          if (response.success && response.objData) {
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }
}
