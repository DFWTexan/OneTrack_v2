import { Component, Injectable, OnInit, OnDestroy, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import {
  AdminDataService,
  AgentComService,
  AgentDataService,
  AppComService,
  ConstantsDataService,
  UserAcctInfoDataService,
} from '../../../../_services';

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
  subscriptionMode: Subscription = new Subscription();
  subscriptionData: Subscription = new Subscription();

  constructor(
    private conService: ConstantsDataService,
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.transferHistoryForm = new FormGroup({
      transferHistoryID: new FormControl({ value: '', disabled: true }),
      branchCode: new FormControl(null),
      workStateAbv: new FormControl(null),
      resStateAbv: new FormControl(null),
      transferDate: new FormControl(null),
      isCurrent: new FormControl(false),
    });
    this.states = ['Select', ...this.conService.getStates()];
    this.agentDataService.fetchBranchCodes().subscribe((response) => {
      this.branchCodes = [{ branchCode: 'Select' }, ...response];
    });

    this.subscriptionMode =
      this.agentComService.modeTransferHistChanged.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.subscriptionData =
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
            );
        } else {
          this.transferHistoryForm.reset();
        }
      });
  }

  onSubmit() {
    let transferHistItem = this.transferHistoryForm.value;
    transferHistItem.employmentID = this.employmentID;
    transferHistItem.employeeID = this.employeeID;
    transferHistItem.transferHistoryID = this.transferHistoryID;
    transferHistItem.isCurrent = transferHistItem.isCurrent ? true : false;
    transferHistItem.userSOEID =
      this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.agentComService.modeTransferHist === 'INSERT') {
      transferHistItem.transferHistoryID = 0;
    }

    this.agentDataService.upsertTransferHistItem(transferHistItem).subscribe({
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
    this.subscriptionMode.unsubscribe();
    this.subscriptionData.unsubscribe();
  }
}
