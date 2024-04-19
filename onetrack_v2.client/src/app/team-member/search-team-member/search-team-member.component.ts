import {
  Component,
  OnInit,
  Injectable,
  OnDestroy,
  // ViewChild
} from '@angular/core';
import {
  NgForm,
  // FormControl
} from '@angular/forms';
import { MatSelectChange } from '@angular/material/select';
import { Subscription } from 'rxjs';

import {
  EmployeeDataService,
  DropdownDataService,
  ConstantsDataService,
  AppComService,
} from '../../_services';
import { EmployeeSearchResult, SearchEmployeeFilter } from '../../_Models';
@Component({
  selector: 'app-search-team-member',
  templateUrl: './search-team-member.component.html',
  styleUrl: './search-team-member.component.css',
})
@Injectable()
export class SearchTeamMemberComponent implements OnInit, OnDestroy {
  // @ViewChild('f') searchForm: NgForm;
  isSubmitted = false;
  agentStatuses: string[] = [];
  states: string[] = [];
  stateProvinces: string[] = [];
  defaultAgentStatus = 'ALL';
  defaultLicenseStatus = 'ALL';
  isShowTickle: boolean = true;
  subscribeTickleToggleChanged: Subscription = new Subscription();

  // Dropdown-Data-Service
  branchNames: { value: string; label: string }[] = [];
  scoreNumbers: { value: string; label: string }[] = [];
  employerAgencies: { value: string; label: string }[] = [];
  licenseStatuses: { value: string; label: string }[] = [];
  licenseNames: { value: string; label: string }[] = [];

  selectedAgentStatuses: string[] = [];
  selectedLicenseStatuses: string[] = [];
  searchEmployeeResult: EmployeeSearchResult[] = [];

  constructor(
    private conService: ConstantsDataService,
    private drpdwnDataService: DropdownDataService,
    private emplyService: EmployeeDataService,
    private appComService: AppComService
  ) {}

  ngOnInit() {
    // LOAD DROPDOWN DATA
    this.agentStatuses = this.conService.getAgentStatuses();
    this.states = this.conService.getStates();
    this.stateProvinces = this.conService.getStateProvinces();
    this.subscribeTickleToggleChanged =
      this.appComService.tickleToggleChanged.subscribe(
        (tickleToggle: boolean) => {
          this.isShowTickle = tickleToggle;
        }
      );
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

  onAgentStatusSelectionChange(event: MatSelectChange) {
    if (event.value.includes('ALL')) {
      this.selectedAgentStatuses = [];
    } else {
      this.selectedAgentStatuses = event.value;
    }

console.log('EMFTEST (onAgentStatusSelectionChange) - selectedAgentStatuses => \n', this.selectedAgentStatuses);

  }

  onLicenseStatusSelectionChange(event: MatSelectChange) {
    if (event.value.includes('All')) {
      this.selectedLicenseStatuses = [];
    } else {
      this.selectedLicenseStatuses = event.value;
    }

console.log('EMFTEST (onLicenseStatusSelectionChange) - selectedLicenseStatuses => \n', this.selectedLicenseStatuses);

  }

  onSubmit(form: NgForm) {
    this.isSubmitted = true;
    const searchFilter: SearchEmployeeFilter = {
      EmployeeSSN: form.value.searchFilter.EmployeeSSN || null,
      TeamMemberGEID: form.value.searchFilter.TeamMemberGEID || 0,
      NationalProducerNumber:
        form.value.searchFilter.NationalProducerNumber || 0,
      LastName: form.value.searchFilter.LastName || null,
      FirstName: form.value.searchFilter.FirstName || null,
      ResState: form.value.searchFilter.ResState || null,
      WrkState: form.value.searchFilter.WrkState || null,
      BranchCode: form.value.searchFilter.BranchCode || null,
      AgentStatus: this.selectedAgentStatuses || null,
      ScoreNumber: form.value.searchFilter.ScoreNumber || null,
      EmployerAgency: form.value.searchFilter.EmployerAgency || null,
      LicStatus: this.selectedLicenseStatuses || null,
      LicState: form.value.searchFilter.LicState || null,
      LicenseName: form.value.searchFilter.LicenseName || null,
    };

    console.log('EMFTEST (onSubmit) - searchFilter => \n', searchFilter);

    this.emplyService.fetchEmployeeSearch(searchFilter).subscribe((results) => {
      this.searchEmployeeResult = results;
    });
  }

  ngOnDestroy(): void {
    this.subscribeTickleToggleChanged.unsubscribe();
  }
}
