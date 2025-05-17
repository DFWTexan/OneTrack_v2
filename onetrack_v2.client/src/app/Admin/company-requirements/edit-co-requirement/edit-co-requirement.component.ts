import { Component, Injectable, OnInit, OnDestroy, Input, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  ConstantsDataService,
  ErrorMessageService,
  UserAcctInfoDataService,
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
  @Output() callParentRefreshData = new EventEmitter<any>();
  @Input() workState: string | null = null;
  @Input() resState: string | null = null;
  isFormSubmitted = false;
  companyReqForm!: FormGroup;
  states: string[] = [];
  requirementTypes: string[] = ['New Hire'];
  isDocumentUploaded: boolean = false;
  FileDisplayMode = 'CHOOSEFILE'; //--> CHOSEFILE / ATTACHMENT
  file: File | null = null;
  fileUri: string | null = null;
  document: string = '';

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    public appComService: AppComService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    private userAcctInfoDataService: UserAcctInfoDataService
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
                this.isDocumentUploaded = companyReq.document ? true : false;
                this.companyReqForm.patchValue({
                  companyRequirementId: companyReq.companyRequirementId,
                  workStateAbv: companyReq.workStateAbv,
                  resStateAbv: companyReq.resStateAbv,
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

                  document: companyReq.document ? companyReq.document : '',
                });
              }
            );
          } else {
            this.companyReqForm.reset();
            this.companyReqForm.patchValue({
              workStateAbv: this.workState ? this.workState : 'Select',
              resStateAbv: this.resState ? this.resState : 'Select',
              requirementType: 'New Hire',
            });
          }
        }
      );
  }

  onSubmit() {
    this.isFormSubmitted = true;
    let companyRequirement: CompanyRequirement = this.companyReqForm.value;
    companyRequirement.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.adminComService.modes.coRequirement.mode === 'INSERT') {
      companyRequirement.companyRequirementId = 0;
    }

    if (companyRequirement.licLevel1 === null) {
      companyRequirement.licLevel1 = false;
    }

    if (companyRequirement.licLevel2 === null) {
      companyRequirement.licLevel2 = false;
    }

    if (companyRequirement.licLevel3 === null) {
      companyRequirement.licLevel3 = false;
    }

    if (companyRequirement.licLevel4 === null) {
      companyRequirement.licLevel4 = false;
    }
    
    if (this.companyReqForm.valid) {
      
      this.subscriptionData.add(
        this.adminDataService
          .upsertConpanyRequirement(companyRequirement)
          .subscribe({
            next: (response) => {
              this.callParentRefreshData.emit();
              this.isDocumentUploaded = true;
              this.appComService.updateAppMessage(
                'Company Requirement saved successfully'
              );
              this.closeModal();
            },
            error: (error) => {
              this.errorMessageService.setErrorMessage(error.message);
            },
          })
      );
      // If file upload is required, handle it separately here.
    }
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

  onOpenDocument(url: string) {
    this.subscriptionData.add(
      this.appComService.openDocument(url).subscribe({
        next: (response: Blob) => {
          const blobUrl = URL.createObjectURL(response);
          window.open(blobUrl, '_blank');
        },
        error: (error) => {
          this.errorMessageService.setErrorMessage(error.message);
        },
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
