import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPreEduItemComponent } from './edit-pre-edu-item.component';

describe('EditPreEduItemComponent', () => {
  let component: EditPreEduItemComponent;
  let fixture: ComponentFixture<EditPreEduItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditPreEduItemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditPreEduItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
