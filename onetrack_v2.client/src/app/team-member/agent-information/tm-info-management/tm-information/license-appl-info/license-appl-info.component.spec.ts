import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseApplInfoComponent } from './license-appl-info.component';

describe('LicenseApplInfoComponent', () => {
  let component: LicenseApplInfoComponent;
  let fixture: ComponentFixture<LicenseApplInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LicenseApplInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LicenseApplInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
