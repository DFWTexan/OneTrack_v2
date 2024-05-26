import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TmEmptransHistoryComponent } from './tm-emptrans-history.component';

describe('TmEmptransHistoryComponent', () => {
  let component: TmEmptransHistoryComponent;
  let fixture: ComponentFixture<TmEmptransHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TmEmptransHistoryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TmEmptransHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
