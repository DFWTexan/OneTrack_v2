import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AgentComService, AgentDataService } from '../../../../../_services';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-edit-diary-entry',
  templateUrl: './edit-diary-entry.component.html',
  styleUrl: './edit-diary-entry.component.css',
})
export class EditDiaryEntryComponent implements OnInit, OnDestroy {
  diaryItemForm!: FormGroup;

  private subscriptions = new Subscription();

  constructor(
    public agentService: AgentDataService,
    public agentComService: AgentComService
  ) {}

  ngOnInit(): void {
    this.diaryItemForm = new FormGroup({
      diaryID: new FormControl({ value: '', disabled: true }),
      soeid: new FormControl({ value: '', disabled: true }),
      diaryName: new FormControl({ value: '', disabled: true }),
      diaryDate: new FormControl({ value: '', disabled: true }),
      notes: new FormControl(null),
    });

    this.subscriptions.add(
      this.agentComService.modeDiaryChanged.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.subscriptions.add(
            this.agentService.diaryEntryChanged.subscribe((diaryItem: any) => {
              this.diaryItemForm.patchValue({
                diaryID: diaryItem.diaryID,
                soeid: diaryItem.soeid,
                diaryName: diaryItem.diaryName,
                diaryDate: formatDate(
                  diaryItem.diaryDate,
                  'yyyy-MM-dd',
                  'en-US'
                ),
                notes: diaryItem.notes,
              });
            })
          );
        } else {
          this.diaryItemForm.reset();
          this.diaryItemForm.patchValue({
            // soeid: this.agentService.agentInfo.soeid,
            // diaryName: this.agentService.agentInfo.agentName,
            diaryDate: formatDate(new Date(), 'yyyy-MM-dd', 'en-US'),
          });
        }
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
