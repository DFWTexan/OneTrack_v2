import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService } from '../../../_services';

@Component({
  selector: 'app-edit-product-item',
  templateUrl: './edit-product-item.component.html',
  styleUrl: './edit-product-item.component.css'
})
@Injectable()
export class EditProductItemComponent implements OnInit, OnDestroy {
  productItemForm!: FormGroup;
  subscriptionData: Subscription = new Subscription();

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService
  ) {}

  ngOnInit(): void {
    this.productItemForm = new FormGroup({
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

  onSubmit(): void {}

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
