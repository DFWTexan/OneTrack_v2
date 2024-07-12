import {
  Component,
  Injectable,
  OnInit,
  OnDestroy,
  Input,
  EventEmitter,
  Output,
} from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../_services';

@Component({
  selector: 'app-edit-state-province',
  templateUrl: './edit-state-province.component.html',
  styleUrl: './edit-state-province.component.css',
})
export class EditStateProvinceComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  @Input() licenseTeches: { value: number; label: string }[] = [];
  @Input() stateProvinces: any[] = [];
  stateProvinceForm!: FormGroup;
  isFormSubmitted = false;
  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.stateProvinceForm = new FormGroup({
      stateProvinceCode: new FormControl(''),
      stateProvinceName: new FormControl(''),
      country: new FormControl(''),
      stateProvinceAbv: new FormControl(''),
      doiAddressID: new FormControl(''),
      licenseTechID: new FormControl(''),
      isActive: new FormControl(''),
      doiName: new FormControl(''),
      teamNum: new FormControl(''),
      techName: new FormControl(''),
      addressType: new FormControl(''),
      address1: new FormControl(''),
      address2: new FormControl(''),
      city: new FormControl(''),
      state: new FormControl(''),
      phone: new FormControl(''),
      zip: new FormControl(''),
      fax: new FormControl(''),
    });

    this.subscriptionData =
      this.adminComService.modes.stateProvince.changed.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.adminDataService.stateProvinceChanged.subscribe(
              (stateProvince: any) => {
                this.stateProvinceForm.patchValue({
                  stateProvinceCode: stateProvince.stateProvinceCode,
                  stateProvinceName: stateProvince.stateProvinceName,
                  country: stateProvince.country,
                  stateProvinceAbv: stateProvince.stateProvinceAbv,
                  doiAddressID: stateProvince.doiAddressID,
                  licenseTechID: stateProvince.licenseTechID,
                  isActive: stateProvince.isActive,
                  doiName: stateProvince.doiName,
                  teamNum: stateProvince.teamNum,
                  techName: stateProvince.techName,
                  addressType: stateProvince.addressType,
                  address1: stateProvince.address1,
                  address2: stateProvince.address2,
                  city: stateProvince.city,
                  state: stateProvince.state,
                  phone: stateProvince.phone,
                  zip: stateProvince.zip,
                  fax: stateProvince.fax,
                });
              }
            );
          } else {
            this.stateProvinceForm.reset();
          }
        }
      );
  }

  onSubmit(): void {
    this.isFormSubmitted = true;
    let stateProvinceItem: any = this.stateProvinceForm.value;
    stateProvinceItem.upsertType =
      this.adminComService.modes.stateProvince.mode;
    stateProvinceItem.userSOEID =
      this.userAcctInfoDataService.userAcctInfo.soeid;

    this.subscriptionData.add(
      this.adminDataService.upSertStateProvince(stateProvinceItem).subscribe({
        next: (response) => {
          this.callParentRefreshData.emit();
          this.forceCloseModal();
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
            this.forceCloseModal();
          }
        },
      })
    );
  }

  private forceCloseModal() {
    const modalDiv = document.getElementById('modal-state-province');
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
