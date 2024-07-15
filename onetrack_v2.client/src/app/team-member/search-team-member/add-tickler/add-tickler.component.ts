import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';

import { DropdownDataService, EmployeeDataService, ErrorMessageService, TicklerMgmtDataService } from '../../../_services';
import { EmployeeSearchResult, StockTickler } from '../../../_Models';

@Component({
  selector: 'app-add-tickler',
  templateUrl: './add-tickler.component.html',
  styleUrl: './add-tickler.component.css',
})
export class AddTicklerComponent implements OnInit, OnDestroy {
  ticklerForm: FormGroup;
  licenseTechs: { value: any; label: string }[] = [];
  stockTicklerItems: any[] = [];
  employee: EmployeeSearchResult | null = null;
  today: string = new Date().toISOString().split('T')[0];

  private subscriptionData = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    public ticklerMgmtDataService: TicklerMgmtDataService,
    private employeeDataService: EmployeeDataService,
    private fb: FormBuilder,
    private drpdwnDataService: DropdownDataService
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
          this.stockTicklerItems = [{ lkpField: 'Tickler', lkpValue: 'Select' }, ...stockTicklerItems];
        })
    );
    this.ticklerForm.reset({ lkpValue: 'Select', licenseTechId: 0 });
  }

  onSubmit() {
    // some code here
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
