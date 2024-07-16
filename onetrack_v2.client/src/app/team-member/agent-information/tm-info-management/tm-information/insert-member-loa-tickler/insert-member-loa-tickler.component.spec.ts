import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsertMemberLoaTicklerComponent } from './insert-member-loa-tickler.component';

describe('InsertMemberLoaTicklerComponent', () => {
  let component: InsertMemberLoaTicklerComponent;
  let fixture: ComponentFixture<InsertMemberLoaTicklerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InsertMemberLoaTicklerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InsertMemberLoaTicklerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
