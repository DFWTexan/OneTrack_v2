import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AgentComService, AgentDataService, AppComService } from '../../../../_services';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-edit-hours-taken',
  templateUrl: './edit-hours-taken.component.html',
  styleUrl: './edit-hours-taken.component.css',
})
@Injectable()
export class EditHoursTakenComponent implements OnInit, OnDestroy {
  contEduCompletedItemForm!: FormGroup;
  subscriptionMode: Subscription = new Subscription();
  subscriptionData: Subscription = new Subscription();

  constructor(
    public agentService: AgentDataService,
    public agentComService: AgentComService,
    public appComService: AppComService
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

    this.subscriptionMode =
      this.agentComService.modeContEduHoursTakenChanged.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.subscriptionData =
              this.agentService.contEduHoursTakenChanged.subscribe(
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
      );
  }

  onFormSubmit(): void {}

  ngOnDestroy(): void {
    this.subscriptionMode.unsubscribe();
    this.subscriptionData.unsubscribe();
  }
}
