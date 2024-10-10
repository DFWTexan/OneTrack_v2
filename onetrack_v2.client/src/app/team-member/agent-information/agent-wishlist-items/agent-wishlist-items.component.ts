import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-agent-wishlist-items',
  templateUrl: './agent-wishlist-items.component.html',
  styleUrl: './agent-wishlist-items.component.css'
})
export class AgentWishlistItemsComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();

  constructor() {}

  ngOnInit(): void {}

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


  ngOnDestroy(): void {}

}
