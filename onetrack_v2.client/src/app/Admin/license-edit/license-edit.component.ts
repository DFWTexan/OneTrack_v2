import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  ConstantsDataService,
  DropdownDataService,
  ModalService,
} from '../../_services';
import { License } from '../../_Models';

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
  lineOfAuthorities: {value: number, label: string}[] = [];
  licenseItems: License[] = [];

  subscriptionData: Subscription = new Subscription();

  constructor(
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    private dropDownDataService: DropdownDataService,
    public modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.stateProvinces = ['Select', ...this.conService.getStates()];
    this.lineOfAuthorities = [{value: 0, label: 'Select'}, ...this.dropDownDataService.lineOfAuthorities];

console.log('EMFTEST () - this.lineOfAuthorities => \n', this.lineOfAuthorities);

    this.getDropdownData();
  }

  private getDropdownData(): void {
    this.subscriptionData.add(
      this.dropDownDataService.lineOfAuthoritiesChanged.subscribe(
        (items: any[]) => {
          this.lineOfAuthorities = [{value: 0, label: 'Select'}, ...items];
        }
      )
    );
  }

  onChildCallRefreshData() {
    this.fetchLicenseItems();
  }

  private fetchLicenseItems(): void {
    this.loading = true;
    this.adminDataService
      .fetchLicenseItems(this.selectedStateProvince)
      .subscribe((response) => {
        this.licenseItems = response;
        this.loading = false;
      });
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
      this.fetchLicenseItems();
    }
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
