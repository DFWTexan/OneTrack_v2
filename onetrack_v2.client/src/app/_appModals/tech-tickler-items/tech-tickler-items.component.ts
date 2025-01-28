import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import {
  AppComService,
  DashboardDataService,
  ModalService,
  TicklerMgmtDataService,
  UserAcctInfoDataService,
} from '../../_services';
import { TechWorklistData, TicklerInfo } from '../../_Models';
import { Router } from '@angular/router';
import { ConfirmDialogComponent } from '../../_components';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-tech-tickler-items',
  templateUrl: './tech-tickler-items.component.html',
  styleUrl: './tech-tickler-items.component.css',
})
export class TechTicklerItemsComponent implements OnInit, OnDestroy {
  loading: boolean = false;
  today: Date = new Date();
  techTicklerItems: any[] = [];
  selectedLicenseTechID: number | null = null;

  subscriptions: Subscription = new Subscription();

  constructor(
    public appComService: AppComService,
    public ticklerMgmtDataService: TicklerMgmtDataService,
    protected modalService: ModalService,
    public dialog: MatDialog,
    public dashboardDataService: DashboardDataService,
    private router: Router,
    public userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.subscriptions.add(
      this.appComService.techTicklerItemsChanged.subscribe(
        (techTicklerItems: any[]) => {
          this.techTicklerItems = techTicklerItems;
        }
      )
    );
  }

  fetchTicklerInfo(): void {
    this.subscriptions.add(
      this.ticklerMgmtDataService
        .fetchTicklerInfo(0, this.selectedLicenseTechID ?? 0, 0)
        .subscribe((ticklerInfoItems: any) => {
          this.techTicklerItems = ticklerInfoItems;
          this.appComService.updateTechTicklerItems(ticklerInfoItems);
          this.loading = false;
        })
    );
  }

  onGotoAgentInfo(employeeID: number): void {
    this.forceCloseModal();
    this.router.navigate(['../../team/agent-info', employeeID, 'tm-info-mgmt']);
  }

  isOverdue(ticklerDueDate: string | Date): boolean {
    const dueDate = new Date(ticklerDueDate);
    return dueDate < this.today;
  }

  onCloseTicklerItem(ticklerInfo: TicklerInfo): void {
    this.forceCloseModal();
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

  forceCloseModal() {
    // this.isFormSubmitted = false;
    // if (this.modeCloseModal === 'EDIT') {
    //   const modalDiv = document.getElementById('modal-edit-license-info');
    //   if (modalDiv != null) {
    //     modalDiv.style.display = 'none';
    //   }
    // } else {
    const modalDiv = document.getElementById('modal-tech-tickler-items');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
    // }
  }

  // onCloseModal() {
  //   if (this.licenseForm.dirty && !this.isFormSubmitted) {
  //     if (
  //       confirm('You have unsaved changes. Are you sure you want to close?')
  //     ) {
  //       // const modalDiv = document.getElementById('modal-edit-license-info');
  //       // if (modalDiv != null) {
  //       //   modalDiv.style.display = 'none';
  //       // }
  //       this.forceCloseModal();
  //       this.licenseForm.reset();
  //       // this.licenseForm.patchValue({
  //       //   jobTitleID: 0,
  //       //   isCurrent: false,
  //       // });
  //     }
  //   } else {
  //     this.isFormSubmitted = false;
  //     // const modalDiv = document.getElementById('modal-edit-license-info');
  //     // if (modalDiv != null) {
  //     //   modalDiv.style.display = 'none';
  //     // }
  //     this.forceCloseModal();
  //   }
  // }

  ngOnDestroy() {
    // this.subscription.unsubscribe();
  }
}
