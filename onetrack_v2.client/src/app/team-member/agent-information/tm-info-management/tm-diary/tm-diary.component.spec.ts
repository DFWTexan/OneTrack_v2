import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TmDiaryComponent } from './tm-diary.component';

describe('TmDiaryComponent', () => {
  let component: TmDiaryComponent;
  let fixture: ComponentFixture<TmDiaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TmDiaryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TmDiaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
