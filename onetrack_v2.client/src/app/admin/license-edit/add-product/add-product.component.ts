import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService, AppComService, ErrorMessageService, UserAcctInfoDataService } from '../../../_services';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.css'
})
export class AddProductComponent implements OnInit, OnDestroy  {
  @Input() products: any[] = [];
  licenseId: number = 0;

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.licenseId = this.adminDataService.licenseID;
    this.subscriptionData.add(
      this.adminDataService.licenseIdChanged.subscribe((id: number) => {
        this.licenseId = id;
      })
    );
  }

  onAddItem(item: any): void {
    console.log('EMFTEST (onAddCompany) - Add Product Item => \n', item);
  }

  onCloseModal() {
    const modalDiv = document.getElementById('modal-add-product-Item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {}
}
