import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import {
  AdminComService,
  AdminDataService,
  ConstantsDataService,
  ModalService,
} from '../../_services';
import { CompanyRequirement } from '../../_Models';

@Component({
  selector: 'app-company-requirements',
  templateUrl: './company-requirements.component.html',
  styleUrl: './company-requirements.component.css',
})
@Injectable()
export class CompanyRequirementsComponent implements OnInit, OnDestroy {
  loading: boolean = false;
  states: any[] = [];
  selectedWorkState: string = 'Select';
  selectedResState: string | null = null;
  companyRequirements: CompanyRequirement[] = [];

  constructor(
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.states = ['Select', ...this.conService.getStates()];
  }

  changeWorkState(event: any) {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedWorkState = value;

    if (value === 'Select') {
      return;
    } else {

console.log('EMFTEST (app-company-requirements) - CompanyRequirementsComponent: changeWorkState: value: ', value);

      this.loading = false;
      this.adminDataService
        .fetchCompanyRequirements(value, this.selectedResState)
        .subscribe((response) => {
          this.companyRequirements = response;
          this.loading = false;
        });
    }
  }

  changeResidentState(event: any) {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedResState = value;
  }

  ngOnDestroy(): void {}
}
