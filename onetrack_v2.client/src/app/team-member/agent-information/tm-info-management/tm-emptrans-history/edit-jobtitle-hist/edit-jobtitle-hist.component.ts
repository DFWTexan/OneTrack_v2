import { Component, Injectable, Input, OnDestroy, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import {
  AgentComService,
  AgentDataService,
  AppComService,
  MiscDataService,
  UserAcctInfoDataService,
} from '../../../../../_services';

@Component({
  selector: 'app-edit-jobtitle-hist',
  templateUrl: './edit-jobtitle-hist.component.html',
  styleUrl: './edit-jobtitle-hist.component.css',
})
@Injectable()
export class EditJobtitleHistComponent implements OnInit, OnDestroy {
  isFormSubmitted: boolean = false;
  jobTitleForm!: FormGroup;
  @Input() employmentID: number = 0;
  @Input() employeeID: number = 0;
  jobTitleList: Array<{ jobTitleID: number; jobTitle: string }> = [];
  subscriptionMode: Subscription = new Subscription();
  subscriptionData: Subscription = new Subscription();

  constructor(
    private fb: FormBuilder,
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    public appComService: AppComService,
    private miscDataService: MiscDataService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.jobTitleForm = this.fb.group({
      employmentJobTitleID: [''],
      employmentID: [''],
      jobTitleDate: ['', Validators.required],
      jobTitleID: [0, Validators.required],
      isCurrent: [''],
    });

    this.subscriptionMode =
      this.agentComService.modeEmploymentJobTitleHistChanged.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.subscriptionData =
              this.agentDataService.employmentJobTitleHistItemChanged.subscribe(
                (jobTitle: any) => {
                  this.jobTitleForm.patchValue({
                    employmentJobTitleID: jobTitle.employmentJobTitleID,
                    jobTitleDate: formatDate(
                      jobTitle.jobTitleDate,
                      'yyyy-MM-dd',
                      'en-US'
                    ),
                    jobTitleID: jobTitle.jobTitleID,
                    isCurrent: jobTitle.isCurrent,
                  });
                }
              );
          } else {
            this.jobTitleForm.reset();
            this.jobTitleForm.patchValue({
              jobTitleID: 0,
              // jobTitleDate: formatDate(new Date(), 'yyyy-MM-dd', 'en-US'),
              isCurrent: false,
            });
          }
        }
      );

    this.miscDataService
      .fetchJobTitles()
      .subscribe(
        (jobTitles: Array<{ jobTitleID: number; jobTitle: string }>) => {
          this.jobTitleList = [
            { jobTitleID: 0, jobTitle: 'Select Job Title' },
            ...jobTitles,
          ];
        }
      );
  }

  onSubmit() {
    this.isFormSubmitted = true;

    let jobTitleItem = this.jobTitleForm.value;
    jobTitleItem.employmentID = this.employmentID;
    jobTitleItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (
      jobTitleItem.jobTitleDate === '' ||
      jobTitleItem.jobTitleDate === null
    ) {
      this.jobTitleForm.controls['jobTitleDate'].setErrors({
        invalid: true,
      });
    }

    if (jobTitleItem.jobTitleID === 0) {
      this.jobTitleForm.controls['jobTitleID'].setErrors({
        invalid: true,
      });
    }

    if (this.jobTitleForm.invalid) {
      return;
    }

    if (this.agentComService.modeEmploymentJobTitleHist === 'INSERT') {
      jobTitleItem.employmentJobTitleID = 0;
    }

    console.log('EMFTEST (app-edit-jobtitle-hist: onSubmit) - jobTitleItem => \n', jobTitleItem);

    this.agentDataService
      .upsertEmploymentJobTitleHistItem(jobTitleItem)
      .subscribe({
        next: (response) => {
          this.isFormSubmitted = true;
          this.closeModal();
        },
        error: (error) => {
          console.error(error);
          // handle the error here
        },
      });
  }

  closeModal() {
    // const modalDiv = document.getElementById('modal-edit-jobTitle-history');
    // if (modalDiv != null) {
    //   modalDiv.style.display = 'none';
    // }
    if (this.jobTitleForm.dirty && !this.isFormSubmitted) {
      if (
        confirm('You have unsaved changes. Are you sure you want to close?')
      ) {
        const modalDiv = document.getElementById('modal-edit-jobTitle-history');
        if (modalDiv != null) {
          modalDiv.style.display = 'none';
        }
        this.jobTitleForm.reset();
        this.jobTitleForm.patchValue({
          jobTitleID: 0,
          isCurrent: false,
        });
      }
    } else {
      this.isFormSubmitted = false;
      const modalDiv = document.getElementById('modal-edit-jobTitle-history');
      if (modalDiv != null) {
        modalDiv.style.display = 'none';
      }
    }
  }

  ngOnDestroy(): void {
    this.subscriptionMode.unsubscribe();
    this.subscriptionData.unsubscribe();
  }
}
