import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import { AgentComService, AgentDataService } from '../../../../../_services';

@Component({
  selector: 'app-edit-license-renewal',
  templateUrl: './edit-license-renewal.component.html',
  styleUrl: './edit-license-renewal.component.css',
})
@Injectable()
export class EditLicenseRenewalComponent implements OnInit, OnDestroy {
  licRenewalForm!: FormGroup;
  subscriptionMode: Subscription = new Subscription();
  subscriptionData: Subscription = new Subscription();

  constructor(
    public agentDataService: AgentDataService,
    public agentComService: AgentComService
  ) {}

  ngOnInit(): void {
    this.licRenewalForm = new FormGroup({
      licenseApplicationID: new FormControl({ value: '', disabled: true }),
      employeeLicenseID: new FormControl({ value: '', disabled: true }),
      sentToAgentDate: new FormControl(null),
      recFromAgentDate: new FormControl(null),
      sentToStateDate: new FormControl(null),
      recFromStateDate: new FormControl(null),
      applicationStatus: new FormControl(null),
      applicationType: new FormControl(null),
      renewalDate: new FormControl(null),
      renewalMethod: new FormControl(null),
    });

    this.subscriptionMode =
      this.agentComService.modeLicRenewalChanged.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.subscriptionData =
            this.agentDataService.licenseRenewalItemChanged.subscribe(
              (licRenewal: any) => {
                this.licRenewalForm
                .patchValue({
                  licenseApplicationID: licRenewal.licenseApplicationID,
                  employeeLicenseID: licRenewal.employeeLicenseID,
                  sentToAgentDate: formatDate(
                    licRenewal.sentToAgentDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  recFromAgentDate: formatDate(
                    licRenewal.recFromAgentDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  sentToStateDate: formatDate(
                    licRenewal.sentToStateDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  recFromStateDate: formatDate(
                    licRenewal.recFromStateDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  applicationStatus: licRenewal.applicationStatus,
                  applicationType: licRenewal.applicationType,
                  renewalDate: formatDate(
                    licRenewal.renewalDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  renewalMethod: licRenewal.renewalMethod,
                });
              }
            );
        } else {
          this.licRenewalForm.reset();
        }
      });
  }

  onSubmit() {}

  ngOnDestroy(): void {
    this.subscriptionMode.unsubscribe();
    this.subscriptionData.unsubscribe();
  }
}
