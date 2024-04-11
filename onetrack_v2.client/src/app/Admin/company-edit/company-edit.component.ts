import { Component, Injectable, OnInit } from '@angular/core';

import { AdminComService, AdminDataService, ModalService } from '../../_services';

@Component({
  selector: 'app-company-edit',
  templateUrl: './company-edit.component.html',
  styleUrl: './company-edit.component.css',
})
@Injectable()
export class CompanyEditComponent implements OnInit {
  loading: boolean = false;
  companyTypes: any[] = ['Loading...'];
  companies: any[] = [];
  selectedCompanyType: string = 'Select Company Type';

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.loading = true;
    this.adminDataService.fetchCompanyTypes().subscribe((response) => {
      this.companyTypes = ['Select Company Type', ...response];
      this.loading = false;
    });
  }

  changeCompanyType(event: any) {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;

    this.selectedCompanyType = value;

    if (value === 'Select Company Type') {
      this.companies = [];
      this.loading = false;
      return;
    } else {
      this.adminDataService.fetchCompanies(value).subscribe((response) => {
        this.companies = response;
        this.loading = false;
      });
    }
  }
}
