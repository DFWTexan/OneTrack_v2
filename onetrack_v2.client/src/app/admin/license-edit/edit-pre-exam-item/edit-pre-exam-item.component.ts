import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService } from '../../../_services';

@Component({
  selector: 'app-edit-pre-exam-item',
  templateUrl: './edit-pre-exam-item.component.html',
  styleUrl: './edit-pre-exam-item.component.css',
})
@Injectable()
export class EditPreExamItemComponent implements OnInit, OnDestroy {
  preExamItemForm!: FormGroup;
  subscriptionData: Subscription = new Subscription();

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService
  ) {}

  ngOnInit(): void {
    this.preExamItemForm = new FormGroup({
      examId: new FormControl(''),
      examName: new FormControl(''),
      stateProvinceAbv: new FormControl(''),
      companyName: new FormControl(''),
      deliveryMethod: new FormControl(''),
      licenseExamID: new FormControl(''),
      examProviderID: new FormControl(''),
      isActive: new FormControl(''),
    });

    this.subscriptionData =
      this.adminComService.modes.preExamItem.changed.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.adminDataService.preExamItemChanged.subscribe(
              (preExamItem: any) => {
                this.preExamItemForm.patchValue({
                  examId: preExamItem.examId,
                  examName: preExamItem.examName,
                  stateProvinceAbv: preExamItem.stateProvinceAbv,
                  companyName: preExamItem.companyName,
                  deliveryMethod: preExamItem.deliveryMethod,
                  licenseExamID: preExamItem.licenseExamID,
                  examProviderID: preExamItem.examProviderID,
                  isActive: preExamItem.isActive,
                });
              }
            );
          } else {
            this.preExamItemForm.reset();
          }
        }
      );
  }

  onCloseModal() {
    const modalDiv = document.getElementById('modal-edit-preExam-Item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  onSubmit(): void {}

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
