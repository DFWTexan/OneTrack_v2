import { Component, Injectable, OnInit } from '@angular/core';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  ModalService,
} from '../../_services';
import { LicenseTech } from '../../_Models';

@Component({
  selector: 'app-license-tech-edit',
  templateUrl: './license-tech-edit.component.html',
  styleUrl: './license-tech-edit.component.css',
})
@Injectable()
export class LicenseTechEditComponent implements OnInit {
  loading: boolean = false;
  licenseTechs: LicenseTech[] = [];

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    public modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.loading = true;
    this.adminDataService.fetchLicenseTechs().subscribe((response) => {
      this.licenseTechs = response;
      this.loading = false;
    });
  }
}
