import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NultiSelectDropdownComponent } from './nulti-select-dropdown.component';

describe('NultiSelectDropdownComponent', () => {
  let component: NultiSelectDropdownComponent;
  let fixture: ComponentFixture<NultiSelectDropdownComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NultiSelectDropdownComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NultiSelectDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
