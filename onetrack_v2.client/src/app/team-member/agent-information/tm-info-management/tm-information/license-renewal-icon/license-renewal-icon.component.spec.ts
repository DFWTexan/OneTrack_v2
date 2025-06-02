import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseRenewalIconComponent } from './license-renewal-icon.component';

describe('LicenseRenewalIconComponent', () => {
  let component: LicenseRenewalIconComponent;
  let fixture: ComponentFixture<LicenseRenewalIconComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LicenseRenewalIconComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LicenseRenewalIconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
