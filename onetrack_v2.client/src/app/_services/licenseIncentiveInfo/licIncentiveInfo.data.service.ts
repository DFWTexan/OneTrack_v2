import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { LicenseAppointment, LicenseIncentiveInfo } from '../../_Models';
import { AgentDataService } from '../agent/agent.data.service';

@Injectable({
  providedIn: 'root',
})
export class LicIncentiveInfoDataService {
  private apiUrl: string = environment.apiUrl + 'LicenseInfo/';
  licenseIncentiveInfo: LicenseIncentiveInfo = {} as LicenseIncentiveInfo;
  licenseIncentiveInfoChanged = new Subject<LicenseIncentiveInfo[]>();

  constructor(
    private http: HttpClient,
    private agentDataService: AgentDataService
  ) {}

  fetchLicIncentiveInfo(
    employeeLicenseId: number
  ): Observable<LicenseIncentiveInfo> {
    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl + 'GetIncentiveInfo/' + employeeLicenseId)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.licenseIncentiveInfo = response.objData;
            this.licenseIncentiveInfoChanged.next([this.licenseIncentiveInfo]);
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  addLicenseAppointment(appointment: LicenseAppointment): Observable<any> {
    this.apiUrl = environment.apiUrl + 'LicenseInfo/AddLicenseAppointment';

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, appointment)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.agentDataService.fetchAgentLicenseAppointments(
              this.agentDataService.agentInformation.employmentID
            );
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((appointments) => {
          // this.agentDataService.agentInformation.agentLicenseAppointments =
          //   appointments;
          // this.agentDataService.agentLicenseAppointmentsChanged.next(
          //   this.agentDataService.agentInformation.agentLicenseAppointments
          // );
          return appointments;
        })
      );
  }

  updateLicenseAppointment(appointment: LicenseAppointment): Observable<any> {
    this.apiUrl = environment.apiUrl + 'LicenseInfo/UpdateLicenseAppointment';

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, appointment)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.agentDataService.fetchAgentLicenseAppointments(
              this.agentDataService.agentInformation.employmentID
            );
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((appointments) => {
          // this.agentDataService.agentInformation.agentLicenseAppointments =
          //   appointments;
          // this.agentDataService.agentLicenseAppointmentsChanged.next(
          //   this.agentDataService.agentInformation.agentLicenseAppointments
          // );

          // this.agentDataService.updateAgentLicenseAppointments(appointments);
          return appointments;
        })
      );
  }

  deleteLicenseAppointment(appointment: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'LicenseInfo/DeleteLicenseAppointment';

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, appointment)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.agentDataService.fetchAgentLicenseAppointments(
              this.agentDataService.agentInformation.employmentID
            );
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((appointments) => {
          // this.agentDataService.agentInformation.agentLicenseAppointments =
          //   appointments;
          // this.agentDataService.agentLicenseAppointmentsChanged.next(
          //   this.agentDataService.agentInformation.agentLicenseAppointments
          // );

          // this.agentDataService.updateAgentLicenseAppointments(appointments);
          return appointments;
        })
      );
  }
}
