import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Company } from '../../_Models';

@Injectable({
  providedIn: 'root',
})
export class AdminComService {
  modes = {
    company: { mode: '', changed: new Subject<string>() },
    coRequirement: { mode: '', changed: new Subject<string>() },
    educationRule: { mode: '', changed: new Subject<string>() },
    dropdownItem: { mode: '', changed: new Subject<string>() },
    examItem: { mode: '', changed: new Subject<string>() },
    jobTitle: { mode: '', changed: new Subject<string>() },
    companyItem: { mode: '', changed: new Subject<string>() },
    productItem: { mode: '', changed: new Subject<string>() },
    preEduItem: { mode: '', changed: new Subject<string>() },
    preExamItem: { mode: '', changed: new Subject<string>() },
    licenseTech: { mode: '', changed: new Subject<string>() },
    preEducation: { mode: '', changed: new Subject<string>() },
    product: { mode: '', changed: new Subject<string>() },
  };

  constructor() {}

  changeMode(
    type:
      | 'company'
      | 'coRequirement'
      | 'educationRule'
      | 'dropdownItem'
      | 'examItem'
      | 'jobTitle'
      | 'companyItem'
      | 'productItem'
      | 'preEduItem'
      | 'preExamItem'
      | 'licenseTech'
      | 'preEducation'
      | 'product',
    mode: string
  ) {
    this.modes[type].mode = mode;
    this.modes[type].changed.next(mode);
  }
}
