import {
  Component,
  Injectable,
  OnInit,
  OnDestroy,
  EventEmitter,
  Output,
} from '@angular/core';
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
  @Output() callParentRefreshData = new EventEmitter<any>();
  isFormSubmitted: boolean = false;
  form = new FormGroup({
    CompanyID: new FormControl({
      value: 0,
      disabled:
        (!this.userAcctInfoDataService.userAcctInfo.isAdminRole ||
        this.userAcctInfoDataService.userAcctInfo.isSuperUser) ?? false
    }),
    Alias: new FormControl({ value: '', disabled: true }),
    GEID: new FormControl({ value: '', disabled: true }),
    NationalProducerNumber: new FormControl(''),
    EmployeeStatus: new FormControl({ value: '', disabled: true }),
    ResStateAbv: new FormControl({ value: '', disabled: true }),
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
      }).subscribe(this.handleDataLoad.bind(this))
    );

    // Subscribe to agentInfo changes
    this.subscriptions.add(
      this.agentDataService.agentInfoChanged.subscribe((agentInfo) => {
        if (agentInfo) {
          this.agentInfo = agentInfo;
          this.updateForm(agentInfo);
        }
      })
    );
  }

  private handleDataLoad({
    agentInfo,
    employerAgencies,
  }: {
    agentInfo: AgentInfo;
    employerAgencies: any;
  }): void {
    this.agentInfo = agentInfo;
    this.employerAgencies = employerAgencies;
    this.updateForm(agentInfo);
  }

  private updateForm(agentInfo: AgentInfo): void {
    const {
      companyID,
      alias,
      geid,
      nationalProdercerNumber,
      employeeStatus,
      state,
      ceRequired,
      excludeFromReports,
    } = agentInfo;

    this.form.patchValue({
      CompanyID: companyID,
      Alias: alias,
      GEID: geid,
      NationalProducerNumber: nationalProdercerNumber,
      EmployeeStatus: employeeStatus,
      ResStateAbv: state,
      CERequired: !!ceRequired,
      ExcludeFromRpts: !!excludeFromReports,
    });
  }

  onSubmit() {
    this.isFormSubmitted = true;

    let agentDetailItem: any = this.form.value;
    agentDetailItem.EmployeeID =
      this.agentDataService.agentInformation.employeeID;
    agentDetailItem.employmentID =
      this.agentDataService.agentInformation.employmentID;
    agentDetailItem.FirstName =
      this.agentDataService.agentInformation.firstName;
    agentDetailItem.LastName = this.agentDataService.agentInformation.lastName;
    agentDetailItem.MiddleName =
      this.agentDataService.agentInformation.middleName;

    agentDetailItem.employeeSSN = this.agentInfo.employeeSSN.replace(/-/g, '');
    agentDetailItem.employeeID = this.agentInfo.employeeID;
    agentDetailItem.lastName = this.agentInfo.lastName.toUpperCase();
    agentDetailItem.firstName = this.agentInfo.firstName.toUpperCase();
    agentDetailItem.middleName = this.agentInfo?.middleName?.toUpperCase();
    agentDetailItem.soeid = this.agentInfo.soeid;
    agentDetailItem.dateOfBirth = this.agentInfo.dateOfBirth;
    //--> agentDetailItem.nationalProducerNumber = this.agentInfo.nationalProdercerNumber;
    //--> agentDetailItem.geid = this.agentInfo.geid;
    //--> agentDetailItem.alias = this.agentInfo.alias;
    //--> agentDetailItem.excludeFromRpts = this.agentInfo.excludeFromReports;
    agentDetailItem.address1 = this.agentInfo.address1;
    agentDetailItem.address2 = this.agentInfo?.address2;
    agentDetailItem.city = this.agentInfo.city;
    agentDetailItem.state = this.agentInfo.state;
    agentDetailItem.zip = this.agentInfo.zip;
    agentDetailItem.phone = this.agentInfo.homePhone;
    agentDetailItem.fax = this.agentInfo.faxPhone;
    agentDetailItem.email = this.agentInfo.email;
    agentDetailItem.workPhone = this.agentInfo.workPhone;
    //--> agentDetailItem.employeeStatus = this.agentInfo.employeeStatus;
    //--> agentDetailItem.companyID = this.agentInfo.companyID;
    agentDetailItem.ceRequired = this.agentInfo.ceRequired;
    agentDetailItem.licenseLevel = this.agentInfo.licenseLevel;
    agentDetailItem.licenseIncentive = this.agentInfo.licenseIncentive;
    agentDetailItem.secondChance =
      this.agentInfo.isLicenseincentiveSecondChance;

    agentDetailItem.UserSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.form.invalid) {
      this.form.setErrors({ invalid: true });
      return;
    }

    this.subscriptions.add(
      this.agentDataService.updateAgentNatNumber(agentDetailItem).subscribe({
        next: (response) => {
          this.callParentRefreshData.emit(response);
          this.forceCloseModal();
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
