import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditJobTitleComponent } from './edit-job-title.component';

describe('EditJobTitleComponent', () => {
  let component: EditJobTitleComponent;
  let fixture: ComponentFixture<EditJobTitleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditJobTitleComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditJobTitleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
