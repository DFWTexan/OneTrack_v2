import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
// import { Subject } from 'rxjs';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class DropdownDataService {
  // private branchNames: string[] = [];
  // private scoreNumbers: string[] = [];
  // private employerAgencies: string[] = [];
  // private licenseStatuses: string[] = [];
  // private licenseNames: string[] = [];

  private url: string = environment.apiUrl + 'Misc/';

  constructor(private http: HttpClient) {}

  fetchDropdownData(vEndpoint: string) {
    const url = this.url + vEndpoint;
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: { value: string; label: string }[];
        errMessage: string;
      }>(url)
      .pipe(
        map((response) => {
          // Check if the response is successful and has data
          if (response.success && response.objData) {
            // Map the objData to the desired format
            return response.objData.map((item) => ({
              value: item.value, // Assuming you want to map 'key' to 'value'
              label: item.label, // 'value' is mapped to 'label'
            }));
          } else {
            // Handle the case where response is not successful or objData is not available
            // This can be adjusted based on how you want to handle errors or empty data
            return [];
          }
        })
      );
  }

  fetchDropdownNumericData(vEndpoint: string, vValue: string | null = null) {
    const url = this.url + vEndpoint + (vValue ? '/' + vValue : '');
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: { value: number; label: string }[];
        errMessage: string;
      }>(url)
      .pipe(
        map((response) => {
          // Check if the response is successful and has data
          if (response.success && response.objData) {
            // Map the objData to the desired format
            return response.objData.map((item) => ({
              value: item.value, // Assuming you want to map 'key' to 'value'
              label: item.label, // 'value' is mapped to 'label'
            }));
          } else {
            // Handle the case where response is not successful or objData is not available
            // This can be adjusted based on how you want to handle errors or empty data
            return [];
          }
        })
      );
  }
}
