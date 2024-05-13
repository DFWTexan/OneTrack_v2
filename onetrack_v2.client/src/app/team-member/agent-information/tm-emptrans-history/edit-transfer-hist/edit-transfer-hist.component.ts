import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import {
  AdminDataService,
  AgentComService,
  AgentDataService,
  AppComService,
  ConstantsDataService,
} from '../../../../_services';

@Component({
  selector: 'app-edit-transfer-hist',
  templateUrl: './edit-transfer-hist.component.html',
  styleUrl: './edit-transfer-hist.component.css',
})
@Injectable()
export class EditTransferHistComponent implements OnInit, OnDestroy {
  transferHistoryForm!: FormGroup;
  branchCodes: Array<{ branchCode: string }> = [];
  states: any[] = [];
  subscriptionMode: Subscription = new Subscription();
  subscriptionData: Subscription = new Subscription();

  constructor(
    private conService: ConstantsDataService,
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    public appComService: AppComService
  ) {}

  ngOnInit(): void {
    this.transferHistoryForm = new FormGroup({
      transferHistoryID: new FormControl({ value: '', disabled: true }),
      branchCode: new FormControl(null),
      workStateAbv: new FormControl(null),
      resStateAbv: new FormControl(null),
      transferDate: new FormControl(null),
      state: new FormControl(null),
      isCurrent: new FormControl(null),
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
                this.transferHistoryForm.patchValue({
                  transferHistoryID: transferHistory.transferHistoryID,
                  branchCode: transferHistory.branchCode,
                  workStateAbv: transferHistory.workStateAbv,
                  resStateAbv: transferHistory.resStateAbv,
                  transferDate: formatDate(
                    transferHistory.transferDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  state: transferHistory.state,
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
    console.log(
      'EMFTest - (app-edit-employment-hist) onSubmit => \n',
      this.transferHistoryForm.value
    );
  }

  closeModal() {
    const modalDiv = document.getElementById('modal-edit-tran-history');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {
    this.subscriptionMode.unsubscribe();
    this.subscriptionData.unsubscribe();
  }
}
