import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditLicenseRenewalComponent } from './edit-license-renewal.component';

describe('EditLicenseRenewalComponent', () => {
  let component: EditLicenseRenewalComponent;
  let fixture: ComponentFixture<EditLicenseRenewalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditLicenseRenewalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditLicenseRenewalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
