import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService } from '../../../_services';

@Component({
  selector: 'app-edit-pre-edu-item',
  templateUrl: './edit-pre-edu-item.component.html',
  styleUrl: './edit-pre-edu-item.component.css'
})
@Injectable()
export class EditPreEduItemComponent implements OnInit, OnDestroy {
  preEduItemForm!: FormGroup;
  subscriptionData: Subscription = new Subscription();

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService
  ) {}

  ngOnInit(): void {
    this.preEduItemForm = new FormGroup({
      licensePreEducationID: new FormControl(''),
      preEducationID: new FormControl(''),
      educationName: new FormControl(''),
      stateProvinceAbv: new FormControl(''),
      creditHours: new FormControl(''),
      companyID: new FormControl(''),
      companyName: new FormControl(''),
      deliveryMethod: new FormControl(''),
      isActive: new FormControl(''),
    });

    this.subscriptionData =
      this.adminComService.modes.preEduItem.changed.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.adminDataService.preEduItemChanged.subscribe((preEduItem: any) => {
            this.preEduItemForm.patchValue({
              licensePreEducationID: preEduItem.licensePreEducationID,
              preEducationID: preEduItem.preEducationID,
              educationName: preEduItem.educationName,
              stateProvinceAbv: preEduItem.stateProvinceAbv,
              creditHours: preEduItem.creditHours,
              companyID: preEduItem,
              companyName: preEduItem.companyName,
              deliveryMethod: preEduItem.deliveryMethod,
              isActive: preEduItem.isActive,
            });
          });
        } else {
          this.preEduItemForm.reset();
        }
      });
  }

  onCloseModal() {
    const modalDiv = document.getElementById('modal-edit-preEdu-Item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  onSubmit(): void {}

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
