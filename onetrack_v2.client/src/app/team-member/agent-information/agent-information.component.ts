import { Component } from '@angular/core';

@Component({
  selector: 'app-agent-information',
  templateUrl: './agent-information.component.html',
  styleUrl: './agent-information.component.css'
})
export class AgentInformationComponent {

  constructor() { }

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
