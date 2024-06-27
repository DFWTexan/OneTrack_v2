import { Component, Injectable, OnInit } from '@angular/core';

import { AdminComService, AdminDataService, AppComService, ModalService } from '../../_services';

@Component({
  selector: 'app-company-edit',
  templateUrl: './company-edit.component.html',
  styleUrl: './company-edit.component.css',
})
@Injectable()
export class CompanyEditComponent implements OnInit {
  isLoading: boolean = false;
  companyType: string = '';
  companyTypes: any[] = ['Loading...'];
  companies: any[] = [];
  selectedCompanyType: string = 'Select Company Type';

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    public modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.adminDataService.fetchCompanyTypes().subscribe((response) => {
      this.companyTypes = ['Select Company Type', ...response];
      this.isLoading = false;
    });
  }

  onChildCallRefreshData() {
    this.fetchCompanyItems();
  }

  changeCompanyType(event: any) {
    this.isLoading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.companyType = value;

    this.selectedCompanyType = value;

    if (value === 'Select Company Type') {
      this.companies = [];
      this.isLoading = false;
      return;
    } else {
      this.fetchCompanyItems();
    }
  }

  fetchCompanyItems() {
    this.isLoading = true;
    this.adminDataService.fetchCompanies(this.companyType).subscribe((response) => {
      this.companies = response;
      this.isLoading = false;
    });
  }
}
