import { Component, EventEmitter, Injectable, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../_services';

@Component({
  selector: 'app-add-company',
  templateUrl: './add-company.component.html',
  styleUrl: './add-company.component.css',
})
@Injectable()
export class AddCompanyComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  @Input() companies: any[] = [];
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
    item.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    this.subscriptionData.add(
      this.adminDataService.addLicenseCompany(item).subscribe({
        next: (response) => {
          alert('Company added successfully');
          this.callParentRefreshData.emit();
          // console.log(
          //   'EMFTEST (app-tm-emptrans-history: deleteJobTitle) - COMPLETED DELETE response => \n',
          //   response
          // );
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
    const modalDiv = document.getElementById('modal-add-company-item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
