import {
  Component,
  Injectable,
  OnInit,
  OnDestroy,
  EventEmitter,
  Output,
  Input,
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
import { DropdownItem } from '../../../_Models';

@Component({
  selector: 'app-edit-dropdown-item',
  templateUrl: './edit-dropdown-item.component.html',
  styleUrl: './edit-dropdown-item.component.css',
})
@Injectable()
export class EditDropdownItemComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  @Input() selectedDropdownListType: string = '';
  isFormSubmitted = false;
  dropdownItemForm!: FormGroup;
  upSertType: string = 'INSERT';

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.dropdownItemForm = new FormGroup({
      lkpField: new FormControl(''),
      lkpValue: new FormControl(''),
      sortOrder: new FormControl(''),
    });

    this.subscriptionData =
      this.adminComService.modes.dropdownItem.changed.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.upSertType = 'UPDATE';
            this.adminDataService.dropdownListItemChanged.subscribe(
              (dropdownItem: DropdownItem) => {
                this.dropdownItemForm.patchValue({
                  lkpField: dropdownItem.lkpField,
                  lkpValue: dropdownItem.lkpValue,
                  sortOrder: dropdownItem.sortOrder,
                });
              }
            );
          } else {
            this.dropdownItemForm.reset();
          }
        }
      );
  }

  onSubmit() {
    this.isFormSubmitted = true;
    let lkpTypeItem: any = this.dropdownItemForm.value;
    lkpTypeItem.upSertType = this.upSertType;
    lkpTypeItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    // console.log(
    //   'EMFTEST () this.adminComService.modes.dropdownItem.mode: ',
    //   this.adminComService.modes.dropdownItem.mode
    // );

    if (this.adminComService.modes.dropdownItem.mode == 'INSERT') {
      if (lkpTypeItem.lkpValue === '' || lkpTypeItem.lkpValue == null) {
        this.dropdownItemForm.controls['lkpValue'].setErrors({
          required: true,
        });
      }
    }

    if (this.dropdownItemForm.invalid) {
      this.dropdownItemForm.setErrors({ invalid: true });
      return;
    }

    this.adminDataService.upSertLkpType(lkpTypeItem).subscribe({
      next: (response) => {
        this.callParentRefreshData.emit();
        this.appComService.updateAppMessage(
            'Dropdown Item saved successfully'
          );
        this.forceCloseModal();
      },
      error: (error) => {
        if (error.error && error.error.errMessage) {
          this.errorMessageService.setErrorMessage(error.error.errMessage);
          this.forceCloseModal();
        }
      },
    });
  }

  forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-dropdown-item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  closeModal() {
    this.forceCloseModal();
  }

  ngOnDestroy() {
    this.subscriptionData.unsubscribe();
  }
}
