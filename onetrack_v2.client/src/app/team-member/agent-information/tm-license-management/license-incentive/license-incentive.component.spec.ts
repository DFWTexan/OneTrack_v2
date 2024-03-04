import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseIncentiveComponent } from './license-incentive.component';

describe('LicenseIncentiveComponent', () => {
  let component: LicenseIncentiveComponent;
  let fixture: ComponentFixture<LicenseIncentiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LicenseIncentiveComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LicenseIncentiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
