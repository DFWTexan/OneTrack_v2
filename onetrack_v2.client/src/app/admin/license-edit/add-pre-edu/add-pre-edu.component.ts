import { Component, Input, OnDestroy, OnInit } from '@angular/core';
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

    console.log('EMFTEST (onAddCompany) - Add PreEdu Item => \n', item);

  }

  onCloseModal() {
    const modalDiv = document.getElementById('modal-add-preEdu-Item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {}
}
