import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTicklerComponent } from './add-tickler.component';

describe('AddTicklerComponent', () => {
  let component: AddTicklerComponent;
  let fixture: ComponentFixture<AddTicklerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddTicklerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddTicklerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
