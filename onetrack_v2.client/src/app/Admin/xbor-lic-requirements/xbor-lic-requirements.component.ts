import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  ConstantsDataService,
  ErrorMessageService,
  FileService,
  ModalService,
  UserAcctInfoDataService,
} from '../../_services';
import { StateRequirement, XborLicenseRequirement } from '../../_Models';
import { ConfirmDialogComponent } from '../../_components';

@Component({
  selector: 'app-xbor-lic-requirements',
  templateUrl: './xbor-lic-requirements.component.html',
  styleUrl: './xbor-lic-requirements.component.css'
})
@Injectable()
export class XborLicRequirementsComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  branchCodes: any[] = [];
  states: any[] = [];
  selectedBranchCode: string = 'Select';
  xborLicRequirements: XborLicenseRequirement[] = [];
  eventAction: string = '';
  vObject: any = {};
  startFullFilePathUri: string | null = null;
  renewalFullFilePathUri: string | null = null;
  uploadStartNewType: string = 'StateNewStartPDF';
  uploadRenewalType: string = 'SateRenewalPDF';

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    public dialog: MatDialog,
    public modalService: ModalService,
    public fileService: FileService,
    public userAcctInfoDataService: UserAcctInfoDataService
  ) {}

// startFileUploadCompleted(filePath: string) {
//     this.startFullFilePathUri = filePath;
//     this.xborLicenseRequirementForm.patchValue({
//       startDocument: filePath,
//     });
//     this.xborLicenseRequirementForm.markAsDirty();
//   }

//   renewalFileUploadCompleted(filePath: string) {
//     this.renewalFullFilePathUri = filePath;
//     this.xborLicenseRequirementForm.patchValue({
//       renewalDocument: filePath,
//     });
//     this.xborLicenseRequirementForm.markAsDirty();
//   }

//   onDeleteStartDocument() {
//     this.xborLicenseRequirementForm.patchValue({
//       startDocument: null,
//     });
//     this.xborLicenseRequirementForm.markAsDirty();
//     this.startFullFilePathUri = null;
//   }
//   onDeleteRenewalDocument() {
//     this.xborLicenseRequirementForm.patchValue({
//       renewalDocument: null,
//     });
//     this.xborLicenseRequirementForm.markAsDirty();
//     this.renewalFullFilePathUri = null;
//   }

  ngOnInit(): void {
    this.states = ['Select', ...this.conService.getStates()];
    this.adminDataService.fetchXBorBranchCodes().subscribe((response) => {
      this.branchCodes = ['Select', ...response];
    });
  }

  refreshData() {
    this.isLoading = true;
    this.fetchXBorLicRequirementItems();
  }

  private fetchXBorLicRequirementItems() {
    this.isLoading = true;

    this.subscriptionData.add(
      this.adminDataService
      .fetchXBorLicRequirements(this.selectedBranchCode)
      .subscribe((response) => {
        this.xborLicRequirements = response;
        this.isLoading = false;
      })
    );
    
  }

  changeBranchCode(event: any) {
    this.isLoading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedBranchCode = value;

    if (value === 'Select') {
      return;
    } else {
      this.isLoading = false;
      // this.adminDataService
      //   .fetchXBorLicRequirements(value)
      //   .subscribe((response) => {
      //     this.xborLicRequirements = response;
      //     this.isLoading = false;
      //   });
      this.fetchXBorLicRequirementItems();
    }
  }

  openConfirmDialog(eventAction: string, msg: string, vObject: any): void {
    this.eventAction = eventAction;
    this.vObject = vObject;

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: 'Confirm Action',
        message:
          'You are about to DELETE XBor Requirement Item (' +
          vObject.branchCode + ' - Work State: ' + vObject.workStateAbv + ' - Res State: ' + vObject.resStateAbv +
          '). Click "Yes" to confirm?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.subscriptionData.add(
          this.adminDataService
            .deleteStateRequirement({
              requiredLicenseID: vObject.requiredLicenseId,
              userSOEID: this.userAcctInfoDataService.userAcctInfo.soeid,
            })
            .subscribe({
              next: (response) => {
                this.fetchXBorLicRequirementItems();
              },
              error: (error) => {
                if (error.error && error.error.errMessage) {
                  this.errorMessageService.setErrorMessage(
                    error.error.errMessage
                  );
                }
              },
            })
        );
      }
    });
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
