import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TechTicklerItemsComponent } from './tech-tickler-items.component';

describe('TechTicklerItemsComponent', () => {
  let component: TechTicklerItemsComponent;
  let fixture: ComponentFixture<TechTicklerItemsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TechTicklerItemsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TechTicklerItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
