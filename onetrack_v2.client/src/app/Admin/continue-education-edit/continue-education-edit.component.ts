import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  ConstantsDataService,
  DropdownDataService,
  ModalService,
} from '../../_services';
import { EducationRule } from '../../_Models';

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

  subscriptionData: Subscription = new Subscription();

  constructor(
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    private dropdownDataService: DropdownDataService,
    public modalService: ModalService
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
          console.log('EMFTEST (ngOnInit) - exemptions => \n', items);

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

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
