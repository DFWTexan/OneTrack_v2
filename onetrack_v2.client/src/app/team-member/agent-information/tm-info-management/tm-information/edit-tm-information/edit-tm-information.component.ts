import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription, forkJoin, take } from 'rxjs';

import {
  AgentDataService,
  ConstantsDataService,
  DropdownDataService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../../../_services';
import { AgentInfo } from '../../../../../_Models';

@Component({
  selector: 'app-edit-tm-information',
  templateUrl: './edit-tm-information.component.html',
  styleUrl: './edit-tm-information.component.css',
})
@Injectable()
export class EditTmInformationComponent implements OnInit, OnDestroy {
  isFormSubmitted: boolean = false;
  form = new FormGroup({
    CompanyID: new FormControl(0),
    preferredName: new FormControl(''),
    GEID: new FormControl(''),
    NationalProducerNumber: new FormControl(''),
    EmployeeStatus: new FormControl(''),
    ResStateAbv: new FormControl(''),
    CERequired: new FormControl(false),
    ExcludeFromRpts: new FormControl(false),
  });

  states: string[] = this.conService.getStates();
  agentStatuses: string[] = this.conService.getAgentStatuses();
  employerAgencies: { value: string; label: string }[] = [];
  agentInfo: AgentInfo = {} as AgentInfo;
  
  private subscriptions = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    private agentDataService: AgentDataService,
    private drpdwnDataService: DropdownDataService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.subscriptions.add(
      forkJoin({
        agentInfo: this.agentDataService.agentInfoChanged.pipe(take(1)),
        employerAgencies: this.drpdwnDataService.fetchDropdownData(
          'GetEmployerAgencies'
        ),
      }).subscribe(({ agentInfo, employerAgencies }) => {
        this.agentInfo = agentInfo;
        this.employerAgencies = employerAgencies;

        this.form.patchValue({
          CompanyID: agentInfo.companyID,
          preferredName: agentInfo.lastName + ', ' + agentInfo.firstName,
          GEID: agentInfo.geid,
          NationalProducerNumber: agentInfo.nationalProdercerNumber,
          EmployeeStatus: agentInfo.employeeStatus,
          ResStateAbv: agentInfo.state,
          CERequired: agentInfo.ceRequired ? true : false,
          ExcludeFromRpts: agentInfo.excludeFromReports ? true : false,
        });
      })
    );
  }

  onSubmit() {
    this.isFormSubmitted = true;

    let agentDetailItem: any = this.form.value;
    agentDetailItem.EmployeeID =
      this.agentDataService.agentInformation.employeeID;
      agentDetailItem.employmentID = this.agentDataService.agentInformation.employmentID;
      agentDetailItem.UserSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.form.invalid) {
      this.form.setErrors({ invalid: true });
      return;
    }

    this.subscriptions.add(
      this.agentDataService
        .updateAgent(agentDetailItem)
        .subscribe({
          next: (response) => {
            this.forceCloseModal();
            // handle the response here
            // console.log(
            //   'EMFTEST () - Agent License added successfully response => \n ',
            //   response
            // );
          },
          error: (error) => {
            if (error.error && error.error.errMessage) {
              this.errorMessageService.setErrorMessage(error.error.errMessage);
            }
            this.forceCloseModal();
          },
        })
    );
  }

forceCloseModal() { 
    const modalDiv = document.getElementById('modal-edit-tm-info');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
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
