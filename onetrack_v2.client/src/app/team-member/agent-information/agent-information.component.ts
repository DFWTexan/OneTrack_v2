import { Component } from '@angular/core';

import { ModalService } from '../../_services';

@Component({
  selector: 'app-agent-information',
  templateUrl: './agent-information.component.html',
  styleUrl: './agent-information.component.css'
})
export class AgentInformationComponent {
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
