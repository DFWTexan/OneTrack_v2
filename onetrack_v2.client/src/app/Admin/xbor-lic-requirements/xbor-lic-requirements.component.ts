import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  ConstantsDataService,
  ErrorMessageService,
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

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    public dialog: MatDialog,
    public modalService: ModalService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.states = ['Select', ...this.conService.getStates()];
    this.adminDataService.fetchXBorBranchCodes().subscribe((response) => {
      this.branchCodes = ['Select', ...response];
    });
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
        // this.subscriptionData.add(
        //   this.adminDataService
        //     .deleteExamItem({
        //       examID: vObject.examId,
        //       userSOEID: this.userAcctInfoDataService.userAcctInfo.soeid,
        //     })
        //     .subscribe({
        //       next: (response) => {
        //         this.fetchXBorLicRequirementItems();
        //       },
        //       error: (error) => {
        //         if (error.error && error.error.errMessage) {
        //           this.errorMessageService.setErrorMessage(
        //             error.error.errMessage
        //           );
        //         }
        //       },
        //     })
        // );
      }
    });
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
