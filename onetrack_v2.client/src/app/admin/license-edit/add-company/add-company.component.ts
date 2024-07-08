import { Component } from '@angular/core';

@Component({
  selector: 'app-add-company',
  templateUrl: './add-company.component.html',
  styleUrl: './add-company.component.css'
})
export class AddCompanyComponent {

  onCloseModal() {
    const modalDiv = document.getElementById('modal-add-company-item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }
}
