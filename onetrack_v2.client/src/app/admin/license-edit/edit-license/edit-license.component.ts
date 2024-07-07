import { Component, Injectable, OnInit, OnDestroy, EventEmitter, Output, Input } from '@angular/core';
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
  @Input() stateProvinces: any[] = [];
  @Input() lineOfAuthorities: { value: number; label: string }[] = [];
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
      stateProvinceAbv: new FormControl('Select'),
      lineOfAuthorityAbv: new FormControl(0),
      lineOfAuthorityId: new FormControl(''),
      agentStateTable: new FormControl(''),
      plS_Incentive1TMPay: new FormControl(0),
      plS_Incentive1MRPay: new FormControl(0),
      incentive2_PlusTMPay: new FormControl(0),
      incentive2_PlusMRPay: new FormControl(0),
      licIncentive3Tmpay: new FormControl(0),
      licIncentive3Mrpay: new FormControl(0),
      isActive: new FormControl(true),
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
                    plS_Incentive1TMPay: license.plsIncentive1Tmpay,
                    plS_Incentive1MRPay: license.plsIncentive1Mrpay,
                    incentive2_PlusTMPay: license.incentive2PlusTmpay,
                    incentive2_PlusMRPay: license.incentive2PlusMrpay,
                    licIncentive3Tmpay: license.licIncentive3Tmpay,
                    licIncentive3Mrpay: license.licIncentive3Mrpay,
                    isActive: license.isActive,
                  });
                }
              )
            );
          } else {
            this.licenseForm.reset();
            this.licenseForm.patchValue({
              stateProvinceAbv: 'Select',
              lineOfAuthorityId: 0,
              plS_Incentive1TMPay: 0,
              plS_Incentive1MRPay: 0,
              incentive2_PlusTMPay: 0,
              incentive2_PlusMRPay: 0,
              licIncentive3Tmpay: 0,
              licIncentive3Mrpay: 0,
              isActive: true,
            });
          }
        }
      )
    );
  }

  onSubmit(): void {
    this.isFormSubmitted = true;
    let licenseItem: any = this.licenseForm.value;
    licenseItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.adminComService.modes.licenseItem.mode === 'INSERT') {
      licenseItem.licenseID = 0;
    }

    // if (examItem.deliveryMethod === 'Select Method') {
    //   examItem.deliveryMethod = '';
    //   this.companyForm.controls['companyType'].setErrors({ incorrect: true });
    // }

    // if (licenseItem.deliveryMethod === 'Select Method') {
    //   licenseItem.deliveryMethod = '';
    // }

    if (!this.licenseForm.valid) {
      this.licenseForm.setErrors({ invalid: true });
      return;
    }

console.log('EMFTEST (onSubmit) - licenseItem => \n', licenseItem);

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
