import { Component, Injectable, OnInit } from '@angular/core';

import {
  AdminComService,
  AdminDataService,
  ConstantsDataService,
  ModalService,
} from '../../_services';
import { StateRequirement } from '../../_Models';

@Component({
  selector: 'app-xbor-lic-requirements',
  templateUrl: './xbor-lic-requirements.component.html',
  styleUrl: './xbor-lic-requirements.component.css'
})
@Injectable()
export class XborLicRequirementsComponent implements OnInit {
  loading: boolean = false;
  branchCodes: any[] = [];
  selectedBranchCode: string = 'Select';
  xborLicRequirements: StateRequirement[] = [];

  constructor(
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.adminDataService.fetchXBorBranchCodes().subscribe((response) => {
      this.branchCodes = ['Select', ...response];
    });
  }

  changeBranchCode(event: any) {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedBranchCode = value;

    if (value === 'Select') {
      return;
    } else {
      this.loading = false;
      this.adminDataService
        .fetchStateRequirements(null, null, value)
        .subscribe((response) => {
          this.xborLicRequirements = response;
          this.loading = false;
        });
    }
  }

}
