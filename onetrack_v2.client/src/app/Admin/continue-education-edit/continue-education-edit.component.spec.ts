import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContinueEducationEditComponent } from './continue-education-edit.component';

describe('ContinueEducationEditComponent', () => {
  let component: ContinueEducationEditComponent;
  let fixture: ComponentFixture<ContinueEducationEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ContinueEducationEditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ContinueEducationEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
