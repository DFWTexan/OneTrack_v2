import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { Subscription } from 'rxjs';
import { FormControl, FormGroup } from '@angular/forms';

import {
  AdminComService,
  AdminDataService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../_services';

@Component({
  selector: 'app-edit-job-title',
  templateUrl: './edit-job-title.component.html',
  styleUrl: './edit-job-title.component.css',
})
export class EditJobTitleComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  @Input() licenseLevels: any[] = [];
  @Input() licenseIncentives: any[] = [];
  isFormSubmitted = false;
  jobTitleForm!: FormGroup;

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.jobTitleForm = new FormGroup({
      jobTitle: new FormControl(''),
      licenseLevel: new FormControl('{NeedsReview}'),
      licenseIncentive: new FormControl('NoIncentive'),
    });
  }

  forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-job-title');
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

  onSubmit(): void {
    this.isFormSubmitted = true;
    let jobTitleItem: any = this.jobTitleForm.value;
    jobTitleItem.jobTitleID = 0;
    jobTitleItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

console.log('EMFTEST (onSubmit) - jobTitleItem => \n', jobTitleItem); 

    this.subscriptionData.add(
      this.adminDataService.upsertJobTitle(jobTitleItem).subscribe({
        next: (response) => {
          this.callParentRefreshData.emit();
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

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
