import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
// import { Subject } from 'rxjs';

import { environment } from '../../environments/environment';
import { SearchEmployeeFilter, EmployeeSearchResult } from '../../_Models';

@Injectable({
  providedIn: 'root',
})
export class EmployeeDataService {
  private apiUrl: string = environment.apiUrl + 'Employee/SearchEmployee_v2';
  employeeSearchResults: EmployeeSearchResult[] = [];
  employeeSearchResultsChanged = new Subject<EmployeeSearchResult[]>();

  constructor(private http: HttpClient) {}

  fetchEmployeeSearch(
    vSearchEmployee: SearchEmployeeFilter
  ): Observable<EmployeeSearchResult[]> {
    return this.http
      .put<{
        success: boolean;
        statusCode: number;
        objData: EmployeeSearchResult[];
        errMessage: string;
      }>(this.apiUrl, vSearchEmployee)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.employeeSearchResults = response.objData;
            this.employeeSearchResultsChanged.next(this.employeeSearchResults);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }
}