import { Component } from '@angular/core';
import { AgentLicenseAppointments } from '../../../../_Models';

@Component({
  selector: 'app-license-info',
  templateUrl: './license-info.component.html',
  styleUrl: './license-info.component.css'
})
export class LicenseInfoComponent {
  licenseData: AgentLicenseAppointments = {} as AgentLicenseAppointments;
}
