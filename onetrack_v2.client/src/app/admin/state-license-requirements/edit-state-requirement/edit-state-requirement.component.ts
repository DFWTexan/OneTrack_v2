import {
  Component,
  Injectable,
  OnInit,
  OnDestroy,
  EventEmitter,
  Output,
} from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../_services';

@Component({
  selector: 'app-edit-state-requirement',
  templateUrl: './edit-state-requirement.component.html',
  styleUrl: './edit-state-requirement.component.css',
})
export class EditStateRequirementComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  isFormSubmitted = false;
  stateRequirementForm!: FormGroup;
  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.stateRequirementForm = new FormGroup({
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
      this.adminComService.modes.stateRequirement.changed.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.adminDataService.stateRequirementChanged.subscribe(
              (stateRequirement: any) => {
                this.stateRequirementForm.patchValue({
                  requiredLicenseId: stateRequirement.requiredLicenseId,
                  workStateAbv: stateRequirement.workStateAbv,
                  resStateAbv: stateRequirement.resStateAbv,
                  licenseId: stateRequirement.licenseId,
                  branchCode: stateRequirement.branchCode,
                  requirementType: stateRequirement.requirementType,
                  licLevel1: stateRequirement.licLevel1,
                  licLevel2: stateRequirement.licLevel2,
                  licLevel3: stateRequirement.licLevel3,
                  licLevel4: stateRequirement.licLevel4,
                  plS_Incentive1: stateRequirement.plS_Incentive1,
                  incentive2_Plus: stateRequirement.incentive2_Plus,
                  licIncentive3: stateRequirement.licIncentive3,
                  licState: stateRequirement.licState,
                  licenseName: stateRequirement.licenseName,
                  startDocument: stateRequirement.startDocument,
                  renewalDocument: stateRequirement.renewalDocument,
                });
              }
            );
          } else {
            this.stateRequirementForm.reset();
          }
        }
      );
  }

  onSubmit(): void {
    this.isFormSubmitted = true;
    let stateReqItem: any = this.stateRequirementForm.value;
    // licenseTechItem.jobTitleID = 0;
    stateReqItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    // this.subscriptionData.add(
    //   this.adminDataService.upsertJobTitle(preEduItem).subscribe({
    //     next: (response) => {
    //       this.callParentRefreshData.emit();
    //       this.forceCloseModal();
    //     },
    //     error: (error) => {
    //       if (error.error && error.error.errMessage) {
    //         this.errorMessageService.setErrorMessage(error.error.errMessage);
    //         this.forceCloseModal();
    //       }
    //     },
    //   })
    // );
  }

  forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-state-requirement');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  closeModal() {
    this.forceCloseModal();
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
