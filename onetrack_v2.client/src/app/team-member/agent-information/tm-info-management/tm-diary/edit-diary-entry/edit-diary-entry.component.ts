import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import {
  AgentComService,
  AgentDataService,
  ErrorMessageService,
} from '../../../../../_services';
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
    private errorMessageService: ErrorMessageService,
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

  onSubmit() {
    // if (error.error && error.error.errMessage) {
    //   this.errorMessageService.setErrorMessage(error.error.errMessage);
    // }
  }

  onCloseModal() {
    const modalDiv = document.getElementById('modal-edit-diary-entry');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }

    // if (this.contEduCompletedItemForm.dirty && !this.isFormSubmitted) {
    //   if (
    //     confirm('You have unsaved changes. Are you sure you want to close?')
    //   ) {
    //     const modalDiv = document.getElementById('modal-edit-diary-entry');
    //     if (modalDiv != null) {
    //       modalDiv.style.display = 'none';
    //     }
    //     this.contEduCompletedItemForm.reset();
    //   }
    // } else {
    //   this.isFormSubmitted = false;
    //   const modalDiv = document.getElementById('modal-edit-diary-entry');
    //   if (modalDiv != null) {
    //     modalDiv.style.display = 'none';
    //   }
    // }
  }
  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
