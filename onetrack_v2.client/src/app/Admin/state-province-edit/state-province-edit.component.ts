import { Component, Injectable, OnInit } from '@angular/core';

import { StateProvince } from '../../_Models';
import {
  AdminComService,
  AdminDataService,
  ModalService,
} from '../../_services';

@Component({
  selector: 'app-state-province-edit',
  templateUrl: './state-province-edit.component.html',
  styleUrl: './state-province-edit.component.css',
})
@Injectable()
export class StateProvinceEditComponent implements OnInit {
  loading: boolean = false;
  stateProvinces: StateProvince[] = [];

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.loading = true;
    this.adminDataService.fetchStateProvinces().subscribe((response) => {
      this.stateProvinces = response;
      this.loading = false;
    });
  }
}
