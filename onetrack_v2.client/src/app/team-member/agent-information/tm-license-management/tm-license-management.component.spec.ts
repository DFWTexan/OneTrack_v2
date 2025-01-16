import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TmLicenseManagementComponent } from './tm-license-management.component';

describe('TmLicenseManagementComponent', () => {
  let component: TmLicenseManagementComponent;
  let fixture: ComponentFixture<TmLicenseManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TmLicenseManagementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TmLicenseManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
