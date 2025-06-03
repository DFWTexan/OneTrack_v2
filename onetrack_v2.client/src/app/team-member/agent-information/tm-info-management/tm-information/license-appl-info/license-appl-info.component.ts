import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-license-appl-info',
  templateUrl: './license-appl-info.component.html',
  styleUrl: './license-appl-info.component.css',
})
@Injectable()
export class LicenseApplInfoComponent implements OnInit, OnDestroy {
  constructor() {}

  ngOnInit(): void {
    // Initialization logic can go here
  }

  onCloseModal() {
    const modalDiv = document.getElementById('modal-license-appl-info');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {
    // Cleanup logic can go here
  }
}
