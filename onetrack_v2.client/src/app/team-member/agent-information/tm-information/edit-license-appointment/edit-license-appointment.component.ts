import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AgentDataService } from '../../../../_services';
import { LicenseAppointment } from '../../../../_Models';

@Component({
  selector: 'app-edit-license-appointment',
  templateUrl: './edit-license-appointment.component.html',
  styleUrl: './edit-license-appointment.component.css'
})
@Injectable()
export class EditLicenseAppointmentComponent implements OnInit, OnDestroy {
  form = new FormGroup({
    licenseID: new FormControl(''),
    employeeAppointmentID: new FormControl(''),
    appointmentEffectiveDate: new FormControl(''),
    appointmentStatus: new FormControl(''),
    employeeLicenseID: new FormControl(''),
    carrierDate: new FormControl(''),
    appointmentExpireDate: new FormControl(''),
    appointmentTerminationDate: new FormControl(''),
    companyID: new FormControl(''),
    retentionDate: new FormControl(''),
    companyAbbr: new FormControl(''),
  });

  licenseAppointment: LicenseAppointment = {} as LicenseAppointment;
  subscribeLicenseAppointment: Subscription;

  constructor(private agentService: AgentDataService) {
    this.subscribeLicenseAppointment = new Subscription();
  }

  ngOnInit(): void {
    this.subscribeLicenseAppointment = this.agentService.licenseAppointmentChanged.subscribe(
      (licenseAppointment: any) => {
        this.licenseAppointment = licenseAppointment;
        this.form.patchValue({
          licenseID: licenseAppointment.licenseID,
          employeeAppointmentID: licenseAppointment.employeeAppointmentID,
          appointmentEffectiveDate: licenseAppointment.appointmentEffectiveDate,
          appointmentStatus: licenseAppointment.appointmentStatus,
          employeeLicenseID: licenseAppointment.employeeLicenseID,
          carrierDate: licenseAppointment.carrierDate,
          appointmentExpireDate: licenseAppointment.appointmentExpireDate,
          appointmentTerminationDate: licenseAppointment.appointmentTerminationDate,
          companyID: licenseAppointment.companyID,
          retentionDate: licenseAppointment.retentionDate,
          companyAbbr: "TBD...",
        });
      }
    );
  }

  onSubmit() {
    console.log('EMFTest - (app-edit-license-appointment) onSubmit => \n', this.form.value);
  }

  ngOnDestroy(): void {
    this.subscribeLicenseAppointment.unsubscribe();
  }
}
