import { Component, Injectable, OnInit } from '@angular/core';

import {
  AdminComService,
  AdminDataService,
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
export class StateLicenseRequirementsComponent implements OnInit {
  loading: boolean = false;
  states: any[] = [];
  selectedWorkState: string = 'Select';
  selectedResState: string | null = null;
  stateRequirements: StateRequirement[] = [];

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
      this.loading = false;
      this.adminDataService
        .fetchStateRequirements(value, this.selectedResState)
        .subscribe((response) => {
          this.stateRequirements = response;
          this.loading = false;
        });
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
        .fetchStateRequirements(this.selectedWorkState, value)
        .subscribe((response) => {
          this.stateRequirements = response;
          this.loading = false;
        });
    }
  }
}
