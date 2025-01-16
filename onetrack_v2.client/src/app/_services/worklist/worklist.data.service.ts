import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../_environments/environment';

@Injectable({
  providedIn: 'root',
})
export class WorkListDataService {
  private apiUrl: string = environment.apiUrl + 'Worklist/';

  worklistData: any[] = [];
  worklistDataChanged = new Subject<any[]>();

  constructor(private http: HttpClient) {}

  fetchWorkListData(
    worklistName: string,
    worklistDate: string | null = null,
    licenseTech: string | null = null
  ) {
    const queryParams = `?worklistName=${worklistName}&worklistDate=${worklistDate}&licenseTech=${licenseTech}`;

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any[];
        errMessage: string;
      }>(`${this.apiUrl}GetWorklistData${queryParams}`)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.worklistData = response.objData;
            this.worklistDataChanged.next(this.worklistData);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }
}
