import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  ErrorMessageService,
  ModalService,
  UserAcctInfoDataService,
} from '../../_services';
import { LicenseTech } from '../../_Models';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../_components';

@Component({
  selector: 'app-license-tech-edit',
  templateUrl: './license-tech-edit.component.html',
  styleUrl: './license-tech-edit.component.css',
})
@Injectable()
export class LicenseTechEditComponent implements OnInit, OnDestroy {
  loading: boolean = false;
  licenseTechs: LicenseTech[] = [];
  eventAction: string = '';
  vObject: any = {};

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    public modalService: ModalService,
    public dialog: MatDialog,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  onChildCallRefreshData() {
    this.fetchLicenseTechItems();
  }

  ngOnInit(): void {
    this.loading = true;
    this.fetchLicenseTechItems();
  }

  private fetchLicenseTechItems(): void {
    this.loading = true;
    this.subscriptionData.add(
      this.adminDataService.fetchLicenseTechs().subscribe((response) => {
        this.licenseTechs = response;
        this.loading = false;
      })
    );
  }

  openConfirmDialog(eventAction: string, msg: string, vObject: any): void {
    this.eventAction = eventAction;
    this.vObject = vObject;

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: 'Confirm Action',
        message:
          'You are about to DELETE License Tech (' +
          vObject.firstName + ' ' + vObject.lastName + ' - ' + vObject.soeid +
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
                this.fetchLicenseTechItems();
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
