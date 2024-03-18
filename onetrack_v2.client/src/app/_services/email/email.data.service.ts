import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { AgentComService } from '../agent/agentInfo.com.service';

@Injectable({
  providedIn: 'root',
})
export class EmailDataService {
    private apiUrl: string = environment.apiUrl;

    constructor(
        private http: HttpClient,
        private agentComService: AgentComService
      ) {}
}
