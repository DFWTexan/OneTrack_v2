import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
@Component({
  selector: 'app-add-indexer',
  templateUrl: './add-indexer.component.html',
  styleUrl: './add-indexer.component.css'
})
export class AddIndexerComponent {
  onSubmit(form: NgForm) {
    // some code here
  }

  onFileSelected(event: any) {
    // some code here
  }

  onCancel() {
    const modalDiv = document.getElementById('modal-document-index');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }
}
