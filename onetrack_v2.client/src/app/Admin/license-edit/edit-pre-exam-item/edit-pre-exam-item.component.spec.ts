import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPreExamItemComponent } from './edit-pre-exam-item.component';

describe('EditPreExamItemComponent', () => {
  let component: EditPreExamItemComponent;
  let fixture: ComponentFixture<EditPreExamItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditPreExamItemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditPreExamItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
