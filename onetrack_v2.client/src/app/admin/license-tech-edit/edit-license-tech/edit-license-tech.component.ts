import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AdminComService, AdminDataService } from '../../../_services';

@Component({
  selector: 'app-edit-license-tech',
  templateUrl: './edit-license-tech.component.html',
  styleUrl: './edit-license-tech.component.css'
})
@Injectable()
export class EditLicenseTechComponent implements OnInit, OnDestroy {
  licenseTechForm!: FormGroup;
  subscriptionData: Subscription = new Subscription();

  constructor(
    public adminDataService: AdminDataService,
    public adminComService: AdminComService
  ) {}

  ngOnInit(): void {
    this.licenseTechForm = new FormGroup({
      licenseTechId: new FormControl(''),
      soeid: new FormControl(''),
      firstName: new FormControl(''),
      lastName: new FormControl(''),
      isActive: new FormControl(''),
      teamNum: new FormControl(''),
      licenseTechPhone: new FormControl(''),
      licenseTechFax: new FormControl(''),
      licenseTechEmail: new FormControl(''),
      techName: new FormControl(''),
    });

    this.subscriptionData =
      this.adminComService.modes.licenseTech.changed.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.adminDataService.licenseTechChanged.subscribe((licenseTech: any) => {
            this.licenseTechForm.patchValue({
              licenseTechId: licenseTech.licenseTechId,
              soeid: licenseTech.soeid,
              firstName: licenseTech.firstName,
              lastName: licenseTech.lastName,
              isActive: licenseTech.isActive,
              teamNum: licenseTech.teamNum,
              licenseTechPhone: licenseTech.licenseTechPhone,
              licenseTechFax: licenseTech.licenseTechFax,
              licenseTechEmail: licenseTech.licenseTechEmail,
              techName: licenseTech.techName,
            });
          });
        } else {
          this.licenseTechForm.reset();
        }
      });
  }

  onSubmit(): void {}

  onCloseModal() {
    const modalDiv = document.getElementById('modal-edit-license-tech');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }

}
