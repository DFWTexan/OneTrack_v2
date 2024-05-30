import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-tickler',
  templateUrl: './add-tickler.component.html',
  styleUrl: './add-tickler.component.css',
})
export class AddTicklerComponent {
  onSubmit(form: NgForm) {
    // some code here
  }

  onCancel() {
    const modalDiv = document.getElementById('modal-new-tickler');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }
}
