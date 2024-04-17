import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddIndexerComponent } from './add-indexer.component';

describe('AddIndexerComponent', () => {
  let component: AddIndexerComponent;
  let fixture: ComponentFixture<AddIndexerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddIndexerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddIndexerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
