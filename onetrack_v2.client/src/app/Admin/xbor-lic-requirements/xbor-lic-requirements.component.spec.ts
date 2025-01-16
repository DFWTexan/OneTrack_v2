import { ComponentFixture, TestBed } from '@angular/core/testing';

import { XborLicRequirementsComponent } from './xbor-lic-requirements.component';

describe('XborLicRequirementsComponent', () => {
  let component: XborLicRequirementsComponent;
  let fixture: ComponentFixture<XborLicRequirementsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [XborLicRequirementsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(XborLicRequirementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
