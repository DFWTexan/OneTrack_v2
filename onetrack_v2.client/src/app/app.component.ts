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
import { MatDialog } from '@angular/material/dialog';

import {
  AgentDataService,
  AppComService,
  DropdownDataService,
  ErrorMessageService,
  LicIncentiveInfoDataService,
  MiscDataService,
  UserAcctInfoDataService,
} from './_services';
import { environment } from './environments/environment';
import { MinLengthValidator } from '@angular/forms';
import { LicenseTech, UserAcctInfo } from './_Models';
import { InfoDialogComponent } from './_components';
import { Router } from '@angular/router';

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
  userAcctInfo: UserAcctInfo = {} as UserAcctInfo;
  branchCodes: any[] = [];
  licenseTechs: any[] = [];
  impesonatorRole: string | null = null;
  openTicklerCount = 0;

  private subscriptions = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    public appComService: AppComService,
    private drpdwnDataService: DropdownDataService,
    private agentDataService: AgentDataService,
    public licIncentiveInfoDataService: LicIncentiveInfoDataService,
    private miscDataService: MiscDataService,
    public dialog: MatDialog,
    private router: Router,
    private userInfoService: UserAcctInfoDataService
  ) {
    this.openTicklerCount = this.appComService.openTicklerCount;
    this.userAcctInfo = this.userInfoService.userAcctInfo;
    this.licenseTechs = this.licIncentiveInfoDataService.licenseTeches;
  }

  ngOnInit() {
    if (environment.isDevLoginEnabled) {
      this.appComService.updateIsLoggedIn(true);
      this.userInfoService.updateUserAcctInfo({
        licenseTechId: null,
        displayName: 'Erish Faggett',
        // displayName: 'Dyan Knapp',
        soeid: 'T2229513',
        // soeid: 'T3304243', // Dyan Knapp
        email: 'erish.faggett@omf.com',
        enabled: true,
        employeeId: '2229513',
        homeDirectory: '\\\\corp.fin\\users\\EVNAS_Users\\3\\T2229513',
        lastLogon: Date.now().toString(),
        isAdminRole: true,
        isTechRole: null,
        isReadRole: true,
        isSuperUser: true,
      });
    }

    this.subscriptions.add(
      this.appComService.isLoggedInChanged.subscribe((isLoggedIn: boolean) => {
        this.appComService.isLoggedIn = isLoggedIn;
      })
    );

    this.subscriptions.add(
      this.appComService.openTicklerCountChanged.subscribe(
        (openTicklerCount: number) => {
          this.openTicklerCount = openTicklerCount;
        }
      )
    );

    this.subscriptions.add(
      this.userInfoService.userAcctInfoChanged.subscribe(
        (userAcctInfo: UserAcctInfo) => {
          this.userAcctInfo = userAcctInfo;
        }
      )
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
    this.subscriptions.add(
      this.drpdwnDataService
        .fetchDropdownNumberValueData('GetJobTitles')
        .subscribe((jobTitles: { value: number; label: string }[]) => {
          //   this.jobTitles = [
          //     { value: '0', label: 'Select Job Title' },
          //     ...jobTitles,
          //   ];
          //   this.newAgentForm.get('JobTitleID')?.setValue(0);
          this.drpdwnDataService.updateJobTitles(jobTitles);
        })
    );
    this.subscriptions.add(
      this.agentDataService
        .fetchBranchCodes()
        .subscribe((branchCodes: any[]) => {
          this.branchCodes = [
            { branchCode: 'Select Branch Code' },
            ...branchCodes,
          ];
          // this.newAgentForm.get('branchCode')?.setValue('Select Branch Code');
        })
    );
    this.subscriptions.add(
      this.drpdwnDataService
        .fetchDropdownData('GetRollOutGroups')
        .subscribe((rollOutGroups: { value: string; label: string }[]) => {
          // this.branchNames = branchNames;
          this.drpdwnDataService.updateRollOutGroups(rollOutGroups);
        })
    );
    this.subscriptions.add(
      this.licIncentiveInfoDataService
        .fetchDMManagers()
        .subscribe((dmManagers: any[]) => {
          this.licIncentiveInfoDataService.updateDMManagers(dmManagers);
        })
    );
    this.subscriptions.add(
      this.licIncentiveInfoDataService
        .fetchBMManagers()
        .subscribe((licIncentives: any[]) => {
          this.licIncentiveInfoDataService.updateBMManagers(licIncentives);
        })
    );
    this.subscriptions.add(
      this.licIncentiveInfoDataService
        .fetchLicenseTeches()
        .subscribe((licTeches: any[]) => {
          this.licIncentiveInfoDataService.updateLicenseTeches(licTeches);
        })
    );
    this.subscriptions.add(
      this.miscDataService
        .fetchConEduInfo('EducationStartDate')
        .subscribe((items: any[]) => {
          this.drpdwnDataService.updateConEduStartDateItems(items);
        })
    );
    this.subscriptions.add(
      this.miscDataService
        .fetchConEduInfo('EducationEndDate')
        .subscribe((items: any[]) => {
          this.drpdwnDataService.updateConEduEndDateItems(items);
        })
    );
    this.subscriptions.add(
      this.miscDataService
        .fetchConEduInfo('Exception')
        .subscribe((items: any[]) => {
          this.drpdwnDataService.updateConEduExceptions(items);
        })
    );
    this.subscriptions.add(
      this.miscDataService
        .fetchConEduInfo('Exemption')
        .subscribe((items: any[]) => {
          this.drpdwnDataService.updateConEduExemptions(items);
        })
    );
    this.subscriptions.add(
      this.drpdwnDataService
        .fetchDropdownNumberValueData('GetLicenseLineOfAuthority')
        .subscribe((items: { value: number; label: string }[]) => {
          this.drpdwnDataService.updateLineOfAuthorities(items);
        })
    );
    this.subscriptions.add(
      this.miscDataService.fetchDocumetTypes().subscribe((items: string[]) => {
        this.drpdwnDataService.updateDocumentTypes(items);
      })
    );
    this.subscriptions.add(
      this.miscDataService
        .fetchLicenseTechs()
        .subscribe((items: LicenseTech[]) => {
          let mappedLicenseTechs: { value: any; label: string }[] = [];
          mappedLicenseTechs = items.map((tech) => ({
            value: tech.licenseTechId,
            label: tech.techName,
          }));
          this.drpdwnDataService.updateLicenseTechs(mappedLicenseTechs);
        })
    );
  }

  onPanelOpened(vTitle: string) {
    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      dialogRef.close();
    }, 200);
  }

  onLinkClick(event: Event, vObject: any) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: vObject.message },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate([vObject.route]);
      });
      dialogRef.close();
    }, 100);
  }

  title = 'onetrack_v2';

  onImpersonateChange(event: Event) {
    const soeid = (event.target as HTMLInputElement).value;
    this.subscriptions.add(
      this.userInfoService
        .fetchLicenseTechBySOEID(soeid)
        .subscribe((licenseTech: any) => {
          this.userInfoService.updateUserAcctInfo({
            licenseTechId: licenseTech.licenseTechId,
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
  }

  onRoleChange(event: Event) {
    const role = (event.target as HTMLInputElement).value;
    this.impesonatorRole = role;

    this.userInfoService.updateUserAcctInfo({
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
