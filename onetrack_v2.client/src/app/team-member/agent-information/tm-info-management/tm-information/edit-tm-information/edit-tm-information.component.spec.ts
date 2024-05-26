import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditTmInformationComponent } from './edit-tm-information.component';

describe('EditTmInformationComponent', () => {
  let component: EditTmInformationComponent;
  let fixture: ComponentFixture<EditTmInformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditTmInformationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditTmInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
