import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentWishlistItemsComponent } from './agent-wishlist-items.component';

describe('AgentWishlistItemsComponent', () => {
  let component: AgentWishlistItemsComponent;
  let fixture: ComponentFixture<AgentWishlistItemsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AgentWishlistItemsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AgentWishlistItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
