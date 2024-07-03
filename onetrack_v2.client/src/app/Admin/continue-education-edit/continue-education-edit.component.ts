import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  ConstantsDataService,
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

  subscriptionData: Subscription = new Subscription();

  constructor(
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.stateProvinces = ['ALL', ...this.conService.getStateProvinces()];

    this.fetchLicenseTypes();

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
