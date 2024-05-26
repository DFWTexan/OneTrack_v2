import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditHoursTakenComponent } from './edit-hours-taken.component';

describe('EditHoursTakenComponent', () => {
  let component: EditHoursTakenComponent;
  let fixture: ComponentFixture<EditHoursTakenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditHoursTakenComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditHoursTakenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
