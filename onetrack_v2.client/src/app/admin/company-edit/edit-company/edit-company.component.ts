import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService } from '../../../_services';
import { Company, CompanyItem } from '../../../_Models';


@Component({
  selector: 'app-edit-company',
  templateUrl: './edit-company.component.html',
  styleUrl: './edit-company.component.css',
})
@Injectable()
export class EditCompanyComponent implements OnInit, OnDestroy {
  companyForm!: FormGroup;
  subscriptionData: Subscription = new Subscription();

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService
  ) {}

  ngOnInit(): void {
    this.companyForm = new FormGroup({
      companyId: new FormControl(''),
      companyAbv: new FormControl(''),
      companyType: new FormControl(''),
      companyName: new FormControl(''),
      tin: new FormControl(''),
      naicnumber: new FormControl(''),
      addressId: new FormControl(''),
      address1: new FormControl(''),
      address2: new FormControl(''),
      city: new FormControl(''),
      state: new FormControl(''),
      phone: new FormControl(''),
      country: new FormControl(''),
      zip: new FormControl(''),
      fax: new FormControl(''),
    });

   this.subscriptionData = this.adminComService.modes.company.changed.subscribe((mode: string) => {
      if (mode === 'EDIT') {
        this.adminDataService.companyChanged.subscribe((company: Company) => {

console.log('EMFTEST (app-edit-company) - company', company);

          this.companyForm.patchValue({
            companyId: company.companyId,
            companyAbv: company.companyAbv,
            companyType: company.companyType,
            companyName: company.companyName,
            tin: company.tin,
            naicnumber: company.naicnumber,
            addressId: company.addressId,
            address1: company.address1,
            address2: company.address2,
            city: company.city,
            state: company.state,
            phone: company.phone,
            country: company.country,
            zip: company.zip,
            fax: company.fax,
          });
        });
      } else {
        this.companyForm.reset();
      }
    });
  }

  onSubmit() {}

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
