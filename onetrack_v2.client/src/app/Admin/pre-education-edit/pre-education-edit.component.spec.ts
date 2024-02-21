import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PreEducationEditComponent } from './pre-education-edit.component';

describe('PreEducationEditComponent', () => {
  let component: PreEducationEditComponent;
  let fixture: ComponentFixture<PreEducationEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PreEducationEditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PreEducationEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
