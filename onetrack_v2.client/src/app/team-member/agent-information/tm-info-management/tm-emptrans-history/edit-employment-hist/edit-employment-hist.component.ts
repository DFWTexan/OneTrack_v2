import { Component, Injectable, OnInit, OnDestroy, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import {
  AgentComService,
  AgentDataService,
  AppComService,
  ErrorMessageService,
  MiscDataService,
  UserAcctInfoDataService,
} from '../../../../../_services';

@Component({
  selector: 'app-edit-employment-hist',
  templateUrl: './edit-employment-hist.component.html',
  styleUrl: './edit-employment-hist.component.css',
})
@Injectable()
export class EditEmploymentHistComponent implements OnInit, OnDestroy {
  isFormSubmitted: boolean = false;
  employmentHistoryForm!: FormGroup;
  @Input() employmentID: number = 0;
  @Input() employeeID: number = 0;
  employmentHistoryID: number = 0;
  backgroundStatuses: Array<{ lkpValue: string }> = [];
  
  private subscriptions = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    private miscDataService: MiscDataService,
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.employmentHistoryForm = new FormGroup({
      employmentHistoryID: new FormControl({ value: '', disabled: true }),
      employmentID: new FormControl({ value: '', disabled: true }),
      employeeID: new FormControl({ value: '', disabled: true }),
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

    this.subscriptions.add(
      this.miscDataService
      .fetchBackgroundStatuses()
      .subscribe((backgroundStatuses: Array<{ lkpValue: string }>) => {
        this.backgroundStatuses = [{ lkpValue: 'N/A' }, ...backgroundStatuses];
      })
    );
    
    this.subscriptions.add(
      this.agentComService.modeEmploymentHistChanged.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.subscriptions.add(
              this.agentDataService.employmentTransferHistItemChanged.subscribe(
                (employmentHistory: any) => {
                  this.employmentHistoryID =
                    employmentHistory.employmentHistoryID;
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
              )
            );
          } else {
            this.employmentHistoryForm.reset();
            this.employmentHistoryForm.patchValue({
              backgroundCheckStatus: 'Pending',
              isCurrent: true,
            });
          }
        }
      )
    );

    this.subscriptions.add(
      this.userAcctInfoDataService.userAcctInfoChanged.subscribe(
        (userAcctInfo) => {
          if (userAcctInfo.soeid) {
            this.userAcctInfoDataService.userAcctInfo = userAcctInfo;
          }
        }
      )
    );
  }

  onSubmit() {
    let empHistItem: any = this.employmentHistoryForm.value;
    empHistItem.EmployeeID = this.employeeID;
    empHistItem.EmploymentID = this.employmentID;
    empHistItem.EmploymentHistoryID = this.employmentHistoryID;
    empHistItem.UserSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.agentComService.modeEmploymentHist === 'INSERT') {
      empHistItem.EmploymentHistoryID = 0;
    }

    this.subscriptions.add(
      this.agentDataService.upsertEmploymentHistItem(empHistItem).subscribe({
        next: (response) => {
          this.isFormSubmitted = true;
          this.closeModal();
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
          }
        },
      })
    );
  }

  alphaNumericOnly(event: any) {
    const pattern = /^[a-zA-Z0-9]*$/; // alphanumeric pattern
    let inputChar = String.fromCharCode(event.charCode);

    if (!pattern.test(inputChar)) {
      // invalid character, prevent input
      event.preventDefault();
    }
  }

  closeModal() {
    // const modalDiv = document.getElementById('modal-edit-emp-history');
    // if (modalDiv != null) {
    //   modalDiv.style.display = 'none';
    // }
    if (this.employmentHistoryForm.dirty && !this.isFormSubmitted) {
      if (
        confirm('You have unsaved changes. Are you sure you want to close?')
      ) {
        const modalDiv = document.getElementById('modal-edit-emp-history');
        if (modalDiv != null) {
          modalDiv.style.display = 'none';
        }
        this.employmentHistoryForm.reset();
        this.employmentHistoryForm.patchValue({
          backgroundCheckStatus: 'Pending',
          isCurrent: true,
        });
      }
    } else {
      this.isFormSubmitted = false;
      const modalDiv = document.getElementById('modal-edit-emp-history');
      if (modalDiv != null) {
        modalDiv.style.display = 'none';
      }
    }
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
