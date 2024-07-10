import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
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
import { StateRequirement } from '../../_Models';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../_components';

@Component({
  selector: 'app-state-license-requirements',
  templateUrl: './state-license-requirements.component.html',
  styleUrl: './state-license-requirements.component.css',
})
@Injectable()
export class StateLicenseRequirementsComponent implements OnInit, OnDestroy {
  loading: boolean = false;
  states: any[] = [];
  selectedWorkState: string = 'Select';
  selectedResState: string | null = null;
  stateRequirements: StateRequirement[] = [];
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
  }

  private fetchStateRequirements() {
    this.loading = true;
    this.subscriptionData.add(
      this.adminDataService
        .fetchStateRequirements(this.selectedWorkState, this.selectedResState)
        .subscribe((response) => {
          this.stateRequirements = response;
          this.loading = false;
        })
    );
  }

  changeWorkState(event: any) {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedWorkState = value;

    if (value === 'Select') {
      return;
    } else {
      this.loading = false;
      this.fetchStateRequirements();
    }
  }

  changeResidentState(event: any) {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedResState = value;

    if (value === 'Select') {
      return;
    } else {
      this.loading = false;
      this.fetchStateRequirements();
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
          'You are about to DELETE License Requirement Item (' +
          vObject.licenseName + ' WorkStateAbv - '+ vObject.workStateAbv + ' ResStateAbv- '+ vObject.resStateAbv +
          '). Click "Yes" to confirm?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.subscriptionData.add(
          this.adminDataService
            .deleteExamItem({
              examID: vObject.examId,
              userSOEID: this.userAcctInfoDataService.userAcctInfo.soeid,
            })
            .subscribe({
              next: (response) => {
                this.fetchStateRequirements();
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
