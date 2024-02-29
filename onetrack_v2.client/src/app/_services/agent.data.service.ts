import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
// import { Subject } from 'rxjs';

import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AgentDataService {
    private apiUrl: string = environment.apiUrl;
    agentInformation: any = [];

    constructor(private http: HttpClient) {}

    fetchAgentInformation(employeeID: number): Observable<any> {
      this.apiUrl = environment.apiUrl + 'Agent/GetAgentByEmployeeID/';

      return this.http.get(this.apiUrl + employeeID);
    }
}