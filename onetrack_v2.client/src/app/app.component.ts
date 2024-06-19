import {
  Component,
  ViewChild,
  OnInit,
  Inject,
  Injectable,
  OnDestroy,
} from '@angular/core';
import { BreakpointObserver } from '@angular/cdk/layout';
import { MatSidenav } from '@angular/material/sidenav';
import { Subscription } from 'rxjs';

import {
  AppComService,
  DropdownDataService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from './_services';
import { environment } from './environments/environment';

@Injectable()
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit, OnDestroy {
  // @ViewChild(MatSidenav)
  // sidenav!: MatSidenav;
  // isMobile = true;
  // isCollapsed = true;
  private subscriptions = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    public appComService: AppComService,
    private drpdwnDataService: DropdownDataService,
    private userInfoService: UserAcctInfoDataService
  ) {}

  ngOnInit() {
    if (environment.isDevLoginEnabled) {
      this.appComService.updateIsLoggedIn(true);
      this.userInfoService.updateUserAcctInfo({
        displayName: 'Erish Faggett',
        // displayName: 'Dyan Knapp',
        soeid: 'T2229513',
        // soeid: 'T3304243', // Dyan Knapp
        email: 'erish.faggett@omf.com',
        enabled: true,
        employeeId: '2229513',
        homeDirectory: '\\\\corp.fin\\users\\EVNAS_Users\\3\\T2229513',
        lastLogon: '2024-05-07T14:30:59.1941967Z',
        isAdminRole: true,
        isTechRole: null,
        isReadRole: true,
        isSuperUser: true,
      });
    }

    // DROPDOWN DATA
    this.subscriptions.add(
      this.drpdwnDataService
        .fetchDropdownData('GetBranches')
        .subscribe((branchNames: { value: string; label: string }[]) => {
          // this.branchNames = branchNames;
          this.drpdwnDataService.updateBranchNames(branchNames);
        })
    );
    this.subscriptions.add(
      this.drpdwnDataService
        .fetchDropdownNumberValueData('GetScoreNumbers')
        .subscribe((scoreNumbers: { value: number; label: string }[]) => {
          // this.scoreNumbers = scoreNumbers;
          this.drpdwnDataService.updateScoreNumbers(scoreNumbers);
        })
    );
    this.subscriptions.add(
      this.drpdwnDataService
        .fetchDropdownNumberValueData('GetEmployerAgencies')
        .subscribe((employerAgencies: { value: number; label: string }[]) => {
          // this.employerAgencies = employerAgencies;
          this.drpdwnDataService.updateEmployerAgencies(employerAgencies);
        })
    );
    this.subscriptions.add(
      this.drpdwnDataService
        .fetchDropdownNumberValueData('GetLicenseStatuses')
        .subscribe((licenseStatuses: { value: number; label: string }[]) => {
          // this.licenseStatuses = licenseStatuses;
          this.drpdwnDataService.updateLicenseStatuses(licenseStatuses);
        })
    );
    this.subscriptions.add(
      this.drpdwnDataService
        .fetchDropdownNumberValueData('GetLicenseNames')
        .subscribe((licenseNames: { value: number; label: string }[]) => {
          // this.licenseNames = licenseNames;
          this.drpdwnDataService.updateLicenseNames(licenseNames);
        })
    );
  }

  title = 'onetrack_v2';

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
