import { Component, Injectable, Input, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import { AgentComService, AgentDataService, AppComService } from '../../../../_services';

@Component({
  selector: 'app-edit-corequirement-hist',
  templateUrl: './edit-corequirement-hist.component.html',
  styleUrl: './edit-corequirement-hist.component.css',
})
@Injectable()
export class EditCorequirementHistComponent implements OnInit, OnDestroy {
  coRequirementsForm!: FormGroup;
  @Input() employmentID: number = 0;
  @Input() employeeID: number = 0;
  coReqAssetIDs: Array<{ lkpValue: string }> = [];
  coReqStatuses: Array<{ lkpValue: string }> = [];
  subscriptionMode: Subscription = new Subscription;
  subscriptionData: Subscription = new Subscription;

  constructor(
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    public appComService: AppComService
  ) {}

  ngOnInit(): void {
    this.coRequirementsForm = new FormGroup({
      assetIdString: new FormControl({ value: '', disabled: true }),
      learningProgramStatus: new FormControl(null),
      learningProgramEnrollmentDate: new FormControl(null),
      learningProgramCompletionDate: new FormControl([
        new Date().toISOString().substring(0, 10),
      ]),
    });

    this.agentDataService.fetchCoReqAssetIDs().subscribe((response) => {
      this.coReqAssetIDs = [{ lkpValue: 'Select' }, ...response];
    });

    this.agentDataService.fetchCoReqStatuses().subscribe((response) => {
      this.coReqStatuses = [{ lkpValue: 'Select' }, ...response];
    });

    this.subscriptionMode = this.agentComService.modeCompanyRequirementsHistChanged.subscribe(
      (mode: string) => {
        if (mode === 'EDIT') {
          this.subscriptionData = this.agentDataService.companyRequirementsHistItemChanged.subscribe(
            (coRequirement: any) => {
              this.coRequirementsForm.patchValue({
                assetIdString: coRequirement.assetIdString,
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
            assetIdString: 'Select',
            learningProgramStatus: 'Select',
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
    console.log(
      'EMFTest - (app-edit-employment-hist) onSubmit => \n',
      this.coRequirementsForm.value
    );
  }
  
  closeModal() {
    const modalDiv = document.getElementById('modal-edit-co-req-history');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy() {
    this.subscriptionMode.unsubscribe();
    this.subscriptionData.unsubscribe();
  }
}
