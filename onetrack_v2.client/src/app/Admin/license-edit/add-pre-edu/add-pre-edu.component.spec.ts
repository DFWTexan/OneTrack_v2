import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPreEduComponent } from './add-pre-edu.component';

describe('AddPreEduComponent', () => {
  let component: AddPreEduComponent;
  let fixture: ComponentFixture<AddPreEduComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddPreEduComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddPreEduComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
