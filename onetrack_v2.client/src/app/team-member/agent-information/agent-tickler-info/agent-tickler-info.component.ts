import {
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AgentDataService, TicklerMgmtDataService, UserAcctInfoDataService } from '../../../_services';
import { ConfirmDialogComponent } from '../../../_components';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-agent-tickler-info',
  templateUrl: './agent-tickler-info.component.html',
  styleUrl: './agent-tickler-info.component.css',
})
export class AgentTicklerInfoComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  today: Date = new Date();
  ticklerItems: any[] = [];

  private subscriptionData = new Subscription();

  constructor(
    private agentDataService: AgentDataService,
    public ticklerMgmtDataService: TicklerMgmtDataService,
    public dialog: MatDialog,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {
    this.ticklerItems = this.agentDataService.agentInformation.ticklerItems;
  }

  ngOnInit(): void {
    this.subscriptionData.add(
      this.agentDataService.agentInfoChanged.subscribe((agentInfo) => {
        this.ticklerItems = agentInfo.ticklerItems;
      })
    );
  }

  isOverdue(ticklerDueDate: string | Date): boolean {
    const dueDate = new Date(ticklerDueDate);
    return dueDate < this.today;
  }

  onCloseTicklerItem(ticklerInfo: any): void {
    this.onCloseModal();
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: 'Confirm Action',
        message:
          'You are about to CLOSE Tickler Item (' +
          ticklerInfo.ticklerID +
          ')' +
          // ticklerInfo.lkpValue +
          '. Do you want to proceed?',
      },
    }); 

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
       
        console.log('EMFTEST (onCloseTicklerItem: afterClosed) - ticklerInfo => \n', ticklerInfo);  
       
        this.ticklerMgmtDataService
          .closeTicklerItem({
            TicklerID: ticklerInfo.ticklerID,
            TicklerCloseByLicenseTechID: ticklerInfo.licenseTechID,
            UserSOEID: this.userAcctInfoDataService.userAcctInfo.soeid,
          })
          .subscribe({
            next: (response) => {
              this.callParentRefreshData.emit();
            },
            error: (error) => {
              console.error(error);
              // handle the error here
            },
          });
      }
    });
  }

  onCloseModal() {
    const modalDiv = document.getElementById('modal-agent-tickler-info');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
    // if (this.jobTitleForm.dirty && !this.isFormSubmitted) {
    //   if (
    //     confirm('You have unsaved changes. Are you sure you want to close?')
    //   ) {
    //     const modalDiv = document.getElementById('modal-edit-jobTitle-history');
    //     if (modalDiv != null) {
    //       modalDiv.style.display = 'none';
    //     }
    //     this.jobTitleForm.reset();
    //     this.jobTitleForm.patchValue({
    //       jobTitleID: 0,
    //       isCurrent: false,
    //     });
    //   }
    // } else {
    //   this.isFormSubmitted = false;
    //   const modalDiv = document.getElementById('modal-edit-jobTitle-history');
    //   if (modalDiv != null) {
    //     modalDiv.style.display = 'none';
    //   }
    // }
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
