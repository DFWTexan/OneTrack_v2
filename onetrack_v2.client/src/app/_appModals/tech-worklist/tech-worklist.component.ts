import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-tech-worklist',
  templateUrl: './tech-worklist.component.html',
  styleUrl: './tech-worklist.component.css'
})
export class TechWorklistComponent implements OnInit, OnDestroy  {
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
      const modalDiv = document.getElementById('modal-tech-work-list');
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
