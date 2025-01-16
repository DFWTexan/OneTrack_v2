import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentTicklerInfoComponent } from './agent-tickler-info.component';

describe('AgentTicklerInfoComponent', () => {
  let component: AgentTicklerInfoComponent;
  let fixture: ComponentFixture<AgentTicklerInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AgentTicklerInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AgentTicklerInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
