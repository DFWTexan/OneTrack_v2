import {
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { Subscription } from 'rxjs';
import {
  AgentDataService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../_services';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../../_components';

@Component({
  selector: 'app-agent-wishlist-items',
  templateUrl: './agent-wishlist-items.component.html',
  styleUrl: './agent-wishlist-items.component.css',
})
export class AgentWishlistItemsComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  worklistItems: any[] = [];

  private subscriptionData = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    private agentDataService: AgentDataService,
    public dialog: MatDialog,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.subscriptionData.add(
      this.agentDataService.agentInfoChanged.subscribe((agentInfo) => {
        this.worklistItems = agentInfo.worklistItems;
      })
    );
  }

  onCloseModal() {
    const modalDiv = document.getElementById('modal-agent-worklist-info');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  onCloseWorklistItem(worklistInfo: any): void {
    this.onCloseModal();
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: 'Confirm Action',
        message:
          'You are about to CLOSE Worklist Item (' +
          worklistInfo.workListDataID +
          ')' +
          // ticklerInfo.lkpValue +
          '. Do you want to proceed?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.agentDataService
          .closeWorklistItem({
            WorkListDataID: worklistInfo.workListDataID,
            UserSOEID: this.userAcctInfoDataService.userAcctInfo.soeid,
          })
          .subscribe({
            next: (response) => {
              this.callParentRefreshData.emit();
            },
            error: (error) => {
              if (error.error && error.error.errMessage) {
                this.errorMessageService.setErrorMessage(
                  error.error.errMessage
                );
              }
            },
          }
        );
      }
    });
  }

  ngOnDestroy(): void {}
}
