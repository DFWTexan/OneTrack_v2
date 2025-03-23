import {
  Component,
  Injectable,
  OnInit,
  OnDestroy,
  Output,
  EventEmitter,
} from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import {
  AgentComService,
  AgentDataService,
  AppComService,
  ConstantsDataService,
  DropdownDataService,
  ErrorMessageService,
  LicIncentiveInfoDataService,
  UserAcctInfoDataService,
} from '../../../../../_services';
import { AgentLicenseAppointments } from '../../../../../_Models';
import { dateValidator } from '../../../../../_shared';

@Component({
  selector: 'app-edit-lic-pre-exam',
  templateUrl: './edit-lic-pre-exam.component.html',
  styleUrl: './edit-lic-pre-exam.component.css',
})
@Injectable()
export class EditLicPreExamComponent implements OnInit, OnDestroy {
  @Output() callParentGetData = new EventEmitter<void>();
  isFormSubmitted: boolean = false;
  licPreExamForm!: FormGroup;
  licenseMgmtData: AgentLicenseAppointments[] =
    [] as AgentLicenseAppointments[];
  currentIndex: number = 0;
  preExamStatuses: string[] = [
    'Select',
    ...this.constDataService.getPreExamStatuses(),
  ];
  preExamNames: { value: number; label: string }[] = [];

  private subscriptions = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    private constDataService: ConstantsDataService,
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    private dropdownDataService: DropdownDataService,
    public appComService: AppComService,
    private licApplicationDataService: LicIncentiveInfoDataService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.licPreExamForm = new FormGroup({
      employeeLicensePreExamID: new FormControl(0),
      employeeLicenseID: new FormControl(null),
      status: new FormControl(null),
      examID: new FormControl(null),
      examName: new FormControl(null),
      examScheduleDate: new FormControl(null, [dateValidator]),
      examTakenDate: new FormControl(null, [dateValidator]),
      additionalNotes: new FormControl(null),
    });

    this.currentIndex = this.agentDataService.licenseMgmtDataIndex;
    this.subscriptions.add(
      this.agentDataService.licenseMgmtDataIndexChanged.subscribe(
        (index: number) => {
          this.currentIndex = index;
        }
      )
    );

    this.licenseMgmtData =
      this.agentDataService.agentInformation.agentLicenseAppointments;
    this.subscriptions.add(
      this.agentDataService.licenseMgmtDataChanged.subscribe(
        (licenseMgmtData: AgentLicenseAppointments) => {
          this.licenseMgmtData = [licenseMgmtData];
        }
      )
    );

    this.subscriptions.add(
      this.dropdownDataService
        .fetchDropdownNumericData(
          'GetPreExamByStateAbv',
          this.licenseMgmtData[this.currentIndex].licenseState
        )
        .subscribe((response) => {
          this.preExamNames = [{ value: 0, label: 'Select' }, ...response];
        })
    );

    this.subscriptions.add(
      this.agentComService.modeLicPreExamChanged.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.subscriptions.add(
            this.agentDataService.licensePreExamItemChanged.subscribe(
              (licPreExam: any) => {
                this.licPreExamForm.patchValue({
                  employeeLicensePreExamID: licPreExam.employeeLicensePreExamID,
                  employeeLicenseID: licPreExam.employeeLicenseID,
                  status: licPreExam.status,
                  examID: licPreExam.examID,
                  examName: licPreExam.examName,
                  examScheduleDate: licPreExam.examScheduleDate
                    ? formatDate(
                        licPreExam.examScheduleDate,
                        'yyyy-MM-dd',
                        'en-US'
                      )
                    : null,
                  examTakenDate: licPreExam.examTakenDate
                    ? formatDate(
                        licPreExam.examTakenDate,
                        'yyyy-MM-dd',
                        'en-US'
                      )
                    : null,
                  additionalNotes: licPreExam.additionalNotes,
                });
              }
            )
          );
        } else {
          this.licPreExamForm.reset();
          this.licPreExamForm.patchValue({
            status: 'Select',
            examID: 0,
          });
        }
      })
    );
  }

  onSubmit(): void {
    let licPreExamItem: any = this.licPreExamForm.value;
    licPreExamItem.employeeLicenseID =
      this.licenseMgmtData[this.currentIndex].employeeLicenseId;
    // licApplicationItem.applicationType = 'Initial Application';
    licPreExamItem.UserSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.licPreExamForm.invalid) {
      this.licPreExamForm.setErrors({ invalid: true });
      return;
    }

    if (this.agentComService.modeLicPreExam === 'INSERT') {
      licPreExamItem.employeeLicensePreExamID = 0;
    }

    if (licPreExamItem.examScheduleDate === '') {
      licPreExamItem.examScheduleDate = null;
    }

    if (licPreExamItem.examTakenDate === '') {
      licPreExamItem.examTakenDate = null;
    }

    this.subscriptions.add(
      this.licApplicationDataService
        .upsertLicensePreExamItem(licPreExamItem)
        .subscribe({
          next: (response) => {
            this.isFormSubmitted = true;
            this.forceCloseModal();
            this.callParentGetData.emit();
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
    const modalDiv = document.getElementById('modal-edit-lic-pre-exam');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  onCloseModal() {
    if (this.licPreExamForm.dirty && !this.isFormSubmitted) {
      if (
        confirm('You have unsaved changes. Are you sure you want to close?')
      ) {
        this.forceCloseModal();
        this.licPreExamForm.reset();
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
    this.subscriptions.unsubscribe();
  }
}
