import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPreEducationComponent } from './edit-pre-education.component';

describe('EditPreEducationComponent', () => {
  let component: EditPreEducationComponent;
  let fixture: ComponentFixture<EditPreEducationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditPreEducationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditPreEducationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
