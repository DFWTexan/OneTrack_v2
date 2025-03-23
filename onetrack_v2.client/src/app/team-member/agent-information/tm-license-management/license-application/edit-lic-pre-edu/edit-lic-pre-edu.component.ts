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
  selector: 'app-edit-lic-pre-edu',
  templateUrl: './edit-lic-pre-edu.component.html',
  styleUrl: './edit-lic-pre-edu.component.css',
})
@Injectable()
export class EditLicPreEduComponent implements OnInit, OnDestroy {
  @Output() callParentGetData = new EventEmitter<void>();
  isFormSubmitted: boolean = false;
  licPreEduForm!: FormGroup;
  licenseMgmtData: AgentLicenseAppointments[] =
    [] as AgentLicenseAppointments[];
  currentIndex: number = 0;
  preEduStatuses: string[] = [
    'Select',
    ...this.constDataService.getPreEducationStatuses(),
  ];
  preEduNames: { value: number; label: string }[] = [];

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
    this.licPreEduForm = new FormGroup({
      employeeLicensePreEducationID: new FormControl(0),
      status: new FormControl(null),
      educationEndDate: new FormControl(null, [dateValidator]),
      preEducationID: new FormControl(null),
      educationStartDate: new FormControl(null, [dateValidator]),
      employeeLicenseID: new FormControl(null),
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
          'GetPreEducationByStateAbv',
          this.licenseMgmtData[this.currentIndex].licenseState
        )
        .subscribe((response) => {
          this.preEduNames = [{ value: 0, label: 'Select' }, ...response];
        })
    );

    this.subscriptions.add(
      this.agentComService.modeLicPreEduChanged.subscribe((mode: string) => {
        if (mode === 'EDIT') {
          this.subscriptions.add(
            this.agentDataService.licensePreEducationItemChanged.subscribe(
              (licPreEdu: any) => {
                this.licPreEduForm.patchValue({
                  employeeLicensePreEducationID:
                    licPreEdu.employeeLicensePreEducationID,
                  status: licPreEdu.status,
                  educationStartDate: licPreEdu.educationStartDate
                    ? formatDate(
                        licPreEdu.educationStartDate,
                        'yyyy-MM-dd',
                        'en-US'
                      )
                    : null,
                  educationEndDate: licPreEdu.educationEndDate
                    ? formatDate(
                        licPreEdu.educationEndDate,
                        'yyyy-MM-dd',
                        'en-US'
                      )
                    : null,
                  preEducationID: licPreEdu.preEducationID,
                  // companyID: licPreEdu.companyID,
                  // educationName: licPreEdu.educationName,
                  employeeLicenseID: licPreEdu.employeeLicenseID,
                  additionalNotes: licPreEdu.additionalNotes,
                });
              }
            )
          );
        } else {
          this.licPreEduForm.reset();
          this.licPreEduForm.patchValue({
            status: 'Select',
            preEducationID: 0,
          });
        }
      })
    );
  }

  onSubmit(): void {
    let licPreEduItem: any = this.licPreEduForm.value;
    licPreEduItem.employeeLicenseID =
      this.licenseMgmtData[this.currentIndex].employeeLicenseId;
    // licApplicationItem.applicationType = 'Initial Application';
    licPreEduItem.UserSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.licPreEduForm.invalid) {
      this.licPreEduForm.setErrors({ invalid: true });
      return;
    }

    if (this.agentComService.modeLicPreEdu === 'INSERT') {
      licPreEduItem.employeeLicensePreEducationID = 0;
    }

    if (licPreEduItem.educationStartDate === '') {
      licPreEduItem.educationStartDate = null;
    }

    if (licPreEduItem.educationEndDate === '') {
      licPreEduItem.educationEndDate = null;
    }

    if (licPreEduItem.additionalNotes === '') {
      licPreEduItem.additionalNotes = null;
    }

    this.subscriptions.add(
      this.licApplicationDataService
        .upsertLicensePreEducationItem(licPreEduItem)
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
    const modalDiv = document.getElementById('modal-edit-lic-pre-edu');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  onCloseModal() {
    if (this.licPreEduForm.dirty && !this.isFormSubmitted) {
      if (
        confirm('You have unsaved changes. Are you sure you want to close?')
      ) {
        this.forceCloseModal();
        this.licPreEduForm.reset();
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
