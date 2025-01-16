import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditLicenseTechComponent } from './edit-license-tech.component';

describe('EditLicenseTechComponent', () => {
  let component: EditLicenseTechComponent;
  let fixture: ComponentFixture<EditLicenseTechComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditLicenseTechComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditLicenseTechComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
