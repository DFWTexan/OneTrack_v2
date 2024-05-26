import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TmContinuingEduComponent } from './tm-continuing-edu.component';

describe('TmContinuingEduComponent', () => {
  let component: TmContinuingEduComponent;
  let fixture: ComponentFixture<TmContinuingEduComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TmContinuingEduComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TmContinuingEduComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
