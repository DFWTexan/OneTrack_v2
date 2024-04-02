import { Component, Injectable, OnInit } from '@angular/core';
import { AdminComService, AdminDataService, ConstantsDataService, ModalService } from '../../_services';
import { License } from '../../_Models';

@Component({
  selector: 'app-license-edit',
  templateUrl: './license-edit.component.html',
  styleUrl: './license-edit.component.css'
})
@Injectable()
export class LicenseEditComponent implements OnInit {
  loading: boolean = false;
  stateProvinces: any[] = [];
  selectedStateProvince: string = 'Select';
  licenseItems: License[] = [];

  constructor(
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public modalService: ModalService
  ) {}

ngOnInit(): void {
  this.stateProvinces = ['Select', ...this.conService.getStates()];
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
    this.adminDataService.fetchLicenseItems(value).subscribe((response) => {
      this.licenseItems = response;
    });
  }
}

}
