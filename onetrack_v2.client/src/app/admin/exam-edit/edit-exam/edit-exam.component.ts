import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService } from '../../../_services';
@Component({
  selector: 'app-edit-exam',
  templateUrl: './edit-exam.component.html',
  styleUrl: './edit-exam.component.css',
})
@Injectable()
export class EditExamComponent implements OnInit, OnDestroy {
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
      deliveryMethod: new FormControl(''),
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
              deliveryMethod: exam.deliveryMethod,
            });
          });
        } else {
          this.examForm.reset();
        }
      });
  }

  onSubmit(): void {}

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
