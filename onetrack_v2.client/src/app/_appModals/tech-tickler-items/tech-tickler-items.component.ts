import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-tech-tickler-items',
  templateUrl: './tech-tickler-items.component.html',
  styleUrl: './tech-tickler-items.component.css'
})
export class TechTicklerItemsComponent implements OnInit, OnDestroy {

  constructor() { }

  ngOnInit(): void {
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
