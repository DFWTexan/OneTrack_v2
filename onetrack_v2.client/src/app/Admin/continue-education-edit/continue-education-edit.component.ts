import { Component, Injectable, OnInit } from '@angular/core';
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
export class ContinueEducationEditComponent implements OnInit {
  loading: boolean = false;
  stateProvinces: any[] = [];
  licenseTypes: any[] = ['Loading...'];
  selectedStateProvince: string | null = '';
  selectedLicenseType: string | null = '';
  contEducationRules: EducationRule[] = [] as EducationRule[];

  constructor(
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.stateProvinces = ['ALL', ...this.conService.getStateProvinces()];
    this.adminDataService.fetchLicenseTypes().subscribe((response) => {
      this.licenseTypes = ['ALL', ...response];
    });
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

  fetchEducationRules() {
    this.loading = true;
    this.adminDataService
      .fetchEducationRules(this.selectedStateProvince, this.selectedLicenseType)
      .subscribe((response) => {

console.log('EMFTEST (app-continue-education-edit) - response => \n', response);

        this.contEducationRules = response;
        this.loading = false;
      });
  }
}
