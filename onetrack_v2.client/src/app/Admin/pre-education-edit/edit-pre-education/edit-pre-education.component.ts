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
  AppComService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../_services';

@Component({
  selector: 'app-edit-pre-education',
  templateUrl: './edit-pre-education.component.html',
  styleUrl: './edit-pre-education.component.css',
})
@Injectable()
export class EditPreEducationComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  @Input() deliveryMethods: any[] = [];
  @Input() providers: { value: number; label: string }[] = [];
  @Input() stateProvinces: any[] = [];
  isFormSubmitted = false;
  preEducationForm!: FormGroup;

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.preEducationForm = new FormGroup({
      preEducationId: new FormControl(0),
      companyID: new FormControl(0),
      educationName: new FormControl(''),
      stateProvinceAbv: new FormControl(''),
      creditHours: new FormControl(''),
      deliveryMethod: new FormControl(''),
      educationProviderID: new FormControl(0),
    });

    function capitalizeFirstLetter(string: any) {
      if (typeof string !== 'string') return '';
      return string
        .split('-')
        .map(
          (part: any) =>
            part.charAt(0).toUpperCase() + part.slice(1).toLowerCase()
        )
        .join('-');
    }

    this.subscriptionData.add(
      this.adminComService.modes.preEducation.changed.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.adminDataService.preEducationChanged.subscribe(
              (preEducation: any) => {
                this.preEducationForm.patchValue({
                  preEducationId: preEducation.preEducationId,
                  educationName: preEducation.educationName,
                  stateProvinceAbv: preEducation.stateProvinceAbv,
                  creditHours: preEducation.creditHours,
                  deliveryMethod: capitalizeFirstLetter(
                    preEducation.deliveryMethod
                  ),
                  educationProviderID: preEducation.companyID,
                });
              }
            );
          } else {
            let hasSelect = this.deliveryMethods.includes('Select');
            this.deliveryMethods = !hasSelect
              ? ['Select', ...this.deliveryMethods]
              : this.deliveryMethods;
            let hasProvSelect = this.providers.some(
              (provider) => provider.value === 0 && provider.label === 'Select'
            );
            this.providers = !hasProvSelect
              ? [{ value: 0, label: 'Select' }, ...this.providers]
              : this.providers;
            this.preEducationForm.reset({
              stateProvinceAbv: 'Select',
              deliveryMethod: 'Select',
              preEducationId: 0,
              educationProviderID: 0,
            });
          }
        }
      )
    );
  }

  onSubmit(): void {
    this.isFormSubmitted = true;
    let preEduItem: any = this.preEducationForm.value;
    preEduItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.adminComService.modes.preEducation.mode === 'INSERT') {
      preEduItem.PreEducationID = 0;
    }

    if (preEduItem.educationName === '' || preEduItem.educationName === null) {
      this.preEducationForm.controls['educationName'].setErrors({
        required: true,
      });
    }

    if (
      preEduItem.deliveryMethod === 'Select' ||
      preEduItem.deliveryMethod === null
    ) {
      this.preEducationForm.controls['deliveryMethod'].setErrors({
        required: true,
      });
    }

    if (
      preEduItem.educationProviderID === 0 ||
      preEduItem.educationProviderID === null
    ) {
      this.preEducationForm.controls['educationProviderID'].setErrors({
        required: true,
      });
    }

    if (
      preEduItem.stateProvinceAbv === 'Select' ||
      preEduItem.stateProvinceAbv === null
    ) {
      this.preEducationForm.controls['stateProvinceAbv'].setErrors({
        required: true,
      });
    }

    if (!this.preEducationForm.valid) {
      this.preEducationForm.setErrors({ invalid: true });
      return;
    }

    this.subscriptionData.add(
      this.adminDataService.upsertPreEducationItem(preEduItem).subscribe({
        next: (response) => {
          this.callParentRefreshData.emit();
          this.appComService.updateAppMessage(
            'Pre-Education saved successfully'
          );
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

  forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-pre-education');
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
