import {
  Component,
  Injectable,
  OnInit,
  OnDestroy,
  EventEmitter,
  Output,
  Input,
} from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  DropdownDataService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../_services';

@Component({
  selector: 'app-edit-xbor-requirement',
  templateUrl: './edit-xbor-requirement.component.html',
  styleUrl: './edit-xbor-requirement.component.css',
})
@Injectable()
export class EditXborRequirementComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  @Input() stateProvinces: any[] = [];
  @Input() selectedBranchCode: string = 'Select';
  isFormSubmitted = false;
  xborLicenseRequirementForm!: FormGroup;
  stateLicenseItems: { value: number; label: string }[] = [];
  selectedLicenseState: string = 'Select';
  isLicenseNameDisabled = true;
  isStartDocUploaded = false;
  isRenewalDocUploaded = false;
  FileDisplayMode = 'CHOOSEFILE'; //--> CHOSEFILE / ATTACHMENT
  file: File | null = null;
  fileUri: string | null = null;
  startFullFilePathUri: string | null = null;
  renewalFullFilePathUri: string | null = null;
  document: string = '';
  uploadStartNewType: string = 'StateNewStartPDF';
  uploadRenewalType: string = 'SateRenewalPDF';

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    public dropDownDataService: DropdownDataService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  startFileUploadCompleted(filePath: string) {
    this.startFullFilePathUri = filePath;
    this.xborLicenseRequirementForm.patchValue({
      startDocument: filePath,
    });
    this.xborLicenseRequirementForm.markAsDirty();
  }

  renewalFileUploadCompleted(filePath: string) {
    this.renewalFullFilePathUri = filePath;
    this.xborLicenseRequirementForm.patchValue({
      renewalDocument: filePath,
    });
    this.xborLicenseRequirementForm.markAsDirty();
  }

  onDeleteStartDocument() {
    this.xborLicenseRequirementForm.patchValue({
      startDocument: null,
    });
    this.xborLicenseRequirementForm.markAsDirty();
    this.startFullFilePathUri = null;
  }
  onDeleteRenewalDocument() {
    this.xborLicenseRequirementForm.patchValue({
      renewalDocument: null,
    });
    this.xborLicenseRequirementForm.markAsDirty();
    this.renewalFullFilePathUri = null;
  }
  
  ngOnInit(): void {
    this.xborLicenseRequirementForm = new FormGroup({
      requiredLicenseId: new FormControl(''),
      workStateAbv: new FormControl(''),
      resStateAbv: new FormControl({ value: 'Select', disabled: false }),
      licenseId: new FormControl({ value: 0, disabled: true }),
      branchCode: new FormControl(''),
      requirementType: new FormControl(''),
      licLevel1: new FormControl(false),
      licLevel2: new FormControl(''),
      licLevel3: new FormControl(''),
      licLevel4: new FormControl(''),
      plS_Incentive1: new FormControl(''),
      incentive2_Plus: new FormControl(''),
      licIncentive3: new FormControl(''),
      licState: new FormControl(''),
      licenseName: new FormControl({ value: 'Select', disabled: true }),
      startDocument: new FormControl(''),
      renewalDocument: new FormControl(''),
    });

    this.subscriptionData.add(
      this.adminComService.modes.xborLicenseRequirement.changed.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.stateProvinces = this.stateProvinces.filter(
              (item) => item !== 'Select'
            );
            this.subscriptionData.add(
              this.adminDataService.xborLicenseRequirementChanged.subscribe(
                (xborLicenseRequirement: any) => {
                  this.isStartDocUploaded = xborLicenseRequirement.startDocument
                    ? true
                    : false;
                  this.isRenewalDocUploaded =
                    xborLicenseRequirement.renewalDocument ? true : false;
                  if (xborLicenseRequirement.licenseId !== 0) {
                    this.selectedLicenseState = xborLicenseRequirement.licState;
                    this.fetchStateLicenseItems();
                  }

// console.log('EMFTEST (ngOnInit) - xborLicenseRequirement', xborLicenseRequirement);
                  
                  this.xborLicenseRequirementForm.reset();

                  this.xborLicenseRequirementForm.patchValue({
                    requiredLicenseId: xborLicenseRequirement.requiredLicenseId,
                    workStateAbv: xborLicenseRequirement.workStateAbv,
                    resStateAbv: xborLicenseRequirement.resStateAbv,
                    licenseId: xborLicenseRequirement.licenseId,
                    branchCode: xborLicenseRequirement.branchCode,
                    requirementType: xborLicenseRequirement.requirementType,
                    licLevel1: xborLicenseRequirement.licLevel1 === 1, // Convert number to boolean
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
                }
              )
            );
          } else {
            this.xborLicenseRequirementForm.reset();
          }
        }
      )
    );
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

  onChangeLicenseState(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedLicenseState = value;

    if (value === 'Select') {
      this.xborLicenseRequirementForm.get('licenseId')?.disable();
      return;
    } else {
      this.xborLicenseRequirementForm.get('licenseId')?.enable();
      this.fetchStateLicenseItems();
    }
  }

  onSubmit(): void {
    this.isFormSubmitted = true;
    let xBorReqItem: any = this.xborLicenseRequirementForm.value;
    xBorReqItem.branchCode = this.selectedBranchCode;
    xBorReqItem.requirementType = 'X-Border';
    xBorReqItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    // Convert checkbox boolean to number (true = 1, false = 0)
    xBorReqItem.licLevel1 = this.xborLicenseRequirementForm.get('licLevel1')?.value ? true : false;
    xBorReqItem.licLevel2 = this.xborLicenseRequirementForm.get('licLevel2')?.value ? true : false;
    xBorReqItem.licLevel3 = this.xborLicenseRequirementForm.get('licLevel3')?.value ? true : false;
    xBorReqItem.licLevel4 = this.xborLicenseRequirementForm.get('licLevel4')?.value ? true : false;
    xBorReqItem.plS_Incentive1 = this.xborLicenseRequirementForm.get('plS_Incentive1')?.value ? true : false;
    xBorReqItem.incentive2_Plus = this.xborLicenseRequirementForm.get('incentive2_Plus')?.value ? true : false;
    xBorReqItem.licIncentive3 = this.xborLicenseRequirementForm.get('licIncentive3')?.value || false;

    if (this.adminComService.modes.xborLicenseRequirement.mode === 'INSERT') {
      xBorReqItem.requiredLicenseID = 0;
    }

    this.subscriptionData.add(
      this.adminDataService.upSertStateRequirement(xBorReqItem).subscribe({
        next: (response) => {
          this.callParentRefreshData.emit();
          this.appComService.updateAppMessage(
            'XBor License saved successfully'
          );
          this.forceCloseModal();
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
            this.forceCloseModal();
          }
        },
      })
    );
  }

  private fetchStateLicenseItems(): void {
    this.dropDownDataService
      .fetchDropdownNumericData(
        'GetLicenseNumericNames',
        this.selectedLicenseState
      )
      .subscribe({
        next: (response) => {
          this.stateLicenseItems = [{ value: 0, label: 'Select' }, ...response];
          this.xborLicenseRequirementForm.get('licenseId')?.enable();
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
            this.forceCloseModal();
          }
        },
      });
  }

  private forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-xBor-requirement');
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
