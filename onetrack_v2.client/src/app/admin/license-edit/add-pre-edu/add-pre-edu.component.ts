import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import {
  AdminComService,
  AdminDataService,
  AppComService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../_services';

@Component({
  selector: 'app-add-pre-edu',
  templateUrl: './add-pre-edu.component.html',
  styleUrl: './add-pre-edu.component.css',
})
export class AddPreEduComponent implements OnInit, OnDestroy {
  @Input() preEducations: any[] = [];

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {}

  onCloseModal() {
    const modalDiv = document.getElementById('modal-add-preEdu-Item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {}
}
