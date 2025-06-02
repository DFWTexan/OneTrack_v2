import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-license-appl-icon',
  templateUrl: './license-appl-icon.component.html',
  styleUrl: './license-appl-icon.component.css'
})
@Injectable()
export class LicenseApplIconComponent implements OnInit, OnDestroy  {

constructor() {}
  ngOnInit(): void {
    // Initialization logic can go here
  }

  forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-lic-pre-exam');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  onCloseModal() {
    // if (this.licPreExamForm.dirty && !this.isFormSubmitted) {
    //   if (
    //     confirm('You have unsaved changes. Are you sure you want to close?')
    //   ) {
    //     this.forceCloseModal();
    //     this.licPreExamForm.reset();
    //     // this.licPreEduForm.patchValue({
    //     //   applicationStatus: 'Select',
    //     //   applicationType: 'Select',
    //     // });
    //   }
    // } else {
    //   this.isFormSubmitted = false;
      this.forceCloseModal();
    // }
  }

  ngOnDestroy(): void {
    // Cleanup logic can go here
  }
}
