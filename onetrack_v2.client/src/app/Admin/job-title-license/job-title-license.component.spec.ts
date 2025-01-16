import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobTitleLicenseComponent } from './job-title-license.component';

describe('JobTitleLicenseComponent', () => {
  let component: JobTitleLicenseComponent;
  let fixture: ComponentFixture<JobTitleLicenseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [JobTitleLicenseComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(JobTitleLicenseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
