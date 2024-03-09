import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import { AgentComService, AgentDataService } from '../../../../_services';

@Component({
  selector: 'app-edit-jobtitle-hist',
  templateUrl: './edit-jobtitle-hist.component.html',
  styleUrl: './edit-jobtitle-hist.component.css',
})
@Injectable()
export class EditJobtitleHistComponent implements OnInit, OnDestroy {
  jobTitleForm!: FormGroup;
  subscriptionMode: Subscription = new Subscription;
  subscriptionData: Subscription = new Subscription;

  constructor(
    public agentService: AgentDataService,
    public agentComService: AgentComService
  ) {}

  ngOnInit(): void {
    this.jobTitleForm = new FormGroup({
      employmentJobTitleID: new FormControl({ value: '', disabled: true }),
      employmentID: new FormControl(null),
      jobTitleDate: new FormControl(null),
      jobCode: new FormControl(null),
      jobTitle: new FormControl(null),
      isCurrent: new FormControl(null),
    });

    this.subscriptionMode = this.agentComService.modeEmploymentJobTitleHistChanged.subscribe(
      (mode: string) => {
        if (mode === 'EDIT') {
          this.subscriptionData = this.agentService.employmentJobTitleHistItemChanged.subscribe(
            (jobTitle: any) => {
              this.jobTitleForm.patchValue({
                employmentJobTitleID: jobTitle.employmentJobTitleID,
                employmentID: jobTitle.employmentID,
                jobTitleDate: formatDate(
                  jobTitle.jobTitleDate,
                  'yyyy-MM-dd',
                  'en-US'
                ),
                jobCode: jobTitle.jobCode,
                jobTitle: jobTitle.jobTitle,
                isCurrent: jobTitle.isCurrent,
              });
            }
          );
        } else {
          this.jobTitleForm.reset();
        }
      }
    );
  }

  onSubmit() {
    console.log('EMFTest - (app-edit-jobtitle-hist) onSubmit => \n', this.jobTitleForm.value);
  }

  ngOnDestroy(): void {
    this.subscriptionMode.unsubscribe();
    this.subscriptionData.unsubscribe();
  }
}
