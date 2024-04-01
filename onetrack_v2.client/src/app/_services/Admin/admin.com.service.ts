import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Company } from '../../_Models';

@Injectable({
  providedIn: 'root',
})
export class AdminComService {
//Company
  modeCompany: string = '';
  modeCompanyChanged = new Subject<string>();
  //CompanyRequirement
  modeCoRequirement: string = '';
  modeCoRequirementChanged = new Subject<string>();
  //EducationRule
  modeEducationRule: string = '';
  modeEducationRuleChanged = new Subject<string>();

  constructor() {}

  modeCompanyModal(mode: string) {
    this.modeCompany = mode;
    this.modeCompanyChanged.next(this.modeCompany);
  }
  
  modeCoRequirementModal(mode: string) {
    this.modeCoRequirement = mode;
    this.modeCoRequirementChanged.next(this.modeCoRequirement);
  }

  modeEducationRuleModal(mode: string) {
    this.modeEducationRule = mode;
    this.modeEducationRuleChanged.next(this.modeEducationRule);
  }
}
