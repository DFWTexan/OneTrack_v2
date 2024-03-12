import { Component, Injectable } from '@angular/core';

import { AgentDataService } from '../../../_services';

@Component({
  selector: 'app-tm-license-management',
  templateUrl: './tm-license-management.component.html',
  styleUrl: './tm-license-management.component.css',
})
@Injectable()
export class TmLicenseManagementComponent {
  constructor(private agentData: AgentDataService) {}
}
