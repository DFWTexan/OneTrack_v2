import { Component, Injectable, OnInit } from '@angular/core';
import {
  AdminComService,
  AdminDataService,
  ConstantsDataService,
  ModalService,
} from '../../_services';

@Component({
  selector: 'app-continue-education-edit',
  templateUrl: './continue-education-edit.component.html',
  styleUrl: './continue-education-edit.component.css'
})
@Injectable()
export class ContinueEducationEditComponent implements OnInit {
  loading: boolean = false;
stateProvinces: any[] = [];
licenseTypes: any[] = [];
selectedStateProvince: string = 'Select';
selectedLicenseType: number = 0;

  constructor(
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.stateProvinces = ['ALL', ...this.conService.getStateProvinces()];
    this.adminDataService.fetchLicenseTypes().subscribe((response) => {

console.log(response);

      this.licenseTypes = ['ALL', ...response];
    });
  }

changeStateProvince(event: any) {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedStateProvince = value;

    if (value === 'Select') {
      return;
    } else {
      // this.loading = false;
      // this.adminDataService
      //   .fetchContinueEducation(value)
      //   .subscribe((response) => {
      //     this.continueEducation = response;
      //     this.loading = false;
      //   });
    }
  }

  changeLicenseType(event: any) { 
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedLicenseType = +value;

    if (+value === 0) {
      return;
    } else {
      // this.loading = false;
      // this.adminDataService
      //   .fetchContinueEducation(value)
      //   .subscribe((response) => {
      //     this.continueEducation = response;
      //     this.loading = false;
      //   });
    }
  }

}
