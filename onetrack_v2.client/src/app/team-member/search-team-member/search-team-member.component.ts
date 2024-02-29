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
      // NationalProducerNumber: 0,
      // AgentStatus: ['All'], // if NationalProducerNumber is not part of the form, you can set it manually
      ...form.value.searchFilter
      // AgentStatus: ['Active', 'Inactive'], // if AgentStatus is not part of the form, you can set it manually
      // Other criteria as needed
    };

    // console.log('EMFTest - searchFilter => \n ', searchFilter);

    this.emplyService
      .fetchEmployeeSearch(searchFilter)
      .subscribe((results) => {
        // console.log('EMFTest - EmpSearchResults => \n', results); // Do something with the results
        this.searchEmployeeResult = results;
      });
  }
  
}
