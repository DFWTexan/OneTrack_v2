import { Component, OnInit, Injectable } from '@angular/core';
import { NgForm,FormControl } from '@angular/forms';

import { ConstantsService } from '../../_services/constants.service';
import { DropdownDataService } from '../../_services/dropDown.data.service';

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
  
  // Dropdown-Data-Service
  branchNames: string[] = [];
  scoreNumbers: string[] = [];
  employerAgencies: string[] = [];
  licenseStatuses: string[] = [];
  licenseNames: string[] = [];
  
  constructor(private conService: ConstantsService, private drpdwnDataService: DropdownDataService) {}

  ngOnInit() {
    this.agentStatuses = this.conService.getAgentStatuses();
    this.states = this.conService.getStates();
    this.stateProvinces = this.conService.getStateProvinces();
    
    this.branchNames = this.drpdwnDataService.getBranchNames();
    this.scoreNumbers = this.drpdwnDataService.getScoreNumbers();
    this.employerAgencies = this.drpdwnDataService.getEmployerAgencies();
    this.licenseStatuses = this.drpdwnDataService.getLicenseStatuses();
    this.licenseNames = this.drpdwnDataService.getLicenseNames();
  }

  onSubmit(form: NgForm) {
    console.log(form);
  }
}
