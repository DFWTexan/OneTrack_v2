import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditLicApplInfoComponent } from './edit-lic-appl-info.component';

describe('EditLicApplInfoComponent', () => {
  let component: EditLicApplInfoComponent;
  let fixture: ComponentFixture<EditLicApplInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditLicApplInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditLicApplInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
