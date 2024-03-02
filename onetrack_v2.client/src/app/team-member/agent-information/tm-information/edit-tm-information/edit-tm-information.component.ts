import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-edit-tm-information',
  templateUrl: './edit-tm-information.component.html',
  styleUrl: './edit-tm-information.component.css'
})
export class EditTmInformationComponent {
  form = new FormGroup({
    preferredName: new FormControl(''),
    licenseId: new FormControl(''),
    licenseState: new FormControl(''),
    licenseName: new FormControl(''),
    licenseNumber: new FormControl(''),
    licenseStatus: new FormControl(''),
    affiliatedLicense: new FormControl(''),
    originalIssueDate: new FormControl(''),
    lineOfAuthIssueDate: new FormControl(''),
    effectiveDate: new FormControl(''),
    expirationDate: new FormControl(''),
    note: new FormControl('')
  });

  onSubmit() {
    console.log(this.form.value);
  }
}
