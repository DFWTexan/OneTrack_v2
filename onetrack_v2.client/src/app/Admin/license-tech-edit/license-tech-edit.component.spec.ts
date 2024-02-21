import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseTechEditComponent } from './license-tech-edit.component';

describe('LicenseTechEditComponent', () => {
  let component: LicenseTechEditComponent;
  let fixture: ComponentFixture<LicenseTechEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LicenseTechEditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LicenseTechEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
