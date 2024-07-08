import { Component } from '@angular/core';

@Component({
  selector: 'app-add-pre-exam',
  templateUrl: './add-pre-exam.component.html',
  styleUrl: './add-pre-exam.component.css'
})
export class AddPreExamComponent {
  onCloseModal() {
    const modalDiv = document.getElementById('modal-add-preExam-item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }
}
