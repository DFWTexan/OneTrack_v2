import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../_environments/environment';
import { AgentComService } from '../agent/agentInfo.com.service';
import { EmailComTemplate, SendEmailData } from '../../_Models';

@Injectable({
  providedIn: 'root',
})
export class EmailDataService {
  private apiUrl: string = environment.apiUrl + 'Email/';
  emailComTemplates: EmailComTemplate[] = [];
  emailComTemplatesChanged = new Subject<EmailComTemplate[]>();
  htmlContent: string = '';
  htmlContentChanged = new Subject<string>();
  attachedFiles: File[] = [];
  attachedFilesChanged = new Subject<File[]>();

  constructor(
    private http: HttpClient,
    private agentComService: AgentComService
  ) {}

  setAttachedFiles(files: File[]): void {
    this.attachedFiles = files;
    this.attachedFilesChanged.next(this.attachedFiles);
  }
  getAttachedFiles(): File[] {
    return this.attachedFiles;
  }

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

  fetchEmailComTemplateByID(
    communicationID: number,
    employmentID: number
  ): Observable<string> {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(
        this.apiUrl + 'GetEmailTemplate/' + communicationID + '/' + employmentID
      )
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

  sendEmail(sendEmailData: any): Observable<SendEmailData> {
    const formData = new FormData();
  
    // Append form fields
    formData.append('communicationID', sendEmailData.communicationID);
    formData.append('emailSubject', sendEmailData.emailSubject);
    formData.append('emailBody', sendEmailData.emailBody || '');
    formData.append('emailAttachments', sendEmailData.emailAttachments || '');
    formData.append('employeeID', sendEmailData.employeeID.toString());
    formData.append('EmploymentID', sendEmailData.EmploymentID.toString());
    formData.append('CommunicationID', sendEmailData.CommunicationID.toString());
    formData.append('EmailTo', sendEmailData.EmailTo);
    // formData.append('CcEmail', JSON.stringify(sendEmailData.CcEmail || []));
    formData.append('CcEmail', (sendEmailData.CcEmail || []).join(','));
    formData.append('EmailContent', sendEmailData.EmailContent);
    formData.append('UserSOEID', sendEmailData.UserSOEID);
  
    // Append file attachments if any
    if (sendEmailData.fileAttachments && sendEmailData.fileAttachments.length > 0) {
      sendEmailData.fileAttachments.forEach((file: File, index: number) => {
          formData.append('FileAttachments', file, file.name); // Key must match the property name in .NET
      });
  }
  
    // Send the FormData to the endpoint
    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: SendEmailData;
        errMessage: string;
      }>(this.apiUrl + 'Send', formData)
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
