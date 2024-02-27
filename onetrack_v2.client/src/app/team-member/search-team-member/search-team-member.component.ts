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

import { ConstantsService } from '../../_services/constants.service';
import { DropdownDataService } from '../../_services/dropDown.data.service';

@Component({
  selector: 'app-search-team-member',
  templateUrl: './search-team-member.component.html',
  styleUrl: './search-team-member.component.css',
})
@Injectable()
export class SearchTeamMemberComponent implements OnInit {
  // @ViewChild('f') searchForm: NgForm;
  agentStatuses: string[] = [];
  states: string[] = [];
  stateProvinces: string[] = [];

  // Dropdown-Data-Service
  branchNames: { value: string, label: string }[] = [];
  scoreNumbers: { value: string, label: string }[] = [];
  employerAgencies: { value: string, label: string }[] = [];
  licenseStatuses: { value: string, label: string }[] = [];
  licenseNames: { value: string, label: string }[] = [];

  constructor(
    private conService: ConstantsService,
    private drpdwnDataService: DropdownDataService
  ) {}

  ngOnInit() {
    this.agentStatuses = this.conService.getAgentStatuses();
    this.states = this.conService.getStates();
    this.stateProvinces = this.conService.getStateProvinces();
    this.drpdwnDataService
      .fetchDropdownData('GetBranches')
      .subscribe((branchNames: { value: string, label: string }[]) => {
        this.branchNames = branchNames;
      });
    this.drpdwnDataService
      .fetchDropdownData('GetScoreNumbers')
      .subscribe((scoreNumbers: { value: string, label: string }[]) => {
        this.scoreNumbers = scoreNumbers;
      });
    this.drpdwnDataService
      .fetchDropdownData('GetEmployerAgencies')
      .subscribe((employerAgencies: { value: string, label: string }[]) => {
        this.employerAgencies = employerAgencies;
      });
    this.drpdwnDataService
      .fetchDropdownData('GetLicenseStatuses')
      .subscribe((licenseStatuses: { value: string, label: string }[]) => {
        this.licenseStatuses = licenseStatuses;
      });
    this.drpdwnDataService
      .fetchDropdownData('GetLicenseNames')
      .subscribe((licenseNames: { value: string, label: string }[]) => {
        this.licenseNames = licenseNames;
      });
  }

  onSubmit(form: NgForm) {
    console.log(form);
  }
}
