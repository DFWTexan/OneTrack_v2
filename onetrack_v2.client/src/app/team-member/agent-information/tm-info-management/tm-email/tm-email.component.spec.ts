import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TmEmailComponent } from './tm-email.component';

describe('TmEmailComponent', () => {
  let component: TmEmailComponent;
  let fixture: ComponentFixture<TmEmailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TmEmailComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TmEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
