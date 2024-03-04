import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
// import { Subject } from 'rxjs';

import { environment } from '../environments/environment';
import {
  AgentInfo,
  AgentLicenseAppointments,
  LicenseAppointment,
} from '../_Models';

@Injectable({
  providedIn: 'root',
})
export class AgentDataService {
  private apiUrl: string = environment.apiUrl;
  agentInformation: AgentInfo = {} as AgentInfo;
  agentInfoChanged = new Subject<AgentInfo>();
  agentLicenseAppointments: AgentLicenseAppointments[];
  agentLicenseAppointmentsChanged = new Subject<AgentLicenseAppointments[]>();
  licenseAppointment: LicenseAppointment = {} as LicenseAppointment;
  licenseAppointmentChanged = new Subject<LicenseAppointment>();

  constructor(private http: HttpClient) {
    this.agentLicenseAppointments = [];
  }

  fetchAgentInformation(employeeID: number): Observable<AgentInfo> {
    this.apiUrl = environment.apiUrl + 'Agent/GetAgentByEmployeeID/';

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: AgentInfo;
        errMessage: string;
      }>(this.apiUrl + employeeID)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.agentInfoChanged.next(response.objData);
            this.agentInformation = response.objData;
            this.fetchAgentLicenseAppointments(response.objData.employmentID);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  fetchAgentLicenseAppointments(employmentID: number) {
    this.apiUrl = environment.apiUrl + 'Agent/GetLicenseAppointments/';

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: AgentLicenseAppointments[];
        errMessage: string;
      }>(this.apiUrl + employmentID)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.agentInformation.agentLicenseAppointments = response.objData;
            this.agentLicenseAppointmentsChanged.next(
              this.agentInformation.agentLicenseAppointments
            );
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  storeLicenseAppointment(appointment: LicenseAppointment) {
    this.licenseAppointment = appointment;
    this.licenseAppointmentChanged.next(appointment);
  }
}
