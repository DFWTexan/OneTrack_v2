import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MultiSelectDdlComponent } from './multi-select-ddl.component';

describe('MultiSelectDdlComponent', () => {
  let component: MultiSelectDdlComponent;
  let fixture: ComponentFixture<MultiSelectDdlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MultiSelectDdlComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MultiSelectDdlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
