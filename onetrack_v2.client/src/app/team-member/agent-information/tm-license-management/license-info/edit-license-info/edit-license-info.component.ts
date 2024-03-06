import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AgentDataService } from '../../../../../_services';
import { AgentLicenseAppointments } from '../../../../../_Models';

@Component({
  selector: 'app-edit-license-info',
  templateUrl: './edit-license-info.component.html',
  styleUrl: './edit-license-info.component.css',
})
export class EditLicenseInfoComponent implements OnInit {
  licenseForm: FormGroup;
  currentIndex: number = 0;
  licenseMgmtData: AgentLicenseAppointments[] = [];

  constructor(
    private agentDataService: AgentDataService,
    private fb: FormBuilder
  ) {
    this.licenseForm = this.fb.group({
      employeeLicenseId: [{ value: '', disabled: true }, Validators.required],
      licenseState: ['', Validators.required],
      lineOfAuthority: ['', Validators.required],
      licenseStatus: ['', Validators.required],
      employmentID: ['', Validators.required],
      licenseName: ['', Validators.required],
      licenseNumber: ['', Validators.required],
      resNoneRes: [''],
      originalIssueDate: ['', Validators.required],
      lineOfAuthIssueDate: ['', Validators.required],
      licenseEffectiveDate: ['', Validators.required],
      licenseExpirationDate: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.currentIndex = this.agentDataService.licenseMgmtDataIndex;
    this.licenseMgmtData =
      this.agentDataService.agentInformation.agentLicenseAppointments;
      this.licenseForm.patchValue({
        employeeLicenseId: this.licenseMgmtData[this.currentIndex].employeeLicenseId,
        licenseState: 'MO',
        lineOfAuthority: 'CL',
        licenseStatus: 'Active',
        employmentID: '606709',
        licenseName: 'CREDIT',
        licenseNumber: '3002117601',
        resNoneRes: null,
        originalIssueDate: '2022-08-30T00:00:00',
        lineOfAuthIssueDate: '2022-08-30T00:00:00',
        licenseEffectiveDate: '2022-08-30T00:00:00',
        licenseExpirationDate: '2025-05-30T00:00:00',
      });
  }
}
