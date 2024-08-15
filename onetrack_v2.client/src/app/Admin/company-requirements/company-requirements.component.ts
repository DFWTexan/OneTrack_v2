import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  ConstantsDataService,
  ErrorMessageService,
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

  subscriptionData: Subscription = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
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
      this.loading = false;
      this.subscriptionData.add(
        this.adminDataService
          .fetchCompanyRequirements(value, this.selectedResState)
          .subscribe({
            next: (response) => {
            this.companyRequirements = response;
            this.loading = false;
          },
          error: (error) => {
            if (error.error && error.error.errMessage) {
              this.errorMessageService.setErrorMessage(error.error.errMessage);
            }
          }})
      );
    }
  }

  changeResidentState(event: any) {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedResState = value;

    if (value === 'Select') {
      return;
    } else {
      this.loading = false;
      this.adminDataService
        .fetchCompanyRequirements(this.selectedWorkState, value)
        .subscribe((response) => {
          this.companyRequirements = response;
          this.loading = false;
        });
    }
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
