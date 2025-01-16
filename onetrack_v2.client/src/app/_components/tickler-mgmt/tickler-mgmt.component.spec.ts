import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicklerInfoComponent } from './tickler-mgmt.component';

describe('TicklerInfoComponent', () => {
  let component: TicklerInfoComponent;
  let fixture: ComponentFixture<TicklerInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TicklerInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TicklerInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
