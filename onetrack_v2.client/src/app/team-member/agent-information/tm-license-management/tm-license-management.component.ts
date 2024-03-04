import { Component, OnInit } from '@angular/core';

import { AgentLicenseAppointments } from '../../../_Models';

@Component({
  selector: 'app-tm-license-management',
  templateUrl: './tm-license-management.component.html',
  styleUrl: './tm-license-management.component.css'
})
export class TmLicenseManagementComponent implements OnInit{
  licenseData: AgentLicenseAppointments = {} as AgentLicenseAppointments;

  constructor() {}

  ngOnInit() {
  }
}
