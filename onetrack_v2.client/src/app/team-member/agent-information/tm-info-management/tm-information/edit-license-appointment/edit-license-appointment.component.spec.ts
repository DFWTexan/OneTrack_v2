import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditLicenseAppointmentComponent } from './edit-license-appointment.component';

describe('EditLicenseAppointmentComponent', () => {
  let component: EditLicenseAppointmentComponent;
  let fixture: ComponentFixture<EditLicenseAppointmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditLicenseAppointmentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditLicenseAppointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
