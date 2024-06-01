import { Component, Injectable, OnInit, OnDestroy, Input } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import {
  AgentComService,
  AgentDataService,
  AppComService,
  UserAcctInfoDataService,
} from '../../../../../_services';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-edit-hours-taken',
  templateUrl: './edit-hours-taken.component.html',
  styleUrl: './edit-hours-taken.component.css',
})
@Injectable()
export class EditHoursTakenComponent implements OnInit, OnDestroy {
  isFormSubmitted: boolean = false;
  contEduCompletedItemForm!: FormGroup;
  @Input() employmentID: number = 0;
  @Input() employeeID: number = 0;

  private subscriptions = new Subscription();

  constructor(
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.contEduCompletedItemForm = new FormGroup({
      employeeEducationID: new FormControl({ value: '', disabled: true }),
      educationName: new FormControl(null),
      contEducationRequirementID: new FormControl(null),
      contEducationTakenDate: new FormControl(null),
      creditHoursTaken: new FormControl(null),
      additionalNotes: new FormControl(null),
    });

    this.subscriptions.add(
      this.agentComService.modeContEduHoursTakenChanged.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.subscriptions.add(
              this.agentDataService.contEduHoursTakenChanged.subscribe(
                (contEduCompletedItem: any) => {
                  this.contEduCompletedItemForm.patchValue({
                    employeeEducationID:
                      contEduCompletedItem.employeeEducationID,
                    educationName: contEduCompletedItem.educationName,
                    contEducationRequirementID:
                      contEduCompletedItem.contEducationRequirementID,
                    contEducationTakenDate: formatDate(
                      contEduCompletedItem.contEducationTakenDate,
                      'yyyy-MM-dd',
                      'en-US'
                    ),
                    creditHoursTaken: contEduCompletedItem.creditHoursTaken
                      ? parseFloat(
                          contEduCompletedItem.creditHoursTaken
                        ).toFixed(2)
                      : null,
                    additionalNotes: contEduCompletedItem.additionalNotes,
                  });
                }
              )
            );
          } else {
            this.contEduCompletedItemForm.reset();
            this.contEduCompletedItemForm.patchValue({
              educationName: 'CE CLASS',
              contEducationTakenDate: null,
              creditHoursTaken: 0,
            });
          }
        }
      )
    );
  }

  onSubmit(): void {
    let contEduCompletedItem = this.contEduCompletedItemForm.value;
    contEduCompletedItem.employmentID = this.employmentID;
    contEduCompletedItem.employeeID = this.employeeID;

    contEduCompletedItem.userSOEID =
      this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.agentComService.modeContEduHoursTaken === 'INSERT') {
      contEduCompletedItem.ContEducationID = 0;
    }
    this.subscriptions.add(
      this.agentDataService
        .upsertContEduHoursTaken(contEduCompletedItem)
        .subscribe({
          next: (response) => {
            this.isFormSubmitted = true;
            this.onCloseModal();
          },
          error: (error) => {
            console.error(error);
            // handle the error here
          },
        })
    );
  }

  onCloseModal() {
    // const modalDiv = document.getElementById('modal-edit-edu-hours-taken');
    // if (modalDiv != null) {
    //   modalDiv.style.display = 'none';
    // }

    if (this.contEduCompletedItemForm.dirty && !this.isFormSubmitted) {
      if (
        confirm('You have unsaved changes. Are you sure you want to close?')
      ) {
        const modalDiv = document.getElementById('modal-edit-edu-hours-taken');
        if (modalDiv != null) {
          modalDiv.style.display = 'none';
        }
        this.contEduCompletedItemForm.reset();
      }
    } else {
      this.isFormSubmitted = false;
      const modalDiv = document.getElementById('modal-edit-edu-hours-taken');
      if (modalDiv != null) {
        modalDiv.style.display = 'none';
      }
    }
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
