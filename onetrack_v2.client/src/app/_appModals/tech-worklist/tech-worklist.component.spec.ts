import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TechWorklistComponent } from './tech-worklist.component';

describe('TechWorklistComponent', () => {
  let component: TechWorklistComponent;
  let fixture: ComponentFixture<TechWorklistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TechWorklistComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TechWorklistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
