import { Component, OnInit, OnDestroy, ChangeDetectorRef, inject } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSelectChange } from '@angular/material/select';
import { Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import {
  EmployeeDataService,
  DropdownDataService,
  ConstantsDataService,
  AppComService,
  PaginationComService,
  UserAcctInfoDataService,
} from '../../_services';
import {
  EmployeeSearchResult,
  SearchEmployeeFilter,
  UserAcctInfo,
} from '../../_Models';

@Component({
  selector: 'app-search-team-member',
  templateUrl: './search-team-member.component.html',
  styleUrls: ['./search-team-member.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatButtonModule,
    MatProgressSpinnerModule
    // Import any other components used in your template
  ]
})
export class SearchTeamMemberComponent implements OnInit, OnDestroy {
  searchForm: FormGroup;
  loading = false;
  isSubmitted = false;

  // Inject services
  private conService = inject(ConstantsDataService);
  private drpdwnDataService = inject(DropdownDataService);
  private emplyService = inject(EmployeeDataService);
  private appComService = inject(AppComService);
  public paginationComService = inject(PaginationComService);
  private userInfoService = inject(UserAcctInfoDataService);
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private cdr = inject(ChangeDetectorRef);

  agentStatuses: string[] = ['All', ...this.conService.getAgentStatuses()];
  states: string[] = this.conService.getStates();
  stateProvinces: string[] = [];
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

  selectedAgentStatuses: string[] = ['All'];
  selectedLicenseStatuses: string[] = ['All'];
  searchEmployeeResult: EmployeeSearchResult[] = [];
  selectAllAgentsFirstemployeeID: number = 0;

  constructor() {
    this.searchForm = this.fb.group({
      EmployeeSSN: [''],
      TeamMemberGEID: [''],
      NationalProducerNumber: [''],
      LastName: [''],
      FirstName: [''],
      ResState: [''],
      WrkState: [''],
      EmployerAgency: [''],
      AgentStatus: [this.selectedAgentStatuses],
      ScoreNumber: [''],
      BranchCode: [''],
      LicStatus: [this.selectedLicenseStatuses],
      LicState: [''],
      LicenseName: [''],
    });
  }

  ngOnInit() {
    this.fetchData();

    this.searchForm.patchValue(this.appComService.searchEmployeeFilter);
    if(this.appComService.searchEmployeeResult.length > 0) {
      this.onSubmit();
    }
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
    if (event.value.includes('All')) {
      this.selectedAgentStatuses = ['All'];
    } else {
      this.selectedAgentStatuses = event.value;
    }
  }

  onLicenseStatusSelectionChange(event: MatSelectChange) {
    if (event.value.includes('All')) {
      this.selectedLicenseStatuses = ['All'];
    } else {
      this.selectedLicenseStatuses = event.value;
    }
  }

  onSubmit() {
    this.loading = true;
    this.isSubmitted = true;

    let searchFilter: SearchEmployeeFilter = this.searchForm.value;
    this.appComService.updateSearchEmployeeFilter(searchFilter);

    searchFilter.EmployeeSSN = searchFilter.EmployeeSSN || null;
    searchFilter.TeamMemberGEID = searchFilter.TeamMemberGEID || null;
    searchFilter.NationalProducerNumber =
    searchFilter.NationalProducerNumber || 0;
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
      if (this.searchEmployeeResult.length > 1) {
        const selectAllAgents: number[] = this.searchEmployeeResult.map(employee => employee.employeeID);
        this.selectAllAgentsFirstemployeeID = selectAllAgents[0];
        this.appComService.updateSelectAllAgents(selectAllAgents);
      }
      this.appComService.updateSearchEmployeeResult(this.searchEmployeeResult);
      this.cdr.detectChanges();
    });
  }

  selectAll() {
    this.appComService.updateIsAllAgentsSelected(true);
    this.router.navigate([
      '../../team/agent-info',
      this.selectAllAgentsFirstemployeeID,
      'tm-info-mgmt',
    ]);
  }

  onReset() {
    this.searchForm.get('EmployeeSSN')?.reset();
    this.searchForm.get('TeamMemberGEID')?.reset();
    this.searchForm.get('NationalProducerNumber')?.reset();

    this.searchForm.get('LastName')?.reset();
    this.searchForm.get('FirstName')?.reset();

    this.searchForm.get('ResState')?.reset();
    this.searchForm.get('WrkState')?.reset();
    this.searchForm.get('EmployerAgency')?.reset();
    this.searchForm.get('ScoreNumber')?.reset();
    this.searchForm.get('BranchCode')?.reset();
    this.searchForm.get('LicState')?.reset();
    this.searchForm.get('LicenseName')?.reset();

    this.selectedAgentStatuses = ['All'];
    this.searchForm.get('AgentStatus')?.setValue(this.selectedAgentStatuses);

    this.selectedLicenseStatuses = ['All'];
    this.searchForm.get('LicStatus')?.setValue(this.selectedLicenseStatuses);

    this.isSubmitted = false;
    this.searchEmployeeResult = [];
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
    this.searchEmployeeResult = [];
  }
}