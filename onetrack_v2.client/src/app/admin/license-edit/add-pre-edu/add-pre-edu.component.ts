import { Component } from '@angular/core';

@Component({
  selector: 'app-add-pre-edu',
  templateUrl: './add-pre-edu.component.html',
  styleUrl: './add-pre-edu.component.css'
})
export class AddPreEduComponent {
  onCloseModal() {
    const modalDiv = document.getElementById('modal-add-preEdu-Item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }
}
