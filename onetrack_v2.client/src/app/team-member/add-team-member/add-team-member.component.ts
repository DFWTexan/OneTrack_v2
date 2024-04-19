import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { zip } from 'rxjs';

@Component({
  selector: 'app-add-team-member',
  templateUrl: './add-team-member.component.html',
  styleUrl: './add-team-member.component.css',
})
export class AddTeamMemberComponent {
  newAgentForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.newAgentForm = this.fb.group({
      firstName: [''],
      middleName: [''],
      lastName: [''],
      preferedName: [''],
      dateOfBirth: [''],
      employeeSSN: [''],
      teamID: [''],
      soeid: [''],
      nationalProducerNumber: [''],
      employerAgency: [''],
      workState: [''],
      resState: [''],
      jobTitle: [''],
      hireDate: [''],
      branchCode: [''],
      address1: [''],
      address2: [''],
      city: [''],
      zip: [''],
      email: [''],
      homePhone: [''],
      workPhone: [''],
      fax: [''],
    });
  }

  onSubmit() {
    // Handle form submission
  }

  onCancel() {
    // Handle cancel action
  }
}
