import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditStateRequirementComponent } from './edit-state-requirement.component';

describe('EditStateRequirementComponent', () => {
  let component: EditStateRequirementComponent;
  let fixture: ComponentFixture<EditStateRequirementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditStateRequirementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditStateRequirementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
