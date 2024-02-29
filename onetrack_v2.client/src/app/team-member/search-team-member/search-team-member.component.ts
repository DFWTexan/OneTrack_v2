import {
  Component,
  OnInit,
  Injectable,
  // ViewChild
} from '@angular/core';
import {
  NgForm,
  // FormControl
} from '@angular/forms';
import { Subscription } from 'rxjs';

import { ConstantsService } from '../../_services/constants.data.service';
import { DropdownDataService } from '../../_services/dropDown.data.service';
import { EmployeeService } from '../../_services/employee.data.service';
import { EmployeeSearchResult, SearchEmployee } from '../../_Models';

@Component({
  selector: 'app-search-team-member',
  templateUrl: './search-team-member.component.html',
  styleUrl: './search-team-member.component.css',
})
@Injectable()
export class SearchTeamMemberComponent implements OnInit {
  // @ViewChild('f') searchForm: NgForm;
  isSubmitted = false;
  agentStatuses: string[] = [];
  states: string[] = [];
  stateProvinces: string[] = [];
  defaultAgentStatus = 'ALL';

  // Dropdown-Data-Service
  branchNames: { value: string; label: string }[] = [];
  scoreNumbers: { value: string; label: string }[] = [];
  employerAgencies: { value: string; label: string }[] = [];
  licenseStatuses: { value: string; label: string }[] = [];
  licenseNames: { value: string; label: string }[] = [];

  searchEmployeeResult: EmployeeSearchResult[] = [];

  constructor(
    private conService: ConstantsService,
    private drpdwnDataService: DropdownDataService,
    private emplyService: EmployeeService
  ) {}

  ngOnInit() {
    // LOAD DROPDOWN DATA
    this.agentStatuses = this.conService.getAgentStatuses();
    this.states = this.conService.getStates();
    this.stateProvinces = this.conService.getStateProvinces();
    this.drpdwnDataService
      .fetchDropdownData('GetBranches')
      .subscribe((branchNames: { value: string; label: string }[]) => {
        this.branchNames = branchNames;
      });
    this.drpdwnDataService
      .fetchDropdownData('GetScoreNumbers')
      .subscribe((scoreNumbers: { value: string; label: string }[]) => {
        this.scoreNumbers = scoreNumbers;
      });
    this.drpdwnDataService
      .fetchDropdownData('GetEmployerAgencies')
      .subscribe((employerAgencies: { value: string; label: string }[]) => {
        this.employerAgencies = employerAgencies;
      });
    this.drpdwnDataService
      .fetchDropdownData('GetLicenseStatuses')
      .subscribe((licenseStatuses: { value: string; label: string }[]) => {
        this.licenseStatuses = licenseStatuses;
      });
    this.drpdwnDataService
      .fetchDropdownData('GetLicenseNames')
      .subscribe((licenseNames: { value: string; label: string }[]) => {
        this.licenseNames = licenseNames;
      });
  }

  onSubmit(form: NgForm) {
    this.isSubmitted = true;
    const searchFilter: SearchEmployee = {
      EmployeeSSN: form.value.searchFilter.EmployeeSSN || null,
      TeamMemberGEID: form.value.searchFilter.TeamMemberGEID || 0,
      NationalProducerNumber: form.value.searchFilter.NationalProducerNumber || 0,
      LastName: form.value.searchFilter.LastName || null,
      FirstName: form.value.searchFilter.FirstName || null,
      ResState: form.value.searchFilter.ResState || null,
      WrkState: form.value.searchFilter.WrkState || null,
      BranchCode: form.value.searchFilter.BranchCode || null,
      AgentStatus: form.value.searchFilter.AgentStatus || ['All'],
      ScoreNumber: form.value.searchFilter.ScoreNumber || null,
      EmployerAgency: form.value.searchFilter.EmployerAgency || null,
      LicStatus: form.value.searchFilter.LicStatus || null,
      LicState: form.value.searchFilter.LicState || null,
      LicenseName: form.value.searchFilter.LicenseName || null,
    };

    console.log('EMFTest - form => \n ', form);
    console.log('EMFTest - searchFilter => \n ', searchFilter);

    this.emplyService
      .fetchEmployeeSearch(searchFilter)
      .subscribe((results) => {
        // console.log('EMFTest - EmpSearchResults => \n', results); // Do something with the results
        this.searchEmployeeResult = results;
      });
  }
  
}
