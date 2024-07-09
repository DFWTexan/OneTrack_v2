import { Component, Injectable, OnInit, OnDestroy, Input } from '@angular/core';
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
  @Input() deliveryMethods: any[] = [];
  @Input() providers: { value: number; label: string }[] = [];
  @Input() stateProvinces: any[] = [];
  preEducationForm!: FormGroup;

  subscriptionData: Subscription = new Subscription();

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService
  ) {}

  ngOnInit(): void {
    this.preEducationForm = new FormGroup({
      preEducationId: new FormControl(0),
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
          this.deliveryMethods = ['Select', ...this.deliveryMethods];
          this.providers = [{ value: 0, label: 'Select' }, ...this.providers];
          this.preEducationForm.patchValue({
            stateProvinceAbv: 'Select',
            deliveryMethod: 'Select',
            preEducationId: 0,
          });
        }
      });
  }

  onSubmit(): void {}

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
