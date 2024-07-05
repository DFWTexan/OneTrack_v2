import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  ConstantsDataService,
  ErrorMessageService,
  MiscDataService,
  ModalService,
  UserAcctInfoDataService,
} from '../../_services';
import { Exam } from '../../_Models';
import { ConfirmDialogComponent } from '../../_components';

@Component({
  selector: 'app-exam-edit',
  templateUrl: './exam-edit.component.html',
  styleUrl: './exam-edit.component.css',
})
@Injectable()
export class ExamEditComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  stateProvinces: any[] = [];
  examDeliveryMethods: any[] = [];
  examProviders: { value: number; label: string }[] = [];
  selectedStateProvince: string = 'Select';
  examItems: Exam[] = [];
  eventAction: string = '';
  vObject: any = {};

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    public adminDataService: AdminDataService,
    public miscDataService: MiscDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    public dialog: MatDialog,
    public modalService: ModalService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.stateProvinces = ['Select', ...this.conService.getStateProvinces()];
    this.fetchEditExamDropdowns();
  }

  onChildCallRefreshData() {
    this.fetchExamItems();
  }

  private fetchEditExamDropdowns(): void {
    this.subscriptionData.add(
      this.adminDataService
        .fetchDropdownListItems('ExamDelivery')
        .subscribe((response) => {
          this.examDeliveryMethods = [
            'Select Method',
            ...response.map((item: any) => item.lkpValue),
          ];
        })
    );
    this.subscriptionData.add(
      this.miscDataService.fetchExamProviders().subscribe((response) => {
        this.examProviders = [
          { value: 0, label: 'Select Provider' },
          ...response,
        ];
      })
    );
  }

  private fetchExamItems(): void {
    this.subscriptionData.add(
      this.adminDataService
        .fetchExamItems(this.selectedStateProvince)
        .subscribe((response) => {
          this.examItems = response;
        })
    );
  }

  changeStateProvince(event: any) {
    const target = event.target as HTMLInputElement;
    const value = target.value;

    this.selectedStateProvince = value;

    if (value === 'Select') {
      this.examItems = [];
    } else {
      this.fetchExamItems();
    }
  }

  openConfirmDialog(eventAction: string, msg: string, vObject: any): void {
    this.eventAction = eventAction;
    this.vObject = vObject;

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: {
        title: 'Confirm Action',
        message:
          'You are about to DELETE Eam Item (' +
          vObject.examName +
          '). Click "Yes" to confirm?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.subscriptionData.add(
          this.adminDataService
            .deleteExamItem({
              examID: vObject.examId,
              userSOEID: this.userAcctInfoDataService.userAcctInfo.soeid,
            })
            .subscribe({
              next: (response) => {
                this.fetchExamItems();
              },
              error: (error) => {
                if (error.error && error.error.errMessage) {
                  this.errorMessageService.setErrorMessage(
                    error.error.errMessage
                  );
                }
              },
            })
        );
      }
    });
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
