import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AppComService,
  ConfigService,
  LicIncentiveInfoDataService,
  UserAcctInfoDataService,
} from '../../_services';
import { UserAcctInfo } from '../../_Models';
import { InfoDialogComponent } from '../info-dialog/info-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-impersonation',
  templateUrl: './impersonation.component.html',
  styleUrl: './impersonation.component.css',
})
export class ImpersonationComponent implements OnInit, OnDestroy {
  selectedSOIED: string | null = null;
  userAcctInfo: UserAcctInfo = {} as UserAcctInfo;
  config: any = null;
  licenseTechs: any[] = [{ value: null, label: 'Loading...' }];
  impesonatorRole: string | null = null;
  isLoggedIn: boolean = false;

  private subscriptions = new Subscription();

  constructor(
    public appComService: AppComService,
    public configService: ConfigService,
    public dialog: MatDialog,
    private router: Router,
    public userAcctInfoDataService: UserAcctInfoDataService,
    public licIncentiveInfoDataService: LicIncentiveInfoDataService
  ) {
    this.licenseTechs = [
      { value: null, label: 'Select' },
      ...this.licIncentiveInfoDataService.licenseTeches,
    ];
    this.userAcctInfo = this.userAcctInfoDataService.userAcctInfo;
  }

  ngOnInit() {
    this.configService.loadConfig();
    this.config = this.configService.getConfig();

    this.subscriptions.add(
      this.appComService.isLoggedInChanged.subscribe((isLoggedIn: boolean) => {
        this.isLoggedIn = isLoggedIn;
      })
    );

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
    this.selectedSOIED = (event.target as HTMLInputElement).value;
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

  onImpersonate() {
    if (this.selectedSOIED && this.selectedSOIED !== 'null') {
      this.subscriptions.add(
        this.userAcctInfoDataService
          .fetchLicenseTechBySOEID(this.selectedSOIED)
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
              isQARole: false,
              isSuperUser: false,
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

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Load User...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/dashboard']);
      });
      dialogRef.close();
    }, 100);
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
