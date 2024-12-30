import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuickFindComponent } from './quick-find.component';

describe('QuickFindComponent', () => {
  let component: QuickFindComponent;
  let fixture: ComponentFixture<QuickFindComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [QuickFindComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(QuickFindComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
