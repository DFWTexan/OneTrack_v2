import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCorequirementHistComponent } from './edit-corequirement-hist.component';

describe('EditCorequirementHistComponent', () => {
  let component: EditCorequirementHistComponent;
  let fixture: ComponentFixture<EditCorequirementHistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditCorequirementHistComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditCorequirementHistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
