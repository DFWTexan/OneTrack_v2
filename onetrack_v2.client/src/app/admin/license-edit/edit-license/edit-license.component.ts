import { Component, Injectable, OnInit, OnDestroy, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService, ErrorMessageService, UserAcctInfoDataService } from '../../../_services';

@Component({
  selector: 'app-edit-license',
  templateUrl: './edit-license.component.html',
  styleUrl: './edit-license.component.css',
})
@Injectable()
export class EditLicenseComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  isFormSubmitted = false;
  licenseForm!: FormGroup;

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.licenseForm = new FormGroup({
      licenseId: new FormControl(''),
      licenseName: new FormControl(''),
      licenseAbv: new FormControl(''),
      stateProvinceAbv: new FormControl(''),
      lineOfAuthorityAbv: new FormControl(''),
      lineOfAuthorityId: new FormControl(''),
      agentStateTable: new FormControl(''),
      plsIncentive1Tmpay: new FormControl(''),
      plsIncentive1Mrpay: new FormControl(''),
      incentive2PlusTmpay: new FormControl(''),
      incentive2PlusMrpay: new FormControl(''),
      licIncentive3Tmpay: new FormControl(''),
      licIncentive3Mrpay: new FormControl(''),
      isActive: new FormControl(''),
    });

    this.subscriptionData.add(
      this.adminComService.modes.licenseItem.changed.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.subscriptionData.add(
              this.adminDataService.licenseItemChanged.subscribe(
                (license: any) => {
                  this.licenseForm.patchValue({
                    licenseId: license.licenseId,
                    licenseName: license.licenseName,
                    licenseAbv: license.licenseAbv,
                    stateProvinceAbv: license.stateProvinceAbv,
                    lineOfAuthorityAbv: license.lineOfAuthorityAbv,
                    lineOfAuthorityId: license.lineOfAuthorityId,
                    agentStateTable: license.agentStateTable,
                    plsIncentive1Tmpay: license.plsIncentive1Tmpay,
                    plsIncentive1Mrpay: license.plsIncentive1Mrpay,
                    incentive2PlusMrpay: license.incentive2PlusMrpay,
                    licIncentive3Tmpay: license.licIncentive3Tmpay,
                    licIncentive3Mrpay: license.licIncentive3Mrpay,
                    isActive: license.isActive,
                  });
                }
              )
            );
          } else {
            this.licenseForm.reset();
          }
        }
      )
    );
  }

  onSubmit(): void {
    this.isFormSubmitted = true;
    let licenseItem: any = this.licenseForm.value;
    licenseItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.adminComService.modes.examItem.mode === 'INSERT') {
      licenseItem.licenseID = 0;
    }

    // if (examItem.deliveryMethod === 'Select Method') {
    //   examItem.deliveryMethod = '';
    //   this.companyForm.controls['companyType'].setErrors({ incorrect: true });
    // }

    if (licenseItem.deliveryMethod === 'Select Method') {
      licenseItem.deliveryMethod = '';
    }

    // if (!this.companyForm.valid) {
    //   this.companyForm.setErrors({ invalid: true });
    //   return;
    // }
    this.subscriptionData.add(
      this.adminDataService.upsertLicenseItem(licenseItem).subscribe({
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

  forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-license');
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
