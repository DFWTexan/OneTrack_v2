import { Component, OnInit, Injectable } from '@angular/core';
import { NgForm,FormControl } from '@angular/forms';

import { ConstantsService } from '../../_services/constants.service';

@Component({
  selector: 'app-search-team-member',
  templateUrl: './search-team-member.component.html',
  styleUrl: './search-team-member.component.css'
})

@Injectable()
export class SearchTeamMemberComponent implements OnInit{
  // @ViewChild('f') searchForm: NgForm;
  agentStatuses: string[] = [];
  states: string[] = [];
  stateProvinces: string[] = [];
  frmCtrlAgentStatus: FormControl = new FormControl('');
  
  constructor(private conService: ConstantsService) {}

  ngOnInit() {
    this.agentStatuses = this.conService.getAgentStatuses();
    this.states = this.conService.getStates();
    this.stateProvinces = this.conService.getStateProvinces();
  }

  onSubmit(form: NgForm) {
    console.log(form);
  }
}
