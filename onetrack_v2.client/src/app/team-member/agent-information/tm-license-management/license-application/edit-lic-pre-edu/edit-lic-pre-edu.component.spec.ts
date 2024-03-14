import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditLicPreEduComponent } from './edit-lic-pre-edu.component';

describe('EditLicPreEduComponent', () => {
  let component: EditLicPreEduComponent;
  let fixture: ComponentFixture<EditLicPreEduComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditLicPreEduComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditLicPreEduComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
