import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';
import { AgentComService, AgentDataService } from '../../../../../_services';

@Component({
  selector: 'app-edit-lic-pre-exam',
  templateUrl: './edit-lic-pre-exam.component.html',
  styleUrl: './edit-lic-pre-exam.component.css',
})
@Injectable()
export class EditLicPreExamComponent implements OnInit, OnDestroy {
  licPreExamForm!: FormGroup;
  subscriptionMode: Subscription = new Subscription();
  subscriptionData: Subscription = new Subscription();

  constructor(
    public agentDataService: AgentDataService,
    public agentComService: AgentComService
  ) {}

  ngOnInit(): void {
    this.licPreExamForm = new FormGroup({
      employeeLicensePreExamID: new FormControl({
        value: '',
        disabled: true,
      }),
      employeeLicenseID: new FormControl(null),
      status: new FormControl(null),
      examID: new FormControl(null),
      examName: new FormControl(null),
      examScheduleDate: new FormControl(null),
      examTakenDate: new FormControl(null),
      additionalNotes: new FormControl(null),
    });

    this.subscriptionMode =
      this.agentComService.modeLicPreExamChanged.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.subscriptionData =
            this.agentDataService.licensePreExamItemChanged.subscribe(
              (licPreExam: any) => {
                this.licPreExamForm.patchValue({
                  employeeLicensePreExamID: licPreExam.employeeLicensePreExamID,
                  employeeLicenseID: licPreExam.employeeLicenseID,
                  status: licPreExam.status,
                  examID: licPreExam.examID,
                  examName: licPreExam.examName,
                  examScheduleDate: formatDate(
                    licPreExam.examScheduleDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  examTakenDate: formatDate(
                    licPreExam.examTakenDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  additionalNotes: licPreExam.additionalNotes,
                });
              }
            );
        } else {
          this.licPreExamForm.reset();
        }
      });
  }

  onSubmit(): void { }

  ngOnDestroy(): void {
    this.subscriptionMode.unsubscribe();
    this.subscriptionData.unsubscribe();
  }
}
