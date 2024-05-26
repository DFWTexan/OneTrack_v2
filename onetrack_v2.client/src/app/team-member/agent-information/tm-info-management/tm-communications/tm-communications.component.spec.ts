import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TmCommunicationsComponent } from './tm-communications.component';

describe('TmCommunicationsComponent', () => {
  let component: TmCommunicationsComponent;
  let fixture: ComponentFixture<TmCommunicationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TmCommunicationsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TmCommunicationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
