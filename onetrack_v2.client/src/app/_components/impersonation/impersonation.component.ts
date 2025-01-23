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
    this.userAcctInfo = this.userAcctInfoDataService.userAcctInfo;
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
              isAdminRole: this.impesonatorRole === 'ADMIN' ? true : false,
              isTechRole: this.impesonatorRole === 'TECH' ? true : false,
              isReadRole: this.impesonatorRole === 'READ' ? true : false,
              isSuperUser:
                this.impesonatorRole === 'DVLPER'
                  ? true
                  : this.impesonatorRole === null
                  ? true
                  : false,
            });
          })
      );
    } else {
      if (this.config.environment === 'LOCAL') {
        if (this.config.userInfo) {
          this.userAcctInfoDataService.updateUserAcctInfo(this.config.userInfo);
        }
      } else {
        this.subscriptions.add(
          this.userAcctInfoDataService
            .fetchUserAcctInfo(
              this.userAcctInfoDataService.userID,
              this.userAcctInfoDataService.userPassWord
            )
            .subscribe((response) => {
              // console.log('EMFTEST - (app-login): response => \n', response);
            })
        );
      }
    }
  }

  onRoleChange(event: Event) {
    const role = (event.target as HTMLInputElement).value;
    this.impesonatorRole = role;

    (this.userAcctInfo.isAdminRole = role === 'ADMIN' ? true : false),
      (this.userAcctInfo.isTechRole = role === 'TECH' ? true : false),
      (this.userAcctInfo.isReadRole = role === 'READ' ? true : false),
      (this.userAcctInfo.isSuperUser =
        role === 'DVLPER' ? true : role === null ? true : false),
      this.userAcctInfoDataService.updateUserAcctInfo(this.userAcctInfo);
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
