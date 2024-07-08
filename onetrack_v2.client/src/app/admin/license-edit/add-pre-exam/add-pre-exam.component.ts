import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import {
  AdminComService,
  AdminDataService,
  AppComService,
  ErrorMessageService,
  UserAcctInfoDataService,
} from '../../../_services';

@Component({
  selector: 'app-add-pre-exam',
  templateUrl: './add-pre-exam.component.html',
  styleUrl: './add-pre-exam.component.css',
})
export class AddPreExamComponent implements OnInit, OnDestroy {
  @Input() preExams: any[] = [];

  constructor(
    private errorMessageService: ErrorMessageService,
    public adminDataService: AdminDataService,
    public adminComService: AdminComService,
    public appComService: AppComService,
    private userAcctInfoDataService: UserAcctInfoDataService
  ) {}

  ngOnInit(): void {}

  onCloseModal() {
    const modalDiv = document.getElementById('modal-add-preExam-item');
    if (modalDiv != null) {
      modalDiv.style.display = 'none';
    }
  }

  ngOnDestroy(): void {}
}
