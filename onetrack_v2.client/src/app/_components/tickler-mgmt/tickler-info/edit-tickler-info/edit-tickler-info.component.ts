import { Component, Injectable, OnInit, OnDestroy, Input } from '@angular/core';
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
  @Input() licenseTechItems: any[] = [];
  @Input() stockTicklerItems: any[] = [];
  ticklerForm!: FormGroup;
  isFormSubmitted: boolean = false;

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
    // this.subscriptionData.add(
    //   this.ticklerDataService
    //     .fetchLicenseTech(0, null)
    //     .subscribe((licenseTechItems) => {
    //       this.licenseTechItems =
    //         this.ticklerComService.modeTicklerMgmt == 'EDIT'
    //           ? licenseTechItems
    //           : ['Select', ...licenseTechItems];
    //     })
    // );

    this.licenseTechItems =
      this.ticklerComService.modeTicklerMgmt == 'EDIT'
        ? this.licenseTechItems
        : ['Select', ...this.licenseTechItems];

    // this.subscriptionData.add(
    //   this.ticklerDataService
    //     .fetchStockTickler()
    //     .subscribe((stockTicklerItems) => {
    //       this.stockTicklerItems =
    //         this.ticklerComService.modeTicklerMgmt == 'EDIT'
    //           ? stockTicklerItems
    //           : ['Select', ...stockTicklerItems];
    //     })
    // );

    this.ticklerComService.modeTicklerMgmt == 'EDIT'
      ? this.stockTicklerItems
      : ['Select', ...this.stockTicklerItems];

    this.subscriptionData.add(
      this.ticklerComService.modeTicklerMgmtChanged.subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.subscriptionData.add(
              this.ticklerDataService.ticklerInfoChanged.subscribe(
                (ticklerInfo) => {
                  this.ticklerForm.patchValue({
                    ticklerId: ticklerInfo.ticklerId,
                    ticklerDate: formatDate(
                      ticklerInfo.ticklerDate,
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
              )
            );
          } else {
            this.ticklerForm.reset();
          }
        }
      )
    );
  }

  onSubmit() {
    console.log(this.ticklerForm.value);
  }

  forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-tickler');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  onCloseModal() {
    if (this.ticklerForm.dirty && !this.isFormSubmitted) {
      if (
        confirm('You have unsaved changes. Are you sure you want to close?')
      ) {
        this.forceCloseModal();
        this.ticklerForm.reset();
        // this.licPreEduForm.patchValue({
        //   applicationStatus: 'Select',
        //   applicationType: 'Select',
        // });
      }
    } else {
      this.isFormSubmitted = false;
      this.forceCloseModal();
    }
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
