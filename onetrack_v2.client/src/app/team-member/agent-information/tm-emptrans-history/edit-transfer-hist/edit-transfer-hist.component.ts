import { Component, Injectable, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

import { AgentComService, AgentDataService } from '../../../../_services';

@Component({
  selector: 'app-edit-transfer-hist',
  templateUrl: './edit-transfer-hist.component.html',
  styleUrl: './edit-transfer-hist.component.css'
})
@Injectable()
export class EditTransferHistComponent implements OnInit {
  transferHistoryForm!: FormGroup;

  constructor(
    public agentService: AgentDataService,
    public agentComService: AgentComService
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

    this.agentComService.modeTransferHistChanged.subscribe((mode: string) => {
      if (mode === 'EDIT') {
        this.agentService.transferHistItemChanged.subscribe(
          (transferHistory: any) => {
            this.transferHistoryForm.patchValue({
              transferHistoryID: transferHistory.transferHistoryID,
              branchCode: transferHistory.branchCode,
              workStateAbv: transferHistory.workStateAbv,
              resStateAbv: transferHistory.resStateAbv,
              transferDate: transferHistory.transferDate,
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
    console.log('EMFTest - (app-edit-employment-hist) onSubmit => \n', this.transferHistoryForm.value);
  }
}
