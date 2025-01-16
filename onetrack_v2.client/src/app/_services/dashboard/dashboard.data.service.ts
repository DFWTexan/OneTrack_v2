import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

import { environment } from '../../_environments/environment';
import { AuditModifyBy, DashboardData } from '../../_Models';
import { ConfigService } from '../config/config.service';
import { AgentDataService } from '../agent/agent.data.service';

@Injectable({
  providedIn: 'root',
})
export class DashboardDataService {
  // private apiUrl: string = environment.apiUrl + 'Dashboard/';
  private apiUrl: string = this.configService.config.apiUrl + 'Dashboard/';
  adBankerIncompleteCount: number = 0;
  adBankerIncompleteCountChanged = new Subject<number>();

  constructor(
    private http: HttpClient,
    public configService: ConfigService,
  ) {}

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
            // this.adBankerIncompleteCount = response.objData.filter(
            //   (item: any) => !item.isImportComplete
            // ).length;

            // this.adBankerIncompleteCountChanged.next(
            //   this.adBankerIncompleteCount
            // );
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  fetchIncompleteAdBankerCount() {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetAdBankerIncompleteCount')
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.adBankerIncompleteCount = response.objData;
            this.adBankerIncompleteCountChanged.next(
              this.adBankerIncompleteCount
            );
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  fetchEmployeeIdWithTMemberID(memberID: string): Observable<any> {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetEmployeeIdWithTMemberID/' + memberID )
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

  updateIncompleteStatus(vObject: any) {
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'CompleteImportStatus', vObject)
      .pipe(
        map((response) => {
          return response;
        })
      );
  }

  fetchAuditModifiedBy(isActive: boolean): Observable<AuditModifyBy[]> {
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

  closeWorklistItem(wishlistItem: any): Observable<any> {
    this.apiUrl = this.configService.config.apiUrl + 'Agent/CloseWorklistItem';

    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, wishlistItem)
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
