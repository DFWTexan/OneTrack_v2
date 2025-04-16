import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  AgentDataService,
  DropdownDataService,
  EmployeeDataService,
  ErrorMessageService,
  TicklerMgmtDataService,
  UserAcctInfoDataService,
} from '../../../_services';
import {
  AgentInfo,
  EmployeeSearchResult,
  StockTickler,
} from '../../../_Models';

@Component({
  selector: 'app-add-tickler',
  templateUrl: './add-tickler.component.html',
  styleUrl: './add-tickler.component.css',
})
export class AddTicklerComponent implements OnInit, OnDestroy {
  ticklerForm: FormGroup;
  isFormSubmitted: boolean = false;
  licenseTechs: { value: any; label: string }[] = [];
  stockTicklerItems: any[] = [];
  employee: EmployeeSearchResult | null = null;
  today: string = new Date().toISOString().split('T')[0];
  isDisplayOtherMsg: boolean = false;
  agentInfo: AgentInfo = {} as AgentInfo;

  private subscriptionData = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    public ticklerMgmtDataService: TicklerMgmtDataService,
    private employeeDataService: EmployeeDataService,
    private agentDataService: AgentDataService,
    private fb: FormBuilder,
    private drpdwnDataService: DropdownDataService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {
    this.ticklerForm = this.fb.group({
      ticklerId: [{ value: null, disabled: true }],
      ticklerDate: [null],
      ticklerDueDate: [null],
      licenseTechId: [null],
      employmentId: [null],
      employeeLicenseId: [null],
      employeeId: [null],
      ticklerCloseDate: [null],
      ticklerCloseByLicenseTechId: [null],
      lineOfAuthorityName: [null],
      teamMemberName: [null],
      geid: [null],
      message: [null],
      lkpValue: [null],
    });
  }

  onChangeTiclerMsg(event: any) {
    if (event.target.value === 'Other') {
      this.isDisplayOtherMsg = true;
    } else {
      this.isDisplayOtherMsg = false;
    }
  }

  ngOnInit() {
    this.licenseTechs = [
      { value: 0, label: 'Select' },
      ...this.drpdwnDataService.licenseTechs,
    ];
    this.employee = this.employeeDataService.selectedEmployee;
    this.subscriptionData.add(
      this.employeeDataService.selectedEmployeeChanged.subscribe(
        (employee: EmployeeSearchResult | null) => {
          if (employee !== null) {
            this.employee = employee;
          }
        }
      )
    );
    this.subscriptionData.add(
      this.ticklerMgmtDataService
        .fetchStockTickler()
        .subscribe((stockTicklerItems) => {
          this.stockTicklerItems = [
            { lkpField: 'Tickler', lkpValue: 'Select' },
            ...stockTicklerItems,
          ];
        })
    );
    this.ticklerForm.reset({ lkpValue: 'Select', licenseTechId: 0 });

    this.agentInfo = this.agentDataService.agentInformation;
    this.subscriptionData.add(
      this.agentDataService.agentInfoChanged
        // .subscribe(
        //   (agentInfo: AgentInfo) => {
        //     this.isLoading = false;
        //     this.agentInfo = agentInfo;
        //   }
        // )
        .subscribe({
          next: (agentInfo: AgentInfo) => {
            // this.isLoading = false;
            this.agentInfo = agentInfo;
            // this.sortedLicenseItems = [...agentInfo.licenseItems];
            // this.sortedAppointmentItems = [...agentInfo.appointmentItems];
            this.ticklerForm.reset({ lkpValue: 'Select', licenseTechId: 0 });
          },
          error: (error) => {
            if (error.error && error.error.errMessage) {
              this.errorMessageService.setErrorMessage(error.error.errMessage);
            }
          },
        })
    );
  }

  onSubmit() {
    this.isFormSubmitted = true;
    let ticklerItem: any = this.ticklerForm.value;
    ticklerItem.PreEducationID = 0;
    ticklerItem.EmployeeID = this.agentInfo.employeeID;
    ticklerItem.EmploymentID = this.agentInfo.employmentID;
    ticklerItem.ticklerID = this.ticklerForm.get('ticklerId')?.value;
    ticklerItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.ticklerForm.invalid) {
      this.ticklerForm.setErrors({ invalid: true });
      return;
    }

    this.subscriptionData.add(
      this.ticklerMgmtDataService.upsertTickerItem(ticklerItem).subscribe({
        next: (response) => {
          alert('Tickler has been successfully added');
          this.onCancel();
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
            this.onCancel();
          }
        },
      })
    );
  }

  onCancel() {
    const modalDiv = document.getElementById('modal-new-tickler');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy() {
    this.subscriptionData.unsubscribe();
  }
}
