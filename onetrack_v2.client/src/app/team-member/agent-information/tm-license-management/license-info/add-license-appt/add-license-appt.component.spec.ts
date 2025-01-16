import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddLicenseApptComponent } from './add-license-appt.component';

describe('AddLicenseApptComponent', () => {
  let component: AddLicenseApptComponent;
  let fixture: ComponentFixture<AddLicenseApptComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddLicenseApptComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddLicenseApptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
