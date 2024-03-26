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

  constructor() {}

  modeCompanyModal(mode: string) {
    this.modeCompany = mode;
    this.modeCompanyChanged.next(this.modeCompany);
  }
  
}
