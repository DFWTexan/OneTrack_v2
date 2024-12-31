import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
// import { Subject } from 'rxjs';

import { environment } from '../../_environments/environment';
import { SearchEmployeeFilter, EmployeeSearchResult, EmployeeFind } from '../../_Models';

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

  fetchEmployeeByTmNumber(vTmNumber: string): Observable<EmployeeFind> {
    console.log(
      'EMFTEST (employee.data.service: fetchEmployeeByTmNumber) - vTmNumber => \n',
      vTmNumber
    );

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: EmployeeFind;
        errMessage: string;
      }>(environment.apiUrl + 'Employee/SearchEmployeeTMNumber' + '/' + vTmNumber)
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

  fetchEmployeeByAgentName(vAgentName: string): Observable<EmployeeFind[]> {
    console.log(
      'EMFTEST (employee.data.service: fetchEmployeeByAgentName) - vAgentName => \n',
      vAgentName
    );

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: EmployeeFind[];
        errMessage: string;
      }>(environment.apiUrl + 'Employee/SearchEmployeeName' + '/' + vAgentName)
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
