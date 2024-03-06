import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditTmDetailComponent } from './edit-tm-detail.component';

describe('EditTmDetailComponent', () => {
  let component: EditTmDetailComponent;
  let fixture: ComponentFixture<EditTmDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditTmDetailComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditTmDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
