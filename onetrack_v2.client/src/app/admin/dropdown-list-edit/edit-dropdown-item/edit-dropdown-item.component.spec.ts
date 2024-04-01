import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditDropdownItemComponent } from './edit-dropdown-item.component';

describe('EditDropdownItemComponent', () => {
  let component: EditDropdownItemComponent;
  let fixture: ComponentFixture<EditDropdownItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditDropdownItemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditDropdownItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
