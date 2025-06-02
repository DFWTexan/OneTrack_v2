import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseApplIconComponent } from './license-appl-icon.component';

describe('LicenseApplIconComponent', () => {
  let component: LicenseApplIconComponent;
  let fixture: ComponentFixture<LicenseApplIconComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LicenseApplIconComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LicenseApplIconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
