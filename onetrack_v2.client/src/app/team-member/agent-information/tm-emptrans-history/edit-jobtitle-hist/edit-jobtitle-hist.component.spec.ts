import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditJobtitleHistComponent } from './edit-jobtitle-hist.component';

describe('EditJobtitleHistComponent', () => {
  let component: EditJobtitleHistComponent;
  let fixture: ComponentFixture<EditJobtitleHistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditJobtitleHistComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditJobtitleHistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
