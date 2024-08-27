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
  stateRes: any[] = [];
  selectedWorkState: string | null = null;
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
    this.stateRes = ['All', ...this.conService.getStates()];
  }

  changeWorkState(event: any) {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedWorkState = value;

    if (value === 'Select') {
      this.selectedWorkState = null;
      this.selectedResState = null;
      this.loading = false;
      this.companyRequirements = [];
      return;
    } else {
      this.loading = true;
      // this.subscriptionData.add(
      //   this.adminDataService
      //     .fetchCompanyRequirements(value, this.selectedResState)
      //     .subscribe({
      //       next: (response) => {
      //         this.companyRequirements = response;
      //         this.loading = false;
      //       },
      //       error: (error) => {
      //         if (error.error && error.error.errMessage) {
      //           this.errorMessageService.setErrorMessage(
      //             error.error.errMessage
      //           );
      //         }
      //       },
      //     })
      // );
      this.fetchCompanyRequirements();
    }
  }

  changeResidentState(event: any) {
    this.loading = true;
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedResState = value;

    if (value === 'All') {
      this.selectedResState = null;
    } 
    this.fetchCompanyRequirements();
  }

  fetchCompanyRequirements() {
    this.loading = true;
    this.subscriptionData.add(
      this.adminDataService
        .fetchCompanyRequirements(this.selectedWorkState, this.selectedResState)
        .subscribe({
          next: (response) => {
            this.companyRequirements = response;
            this.loading = false;
          },
          error: (error) => {
            if (error.error && error.error.errMessage) {
              this.errorMessageService.setErrorMessage(error.error.errMessage);
            }
          },
        })
    );
  }

  onOpenDocument(url: string) {
    this.subscriptionData.add(
      this.appComService.openDocument(url).subscribe({
        next: (response: Blob) => {
          const blobUrl = URL.createObjectURL(response);
          window.open(blobUrl, '_blank');
        },
        error: (error) => {
          this.errorMessageService.setErrorMessage(error.message);
        },
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
