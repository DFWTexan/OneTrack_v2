import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TmInfoManagementComponent } from './tm-info-management.component';

describe('TmInfoManagementComponent', () => {
  let component: TmInfoManagementComponent;
  let fixture: ComponentFixture<TmInfoManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TmInfoManagementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TmInfoManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
