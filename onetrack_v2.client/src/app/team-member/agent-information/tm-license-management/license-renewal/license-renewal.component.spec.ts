import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseRenewalComponent } from './license-renewal.component';

describe('LicenseRenewalComponent', () => {
  let component: LicenseRenewalComponent;
  let fixture: ComponentFixture<LicenseRenewalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LicenseRenewalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LicenseRenewalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
