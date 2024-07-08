import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPreExamComponent } from './add-pre-exam.component';

describe('AddPreExamComponent', () => {
  let component: AddPreExamComponent;
  let fixture: ComponentFixture<AddPreExamComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddPreExamComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddPreExamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
