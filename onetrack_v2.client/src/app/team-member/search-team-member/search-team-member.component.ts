import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-search-team-member',
  templateUrl: './search-team-member.component.html',
  styleUrl: './search-team-member.component.css'
})
export class SearchTeamMemberComponent {
  // @ViewChild('f') searchForm: NgForm;
  
  constructor() {}

  onSubmit(form: NgForm) {
    console.log(form);
  }
}
