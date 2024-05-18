import { Component, Injectable, Input, OnDestroy, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder,
} from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import {
  AgentComService,
  AgentDataService,
  AppComService,
  UserAcctInfoDataService,
} from '../../../../_services';

@Component({
  selector: 'app-edit-corequirement-hist',
  templateUrl: './edit-corequirement-hist.component.html',
  styleUrl: './edit-corequirement-hist.component.css',
})
@Injectable()
export class EditCorequirementHistComponent implements OnInit, OnDestroy {
  isFormSubmitted: boolean = false;
  coRequirementsForm!: FormGroup;
  @Input() employmentID: number = 0;
  @Input() employeeID: number = 0;
  coReqAssetIDs: Array<{ lkpValue: string }> = [];
  coReqStatuses: Array<{ lkpValue: string }> = [];
  subscriptionMode: Subscription = new Subscription();
  subscriptionData: Subscription = new Subscription();

  constructor(
    private fb: FormBuilder,
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.coRequirementsForm = this.fb.group({
      companyRequirementID: [''],
      assetIdString: ['', Validators.required],
      assetSk: [''],
      learningProgramStatus: ['', Validators.required],
      learningProgramEnrollmentDate: ['', Validators.required],
      learningProgramCompletionDate: [''],
    });

    this.agentDataService.fetchCoReqAssetIDs().subscribe((response) => {
      this.coReqAssetIDs = [{ lkpValue: 'Select Asset ID' }, ...response];
    });

    this.agentDataService.fetchCoReqStatuses().subscribe((response) => {
      this.coReqStatuses = [{ lkpValue: 'Select Status' }, ...response];
    });

    this.subscriptionMode =
      this.agentComService.modeCompanyRequirementsHistChanged.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.subscriptionData =
              this.agentDataService.companyRequirementsHistItemChanged.subscribe(
                (coRequirement: any) => {
                  this.coRequirementsForm.patchValue({
                    companyRequirementID: coRequirement.companyRequirementID,
                    assetIdString: coRequirement.assetIdString,
                    assetSk: coRequirement.assetSk,
                    learningProgramStatus: coRequirement.learningProgramStatus,
                    learningProgramEnrollmentDate: formatDate(
                      coRequirement.learningProgramEnrollmentDate,
                      'yyyy-MM-dd',
                      'en-US'
                    ),
                    learningProgramCompletionDate: formatDate(
                      coRequirement.learningProgramCompletionDate,
                      'yyyy-MM-dd',
                      'en-US'
                    ),
                  });
                }
              );
          } else {
            this.coRequirementsForm.reset();
            this.coRequirementsForm.patchValue({
              assetIdString: 'Select Asset ID',
              learningProgramStatus: 'Select Status',
              // learningProgramEnrollmentDate: formatDate(
              //   new Date(),
              //   'yyyy-MM-dd',
              //   'en-US'
              // ),
              // learningProgramCompletionDate: formatDate(
              //   new Date(),
              //   'yyyy-MM-dd',
              //   'en-US'
              // ),
            });
          }
        }
      );
  }

  onSubmit() {
    this.isFormSubmitted = true;

    let coReqItem = this.coRequirementsForm.value;
    coReqItem.employmentID = this.employmentID;
    coReqItem.UserSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (coReqItem.assetIdString === 'Select Asset ID') {
      this.coRequirementsForm.controls['assetIdString'].setErrors({
        invalid: true,
      });
    }

    if (coReqItem.learningProgramStatus === 'Select Status') {
      this.coRequirementsForm.controls['learningProgramStatus'].setErrors({
        invalid: true,
      });
    }

    if (
      coReqItem.learningProgramEnrollmentDate === '' ||
      coReqItem.learningProgramEnrollmentDate === null
    ) {
      this.coRequirementsForm.controls[
        'learningProgramEnrollmentDate'
      ].setErrors({
        invalid: true,
      });
    }

    if (this.coRequirementsForm.invalid) {
      this.coRequirementsForm.setErrors({ invalid: true });
      return;
    }

    if (this.agentComService.modeCompanyRequirementsHist === 'INSERT') {
      coReqItem.companyRequirementID = 0;
    }

    this.agentDataService
      .upsertCompanyRequirementsHistItem(coReqItem)
      .subscribe({
        next: (response) => {
          this.isFormSubmitted = true;
          this.closeModal();
        },
        error: (error) => {
          console.error(error);
          // handle the error here
        },
      });
  }

  closeModal() {
    // const modalDiv = document.getElementById('modal-edit-co-req-history');
    // if (modalDiv != null) {
    //   modalDiv.style.display = 'none';
    // }
    if (this.coRequirementsForm.dirty && !this.isFormSubmitted) {
      if (
        confirm('You have unsaved changes. Are you sure you want to close?')
      ) {
        const modalDiv = document.getElementById('modal-edit-co-req-history');
        if (modalDiv != null) {
          modalDiv.style.display = 'none';
        }
        this.coRequirementsForm.reset();
        this.coRequirementsForm.patchValue({
          assetIdString: 'Select',
          learningProgramStatus: 'Select',
        });
      }
    } else {
      this.isFormSubmitted = false;
      const modalDiv = document.getElementById('modal-edit-co-req-history');
      if (modalDiv != null) {
        modalDiv.style.display = 'none';
      }
    }
  }

  ngOnDestroy() {
    this.subscriptionMode.unsubscribe();
    this.subscriptionData.unsubscribe();
  }
}
