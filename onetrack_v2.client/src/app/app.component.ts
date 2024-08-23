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
import { LicenseTech } from './_Models';
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
  branchCodes: any[] = [];

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

  onDashboardClick(event: Event) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading Dashboard...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/dashboard']);
      });
      dialogRef.close();
    }, 100);
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

  // Administration
  onCompanyEditClick(event: Event) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading Company Edit...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/admin/company-edit']);
      });
      dialogRef.close();
    }, 100);
  }

  onCompanyRequirementsClick(event: Event) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading Company Requirements...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/admin/company-requirements']);
      });
      dialogRef.close();
    }, 100);
  }

  onConEduEditClick(event: Event) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading Contiuing Edu Edit...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/admin/con-education-edit']);
      });
      dialogRef.close();
    }, 100);
  }

  onDropdownListEditClick(event: Event) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading Dropdown List Edit...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/admin/dropdown-list-edit']);
      });
      dialogRef.close();
    }, 100);
  }

  onExamEditClick(event: Event) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading Exam Edit...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/admin/examp-edit']);
      });
      dialogRef.close();
    }, 100);
  }

  onJobTitleLicensedClick(event: Event) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading Job Title Licensed...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/admin/job-title-license']);
      });
      dialogRef.close();
    }, 100);
  }

  onLicenseEditClick(event: Event) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading License Edit...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/admin/license-edit']);
      });
      dialogRef.close();
    }, 100);
  }

  onLicenseTechEditClick(event: Event) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading License Tech Edit...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/admin/license-tech-edit']);
      });
      dialogRef.close();
    }, 100);
  }

  onPreEduEditClick(event: Event) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading Pre Edu Edit...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/admin/pre-education-edit']);
      });
      dialogRef.close();
    }, 100);
  }

  onProductEditClick(event: Event) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading Product Edit...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/admin/product-edit']);
      });
      dialogRef.close();
    }, 100);
  }

  onStateLicenseRequirementsClick(event: Event) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading State License Requirements...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/admin/state-lic-reuirements']);
      });
      dialogRef.close();
    }, 100);
  }

  onStateProvinceEditClick(event: Event) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading State Province Edit...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/admin/state-province-edit']);
      });
      dialogRef.close();
    }, 100);
  }

  onXBorLicenseRequirementsClick(event: Event) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading XBor License Requirements...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/admin/xbor-lic-requirements']);
      });
      dialogRef.close();
    }, 100);
  }

  // Team Members
  onAddMemberClick(event: Event) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading Add Team Member...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/team/add-member']);
      });
      dialogRef.close();
    }, 100);
  }

  onSearchMemberClick(event: Event) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading Search Team Member...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/team/search-members']);
      });
      dialogRef.close();
    }, 100);
  }
  
  // Worklists
  onWorklistClick(event: Event) {
    event.preventDefault();

    const dialogRef = this.dialog.open(InfoDialogComponent, {
      data: { message: 'Loading Worklist...' },
    });

    // Delay the execution of the blocking operation
    setTimeout(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/work-lists']);
      });
      dialogRef.close();
    }, 100);
  }

  title = 'onetrack_v2';

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
