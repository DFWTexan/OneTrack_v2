import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncentiveInfoComponent } from './incentive-info.component';

describe('IncentiveInfoComponent', () => {
  let component: IncentiveInfoComponent;
  let fixture: ComponentFixture<IncentiveInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [IncentiveInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(IncentiveInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
