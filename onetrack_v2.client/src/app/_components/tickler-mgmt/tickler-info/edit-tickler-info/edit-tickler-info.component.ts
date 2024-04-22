import { Component, Injectable, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  TicklerMgmtComService,
  TicklerMgmtDataService,
} from '../../../../_services';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-edit-tickler-info',
  templateUrl: './edit-tickler-info.component.html',
  styleUrl: './edit-tickler-info.component.css',
})
@Injectable()
export class EditTicklerInfoComponent implements OnInit, OnDestroy {
  ticklerForm!: FormGroup;
  licenseTechItems: any[] = [];
  stockTicklerItems: any[] = [];
  subscriptionData: Subscription = new Subscription();

  constructor(
    private fb: FormBuilder,
    public ticklerDataService: TicklerMgmtDataService,
    public ticklerComService: TicklerMgmtComService
  ) {
    this.createForm();
  }

  createForm() {
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

  ngOnInit(): void {
    this.subscriptionData =
      this.ticklerComService.modeTicklerMgmtChanged.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.ticklerDataService.ticklerInfoChanged.subscribe(
              (ticklerInfo) => {
                this.ticklerForm.patchValue({
                  ticklerId: ticklerInfo.ticklerId,
                  ticklerDate:  formatDate(ticklerInfo.ticklerDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  ticklerDueDate: formatDate(
                    ticklerInfo.ticklerDueDate,
                    'yyyy-MM-dd',
                    'en-US'
                  ),
                  licenseTechId: ticklerInfo.licenseTechId,
                  employmentId: ticklerInfo.employmentId,
                  employeeLicenseId: ticklerInfo.employeeLicenseId,
                  employeeId: ticklerInfo.employeeId,
                  ticklerCloseDate: ticklerInfo.ticklerCloseDate,
                  ticklerCloseByLicenseTechId:
                    ticklerInfo.ticklerCloseByLicenseTechId,
                  lineOfAuthorityName: ticklerInfo.lineOfAuthorityName,
                  teamMemberName: ticklerInfo.teamMemberName,
                  geid: ticklerInfo.geid,
                  message: ticklerInfo.message,
                  lkpValue: ticklerInfo.lkpValue,
                });
              }
            );
          } else {
            this.ticklerForm.reset();
          } 
        }
      );
      this.ticklerDataService.fetchLicenseTech(0, null).subscribe(
        (licenseTechItems) => {
          this.licenseTechItems = licenseTechItems;
        }
      );
      this.ticklerDataService.fetchStockTickler().subscribe(
        (stockTicklerItems) => {
          this.stockTicklerItems = stockTicklerItems;
        }
      );
  }

  ngOnDestroy(): void {}

  onSubmit() {
    console.log(this.ticklerForm.value);
  }
}
