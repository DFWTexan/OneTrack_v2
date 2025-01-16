import { Component, Injectable, OnInit, OnDestroy, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import {
  AdminDataService,
  AgentComService,
  AgentDataService,
  AppComService,
  ConstantsDataService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../../../_services';

@Component({
  selector: 'app-edit-transfer-hist',
  templateUrl: './edit-transfer-hist.component.html',
  styleUrl: './edit-transfer-hist.component.css',
})
@Injectable()
export class EditTransferHistComponent implements OnInit, OnDestroy {
  isFormSubmitted: boolean = false;
  transferHistoryForm!: FormGroup;
  @Input() employmentID: number = 0;
  @Input() employeeID: number = 0;
  transferHistoryID: number = 0;
  branchCodes: Array<{ branchCode: string }> = [];
  states: any[] = [];

  private subscriptions = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.transferHistoryForm = new FormGroup({
      transferHistoryID: new FormControl({ value: '', disabled: true }),
      branchCode: new FormControl('', Validators.required),
      workStateAbv: new FormControl(null),
      resStateAbv: new FormControl(null),
      transferDate: new FormControl(null),
      isCurrent: new FormControl(false),
    });
    this.states = ['Select', ...this.conService.getStates()];
    this.agentDataService.fetchBranchCodes().subscribe((response) => {
      this.branchCodes = [{ branchCode: 'Select' }, ...response];
    });

    this.subscriptions.add(
      this.agentComService.modeTransferHistChanged.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.subscriptions.add(
            this.agentDataService.transferHistItemChanged.subscribe(
              (transferHistory: any) => {
                this.transferHistoryID = transferHistory.transferHistoryID;
                this.transferHistoryForm.patchValue({
                  transferHistoryID: transferHistory.transferHistoryID,
                  branchCode: transferHistory.branchCode,
                  workStateAbv: transferHistory.workStateAbv,
                  resStateAbv: transferHistory.resStateAbv,
                  transferDate: transferHistory.transferDate
                    ? formatDate(
                        transferHistory.transferDate,
                        'yyyy-MM-dd',
                        'en-US'
                      )
                    : null,
                  isCurrent: transferHistory.isCurrent,
                });
              }
            )
          );
        } else {
          this.transferHistoryForm.reset({
            branchCode: 'Select',
            workStateAbv: 'Select',
            resStateAbv: 'Select',
          });
        }
      })
    );
  }

  onSubmit() {
    let transferHistItem = this.transferHistoryForm.value;
    transferHistItem.employmentID = this.employmentID;
    transferHistItem.employeeID = this.employeeID;
    transferHistItem.transferHistoryID = this.transferHistoryID;
    transferHistItem.isCurrent = transferHistItem.isCurrent ? true : false;
    transferHistItem.userSOEID =
      this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.transferHistoryForm.invalid) {
      this.transferHistoryForm.setErrors({ invalid: true });
      return;
    }

    if (this.agentComService.modeTransferHist === 'INSERT') {
      transferHistItem.transferHistoryID = 0;
    }

    if (
      transferHistItem.branchCode === 'Select' ||
      transferHistItem.branchCode === '' ||
      transferHistItem.branchCode === null
    ) {
      this.transferHistoryForm.controls['branchCode'].setErrors({
        required: true,
      });
    }

    if (
      transferHistItem.workStateAbv === 'Select' ||
      transferHistItem.workStateAbv === '' ||
      transferHistItem.workStateAbv === null
    ) {
      this.transferHistoryForm.controls['workStateAbv'].setErrors({
        required: true,
      });
    }

    if (
      transferHistItem.resStateAbv === 'Select' ||
      transferHistItem.resStateAbv === '' ||
      transferHistItem.resStateAbv === null
    ) {
      this.transferHistoryForm.controls['resStateAbv'].setErrors({
        required: true,
      });
    }

    if (
      transferHistItem.transferDate === '' ||
      transferHistItem.transferDate === null
    ) {
      this.transferHistoryForm.controls['transferDate'].setErrors({
        required: true,
      });
    }

    if (!this.transferHistoryForm.valid) {
      this.transferHistoryForm.setErrors({ invalid: true });
      return;
    }

    this.subscriptions.add(
      this.agentDataService.upsertTransferHistItem(transferHistItem).subscribe({
        next: (response) => {
          this.isFormSubmitted = true;
          this.onCloseModal();
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
          }
        },
      })
    );
  }

  onCloseModal() {
    // const modalDiv = document.getElementById('modal-edit-tran-history');
    // if (modalDiv != null) {
    //   modalDiv.style.display = 'none';
    // }
    if (this.transferHistoryForm.dirty && !this.isFormSubmitted) {
      if (
        confirm('You have unsaved changes. Are you sure you want to close?')
      ) {
        const modalDiv = document.getElementById('modal-edit-tran-history');
        if (modalDiv != null) {
          modalDiv.style.display = 'none';
        }
        this.transferHistoryForm.reset();
      }
    } else {
      this.isFormSubmitted = false;
      const modalDiv = document.getElementById('modal-edit-tran-history');
      if (modalDiv != null) {
        modalDiv.style.display = 'none';
      }
    }
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
