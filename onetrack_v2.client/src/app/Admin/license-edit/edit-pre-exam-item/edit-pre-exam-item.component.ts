import { Component, Injectable, OnInit, OnDestroy, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService, AppComService, ErrorMessageService, UserAcctInfoDataService } from '../../../_services';

@Component({
  selector: 'app-edit-pre-exam-item',
  templateUrl: './edit-pre-exam-item.component.html',
  styleUrl: './edit-pre-exam-item.component.css',
})
@Injectable()
export class EditPreExamItemComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  preExamItemForm!: FormGroup;
  isFormSubmitted: boolean = false;
  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.preExamItemForm = new FormGroup({
      licenseID: new FormControl(0),
      examId: new FormControl(''),
      examName: new FormControl(''),
      stateProvinceAbv: new FormControl(''),
      companyName: new FormControl(''),
      deliveryMethod: new FormControl(''),
      licenseExamID: new FormControl(''),
      examProviderID: new FormControl(''),
      isActive: new FormControl(''),
    });

    this.subscriptionData =
      this.adminComService.modes.preExamItem.changed.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.adminDataService.preExamItemChanged.subscribe(
              (preExamItem: any) => {
                this.preExamItemForm.patchValue({
                  licenseID: preExamItem.licenseID,
                  examId: preExamItem.examId,
                  examName: preExamItem.examName,
                  stateProvinceAbv: preExamItem.stateProvinceAbv,
                  companyName: preExamItem.companyName,
                  deliveryMethod: preExamItem.deliveryMethod,
                  licenseExamID: preExamItem.licenseExamID,
                  examProviderID: preExamItem.examProviderID,
                  isActive: preExamItem.isActive,
                });
              }
            );
          } else {
            this.preExamItemForm.reset();
          }
        }
      );
  }

  onCloseModal() {
    const modalDiv = document.getElementById('modal-edit-preExam-item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  onSubmit(): void {
    this.isFormSubmitted = true;
    let licExamItem: any = this.preExamItemForm.value;
    licExamItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.adminComService.modes.examItem.mode === 'INSERT') {
      licExamItem.ExamID = 0;
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
      this.adminDataService.updateLicenseExam(licExamItem).subscribe({
        next: (response) => {
          this.callParentRefreshData.emit();
          this.appComService.updateAppMessage(
            'Pre Exam Saved successfully'
          );
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
