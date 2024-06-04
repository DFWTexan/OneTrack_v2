import { Component, Injectable, Input, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import {
  AgentComService,
  AgentDataService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../../../_services';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-edit-diary-entry',
  templateUrl: './edit-diary-entry.component.html',
  styleUrl: './edit-diary-entry.component.css',
})
export class EditDiaryEntryComponent implements OnInit, OnDestroy {
  isFormSubmitted: boolean = false;
  diaryItemForm!: FormGroup;
  diaryID: number = 0;
  @Input() employmentID: number = 0;
  @Input() employeeID: number = 0;

  private subscriptions = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    private userAcctInfoDataService: UserAcctInfoDataService
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
            this.agentDataService.diaryEntryChanged.subscribe(
              (diaryItem: any) => {
                this.diaryID = diaryItem.diaryID;
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
              }
            )
          );
        } else {
          this.diaryItemForm.reset();
          this.diaryItemForm.patchValue({
            soeid: this.userAcctInfoDataService.userAcctInfo.soeid,
            // diaryName: this.agentService.agentInfo.agentName,
            // diaryDate: formatDate(new Date(), 'yyyy-MM-dd', 'en-US'),
            diaryDate: formatDate(new Date(), 'MM/dd/yyyy - h:mm a', 'en-US'),
          });
        }
      })
    );
  }

  onSubmit(): void {
    let diaryItem = this.diaryItemForm.value;
    diaryItem.diaryID = this.diaryID;
    diaryItem.employmentID = this.employmentID;
    diaryItem.employeeID = this.employeeID;
    diaryItem.diaryDate = new Date(diaryItem.diaryDate);
    diaryItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;
    
    if (this.diaryItemForm.invalid) {
      this.diaryItemForm.setErrors({ invalid: true });
      return;
    }

    if (this.agentComService.modeContEduHoursTaken === 'INSERT') {
      diaryItem.diaryID = 0;
    }

    this.subscriptions.add(
      this.agentDataService.upsertDiaryEntry(diaryItem).subscribe({
        next: (response) => {
          this.isFormSubmitted = true;
          this.onCloseModal();
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
          }

          const modalDiv = document.getElementById(
            'modal-edit-edu-hours-taken'
          );
          if (modalDiv != null) {
            modalDiv.style.display = 'none';
          }
        },
      })
    );
  }

  onCloseModal() {
    // const modalDiv = document.getElementById('modal-edit-diary-entry');
    // if (modalDiv != null) {
    //   modalDiv.style.display = 'none';
    // }

    if (this.diaryItemForm.dirty && !this.isFormSubmitted) {
      if (
        confirm('You have unsaved changes. Are you sure you want to close?')
      ) {
        const modalDiv = document.getElementById('modal-edit-diary-entry');
        if (modalDiv != null) {
          modalDiv.style.display = 'none';
        }
        this.diaryItemForm.reset();
      }
    } else {
      this.isFormSubmitted = false;
      const modalDiv = document.getElementById('modal-edit-diary-entry');
      if (modalDiv != null) {
        modalDiv.style.display = 'none';
      }
    }
  }
  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
