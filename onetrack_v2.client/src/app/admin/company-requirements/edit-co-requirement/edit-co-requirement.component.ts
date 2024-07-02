import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  ConstantsDataService,
  ErrorMessageService,
} from '../../../_services';
import { CompanyRequirement } from '../../../_Models';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-edit-co-requirement',
  templateUrl: './edit-co-requirement.component.html',
  styleUrl: './edit-co-requirement.component.css',
})
@Injectable()
export class EditCoRequirementComponent implements OnInit, OnDestroy {
  companyReqForm!: FormGroup;
  states: string[] = [];
  requirementTypes: string[] = ['New Hire'];
  FileDisplayMode = 'CHOOSEFILE'; //--> CHOSEFILE / ATTACHMENT
  file: File | null = null;
  fileUri: string | null = null;
  document: string = '';

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService
  ) {}

  ngOnInit(): void {
    this.companyReqForm = new FormGroup({
      companyRequirementId: new FormControl(''),
      workStateAbv: new FormControl(''),
      resStateAbv: new FormControl(''),
      requirementType: new FormControl('New Hire'),
      licLevel1: new FormControl(''),
      licLevel2: new FormControl(''),
      licLevel3: new FormControl(''),
      licLevel4: new FormControl(''),
      startAfterDate: new FormControl(''),
      document: new FormControl(''),
    });

    this.states = ['Select', ...this.conService.getStates()];

    this.subscriptionData =
      this.adminComService.modes.coRequirement.changed.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.adminDataService.coRequirementChanged.subscribe(
              (companyReq: CompanyRequirement) => {
                this.companyReqForm.patchValue({
                  companyRequirementId: companyReq.companyRequirementId,
                  workStateAbv: companyReq.workStateAbv,
                  resStateAbv: companyReq.resStateAbv,
                  // requirementType: companyReq.requirementType,
                  licLevel1: companyReq.licLevel1,
                  licLevel2: companyReq.licLevel2,
                  licLevel3: companyReq.licLevel3,
                  licLevel4: companyReq.licLevel4,
                  startAfterDate: companyReq.startAfterDate
                    ? formatDate(
                        companyReq.startAfterDate,
                        'yyyy-MM-dd',
                        'en-US'
                      )
                    : null,
                  document: companyReq.document,
                });
              }
            );
          }
        }
      );
  }

  onSubmit() {
    // if (this.adminComService.modes.coRequirement.mode === 'ADD') {
    //   this.adminDataService.addCompanyRequirement(this.companyReqForm.value);
    // } else {
    //   this.adminDataService.updateCompanyRequirement(this.companyReqForm.value);
    // }
  }

  onFileSelected(event: any) {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length) {
      const file: File = target.files[0];

      if (file) {
        this.document = file.name;
        const formData = new FormData();
        formData.append('thumbnail', file);
        // const upload$ = this.emailDataService.uploadFile(formData);
        // upload$.subscribe();
      }
    }
  }

  forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-co-requirement');
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
