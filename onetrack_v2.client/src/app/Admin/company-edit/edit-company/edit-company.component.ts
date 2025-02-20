import {
  Component,
  Injectable,
  OnInit,
  OnDestroy,
  EventEmitter,
  Output,
} from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Input } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  ConstantsDataService,
  ErrorMessageService,
  UserAcctInfoDataService,
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
  @Output() callParentRefreshData = new EventEmitter<any>();
  isFormSubmitted = false;
  states: any[] = ['Select State', 'Loading...'];
  companyForm!: FormGroup;
  subscriptionData: Subscription = new Subscription();
  defaultCompanyType: string = 'Select Company Type';
  defaultState: string = 'Select State';
  phonePattern: string = '\\(\\d{3}\\) \\d{3}-\\d{4}';

  @Input() companyTypes: any[] = ['Loading...'];

  constructor(
    private errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.companyForm = new FormGroup({
      companyId: new FormControl(''),
      companyAbv: new FormControl(''),
      companyType: new FormControl('', Validators.required),
      companyName: new FormControl('', Validators.required),
      tin: new FormControl('', [
        Validators.pattern('^[0-9]*$'),
        Validators.maxLength(9),
      ]),
      naicnumber: new FormControl('', [
        Validators.pattern('^[0-9]*$'),
        Validators.maxLength(5),
      ]),
      addressId: new FormControl(''),
      address1: new FormControl(''),
      address2: new FormControl(''),
      city: new FormControl(''),
      state: new FormControl('', Validators.required),
      phone: new FormControl('', [
        Validators.pattern(
          '^\\(\\d{3}\\) \\d{3}-\\d{4}$|^\\d{3}-\\d{3}-\\d{4}$'
        ),
      ]),
      country: new FormControl(''),
      zip: new FormControl('', [
        Validators.pattern('^[0-9]{5}(?:-[0-9]{4})?$'),
        Validators.maxLength(10),
      ]),
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
    this.isFormSubmitted = true;
    let company: Company = this.companyForm.value;
    company.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.adminComService.modes.company.mode === 'INSERT') {
      company.companyId = 0;
    }

    if (company.companyType === 'Select Company Type') {
      company.companyType = '';
      this.companyForm.controls['companyType'].setErrors({ incorrect: true });
    }

    if (company.state === 'Select State' || company.state === '') {
      this.companyForm.controls['state'].setErrors({ required: true });
    }

    if (!this.companyForm.valid) {
      this.companyForm.setErrors({ invalid: true });
      return;
    }

    this.adminDataService.upSertCompany(company).subscribe({
      next: (response) => {
        this.callParentRefreshData.emit();
        this.forceCloseModal();
      },
      error: (error) => {
        if (error.error && error.error.errMessage) {
          this.errorMessageService.setErrorMessage(error.error.errMessage);
        }
      },
    });
  }

  forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-company');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  closeModal() {
    this.forceCloseModal();
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
