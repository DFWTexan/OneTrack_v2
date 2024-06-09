import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import { AgentComService, AgentDataService, AppComService, ConstantsDataService, ErrorMessageService } from '../../../../../_services';

@Component({
  selector: 'app-edit-lic-appl-info',
  templateUrl: './edit-lic-appl-info.component.html',
  styleUrl: './edit-lic-appl-info.component.css',
})
@Injectable()
export class EditLicApplInfoComponent implements OnInit, OnDestroy {
  isFormSubmitted: boolean = false;
  licApplicationForm!: FormGroup;
  applicationStatuses: string [] = ['Select', ...this.constDataService.getApplicationStatuses()];

  private subscriptions = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    private constDataService: ConstantsDataService,
    public agentService: AgentDataService,
    public agentComService: AgentComService,
    public appComService: AppComService,
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

    this.subscriptions.add(
      this.agentComService.modeLicAppInfoChanged.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.subscriptions.add(
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
            )
          );
        } else {
          this.licApplicationForm.reset();
          this.licApplicationForm.patchValue({
            applicationStatus: 'Select',
            applicationType: 'Select',
          });
        }
      })
    );
  }

  onSubmit() {
    // ...
  }

  onCloseModal() {
    const modalDiv = document.getElementById('modal-edit-lic-appl-info');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
    // if (this.licApplicationForm.dirty && !this.isFormSubmitted) {
    //   if (
    //     confirm('You have unsaved changes. Are you sure you want to close?')
    //   ) {
    //     const modalDiv = document.getElementById('modal-edit-lic-appl-info');
    //     if (modalDiv != null) {
    //       modalDiv.style.display = 'none';
    //     }
    //     this.licApplicationForm.reset();
    //     this.licApplicationForm.patchValue({
    //       backgroundCheckStatus: 'Pending',
    //       isCurrent: true,
    //     });
    //   }
    // } else {
    //   this.isFormSubmitted = false;
    //   const modalDiv = document.getElementById('modal-edit-lic-appl-info');
    //   if (modalDiv != null) {
    //     modalDiv.style.display = 'none';
    //   }
    // }
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
