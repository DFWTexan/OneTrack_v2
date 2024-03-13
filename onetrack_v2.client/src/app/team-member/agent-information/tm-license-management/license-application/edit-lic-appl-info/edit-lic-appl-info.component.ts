import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import { AgentComService, AgentDataService } from '../../../../../_services';

@Component({
  selector: 'app-edit-lic-appl-info',
  templateUrl: './edit-lic-appl-info.component.html',
  styleUrl: './edit-lic-appl-info.component.css',
})
@Injectable()
export class EditLicApplInfoComponent implements OnInit, OnDestroy {
  licApplicationForm!: FormGroup;
  subscriptionMode: Subscription = new Subscription();
  subscriptionData: Subscription = new Subscription();

  constructor(
    public agentService: AgentDataService,
    public agentComService: AgentComService
  ) {}

  ngOnInit(): void {
    this.licApplicationForm = new FormGroup({
      licenseApplicationID: new FormControl({ value: '', disabled: true }),
      sentToAgentDate: new FormControl(null),
      recFromAgentDate: new FormControl(null),
      sentToStateDate: new FormControl(null),
      recFromStateDate: new FormControl(null),
      applicationStatus: new FormControl(null),
      applicationType: new FormControl(null),
    });

    this.subscriptionMode =
      this.agentComService.modeLicAppInfoChanged.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.subscriptionData =
            this.agentService.licenseApplicationItemChanged.subscribe(
              (licApplication: any) => {
                this.licApplicationForm.patchValue({
                  licenseApplicationID: licApplication.licenseApplicationID,
                  sentToAgentDate: formatDate(
                    licApplication.sentToAgentDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  recFromAgentDate: formatDate(
                    licApplication.recFromAgentDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  sentToStateDate: formatDate(
                    licApplication.sentToStateDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  recFromStateDate: formatDate(
                    licApplication.recFromStateDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  applicationStatus: licApplication.applicationStatus,
                  applicationType: licApplication.applicationType,
                });
              }
            );
        } else {
          this.licApplicationForm.reset();
        }
      });
  }

  onSubmit() {
    // ...
  }

  ngOnDestroy() {
    this.subscriptionMode.unsubscribe();
    this.subscriptionData.unsubscribe();
  }
}
