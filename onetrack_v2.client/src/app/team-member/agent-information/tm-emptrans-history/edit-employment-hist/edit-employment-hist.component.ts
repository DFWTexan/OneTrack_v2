import { Component, Injectable, OnInit, OnDestroy, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import {
  AgentComService,
  AgentDataService,
  AppComService,
  MiscDataService,
  UserAcctInfoDataService,
} from '../../../../_services';

@Component({
  selector: 'app-edit-employment-hist',
  templateUrl: './edit-employment-hist.component.html',
  styleUrl: './edit-employment-hist.component.css',
})
@Injectable()
export class EditEmploymentHistComponent implements OnInit, OnDestroy {
  employmentHistoryForm!: FormGroup;
  @Input()employmentID: number = 0;
  @Input()employeeID: number = 0;
  backgroundStatuses: Array<{ lkpValue: string }> = [];
  subscriptionMode: Subscription = new Subscription();
  subscriptionData: Subscription = new Subscription();

  constructor(
    private miscDataService: MiscDataService,
    public agentService: AgentDataService,
    public agentComService: AgentComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.employmentHistoryForm = new FormGroup({
      employmentHistoryID: new FormControl({ value: '', disabled: true }),
      employmentID : new FormControl({ value: '', disabled: true }),
      employeeID : new FormControl({ value: '', disabled: true }),
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
                    hireDate: employmentHistory.hireDate
                      ? formatDate(
                          employmentHistory.hireDate,
                          'yyyy-MM-dd',
                          'en-US'
                        )
                      : null,
                    rehireDate: employmentHistory.rehireDate
                      ? formatDate(
                          employmentHistory.rehireDate,
                          'yyyy-MM-dd',
                          'en-US'
                        )
                      : null,
                    notifiedTermDate: employmentHistory.notifiedTermDate
                      ? formatDate(
                          employmentHistory.notifiedTermDate,
                          'yyyy-MM-dd',
                          'en-US'
                        )
                      : null,
                    hrTermDate: employmentHistory.hrTermDate
                      ? formatDate(
                          employmentHistory.hrTermDate,
                          'yyyy-MM-dd',
                          'en-US'
                        )
                      : null,
                    hrTermCode: employmentHistory.hrTermCode,
                    isForCause: employmentHistory.isForCause,
                    backgroundCheckStatus:
                      employmentHistory.backgroundCheckStatus,
                    backGroundCheckNotes:
                      employmentHistory.backGroundCheckNotes,
                    isCurrent: employmentHistory.isCurrent,
                    employmentID: employmentHistory.employmentID,
                    employeeID: employmentHistory.employeeID,
                  });
                }
              );
          } else {
            this.employmentHistoryForm.reset();
            this.employmentHistoryForm.patchValue({
              backgroundCheckStatus: 'Pending',
              isCurrent: true,
            });
          }
        }
      );
  }

  onSubmit() {
    let empHistItem: any = this.employmentHistoryForm.value;
    empHistItem.UserSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;
   
    if (this.agentComService.modeEmploymentHist === 'INSERT') {
      empHistItem.EmploymentHistoryID = 0;
      empHistItem.EmployeeID = this.employeeID;
      empHistItem.EmploymentID = this.employmentID;
    }

    this.agentService.upsertEmploymentHistItem(empHistItem).subscribe({
      next: (response) => {
        console.log(response);
        // handle the response here
      },
      error: (error) => {
        console.error(error);
        // handle the error here
      },
    });
  }

  closeModal() {
    const modalDiv = document.getElementById('modal-edit-emp-history');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {
    this.subscriptionMode.unsubscribe();
    this.subscriptionData.unsubscribe();
  }
}
