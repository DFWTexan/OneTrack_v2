import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SideMenuNavComponent } from './side-menu-nav.component';

describe('SideMenuNavComponent', () => {
  let component: SideMenuNavComponent;
  let fixture: ComponentFixture<SideMenuNavComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SideMenuNavComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SideMenuNavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
