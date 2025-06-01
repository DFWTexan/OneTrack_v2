import { Component, Injectable } from '@angular/core';

@Component({
  selector: 'app-incentive-info',
  templateUrl: './incentive-info.component.html',
  styleUrl: './incentive-info.component.css'
})
@Injectable()
export class IncentiveInfoComponent {

  constructor() { }

   onCloseModal() {
    const modalDiv = document.getElementById('modal-incentive-info');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

}
