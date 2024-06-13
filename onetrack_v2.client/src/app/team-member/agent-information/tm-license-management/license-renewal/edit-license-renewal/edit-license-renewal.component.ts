import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import { AgentComService, AgentDataService, ConstantsDataService, ErrorMessageService, UserAcctInfoDataService } from '../../../../../_services';

@Component({
  selector: 'app-edit-license-renewal',
  templateUrl: './edit-license-renewal.component.html',
  styleUrl: './edit-license-renewal.component.css',
})
@Injectable()
export class EditLicenseRenewalComponent implements OnInit, OnDestroy {
  isFormSubmitted: boolean = false;
  licRenewalForm!: FormGroup;
  renewalMethods: string[] = [];

  private subscriptions = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    private userInfoDataService: UserAcctInfoDataService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.licRenewalForm = this.fb.group({
      licenseApplicationID: [],
      employeeLicenseID: new FormControl({ value: '', disabled: true }),
      renewalMethod: [null],
      sentToAgentDate: [null],
      recFromAgentDate: new FormControl(null),
      sentToStateDate: [null],
      recFromStateDate: new FormControl(null),
      renewalDate: [null],
    });

    this.renewalMethods = ['Select', ...this.conService.getLicenseRenewalMethods()];
    this.subscriptions.add(
      this.agentComService.modeLicRenewalChanged.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.subscriptions.add(
            this.agentDataService.licenseRenewalItemChanged.subscribe(
              (licRenewal: any) => {

console.log('EMFTEST () - licRenewal => \n ', licRenewal);

                this.licRenewalForm.patchValue({
                  licenseApplicationID: licRenewal.licenseApplicationID,
                  employeeLicenseID: licRenewal.employeeLicenseID,
                  sentToAgentDate: licRenewal.sentToAgentDate ? formatDate(
                    licRenewal.sentToAgentDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ) : null,
                  recFromAgentDate: licRenewal.recFromAgentDate ? formatDate(
                    licRenewal.recFromAgentDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ) : null,
                  sentToStateDate: licRenewal.sentToStateDate ? formatDate(
                    licRenewal.sentToStateDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ) : null,
                  recFromStateDate: licRenewal.recFromStateDate ? formatDate(
                    licRenewal.recFromStateDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ) : null,
                  renewalDate: licRenewal.renewalDate ? formatDate(
                    licRenewal.renewalDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ) : null,
                  renewalMethod: licRenewal.renewalMethod,
                });
              }
            )
          );
        } else {
          this.licRenewalForm.reset();
        }
      })
    );
  }

  onSubmit() {}

  onCloseModal() {
    if (this.licRenewalForm.dirty && !this.isFormSubmitted) {
      if (
        confirm('You have unsaved changes. Are you sure you want to close?')
      ) {
        this.licRenewalForm.reset();
        this.forceCloseModal();
      }
    } else {
      this.isFormSubmitted = false;
      this.forceCloseModal();
    }
  }

  forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-lic-renewal');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
