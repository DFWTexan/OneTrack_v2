import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AgentComService, AgentDataService } from '../../../../_services';

@Component({
  selector: 'app-edit-employment-hist',
  templateUrl: './edit-employment-hist.component.html',
  styleUrl: './edit-employment-hist.component.css',
})
@Injectable()
export class EditEmploymentHistComponent implements OnInit, OnDestroy {
  employmentHistoryForm!: FormGroup;
  subscriptionData: Subscription = new Subscription;
  subscriptionMode: Subscription = new Subscription;

  constructor(
    public agentService: AgentDataService,
    public agentComService: AgentComService
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

    this.subscriptionData = this.agentComService.modeEmploymentHistChanged.subscribe((mode: string) => {
      if (mode === 'EDIT') {
        this.subscriptionMode = this.agentService.employmentTransferHistItemChanged.subscribe(
          (employmentHistory: any) => {
            this.employmentHistoryForm.patchValue({
              employmentHistoryID: employmentHistory.employmentHistoryID,
              hireDate: employmentHistory.hireDate,
              rehireDate: employmentHistory.rehireDate,
              notifiedTermDate: employmentHistory.notifiedTermDate,
              hrTermDate: employmentHistory.hrTermDate,
              hrTermCode: employmentHistory.hrTermCode,
              isForCause: employmentHistory.isForCause,
              backgroundCheckStatus: employmentHistory.backgroundCheckStatus,
              backGroundCheckNotes: employmentHistory.backGroundCheckNotes,
              isCurrent: employmentHistory.isCurrent,
            });
          }
        );
      } else {
        this.employmentHistoryForm.reset();
      }
    });
  }

  onSubmit() {
    console.log('EMFTest - (app-edit-employment-hist) onSubmit => \n', this.employmentHistoryForm.value);
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
    this.subscriptionMode.unsubscribe();
  }
}