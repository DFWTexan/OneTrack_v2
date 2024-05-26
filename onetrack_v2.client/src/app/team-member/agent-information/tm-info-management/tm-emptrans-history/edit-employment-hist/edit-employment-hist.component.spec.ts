import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditEmploymentHistComponent } from './edit-employment-hist.component';

describe('EditEmploymentHistComponent', () => {
  let component: EditEmploymentHistComponent;
  let fixture: ComponentFixture<EditEmploymentHistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditEmploymentHistComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditEmploymentHistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
