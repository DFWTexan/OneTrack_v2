import { Component, Injectable, Input, OnDestroy, OnInit } from '@angular/core';

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
  @Input() companies: any[] = [];

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {}

  onCloseModal() {
    const modalDiv = document.getElementById('modal-add-company-item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {}
}
