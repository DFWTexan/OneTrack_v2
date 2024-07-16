import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';

import { DropdownDataService, ErrorMessageService, TicklerMgmtDataService } from '../../../../../_services';

@Component({
  selector: 'app-insert-member-loa-tickler',
  templateUrl: './insert-member-loa-tickler.component.html',
  styleUrl: './insert-member-loa-tickler.component.css'
})
export class InsertMemberLoaTicklerComponent implements OnInit, OnDestroy {
  ticklerForm: FormGroup;
  licenseTechs: { value: any; label: string }[] = [];
  stockTicklerItems: any[] = [];
  employee: any | null = null;
  today: string = new Date().toISOString().split('T')[0];

  private subscriptionData = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    public ticklerMgmtDataService: TicklerMgmtDataService,
    // private employeeDataService: EmployeeDataService,
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
    // this.employee = this.employeeDataService.selectedEmployee;
    // this.subscriptionData.add(
    //   this.employeeDataService.selectedEmployeeChanged.subscribe(
    //     (employee: EmployeeSearchResult | null) => {
    //       if (employee !== null) {
    //         this.employee = employee;
    //       }
    //     }
    //   )
    // );
    // this.subscriptionData.add(
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
