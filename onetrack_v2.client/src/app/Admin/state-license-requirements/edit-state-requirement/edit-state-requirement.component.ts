import {
  Component,
  Injectable,
  OnInit,
  OnDestroy,
  EventEmitter,
  Output,
  Input,
} from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  DropdownDataService,
  ErrorMessageService,
  MiscDataService,
  UserAcctInfoDataService,
} from '../../../_services';

@Component({
  selector: 'app-edit-state-requirement',
  templateUrl: './edit-state-requirement.component.html',
  styleUrl: './edit-state-requirement.component.css',
})
export class EditStateRequirementComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  @Input() stateProvinces: any[] = [];
  isFormSubmitted = false;
  stateRequirementForm!: FormGroup;
  selectedLicenseState: string = 'Select';
  stateLicenseItems: { value: number; label: string }[] = [];
  isLicenseNameDisabled = true;
  isStartDocUploaded = false;
  isRenewalDocUploaded = false;
  FileDisplayMode = 'CHOOSEFILE'; //--> CHOSEFILE / ATTACHMENT
  file: File | null = null;
  fileUri: string | null = null;
  document: string = '';
  uploadStartNewType: string = 'StateNewStartPDF';
  uploadRenewalType: string = 'SateRenewalPDF';

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public dropDownDataService: DropdownDataService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.stateRequirementForm = new FormGroup({
      requiredLicenseId: new FormControl(''),
      workStateAbv: new FormControl('Select'),
      resStateAbv: new FormControl({ value: 'Select', disabled: false }),
      licenseId: new FormControl({ value: 0, disabled: true }),
      branchCode: new FormControl(''),
      requirementType: new FormControl(''),
      licLevel1: new FormControl(''),
      licLevel2: new FormControl(''),
      licLevel3: new FormControl(''),
      licLevel4: new FormControl(''),
      plS_Incentive1: new FormControl(''),
      incentive2_Plus: new FormControl(''),
      licIncentive3: new FormControl(''),
      licState: new FormControl('Select'),
      licenseName: new FormControl({ value: 'Select', disabled: true }),
      startDocument: new FormControl(''),
      renewalDocument: new FormControl(''),
    });

    this.subscriptionData.add(
      this.adminComService.modes.stateRequirement.changed.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.stateProvinces = this.stateProvinces.filter(
              (item) => item !== 'Select'
            );
            this.subscriptionData.add(
              this.adminDataService.stateRequirementChanged.subscribe(
                (stateRequirement: any) => {
                  this.isStartDocUploaded = stateRequirement.startDocument
                    ? true
                    : false;
                  this.isRenewalDocUploaded = stateRequirement.renewalDocument
                    ? true
                    : false;
                  if (stateRequirement.licenseId !== 0) {
                    this.selectedLicenseState = stateRequirement.licState;
                    this.fetchStateLicenseItems();
                  }
                  this.stateRequirementForm.patchValue({
                    requiredLicenseId: stateRequirement.requiredLicenseId,
                    workStateAbv: stateRequirement.workStateAbv,
                    resStateAbv: stateRequirement.resStateAbv,
                    licenseId: stateRequirement.licenseId,
                    branchCode: stateRequirement.branchCode,
                    requirementType: stateRequirement.requirementType,
                    licLevel1: stateRequirement.licLevel1,
                    licLevel2: stateRequirement.licLevel2,
                    licLevel3: stateRequirement.licLevel3,
                    licLevel4: stateRequirement.licLevel4,
                    plS_Incentive1: stateRequirement.plS_Incentive1,
                    incentive2_Plus: stateRequirement.incentive2_Plus,
                    licIncentive3: stateRequirement.licIncentive3,
                    licState: stateRequirement.licState,
                    licenseName: stateRequirement.licenseName,
                    startDocument: stateRequirement.startDocument,
                    renewalDocument: stateRequirement.renewalDocument,
                  });
                }
              )
            );
          } else {
            if (!this.stateProvinces.includes('Select')) {
              this.stateProvinces.unshift('Select');
            }
            this.stateRequirementForm.get('licenseId')?.disable();
            this.stateRequirementForm.reset({
              workStateAbv: 'Select',
              resStateAbv: 'Select',
              licState: { value: 'Select', disabled: false },
              licenseId: { value: 0, disabled: true },
            });
          }
        }
      )
    );
  }

  onFileSelected(event: any) {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length) {
      const file: File = target.files[0];

      if (file) {
        this.document = file.name;
        const formData = new FormData();
        formData.append('thumbnail', file);
        // const upload$ = this.emailDataService.uploadFile(formData);
        // upload$.subscribe();
      }
    }
  }

  onSubmit(): void {
    this.isFormSubmitted = true;
    let stateReqItem: any = this.stateRequirementForm.value;
    stateReqItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.adminComService.modes.stateRequirement.mode === 'INSERT') {
      stateReqItem.requiredLicenseID = 0;
    }

    // if (company.companyType === 'Select Company Type') {
    //   company.companyType = '';
    //   this.companyForm.controls['companyType'].setErrors({ incorrect: true });
    // }

    if (!this.stateRequirementForm.valid) {
      this.stateRequirementForm.setErrors({ invalid: true });
      return;
    }

    this.subscriptionData.add(
      this.adminDataService.upSertStateRequirement(stateReqItem).subscribe({
        next: (response) => {
          this.callParentRefreshData.emit();
          this.forceCloseModal();
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
            this.forceCloseModal();
          }
        },
      })
    );
  }

  onChangeLicenseState(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedLicenseState = value;

    if (value === 'Select') {
      this.stateRequirementForm.get('licenseId')?.disable();
      return;
    } else {
      this.stateRequirementForm.get('licenseId')?.enable();
      this.fetchStateLicenseItems();
    }
  }

  private fetchStateLicenseItems(): void {
    this.dropDownDataService
      .fetchDropdownNumericData(
        'GetLicenseNumericNames',
        this.selectedLicenseState
      )
      .subscribe({
        next: (response) => {
          this.stateLicenseItems = [{ value: 0, label: 'Select' }, ...response];
          this.stateRequirementForm.get('licenseId')?.enable();
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
            this.forceCloseModal();
          }
        },
      });
  }

  private forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-state-requirement');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  closeModal() {
    this.forceCloseModal();
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
