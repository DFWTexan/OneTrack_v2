import {
  Component,
  Injectable,
  OnInit,
  OnDestroy,
  EventEmitter,
  Output,
} from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../_services';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrl: './edit-product.component.css',
})
@Injectable()
export class EditProductComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  productForm!: FormGroup;
  isFormSubmitted = false;
  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.productForm = new FormGroup({
      productId: new FormControl(''),
      productName: new FormControl(''),
      productAbv: new FormControl(''),
      effectiveDate: new FormControl(''),
      expireDate: new FormControl(''),
      isActive: new FormControl(''),
    });

    this.subscriptionData =
      this.adminComService.modes.product.changed.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.adminDataService.productChanged.subscribe((product: any) => {
            this.productForm.patchValue({
              productId: product.productId,
              productName: product.productName,
              productAbv: product.productAbv,
              effectiveDate: product.effectiveDate
                ? formatDate(product.effectiveDate, 'yyyy-MM-dd', 'en-US')
                : null,
              expireDate: product.expireDate
                ? formatDate(product.expireDate, 'yyyy-MM-dd', 'en-US')
                : null,
              isActive: product.isActive,
            });
          });
        } else {
          this.productForm.reset();
        }
      });
  }

  onSubmit(): void {
    this.isFormSubmitted = true;
    let productItem: any = this.productForm.value;
    productItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.adminComService.modes.product.mode === 'INSERT') {
      productItem.PreEducationID = 0;
    }

    if (productItem.productAbv === '' || productItem.productAbv === null) {
      this.productForm.controls['productAbv'].setErrors({ required: true });
    }

    if (productItem.productName === '' || productItem.productName === null) {
      this.productForm.controls['productName'].setErrors({ required: true });
    }

    if (!this.productForm.valid) {
      this.productForm.setErrors({ invalid: true });
      return;
    }

    this.subscriptionData.add(
      this.adminDataService.upsertProduct(productItem).subscribe({
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
    const modalDiv = document.getElementById('modal-edit-product');
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
