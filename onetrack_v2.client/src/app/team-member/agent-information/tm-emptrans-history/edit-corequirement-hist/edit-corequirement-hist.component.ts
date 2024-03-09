import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import { AgentComService, AgentDataService } from '../../../../_services';

@Component({
  selector: 'app-edit-corequirement-hist',
  templateUrl: './edit-corequirement-hist.component.html',
  styleUrl: './edit-corequirement-hist.component.css',
})
@Injectable()
export class EditCorequirementHistComponent implements OnInit, OnDestroy {
  coRequirementsForm!: FormGroup;
  subscriptionData: Subscription = new Subscription;
  subscriptionMode: Subscription = new Subscription;

  constructor(
    public agentService: AgentDataService,
    public agentComService: AgentComService
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

    this.subscriptionData = this.agentComService.modeCompanyRequirementsHistChanged.subscribe(
      (mode: string) => {
        if (mode === 'EDIT') {
          this.subscriptionMode = this.agentService.companyRequirementsHistItemChanged.subscribe(
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

  ngOnDestroy() {
    this.subscriptionData.unsubscribe();
    this.subscriptionMode.unsubscribe();
  }
}
