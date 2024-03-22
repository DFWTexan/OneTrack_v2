import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { LicenseIncentiveInfo } from '../../_Models';

@Injectable({
  providedIn: 'root',
})
export class LicIncentiveInfoDataService {
  private apiUrl: string = environment.apiUrl + 'LicenseInfo/';
  licenseIncentiveInfo: LicenseIncentiveInfo = {} as LicenseIncentiveInfo;
  licenseIncentiveInfoChanged = new Subject<LicenseIncentiveInfo[]>();

  constructor(private http: HttpClient) {}

  fetchLicIncentiveInfo(
    employeeLicenseId: number
  ): Observable<LicenseIncentiveInfo> {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetIncentiveInfo/' + employeeLicenseId)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.licenseIncentiveInfo = response.objData;
            this.licenseIncentiveInfoChanged.next([
              this.licenseIncentiveInfo,
            ]);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }
}
