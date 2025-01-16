import { Component, Injectable, OnInit, OnDestroy, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService, ErrorMessageService, UserAcctInfoDataService } from '../../../_services';

@Component({
  selector: 'app-edit-product-item',
  templateUrl: './edit-product-item.component.html',
  styleUrl: './edit-product-item.component.css'
})
@Injectable()
export class EditProductItemComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  productItemForm!: FormGroup;
  isFormSubmitted: boolean = false;

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.productItemForm = new FormGroup({
      licenseID: new FormControl(0),
      licenseProductID: new FormControl(''),
      productID: new FormControl(''),
      productName: new FormControl(''),
      productAbv: new FormControl(''),
      isActive: new FormControl(''),
    });

    this.subscriptionData =
      this.adminComService.modes.productItem.changed.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.adminDataService.productItemChanged.subscribe((productItem: any) => {
            this.productItemForm.patchValue({
              licenseID: productItem.licenseID,
              licenseProductID: productItem.licenseProductID,
              productID: productItem.productID,
              productName: productItem.productName,
              productAbv: productItem.productAbv,
              isActive: productItem.isActive,
            });
          });
        } else {
          this.productItemForm.reset();
        }
      });
  }

  onCloseModal() {
    const modalDiv = document.getElementById('modal-edit-product-Item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  onSubmit(): void {
    this.isFormSubmitted = true;
    let productItem: any = this.productItemForm.value;
    productItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    // if (this.adminComService.modes.preExamItem.mode === 'INSERT') {
    //   productItem.ProductID = 0;
    // }

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
      this.adminDataService.updateLicenseProduct(productItem).subscribe({
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
