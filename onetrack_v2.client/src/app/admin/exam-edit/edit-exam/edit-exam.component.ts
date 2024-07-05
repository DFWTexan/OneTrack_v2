import { Component, Injectable, OnInit, OnDestroy, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService } from '../../../_services';
import { state } from '@angular/animations';
@Component({
  selector: 'app-edit-exam',
  templateUrl: './edit-exam.component.html',
  styleUrl: './edit-exam.component.css',
})
@Injectable()
export class EditExamComponent implements OnInit, OnDestroy {
  @Input() stateProvinces: any[] = [];
  @Input() examDeliveryMethods: { value: number; label: string }[] = [];
  @Input() examProviders: { value: number; label: string }[] = [];
  examForm!: FormGroup;
  
  subscriptionData: Subscription = new Subscription();

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService
  ) {}

  ngOnInit(): void {
    this.examForm = new FormGroup({
      examId: new FormControl(''),
      examName: new FormControl(''),
      examFees: new FormControl(''),
      stateProvinceAbv: new FormControl(''),
      examProviderId: new FormControl(''),
      companyName: new FormControl(''),
      deliveryMethod: new FormControl('Select Method'),
    });

    this.subscriptionData =
      this.adminComService.modes.examItem.changed.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.adminDataService.examItemsChanged.subscribe((exam: any) => {
            this.examForm.patchValue({
              examId: exam.examId,
              examName: exam.examName,
              examFees: exam.examFees,
              stateProvinceAbv: exam.stateProvinceAbv,
              examProviderId: exam.examProviderId,
              companyName: exam.companyName,
              deliveryMethod: exam.deliveryMethod ? exam.deliveryMethod : 'Select Method',
            });
          });
        } else {
          this.examForm.reset();
          this.examForm.patchValue({
            deliveryMethod: 'Select Method',
            stateProvinceAbv: 'Select',
            examProviderId: 0,
          });
        }
      });
  }

  onSubmit(): void {}

  forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-exam');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  closeModal() {
    this.forceCloseModal();
    // if (this.employmentHistoryForm.dirty && !this.isFormSubmitted) {
    //   if (
    //     confirm('You have unsaved changes. Are you sure you want to close?')
    //   ) {
    //     const modalDiv = document.getElementById('modal-edit-emp-history');
    //     if (modalDiv != null) {
    //       modalDiv.style.display = 'none';
    //     }
    //     this.employmentHistoryForm.reset();
    //     this.employmentHistoryForm.patchValue({
    //       backgroundCheckStatus: 'Pending',
    //       isCurrent: true,
    //     });
    //   }
    // } else {
    //   this.isFormSubmitted = false;
    //   const modalDiv = document.getElementById('modal-edit-emp-history');
    //   if (modalDiv != null) {
    //     modalDiv.style.display = 'none';
    //   }
    // }
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
