import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AgentDataService, AppComService, DropdownDataService, ErrorMessageService, TicklerMgmtDataService, UserAcctInfoDataService } from '../../../../../_services';

@Component({
  selector: 'app-insert-member-loa-tickler',
  templateUrl: './insert-member-loa-tickler.component.html',
  styleUrl: './insert-member-loa-tickler.component.css'
})
export class InsertMemberLoaTicklerComponent implements OnInit, OnDestroy {
  // @Input() typeTickler: any = {};  
  @Output() callParentRefreshData = new EventEmitter<any>();
  typeTickler: any = {};
  ticklerForm: FormGroup;
  isFormSubmitted: boolean = false;
  licenseTechs: { value: any; label: string }[] = [];
  stockTicklerItems: any[] = [];
  agentInfo: any | null = null;
  isDisplayOtherMsg: boolean = false;
  today: string = new Date().toISOString().split('T')[0];

  private subscriptionData = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    public ticklerMgmtDataService: TicklerMgmtDataService,
    public agentDataService: AgentDataService,
    public appComService: AppComService,
    public ticklerDataService: TicklerMgmtDataService,
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

  ngOnInit() {
    this.licenseTechs = [{ value: 0, label: 'Select' },
      ...this.drpdwnDataService.licenseTechs];
      this.subscriptionData.add(
        this.drpdwnDataService.licenseTechsChanged.subscribe(
          (licenseTechs: { value: any; label: string }[]) => {
            this.licenseTechs = [{ value: 0, label: 'Select' }, ...licenseTechs];
          }
        )
      );
      this.agentInfo = this.agentDataService.agentInformation;
      this.subscriptionData.add(
        this.agentDataService.agentInfoChanged.subscribe(
          (agent: any | null) => {
            if (agent !== null) {
              this.agentInfo = agent;
            }
          }
        )
      );
      this.subscriptionData.add(
        this.ticklerMgmtDataService
          .fetchStockTickler()
          .subscribe((stockTicklerItems) => {
            this.stockTicklerItems = [{ lkpField: 'Tickler', lkpValue: 'Select' }, ...stockTicklerItems];
          })
      );
      this.ticklerForm.reset({ lkpValue: 'Select', licenseTechId: 0 });
      
      // this.subscriptionData.add(
      //   this.appComService.typeTicklerChanged.subscribe(
      //     (typeTickler: any) => {
      //       this.typeTickler = typeTickler;
      //       // if (this.typeTickler.type === 'LOA') {
      //       //   this.ticklerForm.get('lineOfAuthorityName')?.setValue(this.typeTickler.loa);
      //       // }
      //     }
      //   )
      // );
  }

  onTicklerMsgChange(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;

    if (value === 'Other') {
      this.isDisplayOtherMsg = true;
    } else {
      this.isDisplayOtherMsg = false;
      this.ticklerForm.get('message')?.setValue(null);
    }
  }

  onSubmit() {
    this.isFormSubmitted = true;
    let ticklerItem: any = this.ticklerForm.value;
    ticklerItem.ticklerId = 0;
    ticklerItem.EmployeeID = this.agentInfo.employeeID;
    ticklerItem.EmploymentID = this.agentInfo.employmentID;
    // ticklerItem.ticklerID = this.ticklerForm.get('ticklerId')?.value
    ticklerItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.typeTickler.type === 'LOA') {
      ticklerItem.lineOfAuthorityName = this.typeTickler.loa;
    };

    if (ticklerItem.ticklerDueDate === null || ticklerItem.ticklerDueDate === '') {
      this.ticklerForm.controls['ticklerDueDate'].setErrors({ incorrect: true });
    };

    if(ticklerItem.lkpValue === 'Select') {
      this.ticklerForm.controls['lkpValue'].setErrors({ incorrect: true });
    }

    if(this.isDisplayOtherMsg && (ticklerItem.message === null || ticklerItem.message === '')) {
      this.ticklerForm.controls['message'].setErrors({ incorrect: true });
    }

    if (this.ticklerForm.invalid) {
      this.ticklerForm.setErrors({ invalid: true });
      return;
    }

    this.subscriptionData.add(
      this.ticklerDataService.upsertTickerItem(ticklerItem).subscribe({
        next: (response) => {
          alert('Tickler has been successfully added');

// console.log('EMFTEST (InsertMemberLoaTicklerComponent: onSubmit) - response => \n', response);
         
          // const agent = this.agentDataService.fetchAgentInformation(this.agentInfo.employeeID);

          // this.callParentRefreshData.emit(agent);
           this.agentDataService.fetchAgentInformation(this.agentInfo.employeeID).subscribe({
              next: (agentInfo) => {
                console.log('Agent information fetched successfully:', agentInfo);
                // this.callParentRefreshData.emit(agentInfo);
              },
              error: (error) => {
                console.error('Error fetching agent information:', error);
              },
            });
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
