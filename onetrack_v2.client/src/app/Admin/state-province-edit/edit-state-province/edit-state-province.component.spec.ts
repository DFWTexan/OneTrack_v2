import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditStateProvinceComponent } from './edit-state-province.component';

describe('EditStateProvinceComponent', () => {
  let component: EditStateProvinceComponent;
  let fixture: ComponentFixture<EditStateProvinceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditStateProvinceComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditStateProvinceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
