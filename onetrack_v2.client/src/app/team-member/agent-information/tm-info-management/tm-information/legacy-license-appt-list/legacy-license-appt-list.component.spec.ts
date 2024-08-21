import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LegacyLicenseApptListComponent } from './legacy-license-appt-list.component';

describe('LegacyLicenseApptListComponent', () => {
  let component: LegacyLicenseApptListComponent;
  let fixture: ComponentFixture<LegacyLicenseApptListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LegacyLicenseApptListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LegacyLicenseApptListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
