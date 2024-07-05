import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { LicenseTech } from '../../_Models';

@Injectable({
  providedIn: 'root',
})
export class MiscDataService {
  private url: string = environment.apiUrl + 'Misc/';

  constructor(private http: HttpClient) {}

  // fetchDropdownData(vEndpoint: string) {
  //   const url = this.url + vEndpoint;
  //   return this.http
  //     .get<{
  //       success: boolean;
  //       statusCode: number;
  //       objData: { key: string; value: string }[];
  //       errMessage: string;
  //     }>(url)
  //     .pipe(
  //       map((response) => {
  //         // Check if the response is successful and has data
  //         if (response.success && response.objData) {
  //           // Map the objData to the desired format
  //           return response.objData.map((item) => ({
  //             value: item.key, // Assuming you want to map 'key' to 'value'
  //             label: item.value, // 'value' is mapped to 'label'
  //           }));
  //         } else {
  //           // Handle the case where response is not successful or objData is not available
  //           // This can be adjusted based on how you want to handle errors or empty data
  //           return [];
  //         }
  //       })
  //     );
  // }

  fetchWorkListNames() {
    const url = this.url + 'WorkListNames';
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: string[];
        errMessage: string;
      }>(url)
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

  fetchLicenseTechs() {
    const url = this.url + 'GetLicenseTeches';
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: LicenseTech[];
        errMessage: string;
      }>(url)
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

  fetchBackgroundStatuses() {
    const url = this.url + 'GetBackgroundStatuses';
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: Array<{ lkpValue: string }>;
        errMessage: string;
      }>(url)
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

  fetchJobTitles() {
    const url = this.url + 'GetJobTitles';
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: Array<{ value: number, label: string }>;
        errMessage: string;
      }>(url)
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

  fetchConEduInfo(usageType: string) {
    const url = this.url + 'GetContEduInfo/' + usageType;
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: Array<{ value: number, label: string }>;
        errMessage: string;
      }>(url)
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

  fetchExamProviders() {
    const url = this.url + 'GetExamProviders';
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: Array<{ value: number, label: string }>;
        errMessage: string;
      }>(url)
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
