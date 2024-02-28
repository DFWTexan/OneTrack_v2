import { Component } from '@angular/core';

import { ModalService } from '../_services';

@Component({
  selector: 'app-emf-test-page',
  templateUrl: './emf-test-page.component.html',
  styleUrl: './emf-test-page.component.css'
})
export class EmfTestPageComponent {
  bodyText = 'This text can be updated in modal 1';

  constructor(protected modalService: ModalService) { }

  // Add your code here
  openModal() {
    const modalDiv = document.getElementById('myModal');
    if (modalDiv != null) {
      modalDiv.style.display = 'block';
    }
  }
  closeModal() {
    const modalDiv = document.getElementById('myModal');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }
}
