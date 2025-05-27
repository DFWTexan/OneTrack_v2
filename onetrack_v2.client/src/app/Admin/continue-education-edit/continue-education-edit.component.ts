import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  ConstantsDataService,
  DropdownDataService,
  ErrorMessageService,
  ModalService,
  UserAcctInfoDataService,
} from '../../_services';
import { EducationRule } from '../../_Models';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../_components';

@Component({
  selector: 'app-continue-education-edit',
  templateUrl: './continue-education-edit.component.html',
  styleUrl: './continue-education-edit.component.css',
})
@Injectable()
export class ContinueEducationEditComponent implements OnInit, OnDestroy {
  loading: boolean = false;
  stateProvinces: any[] = [];
  licenseTypes: any[] = ['Loading...'];
  selectedStateProvince: string | null = '';
  selectedLicenseType: string | null = '';
  contEducationRules: EducationRule[] = [] as EducationRule[];
  conStartDates: any[] = [];
  conEndDates: any[] = [];
  exceptions: any[] = [];
  exemptions: any[] = [];
  eventAction: string = '';
  vObject: any = {};

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    private dropdownDataService: DropdownDataService,
    public dialog: MatDialog,
    public modalService: ModalService,
    public userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.stateProvinces = ['ALL', ...this.conService.getStateProvinces()];

    this.conStartDates = [
      { value: null, label: 'Select CE Start' },
      ...this.dropdownDataService.conEduStartDateItems,
    ];
    this.subscriptionData.add(
      this.dropdownDataService.conEduStartDateItemsChanged.subscribe(
        (items: any[]) => {
          this.conStartDates = [
            { value: null, label: 'Select CE Start' },
            ...items,
          ];
        }
      )
    );

    this.conEndDates = [{ value: null, label: 'Select CE End' },
      ...this.dropdownDataService.conEduEndDateItems];
    this.subscriptionData.add(
      this.dropdownDataService.conEduEndtDateItemsChanged.subscribe(
        (items: any[]) => {
          this.conEndDates = [
            { value: null, label: 'Select CE End' },
            ...items,
          ];
        }
      )
    );

    this.exceptions = this.dropdownDataService.conEduExceptions;
    this.subscriptionData.add(
      this.dropdownDataService.conEduExceptionsChanged.subscribe(
        (items: any[]) => {
          this.exceptions = items;
        }
      )
    );

    this.exemptions = this.dropdownDataService.conEduExemptions;
    this.subscriptionData.add(
      this.dropdownDataService.conEduExemptionsChanged.subscribe(
        (items: any[]) => {
          this.exemptions = items;
        }
      )
    );

    this.fetchLicenseTypes();

    this.fetchEducationRules();
  }

  onChildCallRefreshData() {
    this.fetchEducationRules();
  }

  changeStateProvince(event: any) {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedStateProvince = value;

    if (value === 'ALL') {
      this.selectedStateProvince = null;
    }

    this.fetchLicenseTypes();
    this.fetchEducationRules();
  }

  changeLicenseType(event: any) {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedLicenseType = value;

    if (value === 'ALL') {
      this.selectedLicenseType = null;
      return;
    }

    this.fetchEducationRules();
  }

  fetchLicenseTypes() {
    this.subscriptionData.add(
      this.adminDataService
        .fetchLicenseTypes(
          this.selectedStateProvince == 'ALL'
            ? null
            : this.selectedStateProvince
        )
        .subscribe((response) => {
          this.licenseTypes = ['ALL', ...response];
        })
    );
  }

  fetchEducationRules() {
    this.loading = true;

    this.subscriptionData.add(
      this.adminDataService
        .fetchEducationRules(
          this.selectedStateProvince,
          this.selectedLicenseType
        )
        .subscribe((response) => {
          this.contEducationRules = response;
          this.loading = false;
        })
    );
  }

  openConfirmDialog(eventAction: string, msg: string, vObject: EducationRule): void {
    this.eventAction = eventAction;
    this.vObject = vObject;

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: 'Confirm Action',
        message: 'You are about to DELETE Education Rule ' + vObject.ruleNumber + '. Click "Yes" to confirm?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.subscriptionData.add(
          this.adminDataService
            .disableEducationRule({
              ruleNumber: vObject.ruleNumber,
              stateProvince: vObject.stateProvince,
              GEID: this.userAcctInfoDataService.userAcctInfo.soeid,
              userName: this.userAcctInfoDataService.userAcctInfo.displayName,
            })
            .subscribe({
              next: (response) => {
                this.fetchEducationRules();
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
