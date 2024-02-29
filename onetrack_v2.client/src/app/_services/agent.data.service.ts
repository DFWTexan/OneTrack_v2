import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
// import { Subject } from 'rxjs';

import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AgentService {
    private apiUrl: string = environment.apiUrl + 'Employee/SearchEmployee_v2';
    agentInformation: any = [];

    constructor(private http: HttpClient) {}

    fetchAgentInformation(): Observable<any> {
        return this.http.get(this.apiUrl);
    }
}