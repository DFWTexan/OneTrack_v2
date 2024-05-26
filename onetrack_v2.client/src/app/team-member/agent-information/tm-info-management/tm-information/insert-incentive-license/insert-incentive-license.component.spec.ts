import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsertIncentiveLicenseComponent } from './insert-incentive-license.component';

describe('InsertIncentiveLicenseComponent', () => {
  let component: InsertIncentiveLicenseComponent;
  let fixture: ComponentFixture<InsertIncentiveLicenseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InsertIncentiveLicenseComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InsertIncentiveLicenseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
