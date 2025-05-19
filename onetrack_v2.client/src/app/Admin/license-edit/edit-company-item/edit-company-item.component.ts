import { Component, Injectable, OnInit, OnDestroy, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService, AppComService, ErrorMessageService, UserAcctInfoDataService } from '../../../_services';

@Component({
  selector: 'app-edit-company-item',
  templateUrl: './edit-company-item.component.html',
  styleUrl: './edit-company-item.component.css'
})
@Injectable()
export class EditCompanyItemComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  companyItemForm!: FormGroup;
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
    this.companyItemForm = new FormGroup({
      licenseID: new FormControl(0),
      licenseCompanyId: new FormControl(''),
      companyId: new FormControl(''),
      companyAbv: new FormControl(''),
      companyType: new FormControl(''),
      companyName: new FormControl(''),
      tin: new FormControl(''),
      naicNumber: new FormControl(''),
      isActive: new FormControl(''),
      addressId: new FormControl(''),
      address1: new FormControl(''),
      address2: new FormControl(''),
      city: new FormControl(''),
      state: new FormControl(''),
      zip: new FormControl(''),
      phone: new FormControl(''),
      fax: new FormControl(''),
      country: new FormControl(''),
    });

    this.subscriptionData =
      this.adminComService.modes.companyItem.changed.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.adminDataService.companyItemChanged.subscribe((companyItem: any) => {
            this.companyItemForm.patchValue({
              licenseID: companyItem.licenseID,
              licenseCompanyId: companyItem.licenseCompanyId,
              companyId: companyItem.companyId,
              companyAbv: companyItem.companyAbv,
              companyType: companyItem.companyType,
              companyName: companyItem.companyName,
              tin: companyItem.tin,
              naicNumber: companyItem.naicNumber,
              isActive: companyItem.isActive,
              addressId: companyItem.addressId,
              address1: companyItem.address1,
              address2: companyItem.address2,
              city: companyItem.city,
              state: companyItem.state,
              zip: companyItem.zip,
              phone: companyItem.phone,
              fax: companyItem.fax,
              country: companyItem.country,
            });
          });
        } else {
          this.companyItemForm.reset();
        }
      });
  }

  onSubmit(): void {
    this.isFormSubmitted = true;
    let licCompanyItem: any = this.companyItemForm.value;
    licCompanyItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.adminComService.modes.examItem.mode === 'INSERT') {
      licCompanyItem.examID = 0;
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
      this.adminDataService.updateLicenseCompany(licCompanyItem).subscribe({
        next: (response) => {
          this.callParentRefreshData.emit();
          this.appComService.updateAppMessage(
            'License Company updated successfully.');
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

  onCloseModal() {
    const modalDiv = document.getElementById('modal-edit-company-item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }

}
