import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCoRequirementComponent } from './edit-co-requirement.component';

describe('EditCoRequirementComponent', () => {
  let component: EditCoRequirementComponent;
  let fixture: ComponentFixture<EditCoRequirementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditCoRequirementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditCoRequirementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
