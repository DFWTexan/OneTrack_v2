import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  ConfigService,
  LicIncentiveInfoDataService,
  UserAcctInfoDataService,
} from '../../_services';
import { UserAcctInfo } from '../../_Models';

@Component({
  selector: 'app-impersonation',
  templateUrl: './impersonation.component.html',
  styleUrl: './impersonation.component.css',
})
export class ImpersonationComponent implements OnInit, OnDestroy {
  userAcctInfo: UserAcctInfo = {} as UserAcctInfo;
  config: any = null;
  licenseTechs: any[] = [{ value: null, label: 'Loading...' }];
  impesonatorRole: string | null = null;

  private subscriptions = new Subscription();

  constructor(
    public configService: ConfigService,
    public userAcctInfoDataService: UserAcctInfoDataService,
    public licIncentiveInfoDataService: LicIncentiveInfoDataService
  ) {
    this.licenseTechs = this.licIncentiveInfoDataService.licenseTeches;
  }

  ngOnInit() {
    this.configService.loadConfig();
    this.config = this.configService.getConfig();

    this.subscriptions.add(
      this.licIncentiveInfoDataService.licenseTechesChanged.subscribe(
        (licenseTechs: any[]) => {
          this.licenseTechs = [
            { value: null, label: 'Select' },
            ...licenseTechs,
          ];
        }
      )
    );
  }

  onImpersonateChange(event: Event) {
    const soeid = (event.target as HTMLInputElement).value;

console.log('EMFTEST (onImpersonateChange) - soeid: ', soeid);

    if (soeid && soeid !== 'null') {
      this.subscriptions.add(
        this.userAcctInfoDataService
          .fetchLicenseTechBySOEID(soeid)
          .subscribe((licenseTech: any) => {
            this.userAcctInfoDataService.updateUserAcctInfo({
              licenseTechID: licenseTech.licenseTechId,
              displayName: licenseTech.techName,
              soeid: licenseTech.soeid,
              email: licenseTech.email,
              enabled: licenseTech.enabled,
              employeeId: licenseTech.employeeId,
              homeDirectory: licenseTech.homeDirectory,
              lastLogon: Date.now().toString(),
              isAdminRole: this.impesonatorRole === 'Admin',
              isTechRole: this.impesonatorRole === 'Tech',
              isReadRole: this.impesonatorRole === 'Read',
              isSuperUser: this.impesonatorRole === 'DVLPER',
            });
          })
      );
    } else {
      if (this.config.userInfo) {
        this.userAcctInfoDataService.updateUserAcctInfo(this.config.userInfo);
      }
    }
  }

  onRoleChange(event: Event) {
    const role = (event.target as HTMLInputElement).value;
    this.impesonatorRole = role;

    this.userAcctInfoDataService.updateUserAcctInfo({
      ...this.userAcctInfo,
      isAdminRole: role === 'Admin',
      isTechRole: role === 'Tech',
      isReadRole: role === 'Read',
      isSuperUser: role === 'DVLPER',
    });
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
