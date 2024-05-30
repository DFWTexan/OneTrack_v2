import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription, forkJoin, take } from 'rxjs';

import {
  AgentDataService,
  ConstantsDataService,
  DropdownDataService,
} from '../../../../../_services';
import { AgentInfo } from '../../../../../_Models';

@Component({
  selector: 'app-edit-tm-information',
  templateUrl: './edit-tm-information.component.html',
  styleUrl: './edit-tm-information.component.css',
})
@Injectable()
export class EditTmInformationComponent implements OnInit, OnDestroy {
  form = new FormGroup({
    CompanyID: new FormControl(''),
    preferredName: new FormControl(''),
    GEID: new FormControl(''),
    NationalProducerNumber: new FormControl(''),
    EmployeeStatus: new FormControl(''),
    ResStateAbv: new FormControl(''),
    CERequired: new FormControl(''),
    ExcludeFromRpts: new FormControl(''),
  });

  states: string[] = this.conService.getStates();
  agentStatuses: string[] = this.conService.getAgentStatuses();
  employerAgencies: { value: string; label: string }[] = [];
  agentInfo: AgentInfo = {} as AgentInfo;
  
  private subscriptions = new Subscription();

  constructor(
    private conService: ConstantsDataService,
    private agentService: AgentDataService,
    private drpdwnDataService: DropdownDataService
  ) {}

  ngOnInit(): void {
    this.subscriptions.add(
      forkJoin({
        agentInfo: this.agentService.agentInfoChanged.pipe(take(1)),
        employerAgencies: this.drpdwnDataService.fetchDropdownData(
          'GetEmployerAgencies'
        ),
      }).subscribe(({ agentInfo, employerAgencies }) => {
        this.agentInfo = agentInfo;
        this.employerAgencies = employerAgencies;

        this.form.patchValue({
          CompanyID: agentInfo.companyName,
          preferredName: agentInfo.lastName + ', ' + agentInfo.firstName,
          GEID: agentInfo.geid,
          NationalProducerNumber: agentInfo.nationalProdercerNumber,
          EmployeeStatus: agentInfo.employeeStatus,
          ResStateAbv: agentInfo.state,
          CERequired: agentInfo.ceRequired ? 'true' : 'false',
          ExcludeFromRpts: agentInfo.excludeFromReports ? 'true' : 'false',
        });
      })
    );
  }

  onSubmit() {
    console.log(this.form.value);
  }

  onCloseModal() {
    const modalDiv = document.getElementById('modal-edit-tm-info');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
    // if (this.jobTitleForm.dirty && !this.isFormSubmitted) {
    //   if (
    //     confirm('You have unsaved changes. Are you sure you want to close?')
    //   ) {
    //     const modalDiv = document.getElementById('modal-edit-jobTitle-history');
    //     if (modalDiv != null) {
    //       modalDiv.style.display = 'none';
    //     }
    //     this.jobTitleForm.reset();
    //     this.jobTitleForm.patchValue({
    //       jobTitleID: 0,
    //       isCurrent: false,
    //     });
    //   }
    // } else {
    //   this.isFormSubmitted = false;
    //   const modalDiv = document.getElementById('modal-edit-jobTitle-history');
    //   if (modalDiv != null) {
    //     modalDiv.style.display = 'none';
    //   }
    // }
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
function injectable(): (
  target: typeof EditTmInformationComponent
) => void | typeof EditTmInformationComponent {
  throw new Error('Function not implemented.');
}
