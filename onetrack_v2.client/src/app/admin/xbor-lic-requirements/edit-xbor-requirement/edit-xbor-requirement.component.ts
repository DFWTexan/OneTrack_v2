import { Component, Injectable, OnInit, OnDestroy, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService } from '../../../_services';

@Component({
  selector: 'app-edit-xbor-requirement',
  templateUrl: './edit-xbor-requirement.component.html',
  styleUrl: './edit-xbor-requirement.component.css'
})
@Injectable()
export class EditXborRequirementComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  @Input() stateProvinces: any[] = [];
  xborLicenseRequirementForm!: FormGroup;
  subscriptionData: Subscription = new Subscription();

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService
  ) {}

  ngOnInit(): void {
    this.xborLicenseRequirementForm = new FormGroup({
      requiredLicenseId: new FormControl(''),
      workStateAbv: new FormControl(''),
      resStateAbv: new FormControl(''),
      licenseId: new FormControl(''),
      branchCode: new FormControl(''),
      requirementType: new FormControl(''),
      licLevel1: new FormControl(''),
      licLevel2: new FormControl(''),
      licLevel3: new FormControl(''),
      licLevel4: new FormControl(''),
      plS_Incentive1: new FormControl(''),
      incentive2_Plus: new FormControl(''),
      licIncentive3: new FormControl(''),
      licState: new FormControl(''),
      licenseName: new FormControl(''),
      startDocument: new FormControl(''),
      renewalDocument: new FormControl(''),
    });

    this.subscriptionData =
      this.adminComService.modes.xborLicenseRequirement.changed.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.adminDataService.xborLicenseRequirementChanged.subscribe((xborLicenseRequirement: any) => {
            this.xborLicenseRequirementForm.patchValue({
              requiredLicenseId: xborLicenseRequirement.requiredLicenseId,
              workStateAbv: xborLicenseRequirement.workStateAbv,
              resStateAbv: xborLicenseRequirement.resStateAbv,
              licenseId: xborLicenseRequirement.licenseId,
              branchCode: xborLicenseRequirement.branchCode,
              requirementType: xborLicenseRequirement.requirementType,
              licLevel1: xborLicenseRequirement.licLevel1,
              licLevel2: xborLicenseRequirement.licLevel2,
              licLevel3: xborLicenseRequirement.licLevel3,
              licLevel4: xborLicenseRequirement.licLevel4,
              plS_Incentive1: xborLicenseRequirement.plS_Incentive1,
              incentive2_Plus: xborLicenseRequirement.incentive2_Plus,
              licIncentive3: xborLicenseRequirement.licIncentive3,
              licState: xborLicenseRequirement.licState,
              licenseName: xborLicenseRequirement.licenseName,
              startDocument: xborLicenseRequirement.startDocument,
              renewalDocument: xborLicenseRequirement.renewalDocument,
            });
          });
        } else {
          this.xborLicenseRequirementForm.reset();
        }
      });
  }

  onSubmit(): void {}

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
