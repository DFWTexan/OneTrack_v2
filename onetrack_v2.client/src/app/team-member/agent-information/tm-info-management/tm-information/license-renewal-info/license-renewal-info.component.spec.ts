import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseRenewalInfoComponent } from './license-renewal-info.component';

describe('LicenseRenewalInfoComponent', () => {
  let component: LicenseRenewalInfoComponent;
  let fixture: ComponentFixture<LicenseRenewalInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LicenseRenewalInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LicenseRenewalInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
