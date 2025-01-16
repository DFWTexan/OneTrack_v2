import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicklerSearchComponent } from './tickler-search.component';

describe('TicklerSearchComponent', () => {
  let component: TicklerSearchComponent;
  let fixture: ComponentFixture<TicklerSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TicklerSearchComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TicklerSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
