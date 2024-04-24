import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import {
  AgentComService,
  AgentDataService,
  AppComService,
  MiscDataService,
} from '../../../../_services';

@Component({
  selector: 'app-edit-employment-hist',
  templateUrl: './edit-employment-hist.component.html',
  styleUrl: './edit-employment-hist.component.css',
})
@Injectable()
export class EditEmploymentHistComponent implements OnInit, OnDestroy {
  employmentHistoryForm!: FormGroup;
  backgroundStatuses: Array<{ lkpValue: string }> = [];
  subscriptionMode: Subscription = new Subscription();
  subscriptionData: Subscription = new Subscription();

  constructor(
    private miscDataService: MiscDataService,
    public agentService: AgentDataService,
    public agentComService: AgentComService,
    public appComService: AppComService
  ) {}

  ngOnInit(): void {
    this.employmentHistoryForm = new FormGroup({
      employmentHistoryID: new FormControl({ value: '', disabled: true }),
      hireDate: new FormControl(null),
      rehireDate: new FormControl(null),
      notifiedTermDate: new FormControl(null),
      hrTermDate: new FormControl(null),
      hrTermCode: new FormControl(null),
      isForCause: new FormControl(null),
      backgroundCheckStatus: new FormControl(null),
      backGroundCheckNotes: new FormControl(null),
      isCurrent: new FormControl(null),
    });

    this.miscDataService
      .fetchBackgroundStatuses()
      .subscribe((backgroundStatuses: Array<{ lkpValue: string }>) => {
        this.backgroundStatuses = [{ lkpValue: 'N/A' }, ...backgroundStatuses];
      });

    this.subscriptionMode =
      this.agentComService.modeEmploymentHistChanged.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.subscriptionData =
              this.agentService.employmentTransferHistItemChanged.subscribe(
                (employmentHistory: any) => {
                  this.employmentHistoryForm.patchValue({
                    employmentHistoryID: employmentHistory.employmentHistoryID,
                    hireDate: formatDate(
                      employmentHistory.hireDate,
                      'yyyy-MM-dd',
                      'en-US'
                    ),
                    rehireDate: formatDate(
                      employmentHistory.rehireDate,
                      'yyyy-MM-dd',
                      'en-US'
                    ),
                    notifiedTermDate: formatDate(
                      employmentHistory.notifiedTermDate,
                      'yyyy-MM-dd',
                      'en-US'
                    ),
                    hrTermDate: formatDate(
                      employmentHistory.hrTermDate,
                      'yyyy-MM-dd',
                      'en-US'
                    ),
                    hrTermCode: employmentHistory.hrTermCode,
                    isForCause: employmentHistory.isForCause,
                    backgroundCheckStatus:
                      employmentHistory.backgroundCheckStatus,
                    backGroundCheckNotes:
                      employmentHistory.backGroundCheckNotes,
                    isCurrent: employmentHistory.isCurrent,
                  });
                }
              );
          } else {
            this.employmentHistoryForm.reset();
          }
        }
      );
  }

  onSubmit() {
    console.log(
      'EMFTest - (app-edit-employment-hist) onSubmit => \n',
      this.employmentHistoryForm.value
    );
  }

  ngOnDestroy(): void {
    this.subscriptionMode.unsubscribe();
    this.subscriptionData.unsubscribe();
  }
}
