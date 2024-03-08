import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AgentComService {
  isShowLicenseMgmt: boolean = false;
  isShowLicenseMgmtChanged = new Subject<boolean>();
  modeEmploymentHist: string = '';
  modeEmploymentHistChanged = new Subject<string>();
  modeTransferHist: string = '';
  modeTransferHistChanged = new Subject<string>();
  modeCompanyRequirementsHist: string = '';
  modeCompanyRequirementsHistChanged = new Subject<string>();
  modeEmploymentJobTitleHist: string = '';
  modeEmploymentJobTitleHistChanged = new Subject<string>();

  constructor() {}

  showLicenseMgmt() {
    this.isShowLicenseMgmt = !this.isShowLicenseMgmt;
    this.isShowLicenseMgmtChanged.next(this.isShowLicenseMgmt);
  }

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
}
