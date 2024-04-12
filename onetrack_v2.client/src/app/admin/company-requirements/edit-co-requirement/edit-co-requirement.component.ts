import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService } from '../../../_services';
import { CompanyRequirement } from '../../../_Models';

@Component({
  selector: 'app-edit-co-requirement',
  templateUrl: './edit-co-requirement.component.html',
  styleUrl: './edit-co-requirement.component.css'
})
@Injectable()
export class EditCoRequirementComponent implements OnInit, OnDestroy {
  companyReqForm!: FormGroup;
  subscriptionData: Subscription = new Subscription();

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService
  ) {}

  ngOnInit(): void {
    this.companyReqForm = new FormGroup({
      companyRequirementId: new FormControl(''),
      workStateAbv: new FormControl(''),
      resStateAbv: new FormControl(''),
      requirementType: new FormControl(''),
      licLevel1: new FormControl(''),
      licLevel2: new FormControl(''),
      licLevel3: new FormControl(''),
      licLevel4: new FormControl(''),
      startAfterDate: new FormControl(''),
      document: new FormControl(''),
    });

    this.subscriptionData =
      this.adminComService.modes.coRequirement.changed.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.adminDataService.coRequirementChanged.subscribe((companyReq: CompanyRequirement) => {
            this.companyReqForm.patchValue({
              companyRequirementId: companyReq.companyRequirementId,
              workStateAbv: companyReq.workStateAbv,
              resStateAbv: companyReq.resStateAbv,
              requirementType: companyReq.requirementType,
              licLevel1: companyReq.licLevel1,
              licLevel2: companyReq.licLevel2,
              licLevel3: companyReq.licLevel3,
              licLevel4: companyReq.licLevel4,
              startAfterDate: companyReq.startAfterDate,
              document: companyReq.document,
            });
          });
        }
      });
  }

  onSubmit() {
    // if (this.adminComService.modes.coRequirement.mode === 'ADD') {
    //   this.adminDataService.addCompanyRequirement(this.companyReqForm.value);
    // } else {
    //   this.adminDataService.updateCompanyRequirement(this.companyReqForm.value);
    // }
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }

}
