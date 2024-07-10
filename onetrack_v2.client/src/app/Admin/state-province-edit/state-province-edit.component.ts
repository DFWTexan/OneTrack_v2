import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { StateProvince } from '../../_Models';
import {
  AdminComService,
  AdminDataService,
  ConstantsDataService,
  ModalService,
} from '../../_services';

@Component({
  selector: 'app-state-province-edit',
  templateUrl: './state-province-edit.component.html',
  styleUrl: './state-province-edit.component.css',
})
@Injectable()
export class StateProvinceEditComponent implements OnInit, OnDestroy {
  loading: boolean = false;
  states: any[] = [];
  stateProvinces: StateProvince[] = [];
  licenseTeches: { value: number; label: string }[] = [];

  subscriptionData: Subscription = new Subscription();

  constructor(
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.loading = true;
    this.states = ['Select', ...this.conService.getStates()];
    this.fetchStateProvinceItems();
    this.fetchEditInfo();
  }

  private fetchStateProvinceItems() {
    this.subscriptionData.add(
      this.adminDataService.fetchStateProvinces().subscribe((response) => {
        this.stateProvinces = response;
        this.loading = false;
      })
    );
  }

  private fetchEditInfo() {
    this.subscriptionData.add(
      this.adminDataService.fetchLicenseTechs().subscribe((response) => {
        this.licenseTeches = response.map((tech) => ({
          value: tech.licenseTechId,
          label: tech.techName,
        }));
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
