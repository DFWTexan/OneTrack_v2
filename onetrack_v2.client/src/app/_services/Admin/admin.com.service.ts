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
  };

  constructor() {}

  changeMode(
    type: 'company' | 'coRequirement' | 'educationRule' | 'dropdownItem' | 'examItem' | 'jobTitle',
    mode: string
  ) {
    this.modes[type].mode = mode;
    this.modes[type].changed.next(mode);
  }
}
