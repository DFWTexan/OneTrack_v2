import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditTransferHistComponent } from './edit-transfer-hist.component';

describe('EditTransferHistComponent', () => {
  let component: EditTransferHistComponent;
  let fixture: ComponentFixture<EditTransferHistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditTransferHistComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditTransferHistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
