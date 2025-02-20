import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewEmployeeCommunicationComponent } from './view-employee-communication.component';

describe('ViewEmployeeCommunicationComponent', () => {
  let component: ViewEmployeeCommunicationComponent;
  let fixture: ComponentFixture<ViewEmployeeCommunicationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ViewEmployeeCommunicationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewEmployeeCommunicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
