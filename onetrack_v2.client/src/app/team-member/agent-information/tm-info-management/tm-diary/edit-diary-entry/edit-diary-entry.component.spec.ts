import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditDiaryEntryComponent } from './edit-diary-entry.component';

describe('EditDiaryEntryComponent', () => {
  let component: EditDiaryEntryComponent;
  let fixture: ComponentFixture<EditDiaryEntryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditDiaryEntryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditDiaryEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
