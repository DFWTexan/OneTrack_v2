import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService } from '../../../_services';
import { EducationRule } from '../../../_Models';

@Component({
  selector: 'app-edit-edu-rule',
  templateUrl: './edit-edu-rule.component.html',
  styleUrl: './edit-edu-rule.component.css'
})
@Injectable()
export class EditEduRuleComponent implements OnInit, OnDestroy {
  eduRuleForm!: FormGroup;
  subscriptionData: Subscription = new Subscription();

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService
  ) {}

  ngOnInit(): void {
    this.eduRuleForm = new FormGroup({
      ruleNumber: new FormControl(''),
      stateProvince: new FormControl(''),
      licenseType: new FormControl(''),
      requiredCreditHours: new FormControl(''),
      educationStartDateID: new FormControl(''),
      educationStartDate: new FormControl(''),
      educationEndDateID: new FormControl(''),
      educationEndDate: new FormControl(''),
      exceptionID: new FormControl(''),
      exemptionID: new FormControl(''),
      isActive: new FormControl(''),
    });

    this.subscriptionData =
      this.adminComService.modes.educationRule.changed.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.adminDataService.educationRuleChanged.subscribe((eduRule: EducationRule) => {
            this.eduRuleForm.patchValue({
              ruleNumber: eduRule.ruleNumber,
              stateProvince: eduRule.stateProvince,
              licenseType: eduRule.licenseType,
              requiredCreditHours: eduRule.requiredCreditHours,
              educationStartDateID: eduRule.educationStartDateID,
              educationStartDate: eduRule.educationStartDate,
              educationEndDateID: eduRule.educationEndDateID,
              educationEndDate: eduRule.educationEndDate,
              exceptionID: eduRule.exceptionID,
              exemptionID: eduRule.exemptionID,
              isActive: eduRule.isActive,
            });
          });
        } else {
          this.eduRuleForm.reset();
        }
      });
  }

  onSubmit() {
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
