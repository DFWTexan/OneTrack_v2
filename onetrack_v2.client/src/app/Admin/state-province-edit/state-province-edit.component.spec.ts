import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StateProvinceEditComponent } from './state-province-edit.component';

describe('StateProvinceEditComponent', () => {
  let component: StateProvinceEditComponent;
  let fixture: ComponentFixture<StateProvinceEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StateProvinceEditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(StateProvinceEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
