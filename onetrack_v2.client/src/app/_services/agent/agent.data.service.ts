import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map, switchMap, tap } from 'rxjs/operators';
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
import { ErrorMessageService } from '../error/error.message.service';

@Injectable({
  providedIn: 'root',
})
export class AgentDataService {
  private apiUrl: string = environment.apiUrl;
  agentInformation: AgentInfo = {} as AgentInfo;
  agentInfoChanged = new Subject<AgentInfo>();
  // AGENT LICENSE APPOINTMENTS
  agentLicApptLicenseID: number = 0;
  agentLicApptLicenseIDChanged = new Subject<number>();
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
    private agentComService: AgentComService,
    public errorMessageService: ErrorMessageService
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
            this.diaryItems = this.agentInformation.diaryItems;
            this.diaryItemsChanged.next(this.diaryItems);
            this.agentLicenseAppointmentsChanged.next(
              this.agentInformation.agentLicenseAppointments
            );
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

  fetchLicenseLevels() {
    this.apiUrl = environment.apiUrl + 'Agent/GetLicLevels';

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: Array<{ licenseLevel: string; sortOrder: number }>;
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

  fetchLicenseIncentives() {
    this.apiUrl = environment.apiUrl + 'Agent/GetLicIncentives';

    return this.http
      .get<{
        success: boolean;
        statusCode: number;
        objData: Array<{ licenseIncentive: string; sortOrder: number }>;
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

  // EDITS TO AGENT INFORMATION
  upsertAgent(agent: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/UpsertAgent';
    agent.DateOfBirth = new Date(agent.DateOfBirth);
    agent.HireDate = new Date(agent.HireDate);

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

  updateAgent(agent: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/UpdateAgentDetails';

    return this.http
      .put<{
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

  deleteAgent(agent: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/DeleteAgentEmployee';

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

  upsertAgentLicense(licenseInfo: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/UpsertAgentLicense';

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, licenseInfo)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.fetchAgentInformation(this.agentInformation.employeeID);
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((agentInfo) => {
          this.agentInformation = agentInfo;
          this.agentInfoChanged.next(this.agentInformation);
          this.agentLicenseAppointmentsChanged.next(
            this.agentInformation.agentLicenseAppointments
          );
          return this.agentInformation;
        })
      );
  }

  deleteAgentLicense(licenseInfo: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/DeleteAgentLicense';

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, licenseInfo)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.fetchAgentInformation(this.agentInformation.employeeID);
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((agentInfo) => {
          this.agentInformation = agentInfo;
          this.agentInfoChanged.next(this.agentInformation);
          return this.agentInformation;
        })
      );
  }

  // AGENT - EDITS for EMPLOYMENT HISTORY
  upsertEmploymentHistItem(employmentHistItem: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/UpsertEmploymentHistItem';

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, employmentHistItem)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.fetchAgentInformation(this.agentInformation.employeeID);
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((agentInfo) => {
          this.agentInformation = agentInfo;
          this.agentInfoChanged.next(this.agentInformation);
          return this.agentInformation;
        })
      );
  }

  deleteEmploymentHistItem(employmentHistItem: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/DeleteEmploymentHistItem';

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, employmentHistItem)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.fetchAgentInformation(this.agentInformation.employeeID);
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((agentInfo) => {
          this.agentInformation = agentInfo;
          this.agentInfoChanged.next(this.agentInformation);
          return this.agentInformation;
        })
      );
  }

  // refreshAgentInfo(agentInfo: AgentInfo) {
  //   this.agentInfoChanged.next(agentInfo);
  // }

  // EDITS for AGENT TRANSFER HISTORY
  // addTransferHistItem(transferHistItem: any): Observable<any> {
  //   this.apiUrl = environment.apiUrl + 'Agent/InsertTransferHistItem';

  //   return this.http
  //     .post<{
  //       success: boolean;
  //       statusCode: number;
  //       objData: any;
  //       errMessage: string;
  //     }>(this.apiUrl, transferHistItem)
  //     .pipe(
  //       switchMap((response) => {
  //         if (response.success && response.statusCode === 200) {
  //           return this.fetchAgentInformation(this.agentInformation.employeeID);
  //         } else {
  //           throw new Error(response.errMessage || 'Unknown error');
  //         }
  //       }),
  //       map((agentInfo) => {
  //         this.agentInformation = agentInfo;
  //         this.agentInfoChanged.next(this.agentInformation);
  //         return this.agentInformation;
  //       })
  //     );
  // }

  upsertTransferHistItem(transferHistItem: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/UpsertTranserHistItem';

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, transferHistItem)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.fetchAgentInformation(this.agentInformation.employeeID);
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((agentInfo) => {
          this.agentInformation = agentInfo;
          this.agentInfoChanged.next(this.agentInformation);
          return this.agentInformation;
        })
      );
  }

  deleteTransferHistItem(transferHistItem: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/DeleteTransferHistItem';

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, transferHistItem)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.fetchAgentInformation(this.agentInformation.employeeID);
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((agentInfo) => {
          this.agentInformation = agentInfo;
          this.agentInfoChanged.next(this.agentInformation);
          return this.agentInformation;
        })
      );
  }

  upsertCompanyRequirementsHistItem(
    companyRequirementsHistItem: any
  ): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/UpsertCoRequirementItem';

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, companyRequirementsHistItem)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.fetchAgentInformation(this.agentInformation.employeeID);
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((agentInfo) => {
          this.agentInformation = agentInfo;
          this.agentInfoChanged.next(this.agentInformation);
          return this.agentInformation;
        })
      );
  }

  deleteCompanyRequirementsHistItem(
    companyRequirementsItem: any
  ): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/DeleteCoRequirementItem';

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, companyRequirementsItem)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.fetchAgentInformation(this.agentInformation.employeeID);
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((agentInfo) => {
          this.agentInformation = agentInfo;
          this.agentInfoChanged.next(this.agentInformation);
          return this.agentInformation;
        })
      );
  }

  upsertEmploymentJobTitleHistItem(
    employmentJobTitleHistItem: any
  ): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/UpsertEmploymentJobTitleItem';

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, employmentJobTitleHistItem)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.fetchAgentInformation(this.agentInformation.employeeID);
          } else {
            this.errorMessageService.setErrorMessage(response.errMessage);
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((agentInfo) => {
          this.agentInformation = agentInfo;
          this.agentInfoChanged.next(this.agentInformation);
          return this.agentInformation;
        })
      );
  }

  deleteEmploymentJobTitleHistItem(
    employmentJobTitleHistItem: any
  ): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/DeleteEmploymentJobTitleItem';

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, employmentJobTitleHistItem)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.fetchAgentInformation(this.agentInformation.employeeID);
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((agentInfo) => {
          this.agentInformation = agentInfo;
          this.agentInfoChanged.next(this.agentInformation);
          return this.agentInformation;
        })
      );
  }

  upsertContEduHoursTaken(contEduHoursTaken: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/UpsertConEduTaken';

    console.log(
      'EMFTEST (upsertContEduHoursTaken) - contEduHoursTaken => \n',
      contEduHoursTaken
    );

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, contEduHoursTaken)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.fetchAgentInformation(this.agentInformation.employeeID);
          } else {
            this.errorMessageService.setErrorMessage(response.errMessage);
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((agentInfo) => {
          this.agentInformation = agentInfo;
          this.agentInfoChanged.next(this.agentInformation);
          return this.agentInformation;
        })
      );
  }

  deleteContEduHoursTaken(contEduHoursTaken: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/DeleteConEduTaken';

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, contEduHoursTaken)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.fetchAgentInformation(this.agentInformation.employeeID);
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((agentInfo) => {
          this.agentInformation = agentInfo;
          this.agentInfoChanged.next(this.agentInformation);
          return this.agentInformation;
        })
      );
  }

  upsertDiaryEntry(diaryEntry: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/UpsertDiaryItem';

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, diaryEntry)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.fetchAgentInformation(this.agentInformation.employeeID);
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((agentInfo) => {
          this.agentInformation = agentInfo;
          this.agentInfoChanged.next(this.agentInformation);
          return this.agentInformation;
        })
      );
  }

  deleteDiaryEntry(diaryEntry: any): Observable<any> {
    this.apiUrl = environment.apiUrl + 'Agent/DeleteDiaryItem';

    return this.http
      .post<{
        success: boolean;
        statusCode: number;
        objData: any;
        errMessage: string;
      }>(this.apiUrl, diaryEntry)
      .pipe(
        switchMap((response) => {
          if (response.success && response.statusCode === 200) {
            return this.fetchAgentInformation(this.agentInformation.employeeID);
          } else {
            throw new Error(response.errMessage || 'Unknown error');
          }
        }),
        map((agentInfo) => {
          this.agentInformation = agentInfo;
          this.agentInfoChanged.next(this.agentInformation);
          return this.agentInformation;
        })
      );
  }

  // LICENSE APPOINTMENT MANAGEMENT
  storeLicenseAppointment(appointment: LicenseAppointment) {
    this.licenseAppointment = appointment;
    this.licenseAppointmentChanged.next(this.licenseAppointment);
  }

  storeLicApptLicenseID(licenseID: number) {
    this.agentLicApptLicenseID = licenseID;
    this.agentLicApptLicenseIDChanged.next(this.agentLicApptLicenseID);
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

  // SUBJECT UPDATES
  updateAgentLicenseAppointments(appointments: AgentLicenseAppointments[]) {
    this.agentLicenseAppointments = appointments;
    this.agentLicenseAppointmentsChanged.next(this.agentLicenseAppointments);
  }
}
