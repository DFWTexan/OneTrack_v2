import { Component, OnDestroy, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  ErrorMessageService,
  ModalService,
  UserAcctInfoDataService,
} from '../../_services';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../_components';
import { Subscription } from 'rxjs';
import { Company } from '../../_Models';
import { EditCompanyComponent } from './edit-company/edit-company.component';

@Component({
  selector: 'app-company-edit',
  templateUrl: './company-edit.component.html',
  styleUrls: ['./company-edit.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatDialogModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    EditCompanyComponent
  ]
})
export class CompanyEditComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  companyType: string = '';
  companyTypes: any[] = ['Loading...'];
  companies: any[] = [];
  selectedCompanyType: string = 'Select Company Type';
  eventAction: string = '';
  vObject: any = {};

  private subscriptions = new Subscription();

  // Using inject instead of constructor DI
  private errorMessageService = inject(ErrorMessageService);
  public adminDataService = inject(AdminDataService);
  public adminComService = inject(AdminComService);
  public appComService = inject(AppComService);
  public modalService = inject(ModalService);
  public dialog = inject(MatDialog);
  public userAcctInfoDataService = inject(UserAcctInfoDataService);

  ngOnInit(): void {
    this.isLoading = true;
    this.subscriptions.add(
      this.adminDataService.fetchCompanyTypes().subscribe((response) => {
        this.companyTypes = ['Select Company Type', ...response];
        this.isLoading = false;
      })
    );
  }

  onChildCallRefreshData() {
    this.fetchCompanyItems();
  }

  changeCompanyType(event: any) {
    this.isLoading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.companyType = value;
    this.adminDataService.updateCompanyType(value);

    this.selectedCompanyType = value;

    if (value === 'Select Company Type') {
      this.companies = [];
      this.isLoading = false;
      return;
    } else {
      this.fetchCompanyItems();
    }
  }

  fetchCompanyItems() {
    this.isLoading = true;
    this.subscriptions.add(
      this.adminDataService.fetchCompanies().subscribe((response) => {
        this.companies = response;
        this.isLoading = false;
      })
    );
  }

  openConfirmDialog(eventAction: string, msg: string, vObject: Company): void {
    this.eventAction = eventAction;
    this.vObject = vObject;

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: 'Confirm Action',
        message: 'You are about to DELETE \n' + vObject.companyName,
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.subscriptions.add(
          this.adminDataService
            .deleteCompany({
              companyId: vObject.companyId,
              addressId: vObject.addressId,
              userSOEID: this.userAcctInfoDataService.userAcctInfo.soeid,
            })
            .subscribe({
              next: (response) => {
                // this.location.back();
                this.fetchCompanyItems();
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

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}