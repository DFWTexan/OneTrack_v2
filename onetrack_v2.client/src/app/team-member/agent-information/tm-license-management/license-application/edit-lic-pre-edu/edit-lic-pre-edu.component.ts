import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import { AgentComService, AgentDataService } from '../../../../../_services';

@Component({
  selector: 'app-edit-lic-pre-edu',
  templateUrl: './edit-lic-pre-edu.component.html',
  styleUrl: './edit-lic-pre-edu.component.css',
})
@Injectable()
export class EditLicPreEduComponent implements OnInit, OnDestroy {
  licPreEduForm!: FormGroup;
  subscriptionMode: Subscription = new Subscription();
  subscriptionData: Subscription = new Subscription();

  constructor(
    public agentService: AgentDataService,
    public agentComService: AgentComService
  ) {}

  ngOnInit(): void {
    this.licPreEduForm = new FormGroup({
      employeeLicensePreEducationID: new FormControl({
        value: '',
        disabled: true,
      }),
      status: new FormControl(null),
      educationStartDate: new FormControl(null),
      educationEndDate: new FormControl(null),
      preEducationID: new FormControl(null),
      companyID: new FormControl(null),
      educationName: new FormControl(null),
      employeeLicenseID: new FormControl(null),
      additionalNotes: new FormControl(null),
    });

    this.subscriptionMode = this.agentComService.modeLicPreEduChanged.subscribe(
      (mode: string) => {
        if (mode === 'EDIT') {
          this.subscriptionData =
            this.agentService.licensePreEducationItemChanged.subscribe(
              (licPreEdu: any) => {
                this.licPreEduForm.patchValue({
                  employeeLicensePreEducationID:
                    licPreEdu.employeeLicensePreEducationID,
                  status: licPreEdu.status,
                  educationStartDate: formatDate(
                    licPreEdu.educationStartDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  educationEndDate: formatDate(
                    licPreEdu.educationEndDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  preEducationID: licPreEdu.preEducationID,
                  companyID: licPreEdu.companyID,
                  educationName: licPreEdu.educationName,
                  employeeLicenseID: licPreEdu.employeeLicenseID,
                  additionalNotes: licPreEdu.additionalNotes,
                });
              }
            );
        } else {
          this.licPreEduForm.reset();
        }
      }
    );
  }

  onSubmit(): void {
    // ...
  }

  ngOnDestroy(): void {
    this.subscriptionMode.unsubscribe();
    this.subscriptionData.unsubscribe();
  }

}
