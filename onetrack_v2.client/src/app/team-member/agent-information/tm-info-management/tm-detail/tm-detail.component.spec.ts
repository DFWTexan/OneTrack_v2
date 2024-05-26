import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TmDetailComponent } from './tm-detail.component';

describe('TmDetailComponent', () => {
  let component: TmDetailComponent;
  let fixture: ComponentFixture<TmDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TmDetailComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TmDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
