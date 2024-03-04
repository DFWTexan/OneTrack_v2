import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AgentComService {
  isShowLicenseMgmt: boolean = false;
  isShowLicenseMgmtChanged = new Subject<boolean>();

  constructor() {}

  showLicenseMgmt() {
    this.isShowLicenseMgmt = !this.isShowLicenseMgmt;
    this.isShowLicenseMgmtChanged.next(this.isShowLicenseMgmt);
  }
}
