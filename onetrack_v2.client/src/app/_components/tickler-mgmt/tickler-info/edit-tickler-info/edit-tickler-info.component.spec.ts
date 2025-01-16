import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditTicklerInfoComponent } from './edit-tickler-info.component';

describe('EditTicklerInfoComponent', () => {
  let component: EditTicklerInfoComponent;
  let fixture: ComponentFixture<EditTicklerInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditTicklerInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditTicklerInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
