import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  ConstantsDataService,
  ErrorMessageService,
  ModalService,
  UserAcctInfoDataService,
} from '../../_services';
import { PreEducation } from '../../_Models';
import { Subscription } from 'rxjs';
import { ConfirmDialogComponent } from '../../_components';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-pre-education-edit',
  templateUrl: './pre-education-edit.component.html',
  styleUrl: './pre-education-edit.component.css',
})
@Injectable()
export class PreEducationEditComponent implements OnInit, OnDestroy {
  loading: boolean = false;
  stateProvinces: any[] = [];
  selectedStateProvince: string = 'Select';
  preEducationItems: PreEducation[] = [];
  deliveryMethods: any[] = [];
  providers: { value: number; label: string }[] = [];
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
    this.stateProvinces = ['Select', ...this.conService.getStates()];
    this.adminDataService.updateCompanyType('PreEducationProvider');
    this.fetchEditInfo();
  }

  onChildCallRefreshData() {
    this.fetchPreEducationItems();
  }

  private fetchPreEducationItems() {
    this.subscriptionData.add(
      this.adminDataService
        .fetchPreEducationItems(this.selectedStateProvince)
        .subscribe((response) => {
          this.preEducationItems = response;
          this.loading = false;
        })
    );
  }

  private fetchEditInfo() {
    this.subscriptionData.add(
      this.adminDataService
        .fetchDropdownListItems('PreEdDelivery')
        .subscribe((response) => {
          this.deliveryMethods = response.map((item: any) => item.lkpValue);
        })
    );
    this.subscriptionData.add(
      this.adminDataService.fetchCompanies().subscribe((response) => {
        this.providers = response.map((company) => ({
          value: company.companyId,
          label: company.companyName,
        }));
      })
    );
  }

  changeStateProvince(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;

    this.selectedStateProvince = value;

    if (value === 'Select') {
      // this.adminDataService.fetchCities(value).subscribe((response) => {
      //   this.adminDataService.cities = response;
      //   this.adminDataService.citiesChanged.next(this.adminDataService.cities);
      // });
    } else {
      this.loading = true;
      this.fetchPreEducationItems();
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
          'You are about to DELETE Pre-Education Item (' +
          vObject.educationName +
          ' - ' +
          vObject.stateProvinceAbv +
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
                this.fetchPreEducationItems();
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
    this.subscriptionData.unsubscribe();
  }
}
