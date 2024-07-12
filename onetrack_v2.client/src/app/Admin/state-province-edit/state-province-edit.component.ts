import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subscription } from 'rxjs';

import { StateProvince } from '../../_Models';
import {
  AdminComService,
  AdminDataService,
  ConstantsDataService,
  ErrorMessageService,
  ModalService,
  UserAcctInfoDataService,
} from '../../_services';
import { ConfirmDialogComponent } from '../../_components';

@Component({
  selector: 'app-state-province-edit',
  templateUrl: './state-province-edit.component.html',
  styleUrl: './state-province-edit.component.css',
})
@Injectable()
export class StateProvinceEditComponent implements OnInit, OnDestroy {
  loading: boolean = false;
  states: any[] = [];
  stateProvinces: StateProvince[] = [];
  licenseTeches: { value: number; label: string }[] = [];
  eventAction: string = '';
  vObject: any = {};

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public dialog: MatDialog,
    public modalService: ModalService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.loading = true;
    this.states = ['Select', ...this.conService.getStates()];
    this.fetchStateProvinceItems();
    this.fetchEditInfo();
  }

  private fetchStateProvinceItems() {
    this.subscriptionData.add(
      this.adminDataService.fetchStateProvinces().subscribe((response) => {
        this.stateProvinces = response;
        this.loading = false;
      })
    );
  }

  private fetchEditInfo() {
    this.subscriptionData.add(
      this.adminDataService.fetchLicenseTechs().subscribe((response) => {
        this.licenseTeches = response.map((tech) => ({
          value: tech.licenseTechId,
          label: tech.techName,
        }));
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
          'You are about to DELETE State Province Item (' +
          vObject.stateProvinceName +
          '). Click "Yes" to confirm?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.subscriptionData.add(
          this.adminDataService
            .deleteStateProvince({
              stateProvinceCode: vObject.stateProvinceCode,
              doiAddressID: vObject.doiAddressID,
              userSOEID: this.userAcctInfoDataService.userAcctInfo.soeid,
            })
            .subscribe({
              next: (response) => {
                // this.fetchStateRequirements();
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
