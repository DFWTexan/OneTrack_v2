import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditXborRequirementComponent } from './edit-xbor-requirement.component';

describe('EditXborRequirementComponent', () => {
  let component: EditXborRequirementComponent;
  let fixture: ComponentFixture<EditXborRequirementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditXborRequirementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditXborRequirementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
