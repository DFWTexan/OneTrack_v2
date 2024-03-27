import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService, ModalService } from '../../_services';

@Component({
  selector: 'app-company-edit',
  templateUrl: './company-edit.component.html',
  styleUrl: './company-edit.component.css',
})
@Injectable()
export class CompanyEditComponent implements OnInit, OnDestroy {
  loading: boolean = false;
  companyTypes: any[] = [];
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

  ngOnDestroy(): void {
    // this.adminDataService.companyTypesChanged.unsubscribe();
  }
}
