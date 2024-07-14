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
  selectedEmployee: EmployeeSearchResult | null = null;
  selectedEmployeeChanged = new Subject<EmployeeSearchResult | null>();

  constructor(private http: HttpClient) {}

  updateSelectedEmployee(vSelectedEmployee: EmployeeSearchResult | null) {
    this.selectedEmployee = vSelectedEmployee;
    this.selectedEmployeeChanged.next(this.selectedEmployee);
  }

  fetchEmployeeSearch(
    vSearchEmployee: SearchEmployeeFilter
  ): Observable<EmployeeSearchResult[]> {
    console.log(
      'EMFTEST (employee.data.service: fetchEmployeeSearch) - vSearchEmployee => \n',
      vSearchEmployee
    );

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
