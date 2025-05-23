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
import { state } from '@angular/animations';
@Component({
  selector: 'app-edit-exam',
  templateUrl: './edit-exam.component.html',
  styleUrl: './edit-exam.component.css',
})
@Injectable()
export class EditExamComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  @Input() stateProvinces: any[] = [];
  @Input() examDeliveryMethods: { value: number; label: string }[] = [];
  @Input() examProviders: { value: number; label: string }[] = [];
  isFormSubmitted = false;
  examForm!: FormGroup;

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
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

    this.subscriptionData.add(
      this.adminComService.modes.examItem.changed.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.subscriptionData.add(
            this.adminDataService.examItemsChanged.subscribe((exam: any) => {
              this.examForm.patchValue({
                examId: exam.examId,
                examName: exam.examName,
                examFees: exam.examFees,
                stateProvinceAbv: exam.stateProvinceAbv,
                examProviderId: exam.examProviderId,
                companyName: exam.companyName,
                deliveryMethod: exam.deliveryMethod
                  ? exam.deliveryMethod
                  : 'Select Method',
              });
            })
          );
        } else {
          this.examForm.reset();
          this.examForm.patchValue({
            deliveryMethod: 'Select Method',
            stateProvinceAbv: 'Select',
            examProviderId: 0,
          });
        }
      })
    );
  }

  onSubmit(): void {
    this.isFormSubmitted = true;
    let examItem: any = this.examForm.value;
    examItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.adminComService.modes.examItem.mode === 'INSERT') {
      examItem.examID = 0;
    }

    if (examItem.stateProvinceAbv === 'Select') {
      this.examForm.controls['stateProvinceAbv'].setErrors({ required: true });
    }

    if (examItem.examName === '' || examItem.examName === null) {
      this.examForm.controls['examName'].setErrors({ required: true });
    }

    if (examItem.deliveryMethod === 'Select Method') {
      this.examForm.controls['deliveryMethod'].setErrors({ required: true });
    }

    if (examItem.examProviderId === 0 || examItem.examProviderId === null) {
      this.examForm.controls['examProviderId'].setErrors({ required: true });
    }

    if (this.examForm.invalid) {
      this.examForm.setErrors({ invalid: true });
      return;
    }

    this.subscriptionData.add(
      this.adminDataService.upSertExamItem(examItem).subscribe({
        next: (response) => {
          this.callParentRefreshData.emit();
          this.appComService.updateAppMessage(
            'Exam saved successfully'
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
