import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AgentComService {
  modeLicenseMgmt: string = '';
  modeLicenseMgmtChanged = new Subject<string>();
  isShowLicenseMgmt: boolean = false;
  isShowLicenseMgmtChanged = new Subject<boolean>();
  // Employment Transfer History
  modeEmploymentHist: string = '';
  modeEmploymentHistChanged = new Subject<string>();
  modeTransferHist: string = '';
  modeTransferHistChanged = new Subject<string>();
  modeCompanyRequirementsHist: string = '';
  modeCompanyRequirementsHistChanged = new Subject<string>();
  modeEmploymentJobTitleHist: string = '';
  modeEmploymentJobTitleHistChanged = new Subject<string>();
  // License Application Information
  modeLicenseInfo: string = '';
  modeLicenseInfoChanged = new Subject<string>();
  modeLicAppInfo: string = '';
  modeLicAppInfoChanged = new Subject<string>();
  modeLicPreEdu: string = '';
  modeLicPreEduChanged = new Subject<string>();
  modeLicPreExam: string = '';
  modeLicPreExamChanged = new Subject<string>();
  modeLicRenewal: string = '';
  modeLicRenewalChanged = new Subject<string>();
  // Employment Continuing Education
  modeContEduHoursTaken: string = '';
  modeContEduHoursTakenChanged = new Subject<string>();
  // TM Diary
  modeDiary: string = '';
  modeDiaryChanged = new Subject<string>();
  // License Appointment
  modeLicenseAppt: string = '';
  modeLicenseApptChanged = new Subject<string>();

  constructor() {}

  modeLicenseMgmtModal(mode: string) {
    this.modeLicenseMgmt = mode;
    this.modeLicenseMgmtChanged.next(this.modeLicenseMgmt);
  }

  showLicenseMgmt() {
    this.isShowLicenseMgmt = !this.isShowLicenseMgmt;
    this.isShowLicenseMgmtChanged.next(this.isShowLicenseMgmt);
  }

  updateShowLicenseMgmt(show: boolean): void {
    this.isShowLicenseMgmt = show;
    this.isShowLicenseMgmtChanged.next(this.isShowLicenseMgmt);
  }

  // Employment Transfer History
  modeEmploymentHistModal(mode: string) {
    this.modeEmploymentHist = mode;
    this.modeEmploymentHistChanged.next(this.modeEmploymentHist);
  }

  modeTransferHistModal(mode: string) {
    this.modeTransferHist = mode;
    this.modeTransferHistChanged.next(this.modeTransferHist);
  }

  modeCompanyRequirementsHistModal(mode: string) {
    this.modeCompanyRequirementsHist = mode;
    this.modeCompanyRequirementsHistChanged.next(this.modeCompanyRequirementsHist);
  }

  modeEmploymentJobTitleHistModal(mode: string) {
    this.modeEmploymentJobTitleHist = mode;
    this.modeEmploymentJobTitleHistChanged.next(this.modeEmploymentJobTitleHist);
  }

  // Employment Application Information
  modeLicenseInfoModal(mode: string) {
    this.modeLicenseInfo = mode;
    this.modeLicenseInfoChanged.next(this.modeLicenseInfo);
  }
  
  modeLicAppInfoModal(mode: string) {
    this.modeLicAppInfo = mode;
    this.modeLicAppInfoChanged.next(this.modeLicAppInfo);
  }

  modeLicPreEduModal(mode: string) {
    this.modeLicPreEdu = mode;
    this.modeLicPreEduChanged.next(this.modeLicPreEdu);
  }

  modeLicPreExamModal(mode: string) {
    this.modeLicPreExam = mode;
    this.modeLicPreExamChanged.next(this.modeLicPreExam);
  }

  modeLicRenewalModal(mode: string) {
    this.modeLicRenewal = mode;
    this.modeLicRenewalChanged.next(this.modeLicRenewal);
  }

  // Continuing Education Tanken Hours
  modeContEduHoursTakenModal(mode: string) {
    this.modeContEduHoursTaken = mode;
    this.modeContEduHoursTakenChanged.next(this.modeContEduHoursTaken);
  }

  // TM Diary
  modeDiaryEntryModal(mode: string) {
    this.modeDiary = mode;
    this.modeDiaryChanged.next(this.modeDiary);
  }

  // LICENSE APPOINTMENT
  modeLicenseApptModal(mode: string) {
    this.modeLicenseAppt = mode;
    this.modeLicenseApptChanged.next(this.modeLicenseAppt);
  }
}
