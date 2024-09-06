import { AfterViewInit, ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { MatExpansionModule } from '@angular/material/expansion';

import {
  AppComService,
  DashboardDataService,
  // DropdownDataService,
  LicIncentiveInfoDataService,
  MiscDataService,
  ModalService,
  TicklerMgmtComService,
  TicklerMgmtDataService,
  UserAcctInfoDataService,
  WorkListDataService,
} from '../_services';
import { Subscription } from 'rxjs';
import { StockTickler, TicklerInfo, UserAcctInfo } from '../_Models';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../_components';
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

  private subscriptions = new Subscription();

  adBankerImportInfo: any = {};
  worklistData: any[] = [];
  worklistNames: string[] = ['Loading...'];
  defaultWorkListName: string = 'Loading...';

  workListName: string = 'Agent Address Change';
  licenseTech: string = 'T9999999';
  worklistdate: string = new Date().toISOString().split('T')[0];
  licenseTechs: any[] = [];

  selectedWorkListName = 'Loading...';
  selectedLicenseTech = 'T9999999';
  selectedDate: string | null;
  isTechActive: boolean = true;

  adBankerData: any[] = [];
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

  modifiedBy: string[] = ['Loading...'];
  modifiedBySelected: string = 'Select Tech';
  startDate: string = new Date(new Date().setDate(new Date().getDate()))
    .toISOString()
    .split('T')[0];
  endDate: string = new Date(new Date().setDate(new Date().getDate() + 1))
    .toISOString()
    .split('T')[0];
  auditLogData: any[] = [];

  constructor(
    public appComService: AppComService,
    public workListDataService: WorkListDataService,
    public ticklerMgmtDataService: TicklerMgmtDataService,
    public ticklerMgmtComService: TicklerMgmtComService,
    public miscDataService: MiscDataService,
    public dashboardDataService: DashboardDataService,
    // private drpdwnDataService: DropdownDataService,
    public licIncentiveInfoDataService: LicIncentiveInfoDataService,
    public dialog: MatDialog,
    protected modalService: ModalService,
    private cdr: ChangeDetectorRef,
    private router: Router,
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
          if (userAcctInfo.licenseTechId) {
            this.selectedLicenseTechID = userAcctInfo.licenseTechId;
            this.fetchTicklerInfo();
          }
        }
      )
    );

    this.subscriptions.add(
      this.miscDataService.fetchWorkListNames().subscribe((worklistNames) => {
        this.worklistNames = worklistNames;
        this.selectedWorkListName = worklistNames[0];
      })
    );

    this.subscriptions.add(
      this.miscDataService.fetchLicenseTechs().subscribe((items: any[]) => {
        let mappedLicenseTechs: { value: any; label: string }[] = [];
        mappedLicenseTechs = items.map((tech) => ({
          value: tech.licenseTechId,
          label: tech.techName,
        }));
        this.licenseTechs = mappedLicenseTechs;
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
          this.modifiedBy = ['Select Tech', ...modifiedBy];
        })
    );

    this.subscriptions.add(
      this.dashboardDataService
        .fetchAuditLog(this.startDate, this.endDate, null)
        .subscribe((auditLogData) => {
          this.auditLogData = auditLogData;
        })
    );

    setTimeout(() => {
      this.userAcctInfo.licenseTechId = 2; // Example value
      this.cdr.detectChanges(); // Manually trigger change detection
    }, 0);

    this.getAdBankerData();
  }

  ngAfterViewInit(): void {
    if (this.userAcctInfo.licenseTechId) {
      this.selectedLicenseTechID = this.userAcctInfo.licenseTechId;
      this.fetchTicklerInfo();
    }
      
  }

  // WORKLIST
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
        })
    );
  }

  onChangeWorkListName(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedWorkListName = value;
    this.fetchWorkListData();
  }

  onChangeImportDate(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.selectedDate = value;
    this.fetchWorkListData();
  }

  // TICKLER
  fetchTicklerInfo(): void {
    this.subscriptions.add(
      this.ticklerMgmtDataService
        .fetchTicklerInfo(0, this.selectedLicenseTechID ?? 0, 0)
        .subscribe((ticklerInfoItems: any) => {
          this.ticklerInfoItems = ticklerInfoItems;
                    
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
    this.router.navigate(['../../team/agent-info',employeeID,'tm-info-mgmt']);
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
        })
    );
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
  onChangeImportStaus(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;
    this.adBankerImportStatus = value;

    this.getAdBankerData();
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
        .subscribe((auditLogData) => {
          this.auditLogData = auditLogData;
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
  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
