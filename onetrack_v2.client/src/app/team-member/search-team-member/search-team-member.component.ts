import { Component, OnInit, Injectable, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { MatSelectChange } from '@angular/material/select';
import { Subscription } from 'rxjs';

import {
  EmployeeDataService,
  DropdownDataService,
  ConstantsDataService,
  AppComService,
  PaginationComService,
} from '../../_services';
import { EmployeeSearchResult, SearchEmployeeFilter } from '../../_Models';

@Component({
  selector: 'app-search-team-member',
  templateUrl: './search-team-member.component.html',
  styleUrl: './search-team-member.component.css',
})
@Injectable()
export class SearchTeamMemberComponent implements OnInit, OnDestroy {
  searchForm: FormGroup;
  loading = false;
  isSubmitted = false;
  agentStatuses: string[] = ['All', ...this.conService.getAgentStatuses()];
  states: string[] = this.conService.getStates();
  stateProvinces: string[] = [];
  defaultAgentStatus = 'ALL';
  defaultLicenseStatus = 'ALL';
  isShowTickle: boolean = true;
  subscribeTickleToggleChanged: Subscription = new Subscription();

  // Dropdown-Data-Service subscriptions
  private subscriptions = new Subscription();

  // Dropdown-Data-Service
  branchNames: { value: string; label: string }[] = [];
  selectedBranch: string | null = null;
  scoreNumbers: { value: number; label: string }[] = [];
  employerAgencies: { value: number; label: string }[] = [];
  licenseStatuses: { value: number; label: string }[] = [];
  licenseNames: { value: number; label: string }[] = [];

  selectedAgentStatuses: string[] = [];
  selectedLicenseStatuses: string[] = [];
  searchEmployeeResult: EmployeeSearchResult[] = [];

  constructor(
    private conService: ConstantsDataService,
    private drpdwnDataService: DropdownDataService,
    private emplyService: EmployeeDataService,
    private appComService: AppComService,
    public paginationComService: PaginationComService,
    private fb: FormBuilder
  ) {
    this.searchForm = this.fb.group({
      EmployeeSSN: [''],
      TeamMemberGEID: [''],
      NationalProducerNumber: [''],
      LastName: [''],
      FirstName: [''],
      ResState: [''],
      WrkState: [''],
      EmployerAgency: [''],
      AgentStatus: [''],
      ScoreNumber: [''],
      BranchCode: [''],
      LicStatus: [''],
      LicState: [''],
      LicenseName: ['']
    });
  }

  ngOnInit() {
    this.fetchData();
  }

  fetchData() {
    // LOAD DROPDOWN DATA
    this.stateProvinces = this.conService.getStateProvinces();
    this.subscriptions.add(
      this.appComService.tickleToggleChanged.subscribe(
        (tickleToggle: boolean) => {
          this.isShowTickle = tickleToggle;
        }
      )
    );
    this.branchNames = this.drpdwnDataService.branchNames;
    this.subscriptions.add(
      this.drpdwnDataService.branchNamesChanged.subscribe(
        (branchNames: { value: string; label: string }[]) => {
          this.branchNames = branchNames;
        }
      )
    );
    this.scoreNumbers = this.drpdwnDataService.scoreNumbers;
    this.subscriptions.add(
      this.drpdwnDataService.scoreNumbersChanged.subscribe(
        (scoreNumbers: { value: number; label: string }[]) => {
          this.scoreNumbers = scoreNumbers;
        }
      )
    );
    this.employerAgencies = this.drpdwnDataService.employerAgencies;
    this.subscriptions.add(
      this.drpdwnDataService.employerAgenciesChanged.subscribe(
        (employerAgencies: { value: number; label: string }[]) => {
          this.employerAgencies = employerAgencies;
        }
      )
    );
    this.licenseStatuses = this.drpdwnDataService.licenseStatuses;
    this.subscriptions.add(
      this.drpdwnDataService.licenseStatusesChanged.subscribe(
        (licenseStatuses: { value: number; label: string }[]) => {
          this.licenseStatuses = licenseStatuses;
        }
      )
    );
    this.licenseNames = this.drpdwnDataService.licenseNames;
    this.subscriptions.add(
      this.drpdwnDataService.licenseNamesChanged.subscribe(
        (licenseNames: { value: number; label: string }[]) => {
          this.licenseNames = licenseNames;
        }
      )
    );
  }

  onAgentStatusSelectionChange(event: MatSelectChange) {
    if (event.value.includes('ALL')) {
      this.selectedAgentStatuses = [];
    } else {
      this.selectedAgentStatuses = event.value;
    }
  }

  onLicenseStatusSelectionChange(event: MatSelectChange) {
    if (event.value.includes('All')) {
      this.selectedLicenseStatuses = [];
    } else {
      this.selectedLicenseStatuses = event.value;
    }
  }

  onSubmit() {
    this.loading = true;
    this.isSubmitted = true;

    let searchFilter: SearchEmployeeFilter = this.searchForm.value;
    // const searchFilter: SearchEmployeeFilter = {
    //   EmployeeSSN: form.value.searchFilter.EmployeeSSN || null,
    //   TeamMemberGEID: form.value.searchFilter.TeamMemberGEID || null,
    //   NationalProducerNumber:
    //     form.value.searchFilter.NationalProducerNumber || 0,
    //   LastName: form.value.searchFilter.LastName || null,
    //   FirstName: form.value.searchFilter.FirstName || null,
    //   ResState: form.value.searchFilter.ResState || null,
    //   WrkState: form.value.searchFilter.WrkState || null,
    //   BranchCode: form.value.searchFilter.BranchCode || null,
    //   AgentStatus: this.selectedAgentStatuses || null,
    //   ScoreNumber: form.value.searchFilter.ScoreNumber || null,
    //   CompanyID: form.value.searchFilter.EmployerAgency || 0,
    //   LicStatus: this.selectedLicenseStatuses || null,
    //   LicState: form.value.searchFilter.LicState || null,
    //   LicenseName: form.value.searchFilter.LicenseName || null,
    // };
    searchFilter.EmployeeSSN = searchFilter.EmployeeSSN || null;
    searchFilter.TeamMemberGEID = searchFilter.TeamMemberGEID || null;
    searchFilter.NationalProducerNumber = searchFilter.NationalProducerNumber || 0;
    searchFilter.LastName = searchFilter.LastName || null;
    searchFilter.FirstName = searchFilter.FirstName || null;
    searchFilter.ResState = searchFilter.ResState || null;
    searchFilter.WrkState = searchFilter.WrkState || null;
    searchFilter.BranchCode = searchFilter.BranchCode || null;
    searchFilter.ScoreNumber = searchFilter.ScoreNumber || null;
    searchFilter.LicState = searchFilter.LicState || null;
    searchFilter.LicenseName = searchFilter.LicenseName || null;
    searchFilter.AgentStatus = this.selectedAgentStatuses || ['All'];
    searchFilter.LicStatus = this.selectedLicenseStatuses || ['All'];

    this.emplyService.fetchEmployeeSearch(searchFilter).subscribe((results) => {
      this.loading = false;
      this.searchEmployeeResult = results;
    });
  }

  onReset() {
    // form.resetForm();
    // this.fetchData();
    // form.controls['LastName'].reset();
    this.searchForm.get('LastName')?.reset();

    this.isSubmitted = false;
    this.searchEmployeeResult = [];
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
