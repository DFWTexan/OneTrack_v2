import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { formatDate } from '@angular/common';
import { Subscription } from 'rxjs';

import {
  AgentComService,
  AgentDataService,
  AppComService,
  ConstantsDataService,
  DropdownDataService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../../../_services';
import { AgentLicenseAppointments } from '../../../../../_Models';

@Component({
  selector: 'app-edit-license-info',
  templateUrl: './edit-license-info.component.html',
  styleUrl: './edit-license-info.component.css',
})
export class EditLicenseInfoComponent implements OnInit, OnDestroy {
  @Input() currentIndex: number = 0;
  @Input() isIndex: boolean = false;
  @Input() modeCloseModal: string = '';
  isFormSubmitted: boolean = false;
  licenseForm: FormGroup;
  licenseInfo: AgentLicenseAppointments = {} as AgentLicenseAppointments;
  licenseMgmtData: AgentLicenseAppointments[] = [];
  licenseStates: string[] = [];
  licenseStatuses: { value: string; label: string }[] = [];
  licenseNames: { value: number; label: string }[] = [];
  affiliatedLicenses: string[] = ['None'];
  defaultLicenseState: string = null || 'Select';
  licenseState: string = '';

  private subscriptions = new Subscription();

  constructor(
    public errorMessageService: ErrorMessageService,
    private conService: ConstantsDataService,
    private drpdwnDataService: DropdownDataService,
    public agentDataService: AgentDataService,
    public agentComService: AgentComService,
    public appComService: AppComService,
    private userInfoDataService: UserAcctInfoDataService,
    private fb: FormBuilder
  ) {
    this.licenseForm = this.fb.group({
      // INSERT FORM FIELDS HERE
      employeeID: [''],
      employmentID: [''],
      ascEmployeeLicenseID: [0],
      licenseID: [0, Validators.required],
      licenseExpireDate: [null],
      licenseState: ['Select'],
      licenseStatus: ['Select', Validators.required],
      licenseNumber: [''],
      reinstatement: [''],
      required: [''],
      nonResident: [''],
      licenseEffectiveDate: [null],
      licenseIssueDate: ['01/01/0001 00:00:00'],
      lineOfAuthorityIssueDate: [null],
      sentToAgentDate: [null],
      licenseNote: [''],
      // UPDATE FORM FIELDS HERE
      // employeeLicenseId: [{ value: '', disabled: true }],
      employeeLicenseId: [null],
      appointmentStatus: [''],
      companyID: [0],
      carrierDate: [null],
      appointmentEffectiveDate: [null],
      appointmentExpireDate: [null],
      appointmentTerminationDate: [null],
    });
  }

  ngOnInit(): void {
    this.licenseStates = ['Select', ...this.conService.getStates()];
    this.subscriptions.add(
      this.agentDataService.agentInfoChanged.subscribe(
        (agentInfo: any) => {
          this.getStateLicenseNames(agentInfo.workStateAbv); 
        }
      )
    );
    
    this.subscriptions.add(
      this.drpdwnDataService
        .fetchDropdownData('GetLicenseStatuses')
        .subscribe((licenseStatuses: { value: string; label: string }[]) => {
          this.licenseStatuses = [
            { value: 'Select', label: 'Select' },
            ...licenseStatuses.filter(
              (item) => item.value !== 'All' && item.label !== 'All'
            ),
          ];
        })
    );

    this.subscriptions.add(
      this.agentDataService.licenseInfoChanged.subscribe(
        (licenseInfo: AgentLicenseAppointments) => {
          // this.getStateLicenseNames(
          //   this.agentDataService.agentInformation.workStateAbv ??
          //     'Select'
          // )
          this.licenseNames = this.licenseNames.filter(item => item.value !== 0 || item.label !== 'Select');
          this.licenseInfo = licenseInfo;
          this.licenseForm.reset({
            agentName:
              this.agentDataService.agentInformation.lastName +
              ', ' +
              this.agentDataService.agentInformation.firstName,
            employeeLicenseId: licenseInfo.employeeLicenseId,
            licenseState: licenseInfo.licenseState,
            licenseName: licenseInfo.licenseName,
            licenseID: licenseInfo.licenseID,
            licenseNumber: licenseInfo.licenseNumber,
            licenseStatus: licenseInfo.licenseStatus,
            affiliatedLicense: 'SELECT-TBD...',
            licenseIssueDate: licenseInfo.originalIssueDate
              ? formatDate(licenseInfo.originalIssueDate, 'yyyy-MM-dd', 'en-US')
              : null,
            lineOfAuthIssueDate: licenseInfo.lineOfAuthIssueDate
              ? formatDate(
                  licenseInfo.lineOfAuthIssueDate,
                  'yyyy-MM-dd',
                  'en-US'
                )
              : null,
            licenseEffectiveDate: licenseInfo.licenseEffectiveDate
              ? formatDate(
                  licenseInfo.licenseEffectiveDate,
                  'yyyy-MM-dd',
                  'en-US'
                )
              : null,
            licenseExpireDate: licenseInfo.licenseExpirationDate
              ? formatDate(
                  licenseInfo.licenseExpirationDate,
                  'yyyy-MM-dd',
                  'en-US'
                )
              : null,
            licenseNote: licenseInfo.licenseNote,
            required: licenseInfo.required,
            // reinstatementDate: licenseInfo.reinstatementDate,
            nonResident: licenseInfo.nonResident,
            reinstatement: licenseInfo.reinstatement,
            employmentID: licenseInfo.employmentID,
          });
        }
      )
    );

    this.subscriptions.add(
      this.agentComService.modeLicenseMgmtChanged.subscribe((mode: string) => {
        // this.getStateLicenseNames(
        //   this.agentDataService.agentInformation.workStateAbv ??
        //     'Select'
        // );

        if (mode === 'EDIT') {
          this.licenseNames = this.licenseNames.filter(item => item.value !== 0 || item.label !== 'Select');
          if (this.isIndex) {
            // GET Data by Index
            this.subscriptions.add(
              this.agentDataService.licenseMgmtDataIndexChanged.subscribe(
                (licenseMgmtDataIndex: any) => {
                  this.currentIndex = licenseMgmtDataIndex;
                }
              )
            );

            this.licenseMgmtData =
              this.agentDataService.agentInformation.agentLicenseAppointments;
            let originalIssueDate =
              this.licenseMgmtData[this.currentIndex].originalIssueDate;
            let lineOfAuthIssueDate =
              this.licenseMgmtData[this.currentIndex].lineOfAuthIssueDate;
            let licenseEffectiveDate =
              this.licenseMgmtData[this.currentIndex].licenseEffectiveDate;
            let licenseExpirationDate =
              this.licenseMgmtData[this.currentIndex].licenseExpirationDate;
            this.defaultLicenseState =
              this.licenseMgmtData[this.currentIndex].licenseState;

            this.licenseForm.reset({
              agentName:
                this.agentDataService.agentInformation.lastName +
                ', ' +
                this.agentDataService.agentInformation.firstName,
              employeeLicenseId:
                this.licenseMgmtData[this.currentIndex].employeeLicenseId,
              licenseState:
                this.licenseMgmtData[this.currentIndex].licenseState,
              licenseName: this.licenseMgmtData[this.currentIndex].licenseName,
              licenseID: this.licenseMgmtData[this.currentIndex].licenseID,
              licenseNumber:
                this.licenseMgmtData[this.currentIndex].licenseNumber,
              licenseStatus:
                this.licenseMgmtData[this.currentIndex].licenseStatus,
              affiliatedLicense: 'SELECT-TBD...',
              licenseIssueDate: originalIssueDate
                ? formatDate(originalIssueDate, 'yyyy-MM-dd', 'en-US')
                : null,
              lineOfAuthIssueDate: lineOfAuthIssueDate
                ? formatDate(lineOfAuthIssueDate, 'yyyy-MM-dd', 'en-US')
                : '01/01/0001 00:00:00',
              licenseEffectiveDate: licenseEffectiveDate
                ? formatDate(licenseEffectiveDate, 'yyyy-MM-dd', 'en-US')
                : null,
              licenseExpireDate: licenseExpirationDate
                ? formatDate(licenseExpirationDate, 'yyyy-MM-dd', 'en-US')
                : null,
              licenseNote: this.licenseMgmtData[this.currentIndex].licenseNote,
              required: this.licenseMgmtData[this.currentIndex].required,
              // reinstatementDate: this.licenseMgmtData[this.currentIndex].reinstatementDate,
              nonResident: this.licenseMgmtData[this.currentIndex].resNoneRes,
              reinstatement:
                this.licenseMgmtData[this.currentIndex].reinstatement,
              employmentID:
                this.licenseMgmtData[this.currentIndex].employmentID,
            });
          } else {
            if (!this.licenseNames.some(item => item.value === 0 && item.label === 'Select')) {
              this.licenseNames.unshift({value: 0, label: 'Select'});
            }
          }
        } else {
          if (!this.licenseNames.some(item => item.value === 0 && item.label === 'Select')) {
            this.licenseNames.unshift({value: 0, label: 'Select'});
          }
          this.licenseForm.reset({
            licenseState:
              this.agentDataService.agentInformation.branchDeptStreetState,
            licenseID: 0,
            licenseStatus: 'Select',
          });
        }
      })
    );
  }

  getStateLicenseNames(state: string) {
    if (state === 'Select') {
      return;
    }
    this.subscriptions.add(
      this.drpdwnDataService
        .fetchDropdownNumericData('GetLicenseNumericNames', state)
        .subscribe((licenseNames: { value: number; label: string }[]) => {
          this.licenseNames = [{ value: 0, label: 'Select' }, ...licenseNames];
        })
    );
  }

  onSubmit() {
    this.isFormSubmitted = true;

console.log('EMFTEST (onSubmit) A - licenseForm => \n ', this.licenseForm.value);   

    let licenseInfo: any = this.licenseForm.value;
    licenseInfo.employeeID = this.agentDataService.agentInformation.employeeID;
    licenseInfo.employmentID =
      this.agentDataService.agentInformation.employmentID;
    licenseInfo.UserSOEID = this.userInfoDataService.userAcctInfo.soeid;

    if (this.agentComService.modeLicenseMgmt == 'EDIT') {
      // licenseInfo.employeeLicenseId =
        // this.licenseMgmtData[this.currentIndex].employeeLicenseId;
        // this.agentDataService.agentLicApptLicenseID;

    } else {
      licenseInfo.employeeLicenseId = 0;
    }

    if (licenseInfo.licenseID === 0) {
      this.licenseForm.controls['licenseID'].setErrors({ invalid: true });
    }

    if (licenseInfo.licenseStatus === 'Select') {
      this.licenseForm.controls['licenseStatus'].setErrors({ invalid: true });
    }

    if (
      licenseInfo.sentToAgentDate === '01/01/0001 00:00:00' ||
      licenseInfo.sentToAgentDate === null && this.agentComService.modeLicenseMgmt != 'EDIT'
    ) {
      this.licenseForm.controls['sentToAgentDate'].setErrors({
        invalid: true,
      });
    }

    if (this.licenseForm.invalid) {
      this.licenseForm.setErrors({ invalid: true });
      return;
    }

console.log('EMFTEST (onSubmit) B - licenseInfo => \n ', licenseInfo);    

    this.subscriptions.add(
      this.agentDataService.upsertAgentLicense(licenseInfo).subscribe({
        next: (response) => {
          // const modalDiv = document.getElementById('modal-edit-license-info');
          // if (modalDiv != null) {
          //   modalDiv.style.display = 'none';
          // }
          this.forceCloseModal();
          // handle the response here
          // console.log(
          //   'EMFTEST () - Agent License added successfully response => \n ',
          //   response
          // );
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
          }
          // const modalDiv = document.getElementById('modal-edit-license-info');
          // if (modalDiv != null) {
          //   modalDiv.style.display = 'none';
          // }
          this.forceCloseModal();
        },
      })
    );
  }

  forceCloseModal() {
    this.isFormSubmitted = false;
    if (this.modeCloseModal === 'EDIT') {
      const modalDiv = document.getElementById('modal-edit-license-info');
      if (modalDiv != null) {
        modalDiv.style.display = 'none';
      }
    } else {
      const modalDiv = document.getElementById('modal-new-license-info');
      if (modalDiv != null) {
        modalDiv.style.display = 'none';
      }
    }
  }

  onCloseModal() {
    if (this.licenseForm.dirty && !this.isFormSubmitted) {
      if (
        confirm('You have unsaved changes. Are you sure you want to close?')
      ) {
        // const modalDiv = document.getElementById('modal-edit-license-info');
        // if (modalDiv != null) {
        //   modalDiv.style.display = 'none';
        // }
        this.forceCloseModal();
        this.licenseForm.reset();
        // this.licenseForm.patchValue({
        //   jobTitleID: 0,
        //   isCurrent: false,
        // });
      }
    } else {
      this.isFormSubmitted = false;
      // const modalDiv = document.getElementById('modal-edit-license-info');
      // if (modalDiv != null) {
      //   modalDiv.style.display = 'none';
      // }
      this.forceCloseModal();
    }
  }

  onChangeLicenseState(event: any) {
    this.licenseForm.patchValue({
      licenseState: event.target.value,
    });

    if (
      event.target.value !==
      this.agentDataService.agentInformation.branchDeptStreetState
    ) {
      alert(
        'Selection is different than Agent Work State: ' +
          this.agentDataService.agentInformation.branchDeptStreetState
      );
    }

    this.getStateLicenseNames(event.target.value);
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
