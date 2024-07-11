import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  ConstantsDataService,
  DropdownDataService,
  ModalService,
} from '../../_services';
import { License } from '../../_Models';
import { ConfirmDialogComponent } from '../../_components';

@Component({
  selector: 'app-license-edit',
  templateUrl: './license-edit.component.html',
  styleUrl: './license-edit.component.css',
})
@Injectable()
export class LicenseEditComponent implements OnInit, OnDestroy {
  loading: boolean = false;
  stateProvinces: any[] = [];
  selectedStateProvince: string = 'Select';
  lineOfAuthorities: { value: number; label: string }[] = [];
  companies: any[] = [];
  preExams: any[] = [];
  preEducations: any[] = [];
  products: any[] = [];
  licenseItems: License[] = [];

  eventAction: string = '';
  vObject: any = {};

  subscriptionData: Subscription = new Subscription();

  constructor(
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    private dropDownDataService: DropdownDataService,
    public modalService: ModalService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.stateProvinces = ['Select', ...this.conService.getStates()];
    this.lineOfAuthorities = [
      { value: 0, label: 'Select' },
      ...this.dropDownDataService.lineOfAuthorities,
    ];
    this.getEditInfo();
  }

  private getEditInfo(): void {
    this.subscriptionData.add(
      this.dropDownDataService.lineOfAuthoritiesChanged.subscribe(
        (items: any[]) => {
          this.lineOfAuthorities = [{ value: 0, label: 'Select' }, ...items];
        }
      )
    );
  }

  onChildCallRefreshData() {
    this.fetchLicenseItems();
  }

  private fetchLicenseItems(): void {
    this.loading = true;
    this.subscriptionData.add(
      this.adminDataService
        .fetchLicenseItems(this.selectedStateProvince)
        .subscribe((response) => {
          this.licenseItems = response;
          this.loading = false;
        })
    );
  }

  changeStateProvince(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;

    this.selectedStateProvince = value;
    this.adminDataService.updateCompanyType('InsuranceCo');

    this.subscriptionData.add(
      this.adminDataService.fetchCompanies().subscribe((response) => {
        this.companies = response;
        this.adminDataService.companiesChanged.next(this.companies);
      })
    );

    this.subscriptionData.add(
      this.adminDataService.fetchExamItems(value).subscribe((response) => {
        this.preExams = response;
      })
    );

    this.subscriptionData.add(
      this.adminDataService
        .fetchPreEducationItems(value)
        .subscribe((response) => {
          this.preEducations = response;
        })
    );

    this.subscriptionData.add(
      this.adminDataService.fetchProducts().subscribe((response) => {
        this.products = response.filter((product) => product.isActive === true);
      })
    );

    if (value === 'Select') {
      // this.adminDataService.fetchCities(value).subscribe((response) => {
      //   this.adminDataService.cities = response;
      //   this.adminDataService.citiesChanged.next(this.adminDataService.cities);
      // });
    } else {
      this.loading = true;
      this.fetchLicenseItems();
    }
  }

  onOpenConfirmDialog(eventAction: string, msg: string, vObject: any): void {
    this.eventAction = eventAction;
    this.vObject = vObject;
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: 'Confirm Action',
        message: 'You are about to DELETE ' + msg,
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        switch (this.eventAction) {
          case 'LICENSEITEM':
            this.deleteLicenseItem(vObject);
            break;
          case 'COMPANYITEM':
            this.delteCompanyItem(vObject);
            break;
          case 'PREEXAMITEM':
            this.deletePreExamItem(vObject);
            break;
          case 'PREEDUCATIONITEM':
            this.deletePreEducationItem(vObject);
            break;
          case 'PRODUCTITEM':
            this.deleteProductItem(vObject);
            break;
          default:
            break;
        }
      }
    });
  }

  private deleteLicenseItem(tem: any): void {
    // this.subscriptions.add(
    // this.agentDataService
    //   .deleteEmploymentJobTitleHistItem({
    //     employmentID: this.agentDataService.agentInformation.employmentID,
    //     employmentJobTitleID: jobTitleItem.employmentJobTitleID,
    //     userSOEID: this.userInfoDataService.userAcctInfo.soeid,
    //   })
    //   .subscribe({
    //     next: (response) => {
    //       // console.log(
    //       //   'EMFTEST (app-tm-emptrans-history: deleteJobTitle) - COMPLETED DELETE response => \n',
    //       //   response
    //       // );
    //     },
    //     error: (error) => {
    //       console.error(error);
    //       // handle the error here
    //     },
    //   })
    // );
  }

  private delteCompanyItem(tem: any): void {
    // this.subscriptions.add(
    // this.agentDataService
    //   .deleteEmploymentJobTitleHistItem({
    //     employmentID: this.agentDataService.agentInformation.employmentID,
    //     employmentJobTitleID: jobTitleItem.employmentJobTitleID,
    //     userSOEID: this.userInfoDataService.userAcctInfo.soeid,
    //   })
    //   .subscribe({
    //     next: (response) => {
    //       // console.log(
    //       //   'EMFTEST (app-tm-emptrans-history: deleteJobTitle) - COMPLETED DELETE response => \n',
    //       //   response
    //       // );
    //     },
    //     error: (error) => {
    //       console.error(error);
    //       // handle the error here
    //     },
    //   })
    // );
  }

  private deletePreExamItem(tem: any): void {
    // this.subscriptions.add(
    // this.agentDataService
    //   .deleteEmploymentJobTitleHistItem({
    //     employmentID: this.agentDataService.agentInformation.employmentID,
    //     employmentJobTitleID: jobTitleItem.employmentJobTitleID,
    //     userSOEID: this.userInfoDataService.userAcctInfo.soeid,
    //   })
    //   .subscribe({
    //     next: (response) => {
    //       // console.log(
    //       //   'EMFTEST (app-tm-emptrans-history: deleteJobTitle) - COMPLETED DELETE response => \n',
    //       //   response
    //       // );
    //     },
    //     error: (error) => {
    //       console.error(error);
    //       // handle the error here
    //     },
    //   })
    // );
  }

  private deletePreEducationItem(tem: any): void {
    // this.subscriptions.add(
    // this.agentDataService
    //   .deleteEmploymentJobTitleHistItem({
    //     employmentID: this.agentDataService.agentInformation.employmentID,
    //     employmentJobTitleID: jobTitleItem.employmentJobTitleID,
    //     userSOEID: this.userInfoDataService.userAcctInfo.soeid,
    //   })
    //   .subscribe({
    //     next: (response) => {
    //       // console.log(
    //       //   'EMFTEST (app-tm-emptrans-history: deleteJobTitle) - COMPLETED DELETE response => \n',
    //       //   response
    //       // );
    //     },
    //     error: (error) => {
    //       console.error(error);
    //       // handle the error here
    //     },
    //   })
    // );
  }

  private deleteProductItem(tem: any): void {
    // this.subscriptions.add(
    // this.agentDataService
    //   .deleteEmploymentJobTitleHistItem({
    //     employmentID: this.agentDataService.agentInformation.employmentID,
    //     employmentJobTitleID: jobTitleItem.employmentJobTitleID,
    //     userSOEID: this.userInfoDataService.userAcctInfo.soeid,
    //   })
    //   .subscribe({
    //     next: (response) => {
    //       // console.log(
    //       //   'EMFTEST (app-tm-emptrans-history: deleteJobTitle) - COMPLETED DELETE response => \n',
    //       //   response
    //       // );
    //     },
    //     error: (error) => {
    //       console.error(error);
    //       // handle the error here
    //     },
    //   })
    // );
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
