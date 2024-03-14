import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditLicPreExamComponent } from './edit-lic-pre-exam.component';

describe('EditLicPreExamComponent', () => {
  let component: EditLicPreExamComponent;
  let fixture: ComponentFixture<EditLicPreExamComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditLicPreExamComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditLicPreExamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
