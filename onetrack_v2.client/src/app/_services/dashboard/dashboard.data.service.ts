import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { DashboardData } from '../../_Models';

@Injectable({
    providedIn: 'root',
  })
export class DashboardDataService {
  private apiUrl: string = environment.apiUrl + 'Dashboard/';

  constructor(private http: HttpClient) {}

  fetchDashboardData(): Observable<DashboardData> {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetDashboardData')
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
}