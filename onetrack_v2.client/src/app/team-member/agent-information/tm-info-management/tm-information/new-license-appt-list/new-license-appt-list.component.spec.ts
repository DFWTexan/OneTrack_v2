import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewLicenseApptListComponent } from './new-license-appt-list.component';

describe('NewLicenseApptListComponent', () => {
  let component: NewLicenseApptListComponent;
  let fixture: ComponentFixture<NewLicenseApptListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NewLicenseApptListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NewLicenseApptListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
