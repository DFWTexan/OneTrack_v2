import { Component, Injectable } from '@angular/core';

import { UserAcctInfoDataService } from '../../../_services';

@Component({
  selector: 'app-tm-info-management',
  templateUrl: './tm-info-management.component.html',
  styleUrl: './tm-info-management.component.css',
})
@Injectable()
export class TmInfoManagementComponent {
  isLoading: boolean = false;

  constructor(public userAcctInfoDataService: UserAcctInfoDataService) {}
}
