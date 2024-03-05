import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-license-info',
  templateUrl: './edit-license-info.component.html',
  styleUrl: './edit-license-info.component.css'
})
export class EditLicenseInfoComponent implements OnInit {
  licenseForm: FormGroup;
  
  constructor(private fb: FormBuilder) {
    this.licenseForm = this.fb.group({
      employeeLicenseId: [{value: '', disabled: true}, Validators.required],
      licenseState: ['', Validators.required],
      lineOfAuthority: ['', Validators.required],
      licenseStatus: ['', Validators.required],
      employmentID: ['', Validators.required],
      licenseName: ['', Validators.required],
      licenseNumber: ['', Validators.required],
      resNoneRes: [''],
      originalIssueDate: ['', Validators.required],
      lineOfAuthIssueDate: ['', Validators.required],
      licenseEffectiveDate: ['', Validators.required],
      licenseExpirationDate: ['', Validators.required],
    });
  }

  ngOnInit(): void {}
}
