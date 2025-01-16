import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DropdownListEditComponent } from './dropdown-list-edit.component';

describe('DropdownListEditComponent', () => {
  let component: DropdownListEditComponent;
  let fixture: ComponentFixture<DropdownListEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DropdownListEditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DropdownListEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
