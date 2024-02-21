import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyRequirementsComponent } from './company-requirements.component';

describe('CompanyRequirementsComponent', () => {
  let component: CompanyRequirementsComponent;
  let fixture: ComponentFixture<CompanyRequirementsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CompanyRequirementsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CompanyRequirementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
