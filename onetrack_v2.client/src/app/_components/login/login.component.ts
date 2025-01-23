import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';

import { AppComService, UserAcctInfoDataService } from '../../_services';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit, OnDestroy {
  subscribeLoginMsgChanged: Subscription = new Subscription();

  constructor(
    private userAcctInfoDataService: UserAcctInfoDataService,
    public appComService: AppComService
  ) {}

  ngOnInit(): void {
    this.subscribeLoginMsgChanged =
      this.appComService.loginErrorMsgChanged.subscribe((errMessage) => {
        // console.log('EMFTEST - (app-login): errMsg - ', errMessage);
      });
  }

  onSubmit(form: NgForm) {
    if (!form.valid) {
      return;
    }

    this.userAcctInfoDataService.storeUserCredentials(
      form.value.loginInfo.userTID,
      form.value.loginInfo.password
    );

    this.subscribeLoginMsgChanged.add(
      this.userAcctInfoDataService
        .fetchUserAcctInfo(
          form.value.loginInfo.userTID,
          form.value.loginInfo.password
        )
        .subscribe((response) => {
          // console.log('EMFTEST - (app-login): response => \n', response);
        })
    );
  }

  ngOnDestroy(): void {
    this.subscribeLoginMsgChanged.unsubscribe();
  }
}
