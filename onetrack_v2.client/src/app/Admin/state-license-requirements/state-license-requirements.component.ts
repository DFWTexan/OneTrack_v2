import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  ConstantsDataService,
  ModalService,
} from '../../_services';
import { StateRequirement } from '../../_Models';

@Component({
  selector: 'app-state-license-requirements',
  templateUrl: './state-license-requirements.component.html',
  styleUrl: './state-license-requirements.component.css',
})
@Injectable()
export class StateLicenseRequirementsComponent implements OnInit, OnDestroy {
  loading: boolean = false;
  states: any[] = [];
  selectedWorkState: string = 'Select';
  selectedResState: string | null = null;
  stateRequirements: StateRequirement[] = [];

  subscriptionData: Subscription = new Subscription();

  constructor(
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
          .fetchStateRequirements(value, this.selectedResState)
          .subscribe((response) => {
            this.stateRequirements = response;
            this.loading = false;
          })
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
      this.subscriptionData.add(
        this.adminDataService
          .fetchStateRequirements(this.selectedWorkState, value)
          .subscribe((response) => {
            this.stateRequirements = response;
            this.loading = false;
          })
      );
    }
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
