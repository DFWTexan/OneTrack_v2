import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
// import { Subject } from 'rxjs';

import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class DropdownDataService {
  private url: string = environment.apiUrl + 'Employee/';

  constructor(private http: HttpClient) {}

  fetchEmployeeSearch(){
    const url = this.url + 'SearchEmployee';
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: { key: string; value: string }[];
        errMessage: string;
      }>(url)
      .pipe(
        map((response) => {
          // Check if the response is successful and has data
          if (response.success && response.objData) {
            // Map the objData to the desired format
            return response.objData.map((item) => ({
              value: item.key, // Assuming you want to map 'key' to 'value'
              label: item.value, // 'value' is mapped to 'label'
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
