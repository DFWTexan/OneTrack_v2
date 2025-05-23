import {
  Component,
  Injectable,
  OnInit,
  OnDestroy,
  EventEmitter,
  Output,
} from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

import {
  AdminComService,
  AdminDataService,
  AppComService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../_services';

@Component({
  selector: 'app-edit-license-tech',
  templateUrl: './edit-license-tech.component.html',
  styleUrl: './edit-license-tech.component.css',
})
@Injectable()
export class EditLicenseTechComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  isLoading: boolean = false;
  isFormSubmitted: boolean = false;
  licenseTechForm!: FormGroup;

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.licenseTechForm = new FormGroup({
      licenseTechId: new FormControl(''),
      soeid: new FormControl(''),
      firstName: new FormControl(''),
      lastName: new FormControl(''),
      isActive: new FormControl(''),
      teamNum: new FormControl(''),
      licenseTechPhone: new FormControl(''),
      licenseTechFax: new FormControl(''),
      licenseTechEmail: new FormControl(''),
      techName: new FormControl(''),
    });

    this.subscriptionData.add(
      this.adminComService.modes.licenseTech.changed
      .subscribe(
        (mode: string) => {
          if (mode === 'EDIT') {
            this.adminDataService.licenseTechChanged
            .subscribe(
              (licenseTech: any) => {
                this.licenseTechForm.patchValue({
                  licenseTechId: licenseTech.licenseTechId,
                  soeid: licenseTech.soeid,
                  firstName: licenseTech.firstName,
                  lastName: licenseTech.lastName,
                  isActive: licenseTech.isActive,
                  teamNum: licenseTech.teamNum,
                  licenseTechPhone: licenseTech.licenseTechPhone,
                  licenseTechFax: licenseTech.licenseTechFax,
                  licenseTechEmail: licenseTech.licenseTechEmail,
                  techName: licenseTech.techName,
                });
              }
            );
          } else {
            this.licenseTechForm.reset();
          }
        }
      )
    );
  }

  onSubmit(): void {
    this.isFormSubmitted = true;
    let licenseTechItem: any = this.licenseTechForm.value;
    licenseTechItem.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    if (this.adminComService.modes.licenseTech.mode === 'INSERT') {
      licenseTechItem.licenseTechId = 0;
    }

    if (licenseTechItem.teamNum === '' || licenseTechItem.teamNum === null) {
      this.licenseTechForm.controls['teamNum'].setErrors({ required: true });
    }

    if (licenseTechItem.lastName === '' || licenseTechItem.lastName === null) {
      this.licenseTechForm.controls['lastName'].setErrors({ required: true });
    }

    if (licenseTechItem.firstName === '' || licenseTechItem.firstName === null) {
      this.licenseTechForm.controls['firstName'].setErrors({ required: true });
    }

    if (!this.licenseTechForm.valid) {
      this.licenseTechForm.setErrors({ invalid: true });
      return;
    }

    this.subscriptionData.add(
      this.adminDataService.upSertLicenseTech(licenseTechItem).subscribe({
        next: (response) => {
          this.callParentRefreshData.emit();
          this.appComService.updateAppMessage(
            'License Tech saved successfully'
          );
          this.forceCloseModal();
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
            this.forceCloseModal();
          }
        },
      })
    );
  }

  private forceCloseModal() {
    const modalDiv = document.getElementById('modal-edit-license-tech');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  onCloseModal() {
    this.forceCloseModal();
  }

  ngOnDestroy(): void {
    this.subscriptionData.unsubscribe();
  }
}
