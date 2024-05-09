import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Input } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  ConstantsDataService,
} from '../../../_services';
import { Company } from '../../../_Models';
import { state } from '@angular/animations';

@Component({
  selector: 'app-edit-company',
  templateUrl: './edit-company.component.html',
  styleUrl: './edit-company.component.css',
})
@Injectable()
export class EditCompanyComponent implements OnInit, OnDestroy {
  states: any[] = ['Select State', 'Loading...'];
  companyForm!: FormGroup;
  formSubmitted = false;
  subscriptionData: Subscription = new Subscription();
  defaultCompanyType: string = 'Select Company Type';
  defaultState: string = 'Select State';

  @Input() companyTypes: any[] = ['Loading...'];

  constructor(
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService
  ) {}

  ngOnInit(): void {
    this.companyForm = new FormGroup({
      companyId: new FormControl(''),
      companyAbv: new FormControl('', Validators.required),
      companyType: new FormControl('', Validators.required),
      companyName: new FormControl('', Validators.required),
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

    this.states = ['Select State', ...this.conService.getStates()];

    this.subscriptionData =
      this.adminComService.modes.company.changed.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.adminDataService.companyChanged.subscribe((company: Company) => {
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
          this.companyForm.patchValue({
            companyType: 'Select Company Type',
            state: 'Select State',
          });
        }
      });
  }

  onSubmit() {
    this.formSubmitted = true;
    let company = this.companyForm.value;

    if (company.companyType === 'Select Company Type') {
      company.companyType = '';
      this.companyForm.controls['companyType'].setErrors({ incorrect: true });
    }

    if (company.state === 'Select State') {
      company.state = '';
    }

    if (!this.companyForm.valid) {
      // If the form is not valid, set an error on the form
      this.companyForm.setErrors({ invalid: true });
      return;
    }

    this.adminDataService.editCompany(company).subscribe({
      next: (response) => {
        console.log(response);
        // handle the response here
      },
      error: (error) => {
        console.error(error);
        // handle the error here
      },
    });
  }

  closeModal() {
    const modalDiv = document.getElementById('modal-edit-company');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
