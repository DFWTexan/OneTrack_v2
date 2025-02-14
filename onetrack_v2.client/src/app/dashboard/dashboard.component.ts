import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { interval, Subscription, switchMap } from 'rxjs';

import {
  AgentComService,
  AgentDataService,
  AppComService,
  DashboardDataService,
  EmployeeDataService,
  ErrorMessageService,
  // DropdownDataService,
  LicIncentiveInfoDataService,
  MiscDataService,
  ModalService,
  TicklerMgmtComService,
  TicklerMgmtDataService,
  UserAcctInfoDataService,
  WorkListDataService,
} from '../_services';
import {
  AuditModifyBy,
  StockTickler,
  TechWorklistData,
  TicklerInfo,
  UserAcctInfo,
} from '../_Models';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent, InfoDialogComponent } from '../_components';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent implements OnInit, AfterViewInit, OnDestroy {
  loading: boolean = false;
  today: Date = new Date();
  panelOpenState = false;
  userAcctInfo: UserAcctInfo = {} as UserAcctInfo;
  ticklerInfoItems: TicklerInfo[] = [];
  stockTicklerItems: StockTickler[] = [];
  licenseTechItems: any = ['Loading...'];
  selectedLicenseTechID: number | null = null;
  isOpenTicklerInfo: boolean = true;
  isStartDateDisabled: boolean = false;
  isEndDateDisabled: boolean = false;
  eventAction: string = '';
  vObject: any = {};

  private subscriptions = new Subscription();

  adBankerImportInfo: any = {};
  worklistData: any[] = [];
  techWorklistData: TechWorklistData[] = [];
  filteredTechWorklistData: TechWorklistData[] = [];
  worklistNames: string[] = ['Loading...'];
  defaultWorkListName: string = 'Loading...';

  workListName: string = 'ALL';
  licenseTech: string = 'T9999999';
  worklistdate: string = new Date().toISOString().split('T')[0];
  licenseTechs: any[] = [{ value: null, label: 'loading...' }];

  selectedWorkListName = 'Loading...';
  selectedLicenseTech = 'T9999999';
  selectedDate: string | null;
  isTechActive: boolean = true;

  adBankerData: any[] = [];
  adBankerDataFiltered: any[] = [];
  adBankerStartDate: string = new Date(
    new Date().setDate(new Date().getDate() - 1)
  )
    .toISOString()
    .split('T')[0];
  adBankerEndDate: string = new Date(
    new Date().setDate(new Date().getDate() + 1)
  )
    .toISOString()
    .split('T')[0];
  adBankerImportStatus: string = 'All';
  adBankerImportStatuses: string[] = ['All', 'Success', 'Failed'];

  modifiedBy: AuditModifyBy[] = [{ modifiedBy: null, fullName: 'Loading...' }];
  modifiedBySelected: string = 'Select Tech';
  startDate: string = new Date(new Date().setDate(new Date().getDate()))
    .toISOString()
    .split('T')[0];
  endDate: string = new Date(new Date().setDate(new Date().getDate() + 1))
    .toISOString()
    .split('T')[0];
  auditLogData: any[] = [];
  adBankerIncompleteCount = 0;
  adBankerAgentName: string = '';
  selectedRowIndex: number | null = null;
  selectedElement: string | null = null;

  constructor(
    public errorMessageService: ErrorMessageService,
    public appComService: AppComService,
    private agentDataService: AgentDataService,
    public workListDataService: WorkListDataService,
    public ticklerMgmtDataService: TicklerMgmtDataService,
    public ticklerMgmtComService: TicklerMgmtComService,
    public miscDataService: MiscDataService,
    public dashboardDataService: DashboardDataService,
    public licIncentiveInfoDataService: LicIncentiveInfoDataService,
    public dialog: MatDialog,
    protected modalService: ModalService,
    private cdr: ChangeDetectorRef,
    private router: Router,
    public agentComService: AgentComService,
    public employeeDataService: EmployeeDataService,
    public userAcctInfoDataService: UserAcctInfoDataService
  ) {
    this.selectedDate = null;
    this.userAcctInfo = this.userAcctInfoDataService.userAcctInfo;
    this.licenseTechs = this.licIncentiveInfoDataService.licenseTeches;
    this.isOpenTicklerInfo = this.appComService.isOpenTicklerInfo;
  }

  ngOnInit(): void {
    this.subscriptions.add(
      this.appComService.isOpenTicklerInfoChanged.subscribe((value) => {
        this.isOpenTicklerInfo = value;
      })
    );

    this.subscriptions.add(
      this.userAcctInfoDataService.userAcctInfoChanged.subscribe(
        (userAcctInfo: UserAcctInfo) => {
          this.userAcctInfo = userAcctInfo;
          if (userAcctInfo.licenseTechID) {
            this.selectedLicenseTechID = userAcctInfo.licenseTechID ?? 0;
            this.appComService.updateIsOpenTicklerInfo(true);
            this.fetchTechWorkListData();
            this.fetchTicklerInfo();
          } else {
            this.appComService.updateIsOpenTicklerInfo(false);
          }
        }
      )
    );

    this.subscriptions.add(
      this.miscDataService.fetchWorkListNames().subscribe((worklistNames) => {
        this.worklistNames = ['ALL', ...worklistNames];
        // this.selectedWorkListName = 'worklistNames[0]';
        this.selectedWorkListName = this.worklistNames[0];
      })
    );

    this.subscriptions.add(
      this.miscDataService.fetchLicenseTechs().subscribe((items: any[]) => {
        let mappedLicenseTechs: { value: any; label: string }[] = [];
        mappedLicenseTechs = items.map((tech) => ({
          value: tech.licenseTechId,
          label: tech.techName,
        }));
        this.licenseTechs = [
          { value: null, label: 'Select Tech' },
          ...mappedLicenseTechs,
        ];
      })
    );

    this.subscriptions.add(
      this.ticklerMgmtDataService
        .fetchLicenseTech(0, null)
        .subscribe((licenseTechItems) => {
          this.licenseTechItems = licenseTechItems;
        })
    );

    this.subscriptions.add(
      this.ticklerMgmtDataService
        .fetchStockTickler()
        .subscribe((stockTicklerItems) => {
          this.stockTicklerItems = stockTicklerItems;
        })
    );

    this.subscriptions.add(
      this.dashboardDataService
        .fetchAdBankerImportInfo()
        .subscribe((adBankerImportInfo) => {
          this.adBankerImportInfo = adBankerImportInfo;
        })
    );

    this.subscriptions.add(
      this.dashboardDataService
        .fetchAuditModifiedBy(this.isTechActive)
        .subscribe((modifiedBy) => {
          this.modifiedBy = [
            { modifiedBy: null, fullName: 'Select Tech' },
            ...modifiedBy,
          ];
        })
    );

    this.subscriptions.add(
      this.dashboardDataService
        .fetchAuditLog(this.startDate, this.endDate, null)
        .subscribe((auditLogData) => {
          this.auditLogData = auditLogData;
        })
    );

    this.adBankerIncompleteCount =
      this.dashboardDataService.adBankerIncompleteCount;
    this.subscriptions.add(
      this.dashboardDataService.adBankerIncompleteCountChanged.subscribe(
        (count) => {
          this.adBankerIncompleteCount = count;
        }
      )
    );

    this.getAdBankerData();
  }

  ngAfterViewInit(): void {
    if (this.userAcctInfo.licenseTechID) {
      this.selectedLicenseTechID = this.userAcctInfo.licenseTechID;
      this.fetchTicklerInfo();
    }
  }

  // WORKLIST
  selectRow(
    event: MouseEvent,
    worklistName: string,
    rowDataEmployeeID: string,
    rowDataTmNumberID: string
  ) {
    event.preventDefault();

    if (worklistName === 'In Process Status') {
      rowDataTmNumberID = rowDataTmNumberID.replace(/^T/, '');

      this.employeeDataService
        .fetchEmployeeByTmNumber(rowDataTmNumberID.trim())
        .subscribe((response) => {
          this.agentComService.updateShowLicenseMgmt(false);
          if (response === null) {
            alert('Employee not found');
          } else {
            this.router.navigate([
              '../../team/agent-info',
              response.employeeId,
              'tm-info-mgmt',
            ]);
          }
        });
    } else {
      const dialogRef = this.dialog.open(InfoDialogComponent, {
        data: { message: 'Loading Agent Info..' },
      });

      // Delay the execution of the blocking operation
      setTimeout(() => {
        this.router
          .navigateByUrl('/', { skipLocationChange: true })
          .then(() => {
            this.router.navigate([
              '../../team/agent-info',
              rowDataEmployeeID,
              'tm-info-mgmt',
            ]);
          });
        dialogRef.close();
      }, 100);
    }
  }
  fetchWorkListData(): void {
    this.subscriptions.add(
      this.workListDataService
        .fetchWorkListData(
          this.selectedWorkListName,
          this.selectedDate,
          this.userAcctInfoDataService.userAcctInfo.soeid
        )
        .subscribe((worklistData) => {
          this.worklistData = worklistData;
          this.cdr.detectChanges();
        })
    );
  }
  fetchTechWorkListData(): void {
    this.subscriptions.add(
      this.dashboardDataService
        .fetchTechWorklistData(this.userAcctInfoDataService.userAcctInfo.soeid)
        .subscribe((techWrklistData) => {
          this.techWorklistData = techWrklistData;
          this.appComService.updateTechWorklistItems(techWrklistData);
          this.filteredTechWorklistData = this.techWorklistData;
          this.cdr.detectChanges();
        })
    );
  }
  getWorkListDataField(workListData: string, index: number): string {
    const fields = workListData.split('|');
    return fields[index] || '';
  }
  onChangeWorkListName(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;

    this.selectedWorkListName = value;
    // this.fetchWorkListData();
    if (value === 'ALL') {
      this.filteredTechWorklistData = this.techWorklistData;
    } else {
      this.filteredTechWorklistData = this.techWorklistData.filter(
        (data) => data.workListName === value
      );
    }
  }
  onChangeImportDate(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedDate = value;
    this.fetchWorkListData();
  }
  onCloseWorklistItem(worklistDataID: TechWorklistData): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: 'Confirm Action',
        message:
          'You are about to CLOSE Worklist Item (' +
          worklistDataID +
          '). Do you want to proceed?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.dashboardDataService
          .closeWorklistItem({
            WorkListDataID: worklistDataID.workListDataId,
            UserSOEID: this.userAcctInfoDataService.userAcctInfo.soeid,
          })
          .subscribe({
            next: (response) => {
              // this.fetchWorkListData();
              this.fetchTechWorkListData();
            },
            error: (error) => {
              if (error.error && error.error.errMessage) {
                this.errorMessageService.setErrorMessage(
                  error.error.errMessage
                );
              }
            },
          });
      }
    });
  }

  // TICKLER
  fetchTicklerInfo(): void {
    this.subscriptions.add(
      this.ticklerMgmtDataService
        .fetchTicklerInfo(0, this.selectedLicenseTechID ?? 0, 0)
        .subscribe((ticklerInfoItems: any) => {
          this.ticklerInfoItems = ticklerInfoItems;
          this.appComService.updateTechTicklerItems(ticklerInfoItems);
          this.loading = false;
        })
    );
  }

  onChildCallRefreshData(): void {
    this.fetchTicklerInfo();
  }

  isOverdue(ticklerDueDate: string | Date): boolean {
    const dueDate = new Date(ticklerDueDate);
    return dueDate < this.today;
  }

  onCloseTicklerItem(ticklerInfo: TicklerInfo): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: 'Confirm Action',
        message:
          'You are about to CLOSE Tickler Item (' +
          ticklerInfo.ticklerId +
          ') - ' +
          ticklerInfo.lkpValue +
          '. Do you want to proceed?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.ticklerMgmtDataService
          .closeTicklerItem({
            TicklerID: ticklerInfo.ticklerId,
            TicklerCloseByLicenseTechID: ticklerInfo.licenseTechId,
            UserSOEID: this.userAcctInfoDataService.userAcctInfo.soeid,
          })
          .subscribe({
            next: (response) => {
              this.fetchTicklerInfo();
            },
            error: (error) => {
              console.error(error);
              // handle the error here
            },
          });
      }
    });
  }

  onDeleteTicklerItem(ticklerInfo: TicklerInfo): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: 'Confirm Action',
        message:
          'You are about to DELETE Tickler Item (' +
          ticklerInfo.ticklerId +
          ') - ' +
          ticklerInfo.lkpValue +
          '. Do you want to proceed?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.ticklerMgmtDataService
          .deleteTicklerItem({
            TicklerID: ticklerInfo.ticklerId,
            UserSOEID: this.userAcctInfoDataService.userAcctInfo.soeid,
          })
          .subscribe({
            next: (response) => {
              this.fetchTicklerInfo();
            },
            error: (error) => {
              console.error(error);
              // handle the error here
            },
          });
      }
    });
  }

  onLicenseTechChange(event: Event) {
    const licenseID = (event.target as HTMLInputElement).value;
    this.selectedLicenseTechID = parseInt(licenseID);
    this.fetchTicklerInfo();
  }

  onGotoAgentInfo(employeeID: number): void {
    this.router.navigate(['../../team/agent-info', employeeID, 'tm-info-mgmt']);
  }

  // ADBANKER
  getAdBankerData(): void {
    this.subscriptions.add(
      this.dashboardDataService
        .fetchADBankerData(
          this.adBankerStartDate,
          this.adBankerEndDate,
          this.adBankerImportStatus
        )
        .subscribe((data) => {
          this.adBankerData = data;
          this.adBankerDataFiltered = data;
        })
    );

    this.subscriptions.add(
      this.dashboardDataService
        .fetchIncompleteAdBankerCount()
        .subscribe((count) => {
          this.adBankerIncompleteCount = count;
        })
    );
  }
  isAdBankerIncompleted() {
    return this.adBankerIncompleteCount > 0;
  }

  onChangeADBankerStartDate(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.startDate = value;

    this.getAdBankerData();
  }
  onChangeADBankerEndDate(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.endDate = value;
    this.getAdBankerData();
  }
  onChangeADBankerAgentName(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    // this.adBankerAgentName = value;
    // this.getAdBankerData();
    this.adBankerDataFiltered = this.adBankerData.filter((data) =>
      data.studentName.toLowerCase().includes(value.toLowerCase())
    );
  }
  onChangeImportStaus(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.adBankerImportStatus = value;
    if (value == 'Failed') {
      this.adBankerStartDate = '2024-01-01';
      this.startDate = '2024-01-01';

      this.isStartDateDisabled = true;
      this.isEndDateDisabled = true;
    } else {
      this.adBankerStartDate = new Date(
        new Date().setDate(new Date().getDate() - 1)
      )
        .toISOString()
        .split('T')[0];
      this.startDate = new Date(new Date().setDate(new Date().getDate() - 1))
        .toISOString()
        .split('T')[0];
      this.isStartDateDisabled = false;
      this.isEndDateDisabled = false;
    }

    this.getAdBankerData();
  }
  openConfirmDialog(
    eventAction: string,
    msg: string,
    vObject: any,
    checkbox: HTMLInputElement
  ): void {
    this.eventAction = eventAction;
    this.vObject = vObject;
    this.vObject.UserSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;
    switch (eventAction) {
      case 'completeStatus':
        const dialogRef = this.dialog.open(ConfirmDialogComponent, {
          width: '250px',
          data: {
            title: 'Confirm Action',
            message:
              'You want to complete (' +
              this.vObject.studentName +
              ', ' +
              this.vObject.courseState +
              ')\n' +
              this.vObject.courseTitle,
            // ' ' +
            // this.agentInfo.lastName,
          },
        });

        dialogRef.afterClosed().subscribe((result) => {
          if (result) {
            this.subscriptions.add(
              this.dashboardDataService
                .updateIncompleteStatus(this.vObject)
                .subscribe({
                  next: (response) => {
                    this.getAdBankerData();
                  },
                  error: (error) => {
                    console.error(error);
                    // handle the error here
                    if (error.error && error.error.errMessage) {
                      this.errorMessageService.setErrorMessage(
                        error.error.errMessage
                      );
                    }
                  },
                })
            );
          } else {
            // Uncheck the checkbox if the user selects "No"
            checkbox.checked = false;
          }
        });
        break;
      case 'closeWorklistItem':
        const dialogRef_whisItem = this.dialog.open(ConfirmDialogComponent, {
          width: '250px',
          data: {
            title: 'Confirm Action',
            message:
              'You want to close Worklist Item (' +
              this.vObject.WorkListDataID +
              ')\n',
            // ' ' +
            // this.agentInfo.lastName,
          },
        });

        dialogRef_whisItem.afterClosed().subscribe((result) => {
          if (result) {
            this.dashboardDataService
              .closeWorklistItem({
                WorkListDataID: this.vObject.WorkListDataID,
                UserSOEID: this.userAcctInfoDataService.userAcctInfo.soeid,
              })
              .subscribe({
                next: (response) => {
                  this.fetchWorkListData();
                },
                error: (error) => {
                  if (error.error && error.error.errMessage) {
                    this.errorMessageService.setErrorMessage(
                      error.error.errMessage
                    );
                  }
                },
              });
          } else {
            // Uncheck the checkbox if the user selects "No"
            checkbox.checked = false;
          }
        });
        break;
      // case 'deleteLicense':
      //   const dialogRef_License = this.dialog.open(ConfirmDialogComponent, {
      //     width: '250px',
      //     data: {
      //       title: 'Confirm Action',
      //       message:
      //         'You are about to DELETE license (' +
      //         vObject.employeeLicenseID + ') ' +
      //         vObject.licenseName +
      //         '. Do you want to proceed?',
      //     },
      //   });
      //   dialogRef_License.afterClosed().subscribe((result) => {
      //     if (result) {
      //       this.subscriptions.add(
      //         this.agentDataService
      //         .deleteAgentLicense({
      //           employmentID: this.vObject.employmentID,
      //           employeeLicenseID: this.vObject.employeeLicenseID,
      //           userSOEID: this.userInfoDataService.userAcctInfo.soeid,
      //         })
      //         .subscribe({
      //           next: (response) => {
      //             this.router
      //               .navigateByUrl('/', { skipLocationChange: true })
      //               .then(() => {
      //                 this.router.navigate([
      //                   'team/agent-info',
      //                   this.agentDataService.agentInformation.employeeID,
      //                   'tm-license-mgmt',
      //                 ]);
      //               });
      //           },
      //           error: (error) => {
      //             console.error(error);
      //             // handle the error here
      //           },
      //         })
      //       );
      //     }
      //   });
      //   break;
      default:
        break;
    }
  }
  onSelectADBankerRow(event: MouseEvent, memberID: any): void {
    if (memberID === 0 || memberID === null || memberID === '') return;
    this.subscriptions.add(
      this.dashboardDataService
        .fetchEmployeeIdWithTMemberID(memberID)
        .subscribe((employeeID: number) => {
          this.router.navigate([
            '../../team/agent-info',
            employeeID,
            'tm-info-mgmt',
          ]);
        })
    );
  }

  // AUDIT LOG
  onChangesStartDate(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.startDate = value;

    this.subscriptions.add(
      this.dashboardDataService
        .fetchAuditLog(
          this.startDate,
          this.endDate,
          this.modifiedBySelected == 'Select Tech'
            ? null
            : this.modifiedBySelected
        )
        // .subscribe((auditLogData) => {
        //   this.auditLogData = auditLogData;
        // })
        .subscribe({
          next: (auditLogData) => {
            this.auditLogData = auditLogData;
          },
          error: (error) => {
            if (error.error && error.error.errMessage) {
              this.errorMessageService.setErrorMessage(error.error.errMessage);
            }
          },
        })
    );
  }
  onChangesEndDate(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.endDate = value;

    this.subscriptions.add(
      this.dashboardDataService
        .fetchAuditLog(
          this.startDate,
          this.endDate,
          this.modifiedBySelected == 'Select Tech'
            ? null
            : this.modifiedBySelected
        )
        .subscribe((auditLogData) => {
          this.auditLogData = auditLogData;
        })
    );
  }
  onChangeModifiedBy(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.modifiedBySelected = value;

    this.subscriptions.add(
      this.dashboardDataService
        .fetchAuditLog(
          this.startDate,
          this.endDate,
          value == 'Select Tech' ? null : value
        )
        .subscribe((auditLogData) => {
          this.auditLogData = auditLogData;
        })
    );
  }
  onChangeTechActive(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.checked;
    this.isTechActive = value;

    this.subscriptions.add(
      this.dashboardDataService
        .fetchAuditModifiedBy(value)
        .subscribe((modifiedBy) => {
          this.modifiedBy = [
            { modifiedBy: null, fullName: 'Select Tech' },
            ...modifiedBy,
          ];
        })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
