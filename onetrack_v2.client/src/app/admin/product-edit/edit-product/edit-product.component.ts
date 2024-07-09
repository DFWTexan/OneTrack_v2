import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService } from '../../../_services';


@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrl: './edit-product.component.css',
})
@Injectable()
export class EditProductComponent implements OnInit, OnDestroy {
  productForm!: FormGroup;
  subscriptionData: Subscription = new Subscription();

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService
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
              ? formatDate(
                product.effectiveDate,
                  'yyyy-MM-dd',
                  'en-US'
                )
              : null,
              expireDate: product.expireDate
              ? formatDate(
                product.expireDate,
                  'yyyy-MM-dd',
                  'en-US'
                )
              : null,
              isActive: product.isActive,
            });
          });
        } else {
          this.productForm.reset();
        }
      });
  }

  onSubmit(): void {}

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
