import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService } from '../../../_services';

@Component({
  selector: 'app-edit-license',
  templateUrl: './edit-license.component.html',
  styleUrl: './edit-license.component.css'
})
@Injectable()
export class EditLicenseComponent implements OnInit, OnDestroy{
  licenseForm!: FormGroup;
  subscriptionData: Subscription = new Subscription();

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService
  ) {}

  ngOnInit(): void {
    this.licenseForm = new FormGroup({
      licenseId: new FormControl(''),
      licenseName: new FormControl(''),
      licenseAbv: new FormControl(''),
      stateProvinceAbv: new FormControl(''),
      lineOfAuthorityAbv: new FormControl(''),
      lineOfAuthorityId: new FormControl(''),
      agentStateTable: new FormControl(''),
      plsIncentive1Tmpay: new FormControl(''),
      plsIncentive1Mrpay: new FormControl(''),
      incentive2PlusTmpay: new FormControl(''),
      incentive2PlusMrpay: new FormControl(''),
      licIncentive3Tmpay: new FormControl(''),
      licIncentive3Mrpay: new FormControl(''),
      isActive: new FormControl(''),
    });

    this.subscriptionData =
      this.adminComService.modes.licenseItem.changed.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.adminDataService.licenseItemChanged.subscribe((license: any) => {
            this.licenseForm.patchValue({
              licenseId: license.licenseId,
              licenseName: license.licenseName,
              licenseAbv: license.licenseAbv,
              stateProvinceAbv: license.stateProvinceAbv,
              lineOfAuthorityAbv: license.lineOfAuthorityAbv,
              lineOfAuthorityId: license.lineOfAuthorityId,
              agentStateTable: license.agentStateTable,
              plsIncentive1Tmpay: license.plsIncentive1Tmpay,
              plsIncentive1Mrpay: license.plsIncentive1Mrpay,
              incentive2PlusMrpay: license.incentive2PlusMrpay,
              licIncentive3Tmpay: license.licIncentive3Tmpay,
              licIncentive3Mrpay: license.licIncentive3Mrpay,
              isActive: license.isActive,
            });
          });
        } else {
          this.licenseForm.reset();
        }
      });
  }

  onSubmit(): void {}

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }

}
