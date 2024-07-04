import { Component, Injectable, OnInit, OnDestroy, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  DropdownDataService,
} from '../../../_services';
import { EducationRule } from '../../../_Models';

@Component({
  selector: 'app-edit-edu-rule',
  templateUrl: './edit-edu-rule.component.html',
  styleUrl: './edit-edu-rule.component.css',
})
@Injectable()
export class EditEduRuleComponent implements OnInit, OnDestroy {
  @Input() stateProvinces: any[] = [];
  @Input() selectedStateProvince: string | null = 'Select';
  eduRuleForm!: FormGroup;
  licenseTypes: any[] = [];
  conStartDates: any[] = [];
  conEndDates: any[] = [];
  exceptions: any[] = [];
  exemptions: any[] = [];
  selectedExceptionValues: number[] = [];
  selectedExemptionValues: number[] = [];

  subscriptionData: Subscription = new Subscription();

  constructor(
    public adminDataService: AdminDataService,
    private dropdownDataService: DropdownDataService,
    public adminComService: AdminComService
  ) {}

  ngOnInit(): void {
    this.stateProvinces = this.stateProvinces.map((province) => {
      if (province === 'ALL') {
        return 'Select';
      }

      this.subscriptionData.add(
        this.dropdownDataService.conEduStartDateItemsChanged.subscribe(
          (items: any[]) => {
            this.conStartDates = [
              { value: null, label: 'Select CE Start' },
              ...items,
            ];
          }
        )
      );

      this.subscriptionData.add(
        this.dropdownDataService.conEduEndtDateItemsChanged.subscribe(
          (items: any[]) => {
            this.conEndDates = [
              { value: null, label: 'Select CE End' },
              ...items,
            ];
          }
        )
      );

      this.subscriptionData.add(
        this.dropdownDataService.conEduExceptionsChanged.subscribe(
          (items: any[]) => {
            this.exceptions = items;
          }
        )
      );

      this.subscriptionData.add(
        this.dropdownDataService.conEduExemptionsChanged.subscribe(
          (items: any[]) => {
            this.exemptions = items;
          }
        )
      );

      return province;
    });

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

    this.subscriptionData.add(
      this.adminComService.modes.educationRule.changed.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.subscriptionData.add(
              this.adminDataService.educationRuleChanged.subscribe(
                (eduRule: EducationRule) => {
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
                }
              )
            );
          } else {
            this.eduRuleForm.reset();
            this.eduRuleForm.patchValue({
              stateProvince: this.selectedStateProvince
                ? this.selectedStateProvince
                : 'Select',
            });
          }
        }
      )
    );

    this.fetchLicenseTypes();
  }

  fetchLicenseTypes() {
    this.subscriptionData.add(
      this.adminDataService
        .fetchLicenseTypes(
          this.selectedStateProvince == 'ALL'
            ? null
            : this.selectedStateProvince
        )
        .subscribe((response) => {
          this.licenseTypes = response;
        })
    );
  }

  onCheckboxChange(event: any, value: number, type: string) {
    if (event.target.checked) {
      // this.selectedValues.push(value);
      if (type === 'EXCEPTION') {
        this.selectedExceptionValues.push(value);
      } else {
        this.selectedExemptionValues.push(value);
      }
    } else {
      // const index = this.selectedValues.indexOf(value);
      // if (index > -1) {
      //   this.selectedValues.splice(index, 1);
      // }
      if (type === 'EXCEPTION') {
        const index = this.selectedExceptionValues.indexOf(value);
        if (index > -1) {
          this.selectedExceptionValues.splice(index, 1);
        }
      } else {
        const index = this.selectedExemptionValues.indexOf(value);
        if (index > -1) {
          this.selectedExemptionValues.splice(index, 1);
        }
      }
    }
  }

  onSubmit() {}

  forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-edu-rule');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  closeModal() {
    this.forceCloseModal();
    // if (this.employmentHistoryForm.dirty && !this.isFormSubmitted) {
    //   if (
    //     confirm('You have unsaved changes. Are you sure you want to close?')
    //   ) {
    //     const modalDiv = document.getElementById('modal-edit-emp-history');
    //     if (modalDiv != null) {
    //       modalDiv.style.display = 'none';
    //     }
    //     this.employmentHistoryForm.reset();
    //     this.employmentHistoryForm.patchValue({
    //       backgroundCheckStatus: 'Pending',
    //       isCurrent: true,
    //     });
    //   }
    // } else {
    //   this.isFormSubmitted = false;
    //   const modalDiv = document.getElementById('modal-edit-emp-history');
    //   if (modalDiv != null) {
    //     modalDiv.style.display = 'none';
    //   }
    // }
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
