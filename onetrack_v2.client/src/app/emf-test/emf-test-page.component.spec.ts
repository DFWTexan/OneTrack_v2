import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmfTestPageComponent } from './emf-test-page.component';

describe('EmfTestPageComponent', () => {
  let component: EmfTestPageComponent;
  let fixture: ComponentFixture<EmfTestPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmfTestPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmfTestPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
