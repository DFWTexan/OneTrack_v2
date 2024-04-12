import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService } from '../../../_services';
import { DropdownItem } from '../../../_Models';

@Component({
  selector: 'app-edit-dropdown-item',
  templateUrl: './edit-dropdown-item.component.html',
  styleUrl: './edit-dropdown-item.component.css',
})
@Injectable()
export class EditDropdownItemComponent implements OnInit, OnDestroy {
  dropdownItemForm!: FormGroup;
  subscriptionData: Subscription = new Subscription();

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService
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

  onSubmit() {}

  ngOnDestroy() {
    this.subscriptionData.unsubscribe();
  }
}
