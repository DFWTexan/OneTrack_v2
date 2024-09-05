import { Component, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-agent-tickler-info',
  templateUrl: './agent-tickler-info.component.html',
  styleUrl: './agent-tickler-info.component.css'
})
export class AgentTicklerInfoComponent {
  @Output() callParentRefreshData = new EventEmitter<any>();
  isFormSubmitted: boolean = false;
  form = new FormGroup({
    ticklerID: new FormControl(0),
    licenseTechID: new FormControl(''),
    employmentID: new FormControl(''),
    employeeLicenseID: new FormControl(''),
    ticklerMessage: new FormControl(''),
    ticklerDueDate: new FormControl(''),
  });

  private subscriptionData = new Subscription();

  constructor() {}

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

}
