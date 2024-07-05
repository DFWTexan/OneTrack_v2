import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  ErrorMessageService,
  ModalService,
  UserAcctInfoDataService,
} from '../../_services';
import { ConfirmDialogComponent } from '../../_components';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-dropdown-list-edit',
  templateUrl: './dropdown-list-edit.component.html',
  styleUrl: './dropdown-list-edit.component.css',
})
@Injectable()
export class DropdownListEditComponent implements OnInit, OnDestroy {
  loading: boolean = false;
  dropdownListTypes: any[] = ['Lodding...'];
  dropdownListItems: any[] = [];
  selectedDropdownListType: string = 'AgentStatus';
  eventAction: string = '';
  vObject: any = {};

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public dialog: MatDialog,
    public modalService: ModalService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.loading = true;

    this.fetchDropdown();
    this.fetchDropdownListItems();
  }

  onChildCallRefreshData() {
    this.fetchDropdown();
    this.fetchDropdownListItems();
  }

  onChangeDropdownListType(event: any) {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;

    this.selectedDropdownListType = value;

    // if (value === 'Select Company Type') {
    //   this.companies = [];
    //   this.loading = false;
    //   return;
    // } else {
    //   this.adminDataService.fetchCompanies(value).subscribe((response) => {
    //     this.companies = response;
    //     this.loading = false;
    //   });
    // }
    this.fetchDropdownListItems();
  }

  private fetchDropdown() {
    this.subscriptionData.add(
      this.adminDataService.fetchDropdownListTypes().subscribe((response) => {
        this.dropdownListTypes = response;
        this.loading = false;
      })
    );
  }

  private fetchDropdownListItems() {
    this.loading = true;
    this.subscriptionData.add(
      this.adminDataService
        .fetchDropdownListItems(this.selectedDropdownListType)
        .subscribe((response) => {
          this.dropdownListItems = response;
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
          'You are about to DELETE Dropdown Item (' +
          vObject.lkpValue +
          '). Click "Yes" to confirm?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.subscriptionData.add(
          this.adminDataService
            .deleteLkpType({
              lkpField: vObject.lkpField,
              lkpValue: vObject.lkpValue,
              userSOEID: this.userAcctInfoDataService.userAcctInfo.soeid,
            })
            .subscribe({
              next: (response) => {
                this.fetchDropdownListItems();
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
