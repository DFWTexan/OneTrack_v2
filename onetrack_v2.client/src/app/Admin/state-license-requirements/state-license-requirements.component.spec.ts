import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StateLicenseRequirementsComponent } from './state-license-requirements.component';

describe('StateLicenseRequirementsComponent', () => {
  let component: StateLicenseRequirementsComponent;
  let fixture: ComponentFixture<StateLicenseRequirementsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StateLicenseRequirementsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(StateLicenseRequirementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
