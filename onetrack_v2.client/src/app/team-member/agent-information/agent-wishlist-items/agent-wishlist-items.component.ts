import {
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { Subscription } from 'rxjs';
import { AgentDataService, UserAcctInfoDataService } from '../../../_services';
import { MatDialog } from '@angular/material/dialog';

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

  onCloseWorklistItem(worklistInfo: any): void {
    this.onCloseModal();
  }

  ngOnDestroy(): void {}
}
