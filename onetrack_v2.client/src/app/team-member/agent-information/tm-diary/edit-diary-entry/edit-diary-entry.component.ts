import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AgentComService, AgentDataService } from '../../../../_services';

@Component({
  selector: 'app-edit-diary-entry',
  templateUrl: './edit-diary-entry.component.html',
  styleUrl: './edit-diary-entry.component.css',
})
export class EditDiaryEntryComponent implements OnInit, OnDestroy {
  diaryItemForm!: FormGroup;
  subscriptionMode: Subscription = new Subscription();
  subscriptionData: Subscription = new Subscription();

  constructor(
    public agentService: AgentDataService,
    public agentComService: AgentComService
  ) {}

  ngOnInit(): void {
    this.diaryItemForm = new FormGroup({
      diaryID:new FormControl({ value: '', disabled: true }),
      soeid: new FormControl(null),
      diaryName: new FormControl(null),
      diaryDate: new FormControl(null),
      notes: new FormControl(null)
    });

    this.subscriptionMode =
      this.agentComService.modeDiaryChanged.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.subscriptionData =
              this.agentService.diaryEntryChanged.subscribe(
                (diaryItem: any) => {
                  this.diaryItemForm.patchValue({
                    diaryID: diaryItem.diaryID,
                    soeid: diaryItem.soeid,
                    diaryName: diaryItem.diaryName,
                    diaryDate: diaryItem.diaryDate,
                    notes: diaryItem.notes
                  });
                }
              );
          }
        }
      );
  }

  ngOnDestroy(): void {
    this.subscriptionMode.unsubscribe();
    this.subscriptionData.unsubscribe();
  }
}
