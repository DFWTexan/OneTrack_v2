import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService } from '../../../_services';

@Component({
  selector: 'app-edit-pre-education',
  templateUrl: './edit-pre-education.component.html',
  styleUrl: './edit-pre-education.component.css'
})
@Injectable()
export class EditPreEducationComponent implements OnInit, OnDestroy {
  preEducationForm!: FormGroup;
  subscriptionData: Subscription = new Subscription();

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService
  ) {}

  ngOnInit(): void {
    this.preEducationForm = new FormGroup({
      preEducationId: new FormControl(''),
      educationName: new FormControl(''),
      stateProvinceAbv: new FormControl(''),
      creditHours: new FormControl(''),
      deliveryMethod: new FormControl(''),
    });

    this.subscriptionData =
      this.adminComService.modes.preEducation.changed.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.adminDataService.preEducationChanged.subscribe((preEducation: any) => {
            this.preEducationForm.patchValue({
              preEducationId: preEducation.preEducationId,
              educationName: preEducation.educationName,
              stateProvinceAbv: preEducation.stateProvinceAbv,
              creditHours: preEducation.creditHours,
              deliveryMethod: preEducation.deliveryMethod,
            });
          });
        } else {
          this.preEducationForm.reset();
        }
      });
  }

  onSubmit(): void {}

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
