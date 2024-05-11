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
  LicensePreEducationItem,
  LicensePreExamItem,
  LicenseRenewalItem,
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
  branchCodes: Array<{ branchCode: string }> = [];
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
  licensePreEducationItem: any = {};
  licensePreEducationItemChanged = new Subject<any>();
  licensePreExamItem: any = {};
  licensePreExamItemChanged = new Subject<any>();
  licenseRenewalItem: any = {};
  licenseRenewalItemChanged = new Subject<any>();
  // CONTINUING EDUCATION
  contEduHoursTaken: any = {};
  contEduHoursTakenChanged = new Subject<any>();
  diaryItems: any = {};
  diaryItemsChanged = new Subject<any>();
  // TM DIARY
  diaryEntry: any = {};
  diaryEntryChanged = new Subject<any>();

  constructor(
    private http: HttpClient,
    private agentComService: AgentComService
  ) {
    this.agentLicenseAppointments = [];
  }

  addAgent(agent: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/InsertAgent';

    console.log('EMFTEST (addAgent) - agent => \n', agent);

    return this.http
    .post<{
      success: boolean;
      statusCode: number;
      objData: any;
      errMessage: string;
    }>(this.apiUrl, agent)
    .pipe(
      map((response) => {
        return response;
      })
    );
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
            this.diaryItems = this.agentInformation.diaryItems;
            this.diaryItemsChanged.next(this.diaryItems);
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

  fetchBranchCodes() {
    this.apiUrl = environment.apiUrl + 'Agent/GetBranchCodes';

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: Array<{ branchCode: string }>;
        errMessage: string;
      }>(this.apiUrl)
      .pipe(
        map((response) => {
          if (response.success && response.statusCode === 200) {
            this.branchCodes = response.objData;
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

  fetchCoReqAssetIDs() {
    this.apiUrl = environment.apiUrl + 'Agent/GetCoRequirementAssetIDs';

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: Array<{ lkpValue: string }>;
        errMessage: string;
      }>(this.apiUrl)
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

  fetchCoReqStatuses() {
    this.apiUrl = environment.apiUrl + 'Agent/GetCoRequirementStatuses';

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: Array<{ lkpValue: string }>;
        errMessage: string;
      }>(this.apiUrl)
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

  storeLicPreEdu(
    mode: string | '',
    licensePreEducationItem: LicensePreEducationItem | null
  ) {
    this.agentComService.modeLicPreEduModal(mode);
    this.licensePreEducationItem = licensePreEducationItem || {};
    this.licensePreEducationItemChanged.next(this.licensePreEducationItem);
  }

  storeLicPreExam(
    mode: string | '',
    licensePreExamItem: LicensePreExamItem | null
  ) {
    this.agentComService.modeLicPreExamModal(mode);
    this.licensePreExamItem = licensePreExamItem || {};
    this.licensePreExamItemChanged.next(this.licensePreExamItem);
  }

  storeLicRenewal(
    mode: string | '',
    licenseRenewalItem: LicenseRenewalItem | null
  ) {
    this.agentComService.modeLicRenewalModal(mode);
    this.licenseRenewalItem = licenseRenewalItem || {};
    this.licenseRenewalItemChanged.next(this.licenseRenewalItem);
  }

  // CONTINUING EDUCATION
  storeContEduHoursTaken(mode: string | '', contEduHoursTaken: any | null) {
    this.agentComService.modeContEduHoursTakenModal(mode);
    this.contEduHoursTaken = contEduHoursTaken || {};
    this.contEduHoursTakenChanged.next(this.contEduHoursTaken);
  }

  // TM DIARY
  filterBySOEID(event: Event): void {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    if (value === '{All}') {
      this.diaryItems = this.agentInformation.diaryItems;
      this.diaryItemsChanged.next(this.diaryItems);
      return;
    }
    this.diaryItems = this.agentInformation.diaryItems.filter((item) => {
      if (item && item.soeid !== null) {
        return item.soeid.includes(value);
      } else {
        return false;
      }
    });

    this.diaryItemsChanged.next(this.diaryItems);
  }

  storeDiaryEntry(mode: string | '', diaryEntry: any | null) {
    this.agentComService.modeDiaryEntryModal(mode);
    this.diaryEntry = diaryEntry || {};
    this.diaryEntryChanged.next(this.diaryEntry);
  }
}
