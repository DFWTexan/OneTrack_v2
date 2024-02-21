import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchTeamMemberComponent } from './search-team-member.component';

describe('SearchTeamMemberComponent', () => {
  let component: SearchTeamMemberComponent;
  let fixture: ComponentFixture<SearchTeamMemberComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SearchTeamMemberComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SearchTeamMemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
