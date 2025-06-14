import {
  Component,
  ViewChild,
  OnInit,
  Inject,
  Injectable,
  OnDestroy,
  ChangeDetectorRef,
  AfterViewInit,
} from '@angular/core';
// import { BreakpointObserver } from '@angular/cdk/layout';
// import { MatSidenav } from '@angular/material/sidenav';
import { interval, Subscription, switchMap } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';

import {
  AgentDataService,
  AppComService,
  ConfigService,
  DropdownDataService,
  ErrorMessageService,
  LicIncentiveInfoDataService,
  MiscDataService,
  ModalService,
  TicklerMgmtDataService,
  UserAcctInfoDataService,
} from './_services';
import { LicenseTech, UserAcctInfo } from './_Models';
import { InfoDialogComponent } from './_components';

@Injectable()
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit, OnDestroy, AfterViewInit {
  // config: any;
  userAcctInfo: UserAcctInfo = {} as UserAcctInfo;
  branchCodes: any[] = [];
  licenseTechs: any[] = [{ value: null, label: 'Loading...' }];
  impesonatorRole: string | null = null;
  openTechWorklistCount = 0;
  openTicklerCount = 0;
  isDevLoginEnabled = null;
  isLoggedIn: boolean | null = null;
  isImpersonationEnabled: boolean | null = null;
  isShowModalIncentiveInfo = false;

  title = 'onetrack_v2';
  private subscriptions = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    public appComService: AppComService,
    private drpdwnDataService: DropdownDataService,
    private agentDataService: AgentDataService,
    public licIncentiveInfoDataService: LicIncentiveInfoDataService,
    private miscDataService: MiscDataService,
    public ticklerMgmtDataService: TicklerMgmtDataService,
    public dialog: MatDialog,
    private router: Router,
    protected modalService: ModalService,
    public configService: ConfigService,
    private cdr: ChangeDetectorRef,
    public userAcctInfoDataService: UserAcctInfoDataService
  ) {
    this.isLoggedIn = this.appComService.isLoggedIn;
    this.openTicklerCount = this.appComService.openTicklerCount;
    this.licenseTechs = this.licIncentiveInfoDataService.licenseTeches;
  }

  ngOnInit() {
    this.configService.loadConfig();
    const configuration = this.configService.getConfig();

    console.log(
      'EMFTEST (AppComponent: ngOnInit) - configuration => \n',
      configuration
    );

    if (configuration) {
      if (configuration.environment == 'LOCAL') {
        this.isLoggedIn = true;
        this.appComService.updateIsLoggedIn(this.isLoggedIn);
        this.isDevLoginEnabled = configuration.isDevLoginEnabled;
        this.isImpersonationEnabled = configuration.isImpersonationEnabled;
        if (configuration.userInfo) {
          this.userAcctInfoDataService.updateUserAcctInfo(
            configuration.userInfo
          );
          this.userAcctInfo = configuration.userInfo;
        }
      }
    }

    this.startFetchingTicklerInfo();

    // setTimeout(() => {
    //   this.cdr.detectChanges();
    // }, 0);

    this.subscriptions.add(
      this.appComService.isShowModalIncentiveInfoChanged.subscribe(
        (isShowModalIncentiveInfo: boolean) => {
          this.isShowModalIncentiveInfo = isShowModalIncentiveInfo;
          this.cdr.detectChanges(); // Explicitly trigger change detection
        }
      )
    );

    this.subscriptions.add(
      this.appComService.openTicklerCountChanged.subscribe(
        (openTicklerCount: number) => {
          this.openTicklerCount = openTicklerCount;
          this.cdr.detectChanges(); // Explicitly trigger change detection
        }
      )
    );

    this.subscriptions.add(
      this.appComService.openTechWorklistCountChanged.subscribe(
        (openTechWrklistCount: number) => {
          this.openTechWorklistCount = openTechWrklistCount;
          this.cdr.detectChanges(); // Explicitly trigger change detection
        }
      )
    );

    this.subscriptions.add(
      this.appComService.isLoggedInChanged.subscribe((isLoggedIn: boolean) => {
        this.isLoggedIn = isLoggedIn;
        this.cdr.detectChanges();
      })
    );

    this.subscriptions.add(
      this.userAcctInfoDataService.userAcctInfoChanged.subscribe(
        (userAcctInfo: UserAcctInfo) => {
          this.userAcctInfo = userAcctInfo;

          console.log(
            'EMFTEST (AppComponent: userAcctInfoChanged) - userAcctInfo => \n',
            userAcctInfo
          );
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

  ngAfterViewInit() {
    setTimeout(() => {
      this.cdr.detectChanges();
    }, 0);
  }

  startFetchingTicklerInfo(): void {
    this.subscriptions.add(
      interval(5000)
        .pipe(
          switchMap(() =>
            this.ticklerMgmtDataService.fetchTicklerInfo(
              0,
              this.userAcctInfoDataService.userAcctInfo.licenseTechID ?? 0,
              0
            )
          )
        )
        .subscribe((ticklerInfoItems: any) => {
          // this.ticklerInfoItems = ticklerInfoItems;
          this.cdr.detectChanges();
        })
    );
  }

  isBadgeVisible(): boolean {
    if (this.openTicklerCount > 0) {
      return true;
    } else {
      return false;
    }
  }

  isTechWorklistActive(): boolean {
    const currentRoute = this.router.url;
    if (
      this.openTechWorklistCount > 0 &&
      this.userAcctInfoDataService.userAcctInfo.licenseTechID !== null &&
      this.userAcctInfoDataService.userAcctInfo.licenseTechID !== 0 &&
      this.userAcctInfoDataService.userAcctInfo.licenseTechID !== undefined
    ) {
      // if (this.isRouteActive(currentRoute)) {
      //   return false;
      // } else {
      return true;
      // }
    } else {
      return false;
    }
  }

  isNotificationActive(): boolean {
    const currentRoute = this.router.url;
    if (
      this.openTicklerCount > 0 &&
      this.userAcctInfoDataService.userAcctInfo.licenseTechID !== null &&
      this.userAcctInfoDataService.userAcctInfo.licenseTechID !== 0 &&
      this.userAcctInfoDataService.userAcctInfo.licenseTechID !== undefined
    ) {
      // if (this.isRouteActive(currentRoute)) {
      //   return false;
      // } else {
      return true;
      // }
    } else {
      return false;
    }
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

  isRouteActive(route: string): boolean {
    return this.router.url === route;
  }

  onTechWrkListNotificationClick(event: Event, vObject: any) {
    event.preventDefault();

    if (this.isRouteActive(vObject.route)) {
      console.log('The route is already active.');
      return;
    }

    this.modalService.open('modal-tech-work-list');

    // const dialogRef = this.dialog.open(InfoDialogComponent, {
    //   data: { message: vObject.message },
    // });

    // // Delay the execution of the blocking operation
    // setTimeout(() => {
    //   this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
    //     this.router.navigate([vObject.route]);
    //   });
    //   dialogRef.close();
    // }, 100);
  }

  onTicklerNotificationClick(event: Event, vObject: any) {
    event.preventDefault();

    if (this.isRouteActive(vObject.route)) {
      // console.log('The route is already active.');
      return;
    }

    this.modalService.open('modal-tech-tickler-items');

    // const dialogRef = this.dialog.open(InfoDialogComponent, {
    //   data: { message: vObject.message },
    // });

    // // Delay the execution of the blocking operation
    // setTimeout(() => {
    //   this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
    //     this.router.navigate([vObject.route]);
    //   });
    //   dialogRef.close();
    // }, 100);
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

   openModal() {
    const modalDiv = document.getElementById('myModal');
    if (modalDiv != null) {
      modalDiv.style.display = 'block';
    }
  }
  closeModal() {
    const modalDiv = document.getElementById('myModal');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
