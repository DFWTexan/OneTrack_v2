import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TmInformationComponent } from './tm-information.component';

describe('TmInformationComponent', () => {
  let component: TmInformationComponent;
  let fixture: ComponentFixture<TmInformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TmInformationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TmInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
