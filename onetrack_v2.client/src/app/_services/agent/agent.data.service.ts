import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
// import { Subject } from 'rxjs';

import { environment } from '../../environments/environment';
import {
  AgentInfo,
  AgentLicApplicationInfo,
  AgentLicenseAppointments,
  CompanyRequirementsHistory,
  EmploymentHistory,
  EmploymentJobTitleHistory,
  LicenseApplicationItem,
  LicenseAppointment,
  TransferHistory,
} from '../../_Models';
import { AgentComService } from './agentInfo.com.service';

@Injectable({
  providedIn: 'root',
})
export class AgentDataService {
  private apiUrl: string = environment.apiUrl;
  agentInformation: AgentInfo = {} as AgentInfo;
  agentInfoChanged = new Subject<AgentInfo>();
  // AGENT LICENSE APPOINTMENTS
  agentLicenseAppointments: AgentLicenseAppointments[];
  agentLicenseAppointmentsChanged = new Subject<AgentLicenseAppointments[]>();
  licenseAppointment: LicenseAppointment = {} as LicenseAppointment;
  licenseAppointmentChanged = new Subject<LicenseAppointment>();
  licenseMgmtData: AgentLicenseAppointments = {} as AgentLicenseAppointments;
  licenseMgmtDataChanged = new Subject<AgentLicenseAppointments>();
  licenseMgmtDataIndex: any = 0;
  licenseMgmtDataIndexChanged = new Subject<number>();
  // EMP TRANSFER HISTORY
  employmentTransferHistItem: any = {};
  employmentTransferHistItemChanged = new Subject<any>();
  transferHistItem: any = {};
  transferHistItemChanged = new Subject<any>();
  companyRequirementsHistItem: any = {};
  companyRequirementsHistItemChanged = new Subject<any>();
  employmentJobTitleHistItem: any = {};
  employmentJobTitleHistItemChanged = new Subject<any>();
  // AGENT LICENSE APPLICATION INFO
  agentLicApplicationInfo: AgentLicApplicationInfo =
    {} as AgentLicApplicationInfo;
  agentLicApplicationInfoChanged = new Subject<AgentLicApplicationInfo>();
  licenseApplicationItem: any = {};
  licenseApplicationItemChanged = new Subject<any>();

  constructor(
    private http: HttpClient,
    private agentComService: AgentComService
  ) {
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

  fetchAgentLicApplicationInfo(employeeLicenseID: number) {
    this.apiUrl = environment.apiUrl + 'Agent/GetLicenseApplcationInfo/';

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: AgentLicApplicationInfo;
        errMessage: string;
      }>(this.apiUrl + employeeLicenseID)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.agentLicApplicationInfo = response.objData;
            this.agentLicApplicationInfoChanged.next(
              this.agentLicApplicationInfo
            );
            return response.objData;
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        })
      );
  }

  // LICENSE APPOINTMENT MANAGEMENT
  storeLicenseAppointment(appointment: LicenseAppointment) {
    this.licenseAppointment = appointment;
    this.licenseAppointmentChanged.next(this.licenseAppointment);
  }

  storeLicenseMgmtDataIndex(index: number) {
    this.licenseMgmtDataIndex = index;
    this.licenseMgmtDataIndexChanged.next(this.licenseMgmtDataIndex);
  }

  // TM EMPLOYMENT TRANSFER HISTORY
  storeEmploymentTransferHistory(
    mode: string | '',
    employmentTransferHistory: EmploymentHistory | null
  ) {
    this.agentComService.modeEmploymentHistModal(mode);
    this.employmentTransferHistItem = employmentTransferHistory || {};
    this.employmentTransferHistItemChanged.next(
      this.employmentTransferHistItem
    );
  }

  storeTransferHistory(
    mode: string | '',
    transferHistory: TransferHistory | null
  ) {
    this.agentComService.modeTransferHistModal(mode);
    this.transferHistItem = transferHistory || {};
    this.transferHistItemChanged.next(this.transferHistItem);
  }

  storeCompanyRequirementsHistory(
    mode: string | '',
    companyRequirementsHistory: CompanyRequirementsHistory | null
  ) {
    this.agentComService.modeCompanyRequirementsHistModal(mode);
    this.companyRequirementsHistItem = companyRequirementsHistory || {};
    this.companyRequirementsHistItemChanged.next(
      this.companyRequirementsHistItem
    );
  }

  storeEmploymentJobTitleHistory(
    mode: string | '',
    employmentJobTitleHistory: EmploymentJobTitleHistory | null
  ) {
    this.agentComService.modeEmploymentJobTitleHistModal(mode);
    this.employmentJobTitleHistItem = employmentJobTitleHistory || {};
    this.employmentJobTitleHistItemChanged.next(
      this.employmentJobTitleHistItem
    );
  }

  // TM LICENSE APPLICATION INFORMATION
  storeLicAppInfo(
    mode: string | '',
    licenseApplicationItem: LicenseApplicationItem | null
  ) {
    this.agentComService.modeLicAppInfoModal(mode);
    this.licenseApplicationItem = licenseApplicationItem || {};
    this.licenseApplicationItemChanged.next(this.licenseApplicationItem);
  }
}
