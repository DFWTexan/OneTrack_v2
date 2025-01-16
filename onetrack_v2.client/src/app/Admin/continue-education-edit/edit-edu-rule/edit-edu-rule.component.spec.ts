import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditEduRuleComponent } from './edit-edu-rule.component';

describe('EditEduRuleComponent', () => {
  let component: EditEduRuleComponent;
  let fixture: ComponentFixture<EditEduRuleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditEduRuleComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditEduRuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
