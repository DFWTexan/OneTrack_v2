import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import {
  AdminComService,
  AdminDataService,
  AppComService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../_services';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-pre-edu',
  templateUrl: './add-pre-edu.component.html',
  styleUrl: './add-pre-edu.component.css',
})
export class AddPreEduComponent implements OnInit, OnDestroy {
  @Output() callParentRefreshData = new EventEmitter<any>();
  @Input() preEducations: any[] = [];
  licenseId: number = 0;

  subscriptionData: Subscription = new Subscription();

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {
    this.licenseId = this.adminDataService.licenseID;
    this.subscriptionData.add(
      this.adminDataService.licenseIdChanged.subscribe((id: number) => {
        this.licenseId = id;
      })
    );
  }

  onAddItem(item: any): void {
    item.userSOEID = this.userAcctInfoDataService.userAcctInfo.soeid;

    this.subscriptionData.add(
      this.adminDataService.addLicenseEducation(item).subscribe({
        next: (response) => {
          // alert('PreEducation added successfully');
          this.appComService.updateAppMessage(
            'PreEducation saved successfully'
          );
          this.callParentRefreshData.emit();
          // console.log(
          //   'EMFTEST (app-tm-emptrans-history: deleteJobTitle) - COMPLETED DELETE response => \n',
          //   response
          // );
        },
        error: (error) => {
          if (error.error && error.error.errMessage) {
            this.errorMessageService.setErrorMessage(error.error.errMessage);
            this.onCloseModal();
          }
        },
      })
    );
  }

  onCloseModal() {
    const modalDiv = document.getElementById('modal-add-preEdu-Item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {}
}
