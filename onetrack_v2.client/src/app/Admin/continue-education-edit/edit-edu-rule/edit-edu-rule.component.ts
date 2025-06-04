import {
  Component,
  Injectable,
  OnInit,
  OnDestroy,
  Input,
  EventEmitter,
  Output,
} from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  DropdownDataService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../_services';
import { EducationRule } from '../../../_Models';

@Component({
  selector: 'app-edit-edu-rule',
  templateUrl: './edit-edu-rule.component.html',
  styleUrl: './edit-edu-rule.component.css',
})
@Injectable()
export class EditEduRuleComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  @Input() stateProvinces: any[] = [];
  @Input() selectedStateProvince: string | null = 'Select';
  @Input() conStartDates: any[] = [];
  @Input() conEndDates: any[] = [];
  @Input() exceptions: any[] = [];
  @Input() exemptions: any[] = [];
  eduRuleForm!: FormGroup;
  isFormSubmitted = false;
  isStateProvinceSelected: boolean = false;
  isLicenseTypeSelected: boolean = false;
  licenseTypes: any[] = [];
  licenseTypeSelected: string | null = null;
  licenseTypeItem: string | null = null;

  selectedExceptionValues: number[] = [];
  selectedExemptionValues: number[] = [];

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    // private dropdownDataService: DropdownDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.initializeForm();

    this.subscribeToDropdownDataChanges();

    this.subscribeToModeChanges();

    this.fetchLicenseTypes();
  }

  private initializeForm(): void {
    this.eduRuleForm = new FormGroup({
      ruleNumber: new FormControl(0),
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
  }

  private subscribeToDropdownDataChanges(): void {
    this.stateProvinces = this.stateProvinces.map((province) => {
      if (province === 'ALL') {
        return 'Select';
      }
      return province;
    });
  }

  private subscribeToModeChanges(): void {
    this.subscriptionData.add(
      this.adminComService.modes.educationRule.changed.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.handleEditMode();
          } else {
            this.handleCreateMode();
          }
        }
      )
    );
  }

  private handleEditMode(): void {
    this.isStateProvinceSelected = true;
    this.subscriptionData.add(
      this.adminDataService.educationRuleChanged.subscribe(
        (eduRule: EducationRule) => {
          this.populateFormForEdit(eduRule);
        }
      )
    );
  }

  private handleCreateMode(): void {
    this.eduRuleForm.reset();
    this.eduRuleForm.patchValue({
      stateProvince: this.selectedStateProvince
        ? this.selectedStateProvince
        : 'Select',
      educationStartDateID: null,
      educationEndDateID: null,
    });
  }

  private populateFormForEdit(eduRule: EducationRule): void {
    this.selectedExceptionValues = eduRule.exceptionID
      ? eduRule.exceptionID.split(',').map((id) => Number(id))
      : [];
    this.selectedExemptionValues = eduRule.exemptionID
      ? eduRule.exemptionID.split(',').map((id) => Number(id))
      : [];
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

  onStateSelected(event: any) {
    this.isStateProvinceSelected = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;

    if (value === 'Select') {
      // this.isStateProvinceSelected = false;
    } else {
      // this.isLicenseTypeSelected = true;
    }

    this.licenseTypeItem = null;

    this.selectedStateProvince = value;
    this.fetchLicenseTypes();
  }

  onLicenseSelected(event: any) {
    // this.isLicenseTypeSelected = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.licenseTypeSelected = value;
  }

  onAddLicenseType(addType: string) {
    if (addType === 'SELECTION') {
      if (this.licenseTypeItem == null || this.licenseTypeItem === '') {
        this.licenseTypeItem = this.licenseTypeSelected;
      } else {
        this.licenseTypeItem += `, ${this.licenseTypeSelected}`;
      }
    } else {
      if (this.licenseTypeItem == null || this.licenseTypeItem === '') {
        this.licenseTypeItem = this.licenseTypeSelected;
      } else {
        this.licenseTypeItem += ` + ${this.licenseTypeSelected}`;
      }
    }
    this.eduRuleForm.patchValue({
      licenseType: this.licenseTypeItem,
    });
  }

  onCheckboxChange(event: any, value: number, type: string) {
    if (event.target.checked) {
      if (type === 'EXCEPTION') {
        this.selectedExceptionValues.push(value);
      } else {
        this.selectedExemptionValues.push(value);
      }
    } else {
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

  onSubmit() {
    this.isFormSubmitted = true;

    let eduRuleItem: any = this.eduRuleForm.value;

    eduRuleItem.exceptionID = this.selectedExceptionValues.join(',');
    eduRuleItem.exemptionID = this.selectedExemptionValues.join(',');
    eduRuleItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    // if( eduRuleItem.ruleNumber === '' || eduRuleItem.ruleNumber === null) {
    //   eduRuleItem.ruleNumber = 0;
    // }

    if (
      eduRuleItem.stateProvince === 'Select' ||
      eduRuleItem.stateProvince === ''
    ) {
      this.eduRuleForm.controls['stateProvince'].setErrors({ required: true });
    }

    if (eduRuleItem.licenseType === '' || eduRuleItem.licenseType === null) {
      this.eduRuleForm.controls['licenseType'].setErrors({ required: true });
    }

    if (eduRuleItem.educationStartDateID === null || eduRuleItem.educationStartDateID === 0) {
      this.eduRuleForm.controls['educationStartDateID'].setErrors({
        required: true,
      });
    }

    if (eduRuleItem.educationEndDateID === null || eduRuleItem.educationEndDateID === 0) {
      this.eduRuleForm.controls['educationEndDateID'].setErrors({
        required: true,
      });
    }

    if (this.eduRuleForm.invalid) {
      this.eduRuleForm.setErrors({ invalid: true });
      return;
    }

    this.subscriptionData.add(
      this.adminDataService.upSertEducationRule(eduRuleItem).subscribe({
        next: (response) => {
          this.appComService.updateAppMessage(
              'Data submitted successfully.' // 'Data submitted successfully.'
            );
          this.callParentRefreshData.emit();
          this.forceCloseModal();
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
          }
        },
      })
    );
  }

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
