import { Component, Injectable, OnInit, OnDestroy, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService, ErrorMessageService, UserAcctInfoDataService } from '../../../_services';

@Component({
  selector: 'app-edit-pre-edu-item',
  templateUrl: './edit-pre-edu-item.component.html',
  styleUrl: './edit-pre-edu-item.component.css'
})
@Injectable()
export class EditPreEduItemComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  preEduItemForm!: FormGroup;
  isFormSubmitted: boolean = false;

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.preEduItemForm = new FormGroup({
      licenseID: new FormControl(0),
      licensePreEducationID: new FormControl(''),
      preEducationID: new FormControl(''),
      educationName: new FormControl(''),
      stateProvinceAbv: new FormControl(''),
      creditHours: new FormControl(''),
      companyID: new FormControl(''),
      companyName: new FormControl(''),
      deliveryMethod: new FormControl(''),
      isActive: new FormControl(''),
    });

    this.subscriptionData =
      this.adminComService.modes.preEduItem.changed.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.adminDataService.preEduItemChanged.subscribe((preEduItem: any) => {
            this.preEduItemForm.patchValue({
              licenseID: preEduItem.licenseID,
              licensePreEducationID: preEduItem.licensePreEducationID,
              preEducationID: preEduItem.preEducationID,
              educationName: preEduItem.educationName,
              stateProvinceAbv: preEduItem.stateProvinceAbv,
              creditHours: preEduItem.creditHours,
              companyID: preEduItem,
              companyName: preEduItem.companyName,
              deliveryMethod: preEduItem.deliveryMethod,
              isActive: preEduItem.isActive,
            });
          });
        } else {
          this.preEduItemForm.reset();
        }
      });
  }

  onCloseModal() {
    const modalDiv = document.getElementById('modal-edit-preEdu-Item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  onSubmit(): void {
    this.isFormSubmitted = true;
    let licEduItem: any = this.preEduItemForm.value;
    licEduItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.adminComService.modes.examItem.mode === 'INSERT') {
      licEduItem.PreEducationID = 0;
    }

    // if (examItem.deliveryMethod === 'Select Method') {
    //   examItem.deliveryMethod = '';
    //   this.companyForm.controls['companyType'].setErrors({ incorrect: true });
    // }

    // if (examItem.deliveryMethod === 'Select Method') {
    //   examItem.deliveryMethod = '';
    // }

    // if (!this.companyForm.valid) {
    //   this.companyForm.setErrors({ invalid: true });
    //   return;
    // }
    this.subscriptionData.add(
      this.adminDataService.updateLicenseEducation(licEduItem).subscribe({
        next: (response) => {
          this.callParentRefreshData.emit();
          this.onCloseModal();
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
            this.onCloseModal();
          }
        },
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
