import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { AgentComService } from '../agent/agentInfo.com.service';
import { EmailComTemplate } from '../../_Models';

@Injectable({
  providedIn: 'root',
})
export class EmailDataService {
  private apiUrl: string = environment.apiUrl + 'Email/';
  emailComTemplates: EmailComTemplate[] = [];
  emailComTemplatesChanged = new Subject<EmailComTemplate[]>();
  htmlContent: string = '';
  htmlContentChanged = new Subject<string>();

  constructor(
    private http: HttpClient,
    private agentComService: AgentComService
  ) {}

  fetchEmailComTemplates(): Observable<EmailComTemplate[]> {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: EmailComTemplate[];
        errMessage: string;
      }>(this.apiUrl + 'GetEmailComTemplates')
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.emailComTemplates = response.objData;
            this.emailComTemplatesChanged.next(this.emailComTemplates);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  fetchEmailComTemplateByID(communicationID: number, employmentID: number): Observable<string> {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetEmailTemplate/' + communicationID + '/' + employmentID)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.htmlContent = response.objData;
            this.htmlContentChanged.next(this.htmlContent);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }
}
